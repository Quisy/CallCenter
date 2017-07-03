using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CallCenter.Client.Enums;
using CallCenter.Client.ViewModels.Helpers.Interfaces;
using CallCenter.Client.ViewModels.ViewModels.Base;

namespace CallCenter.Client.ViewModels.ViewModels.Conversation
{
    public class PanelViewModel : BaseViewModel
    {
        public EmployeeStatus SelectedStatus { get; set; }

        public IList<EmployeeStatus> Statuses => Enum.GetValues(typeof(EmployeeStatus)).Cast<EmployeeStatus>().ToList();

        public PanelViewModel(IContainerManager containerManager) : base(containerManager)
        {
            SelectedStatus = EmployeeStatus.Offline;
        }

        public void ChangeStatus()
        {

        }
    }
}
