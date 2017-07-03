using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CallCenter.Client.Enums;
using CallCenter.Client.Services.Interfaces.Services;
using CallCenter.Client.Utils.Helpers.Interfaces;
using CallCenter.Client.ViewModels.Helpers.Interfaces;
using CallCenter.Client.ViewModels.ViewModels.Base;
using CallCenter.Client.ViewModels.ViewModels.Conversation;
using CallCenter.Client.ViewModels.ViewModels.Login;

namespace CallCenter.Client.ViewModels.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly IAppSettingsProvider _appSettingsProvider;
        private readonly ILoginService _loginService;


        public LoginViewModel LoginViewModel { get; set; }
        public PanelViewModel PanelViewModel { get; set; }

        public MainViewModel(IContainerManager containerManager, IAppSettingsProvider appSettingsProvider, ILoginService loginService) : base(containerManager)
        {
            _appSettingsProvider = appSettingsProvider;
            _loginService = loginService;
        }

       

        protected override void OnInitialize()
        {
            base.OnInitialize();

            PanelViewModel = ContainerManager.GetInstance<PanelViewModel>();
            LoginViewModel = ContainerManager.GetInstance<LoginViewModel>();
            base.ActivatePage(LoginViewModel);
        }

        public async void Login()
        {
            var result = await _loginService.Login(LoginViewModel.Username, LoginViewModel.Password);

            if (result != null)
            {
                _appSettingsProvider.Save(Constants.AppSettings.UserTokenKeyName, result.Token);
                base.ActivatePage(PanelViewModel);
            }
               
        }
    }
}
