using System.Web.Mvc;

namespace Reconfig.Web.Controllers
{
    public class PartialViewController : BaseController
    {
        public ActionResult Get(string id)
        {
            return PartialView(id);
        }
    }
}
