using System;
using System.Configuration;
using Reconfig.Configuration;

namespace WebApp.Test
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //AppSettings
            string rootKey = ReconfigManager.AppSettings["root_key"];

            //ConnectionStrings
            ConnectionStringSettings connString = ReconfigManager.ConnectionStrings["sec_core"];
            var providerName = connString.ConnectionString;

            //Custom Sections
            string myKey = ReconfigManager.GetSection("MySection")["MyKey"];

            Response.Write(string.Format("root_key = '{0}';", rootKey));
            Response.Write("<br/>");
            Response.Write(string.Format("connString = '{0}';", connString));
            Response.Write("<br/>");
            Response.Write(string.Format("myKey = '{0}';", myKey));
        }
    }
}