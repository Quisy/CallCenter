using System;
using System.ComponentModel;
using System.Web.Http;
using CallCenter.API.Web.Jobs;
using CallCenter.API.Web.Providers;
using CallCenter.API.Workers.Interfaces.Workers;
using Castle.Windsor;
using Hangfire;
using Hangfire.SqlServer;
using Hangfire.Windsor;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;

[assembly: OwinStartup(typeof(CallCenter.API.Web.Startup))]
namespace CallCenter.API.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureOAuth(app);

            AutoMapperConfiguration.Configure();

            var container = new WindsorContainer().Install(
                new ControllerInstaller(),
                new DefaultInstaller());
            var httpDependencyResolver = new WindsorHttpDependencyResolver(container);

            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
            config.DependencyResolver = httpDependencyResolver;
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);

            //JobStorage.Current = new SqlServerStorage("CallCenterContext");
            //JobActivator.Current = new WindsorJobActivator(container.Kernel);
            //app.UseHangfireDashboard();
            //app.UseHangfireServer();

            //StartJobs(container);

            JobScheduler.Start(container.Resolve<IProcessWorker>());
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new AuthorizationServerProvider()
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

        }

        public void StartJobs(IWindsorContainer container)
        {
            //IProcessWorker processWorker = container.Resolve<IProcessWorker>();
            BackgroundJob.Schedule(() => StartSingleJob(), TimeSpan.FromSeconds(5));
        }

        public void StartSingleJob()
        {
            RecurringJob.AddOrUpdate<IProcessWorker>(x => x.GetFacebookConversationsAndManage(), Cron.Minutely);
        }
    }
}