using System.Web.Mvc;
namespace KnowledgeSharing.CustomFilters
{

    public class UserAuthorizationFilterAttribute : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.RequestContext.HttpContext.Session["CurrentUserName"] == null)
            {
                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { controller = "Account", action = "Login"}));
            }
        }
    }
}