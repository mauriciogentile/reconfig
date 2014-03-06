using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IDB.SecConfig.Domain.Model;

namespace IDB.SecConfig.Domain.Tests
{
    internal static class DB
    {
        public static List<Person> Persons()
        {
            var persons = new List<Person>();
            persons.AddRange(new Person[] 
            { 
                new Person
                {
                    Email = "pipo@CommandHandler",
                    FirstName = "Pipo",
                    Gender = Gender.Male,
                    LastName = "Cipollatti",
                    PersonId = 1,
                    PreferredLanguage = Language.ES
                },
                new Person
                {
                    Email = "tony@CommandHandler",
                    FirstName = "Tony",
                    Gender = Gender.Male,
                    LastName = "Parker",
                    PersonId = 2,
                    PreferredLanguage = Language.FR
                },
                                new Person
                {
                    Email = "spike@CommandHandler",
                    FirstName = "Spike",
                    Gender = Gender.Male,
                    LastName = "Lee",
                    PersonId = 3,
                    PreferredLanguage = Language.EN
                },
                new Person
                {
                    Email = "ronaldinho@CommandHandler",
                    FirstName = "Ronald",
                    Gender = Gender.Male,
                    LastName = "Inho",
                    PersonId = 4,
                    PreferredLanguage = Language.PT
                }
            });

            return persons;
        }

        public static List<Variable> Variables()
        {
            var variables = new List<Variable>();
            return variables;
        }

        public static List<Template> Templates()
        {
            var templates = new List<Template>();
            templates.Add(new Template
            {
                CreatedBy = "mgentile",
                CreatedOn = DateTime.Now,
                Id = Guid.NewGuid().ToString(),
                Name = "TemplateEN",
                Type = TemplateType.Razor,
                Versions = new TemplateVersion[]
                {
                    new TemplateVersion
                    {
                        Body = "<p>@Model.Token1</p>",
                        Header = "TokenHeader",
                        Language = Language.EN
                    }
                }
            });

            templates.Add(new Template
            {
                CreatedBy = "mgentile",
                CreatedOn = DateTime.Now,
                Id = Guid.NewGuid().ToString(),
                Name = "TemplateENES",
                Type = TemplateType.Razor,
                Versions = new TemplateVersion[]
                {
                    new TemplateVersion
                    {
                        Body = "<p>@Model.Token1</p>",
                        Header = "TokenHeader",
                        Language = Language.EN
                    },
                    new TemplateVersion
                    {
                        Body = "<p>@Model.Token1</p>",
                        Header = "TokenHeader",
                        Language = Language.ES
                    }
                }
            });

            templates.Add(new Template
            {
                CreatedBy = "mgentile",
                CreatedOn = DateTime.Now,
                Id = Guid.NewGuid().ToString(),
                Name = "TemplateAll",
                Type = TemplateType.Razor,
                Versions = new TemplateVersion[]
                {
                    new TemplateVersion
                    {
                        Body = "<p>@Model.Token1</p>",
                        Header = "TokenHeader",
                        Language = Language.EN
                    },
                    new TemplateVersion
                    {
                        Body = "<p>@Model.Token1</p>",
                        Header = "TokenHeader",
                        Language = Language.ES
                    },
                    new TemplateVersion
                    {
                        Body = "<p>@Model.Token1</p>",
                        Header = "TokenHeader",
                        Language = Language.FR
                    },
                    new TemplateVersion
                    {
                        Body = "<p>@Model.Token1</p>",
                        Header = "TokenHeader",
                        Language = Language.PT
                    }
                }
            });

            return templates;
        }
    }
}
