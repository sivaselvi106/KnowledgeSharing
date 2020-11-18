using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace KnowledgeSharing.CustomFilters
{
    public class MyAuthenticationFilterAttribute : FilterAttribute { 
        public void OnAuthentication(AuthenticationContext authenticationContext)
        {
            if(!authenticationContext.HttpContext.User.Identity.IsAuthenticated)
            {
            authenticationContext.Result = new HttpUnauthorizedResult();
            }
        }
    }
}