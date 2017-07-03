using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using CallCenter.Client.WinApp.Configuration.Windsor;
using Castle.Windsor;
using Castle.Windsor.Installer;

namespace CallCenter.Client.WinApp
{
    public class AppBootstrapper : BootstrapperBase
    {
        private IWindsorContainer _container;

        public AppBootstrapper()
        {
            Initialize();
        }

        protected override void Configure()
        {

            _container = new WindsorContainer();
            _container.Install(FromAssembly.This());

            ConfigureViewModelLocator();
        }

        protected override object GetInstance(Type service, string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                return _container.Resolve(service);

            return _container.Resolve(key, service);
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ViewModels.ViewModels.MainViewModel>();
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }

        private void ConfigureViewModelLocator()
        {
            var config = new TypeMappingConfiguration
            {
                DefaultSubNamespaceForViewModels = "CallCenter.Client.ViewModels.ViewModels",
                DefaultSubNamespaceForViews = "CallCenter.Client.WinApp.Views"
            };


            ViewLocator.ConfigureTypeMappings(config);
            ViewModelLocator.ConfigureTypeMappings(config);
        }
    }
}
