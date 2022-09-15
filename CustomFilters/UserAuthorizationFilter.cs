using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StackOverFlow.CustomFilters
{
    public class UserAuthorizationFilter: FilterAttribute, IAuthorizationFilter
    {
        //to check if any user has access to particular page or not 
        public void OnAuthorization(AuthorizationContext filtrContext)
        {
            if(filtrContext.RequestContext.HttpContext.Session["CurrentUserName"]==null)
            {
                filtrContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { controller = "Home", action = "Index" }));
            }
        }
    }
}