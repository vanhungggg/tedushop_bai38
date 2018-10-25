﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;

namespace TeduShop.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.SuppressDefaultHostAuthentication();//prevent DefaultHostAuthentication
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));//cai them microsoft.asp.net.webapi.own

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
                
            );
        }
    }
}
