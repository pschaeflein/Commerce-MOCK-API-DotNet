/********************************************************
*                                                        *
*   Copyright (C) Microsoft. All rights reserved.        *
*                                                        *
*********************************************************/

using System.IdentityModel.Tokens;
using System.Web.Configuration;
using System.Web.Http;
using Microsoft.Owin.Security.ActiveDirectory;
using Microsoft.Practices.Unity;
using MSCorp.CrestMockWebApi.Filters;
using MSCorp.CrestMockWebApi.Handlers;
using MSCorp.CrestMockWebApi.Helpers;
using MSCorp.CrestMockWebApi.Interfaces;

using MSCorp.CrestMockWebApi.Repository;
using Owin;

namespace MSCorp.CrestMockWebApi
{
	// Note: By default all requests go through this OWIN pipeline. Alternatively you can turn this off by adding an appSetting owin:AutomaticAppStartup with value “false”. 
	// With this turned off you can still have OWIN apps listening on specific routes by adding routes in global.asax file using MapOwinPath or MapOwinRoute extensions on RouteTable.Routes
	public class Startup
	{
		// Invoked once at startup to configure your application.
		public void Configuration(IAppBuilder builder)
		{

			//builder.UseWindowsAzureActiveDirectoryBearerAuthentication(
			//    new WindowsAzureActiveDirectoryBearerAuthenticationOptions
			//    {
			//        Tenant = WebConfigurationManager.AppSettings["Tenant"],
			//        TokenValidationParameters = new TokenValidationParameters
			//        {
			//            ValidAudiences = new[] { WebConfigurationManager.AppSettings["Audience"] }
			//        }
			//    });

			//todo: need to implement this correctly when verifying SAToken.
			//builder.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions()
			//{
			//    AllowedAudiences = new  []{"urn:cpsvc:ca:86fd35b9-38a2-413c-a9b1-33bb75f3bd4f"},

			//    TokenValidationParameters = new TokenValidationParameters()
			//    {
			//        ValidateLifetime = false,
			//        ValidateIssuer = false,
			//        ValidateActor = false,
			//        ValidateIssuerSigningKey = false,

			//    }
			//});


			HttpConfiguration config = new HttpConfiguration();

			//filters
			config.Filters.Add(new CustomerValidationModelAttribute());
			// config.Filters.Add(new AuthorizeAttribute());

			//Dependency Resolution
			var container = new UnityContainer();
			container.RegisterType<IOrderRepository, InMemoryOrderRepository>(new HierarchicalLifetimeManager());
			container.RegisterType<ICustomerRepository, InMemoryCustomerRepository>(new HierarchicalLifetimeManager());
			container.RegisterType<ISubscriptionRepository, InMemorySubscriptionRepository>(new HierarchicalLifetimeManager());
			container.RegisterType<ITokenRepository, InMemoryTokenRepository>(new HierarchicalLifetimeManager());
			config.DependencyResolver = new UnityResolver(container);

			//Handlers
			config.MessageHandlers.Add(new MsTrackingHandler());

			// Web API configuration and services
			config.Formatters.Remove(config.Formatters.XmlFormatter);

			// Web API routes
			config.MapHttpAttributeRoutes();

			config.EnableSystemDiagnosticsTracing();

			builder.UseWebApi(config);
		}
	}
}