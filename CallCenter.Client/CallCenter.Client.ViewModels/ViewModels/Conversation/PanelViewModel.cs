using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CallCenter.Client.Enums;
using CallCenter.Client.Services.Interfaces.Services;
using CallCenter.Client.ViewModels.Helpers.Interfaces;
using CallCenter.Client.ViewModels.ViewModels.Base;

namespace CallCenter.Client.ViewModels.ViewModels.Conversation
{
    public class PanelViewModel : BaseViewModel
    {
        private int _employeeId;

        public EmployeeStatus SelectedStatus { get; set; }
        private readonly IEmployeeService _employeeService;

        public IList<EmployeeStatus> Statuses => Enum.GetValues(typeof(EmployeeStatus)).Cast<EmployeeStatus>().ToList();

        public PanelViewModel(IContainerManager containerManager, IEmployeeService employeeService) : base(containerManager)
        {
            _employeeService = employeeService;
            SelectedStatus = EmployeeStatus.Offline;
        }

        protected override async void OnActivate()
        {
            base.OnActivate();

            _employeeId = await _employeeService.GetEmployeeId();
        }

        public async void ChangeStatus()
        {
            await _employeeService.UpdateStatus(_employeeId, SelectedStatus);
        }
    }
}
