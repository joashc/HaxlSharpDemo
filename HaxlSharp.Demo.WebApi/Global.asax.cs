using HaxlSharp.Demo.DataLayer;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using HaxlSharp.Demo.WebApi.App_Start;

namespace HaxlSharp.Demo.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var fetcher = new RegisterHandlers(new FriendApiClient(), new PostApiClient()).GetFetcher();
            var container = new Container();
            container.RegisterSingleton(fetcher);
            container.Verify();
            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
