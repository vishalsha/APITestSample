using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Security.Claims;
using APIAuth.Models;

namespace APIAuth.Filters
{
    public class AuthorizationFilterAttribute : AuthorizeAttribute
    {
        EmployeeDBEntities eDB = new EmployeeDBEntities();
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext == null)
            {
                Debug.WriteLine("Context Is NUll");
            }
            else
            {
                IPrincipal incomingPrincipal = actionContext.RequestContext.Principal;
                bool isInRole = incomingPrincipal.IsInRole("Foo");
                if (!IsAuthorized(actionContext))
                {
                    HandleUnauthorizedRequest(actionContext);
                }
            }
        }
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            IPrincipal incomingPrincipal = actionContext.RequestContext.Principal;
            var c = ((ClaimsIdentity)incomingPrincipal.Identity).Claims.ToList();
          //  var subIdClaims = c.Select(f => f.ValueType = Roles);
            var apiUrl = ""; 
            var controllerField = "" ;
            var actionField = "";
            if (actionContext.RequestContext.RouteData.Values.Count() > 0)
            {
                controllerField = actionContext.RequestContext.RouteData.Values.First(f => f.Key == "controller").Value.ToString();
                actionField = actionContext.RequestContext.RouteData.Values.First(f => f.Key == "action").Value.ToString();
                apiUrl = "/api/" + controllerField + "/" + actionField;
            }
            else
            {
               apiUrl = actionContext.RequestContext.Url.Request.RequestUri.AbsolutePath;
            }

            var claimsIdentity = actionContext.RequestContext.Principal.Identity as ClaimsIdentity;
            var userIdClaim = claimsIdentity.Claims.Where(f => f.Type == ClaimTypes.Role);

            var apiAccessData = eDB.APIAccesses.Where(f => f.APIPAth == apiUrl);

            var roleAccess =
                    userIdClaim.Where(p => apiAccessData.Any(p2 => p2.Role == p.Value));

            if (roleAccess.Count() > 0)
            {
                Debug.WriteLine("Authorized");
                return true;
            }
            else
            {
                Debug.WriteLine(string.Format("Principal is authenticated at the start of IsAuthorized in CustomAuthorizationFilterAttribute: {0}", incomingPrincipal.Identity.IsAuthenticated));
                return false;
            }
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            Debug.WriteLine("Running HandleUnauthorizedRequest in CustomAuthorizationFilterAttribute as principal is not authorized.");
            base.HandleUnauthorizedRequest(actionContext);
        }
    }
}