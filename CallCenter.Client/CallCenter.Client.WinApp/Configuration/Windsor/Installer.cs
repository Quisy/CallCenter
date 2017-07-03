using Caliburn.Micro;
using CallCenter.Client.Utils.Helpers;
using CallCenter.Client.Utils.Helpers.Interfaces;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace CallCenter.Client.WinApp.Configuration.Windsor
{
    public class Installer : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromAssemblyContaining<CallCenter.Client.ViewModels.ViewModels.Base.BaseViewModel>()
                .BasedOn<Client.ViewModels.ViewModels.Base.BaseViewModel>()
                .WithService.DefaultInterfaces()
                .LifestyleTransient());

            container.Register(Classes.FromAssemblyContaining<CallCenter.Client.ViewModels.Helpers.BaseHelper>()
                .BasedOn<Client.ViewModels.Helpers.BaseHelper>()
                .WithService.DefaultInterfaces()
                .LifestyleTransient());

            container.Register(Classes.FromAssemblyContaining<CallCenter.Client.Services.Base.BaseService>()
                .BasedOn<Client.Services.Interfaces.IService>()
                .WithService.DefaultInterfaces()
                .LifestyleTransient());



            container.Register(Component.For<IConfigurationProvider>()
                .ImplementedBy<ConfigurationProvider>());

            container.Register(Component.For<IAppSettingsProvider>()
                .ImplementedBy<AppSettingsProvider>());

            container.Register(Component.For<IWindowManager>()
                .ImplementedBy<WindowManager>());
        }
    }
}
