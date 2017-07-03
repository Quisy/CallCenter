using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CallCenter.Client.ViewModels.Helpers.Interfaces;
using CallCenter.Client.ViewModels.ViewModels.Base;

namespace CallCenter.Client.ViewModels.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel(IContainerManager containerManager) : base(containerManager)
        {
        }
    }
}
