using System.Web;
using Reconfig.Domain.Model;

namespace Reconfig.Web.Models
{
    public static class SessionModel
    {
        public static Application SelectedApplication
        {
            get { return (Application)HttpContext.Current.Session["Application"]; }
        }

        public static bool ShowTemplateMenu
        {
            get
            {
                return SelectedApplication != null;
            }
        }

        public static bool HasSelectedApplication
        {
            get
            {
                return SelectedApplication != null;
            }
        }
    }
}