using System.Linq;
using System.Text;
using System.Web.Http.ModelBinding;

namespace Reconfig.Web.Extensions
{
    public static class HtmlExtensions
    {
        public static string GetErrors(this ModelStateDictionary modalState)
        {
            var errorsSb = new StringBuilder();
            var errors = modalState.Values.SelectMany(x => x.Errors.Select(y => string.Format("{0} {1}", y.ErrorMessage, y.Exception))).ToList();
            errors.ForEach(x => errorsSb.AppendLine(x));
            return errorsSb.ToString();
        }

        public static string GetErrors(this System.Web.Mvc.ModelStateDictionary modalState)
        {
            var errorsSb = new StringBuilder();
            var errors = modalState.Values.SelectMany(x => x.Errors.Select(y => y.ErrorMessage)).ToList();
            errors.ForEach(x => errorsSb.AppendLine(x));
            return errorsSb.ToString();
        }
    }
}