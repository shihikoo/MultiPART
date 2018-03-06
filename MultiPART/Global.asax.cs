using System;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using MultiPART.Infrastructure;
using MultiPART.Models.ViewModel;
using WebMatrix.WebData;

namespace MultiPART
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            if (!WebSecurity.Initialized)
            {
               WebSecurity.InitializeDatabaseConnection("DefaultConnection",  "UserProfile", "UserId", "UserName", autoCreateTables: false);
            
            }

            if (!Roles.RoleExists("Administrator"))
            {
                Roles.CreateRole("Administrator");
            }
            if (!WebSecurity.UserExists("admin"))
            {
                WebSecurity.CreateUserAndAccount("admin", "admin", new { ForeName = "admin", SurName = "admin", Email = "jing.liao@ed.ac.uk", Details = "admin", Status = "Current", CreatedOn = DateTimeOffset.Now, LastUpdatedOn = DateTimeOffset.Now, LastUpdatedBy = 1 });
            }
            if (!Roles.GetRolesForUser("admin").Contains("Administrator"))
            {
                Roles.AddUsersToRoles(new[] { "admin" }, new[] { "Administrator" });
            }
             
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
            ModelBinders.Binders.Add(typeof(DataEntryDetailViewModel),new EnhancedDefaultModelBinder());
        }
    }
}