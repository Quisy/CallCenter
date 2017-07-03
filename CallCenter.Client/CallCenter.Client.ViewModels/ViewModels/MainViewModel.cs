using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CallCenter.Client.Enums;
using CallCenter.Client.ViewModels.Helpers.Interfaces;
using CallCenter.Client.ViewModels.ViewModels.Base;
using CallCenter.Client.ViewModels.ViewModels.Conversation;
using CallCenter.Client.ViewModels.ViewModels.Login;

namespace CallCenter.Client.ViewModels.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public LoginViewModel LoginViewModel { get; set; }
        public PanelViewModel PanelViewModel { get; set; }

        public MainViewModel(IContainerManager containerManager) : base(containerManager)
        {
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            PanelViewModel = ContainerManager.GetInstance<PanelViewModel>();
            LoginViewModel = ContainerManager.GetInstance<LoginViewModel>();
            base.ActivatePage(LoginViewModel);
        }

        public void Login()
        {
            base.ActivatePage(PanelViewModel);
        }
    }
}
