using System.Web.Mvc;

namespace IDB.SecConfig.Web.Controllers
{
    public class ApplicationController : BaseController
    {
        public ActionResult Index()
        {
            return PartialView();
        }

        public ActionResult Create()
        {
            return PartialView();
        }

        public ActionResult Edit()
        {
            return PartialView();
        }
    }
}
