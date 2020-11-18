using System.Web.Optimization;
namespace KnowledgeSharing
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts/bootstrap").Include("~/Scripts/jquery-3.4.1.js", "~/Scripts/umd/popper.js", "~/Scripts/bootstrap.js"));
            bundles.Add(new StyleBundle("~/Styles/bootstrap").Include("~/Content/bootstrap.css"));
            bundles.Add(new StyleBundle("~/Styles/site").Include("~/Content/Style.css"));
            BundleTable.EnableOptimizations = true;
        }
    }
}