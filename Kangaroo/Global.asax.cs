using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Kangaroo.Common;
using Kangaroo.Utils;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;

namespace Kangaroo
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static string GetCurrentUser
        {
            get
            {
                return string.IsNullOrEmpty(Thread.CurrentPrincipal.Identity.Name) ? "fakeuser@domain.com" : Thread.CurrentPrincipal.Identity.Name;
            }
        }
        protected void Application_Start()
        {
            ModelBinders.Binders.DefaultBinder = new ObjectIdModelBinder();
            AreaRegistration.RegisterAllAreas();
            // IdentityConfig.ConfigureIdentity();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            BsonSerializer.RegisterSerializationProvider(new CustomDecimalSerializer());

        }
    }
}
