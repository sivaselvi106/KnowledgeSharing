using System;
using System.IO;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace KnowledgeSharing
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            AreaRegistration.RegisterAllAreas();
            UnityConfig.RegisterComponents();
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);            
        }
        protected void Application_Error()
        {
            Exception exec = Server.GetLastError();
            string s = "Message: " + exec.Message + ", Type: " + exec.GetType().ToString() + ", Source: " + exec.Source;
            StreamWriter sw = File.AppendText(HttpContext.Current.Request.PhysicalApplicationPath + "\\ErrorLog.txt");
            sw.WriteLine(s);
            sw.Close();
            Response.Redirect("~/Error/Index");
        }

    }
}