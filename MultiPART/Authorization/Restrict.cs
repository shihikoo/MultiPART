using System;
using System.Web;
using System.Web.Mvc;

namespace MultiPART.Authorization
{
    public class Restrict : AuthorizeAttribute
    {
        private readonly string _role;

        public Restrict(string role)
        {
            _role = role;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");

            if (httpContext.User.IsInRole(_role))
                return false;

            return true;
        }
    }
}
