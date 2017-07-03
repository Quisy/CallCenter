using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CallCenter.Client.ViewModels.Helpers.Interfaces;
using CallCenter.Client.ViewModels.ViewModels.Base;
using CallCenter.Client.ViewModels.ViewModels.Conversation;

namespace CallCenter.Client.ViewModels.ViewModels.Login
{
    public class LoginViewModel : BaseViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public LoginViewModel(IContainerManager containerManager) : base(containerManager)
        {
            
        }

        protected override void OnActivate()
        {
            base.OnActivate();
            var loginViewModel = ContainerManager.GetInstance<PanelViewModel>();
            base.ActivatePage(loginViewModel);
        }
    }
}
