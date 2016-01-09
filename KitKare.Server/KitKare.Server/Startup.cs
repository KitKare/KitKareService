using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using KitKare.Data;
using System.Data.Entity;
using System.Reflection;
using KitKare.Server.App_Start;
using System.Web.Http;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using KitKare.Data.Migrations;

[assembly: OwinStartup(typeof(KitKare.Server.Startup))]

namespace KitKare.Server
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<KitKareDbContext, Configuration>());
            AutomapperConfig.RegisterMappings(Assembly.GetExecutingAssembly());

            ConfigureAuth(app);

            var config = new HttpConfiguration();
            WebApiConfig.Register(config);

            app.UseNinjectMiddleware(() => NinjectConfig.CreateKernel.Value);
            app.UseNinjectWebApi(config);
        }
    }
}
