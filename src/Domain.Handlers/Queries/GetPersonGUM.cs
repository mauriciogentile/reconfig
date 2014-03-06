using System;
using System.Collections.Generic;
using System.Linq;
using IDB.Notifications.Domain.Gum;
using IDB.Notifications.Domain.Model;

namespace IDB.Notifications.Domain.Queries
{
    public class GetPersonGUM : IGetPersonByEmail, IGetPersonById, IGetPersonsByGroupName
    {
        readonly System.Web.Caching.Cache _cache;
        readonly IGetCacheItem _getPersonList;
        readonly IRepository<CacheItem> _repository;

        public GetPersonGUM(System.Web.Caching.Cache cache)
        {
            _cache = cache;
        }

        public GetPersonGUM(IGetCacheItem getPersonList, IRepository<CacheItem> repository)
        {
            _getPersonList = getPersonList;
            _repository = repository;
        }

        Person IGetPersonByEmail.Execute(string email)
        {
            var person = GetByEmail(email);
            return person;
        }

        Person IGetPersonById.Execute(int personId)
        {
            return GetById(personId);
        }

        IEnumerable<Person> IGetPersonsByGroupName.Execute(string groupName)
        {
            return GetByGroupName(groupName);
        }

        Person GetById(int personId)
        {
            using (var gum = new Gum.GUMSoapClient())
            {
                var person = ToPerson(gum.GetPersonInfo(personId));

                return person;
            }
        }

        //Person GetByEmail(string email)
        //{
        //    using (var gum = new Gum.GUMSoapClient())
        //    {
        //        var key = string.Format(Constants.PERSON_BY_GROUP_CACHE_KEY, "IDB");

        //        var persons = GetOrAddValue<IEnumerable<Person>>(Constants.PERSON_LIST_CACHE_KEY,
        //            new Func<IEnumerable<Person>>(() => ToPersons(gum.ListEmailsByGroupNoRecursive(null, "IDB"))));

        //        var contractualEmail = email.ToLower().Replace("iadb.org", "contractual.iadb.org");

        //        return persons
        //            .Where(x => x.Email.Trim().Equals(email, StringComparison.InvariantCultureIgnoreCase)
        //            || x.Email.Trim().Equals(contractualEmail, StringComparison.InvariantCultureIgnoreCase))
        //            .FirstOrDefault();
        //    }
        //}

        Person GetByEmail(string email)
        {
            var person = GetByEmail(email, "IDB");
            if (person == null)
            {
                person = GetByEmail(email, "IIC");
            }
            return person;
        }

        Person GetByEmail(string email, string group)
        {
            var persons = GetByGroupName(group);

            var contractualEmail = email.ToLower().Replace("iadb.org", "contractual.iadb.org");

            return persons
                .Where(x => x.Email.Trim().Equals(email, StringComparison.InvariantCultureIgnoreCase)
                || x.Email.Trim().Equals(contractualEmail, StringComparison.InvariantCultureIgnoreCase))
                .FirstOrDefault();
        }

        IEnumerable<Person> GetByGroupName(string groupName)
        {
            using (var gum = new Gum.GUMSoapClient())
            {
                var key = string.Format(Constants.PERSON_BY_GROUP_CACHE_KEY, groupName.Trim().ToUpper());

                var persons = GetOrAddValue<IEnumerable<Person>>(key,
                    new Func<IEnumerable<Person>>(() => ToPersons(gum.ListMembers(null, true, groupName, false, false))));

                return persons;
            }
        }

        T GetOrAddValue<T>(string cacheKey, Func<T> action)
        {
            var cacheItem = _getPersonList.Execute(cacheKey);
            if (cacheItem != null && cacheItem.ExpirationDate > DateTime.Now)
            {
                return (T)cacheItem.Value;
            }
            try
            {
                if (cacheItem != null)
                {
                    _repository.MakeTransient(cacheItem.Id);
                }
                object invocationResult = action.Invoke();
                _repository.MakePersistent(new CacheItem
                {
                    Key = cacheKey,
                    Value = invocationResult,
                    ExpirationDate = DateTime.Now.AddDays(15)
                });
                return (T)invocationResult;
            }
            catch (Exception exc)
            {
                throw new ApplicationException("Error accessing GUM Service", exc);
            }
        }

        List<Person> ToPersons(IEnumerable<PersonDetails> details)
        {
            return details.Select(x => ToPerson(x)).ToList();
        }

        Person ToPerson(PersonDetails details)
        {
            Language preferredLang = Language.EN;
            var language = string.IsNullOrEmpty(details.PreferredLanguage) ? "EN" : details.PreferredLanguage;

            Enum.TryParse<Language>(language, out preferredLang);

            return new Person
            {
                Email = details.Email.TrimEnd(),
                FirstName = details.FirstName.TrimEnd(),
                Gender = details.Gender == null ? Gender.Male : (details.Gender == "M" ? Gender.Male : Gender.Female),
                LastName = details.LastName.TrimEnd(),
                PersonId = details.PersonAuth != null ? details.PersonAuth.PersonID : 0,
                PreferredLanguage = preferredLang
            };
        }
    }
}
