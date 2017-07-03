using Caliburn.Micro;
using CallCenter.Client.ViewModels.Helpers.Interfaces;

namespace CallCenter.Client.ViewModels.ViewModels.Base
{
    public abstract class BaseViewModel : Conductor<IScreen>.Collection.OneActive
    {
        protected readonly IContainerManager ContainerManager;

        protected BaseViewModel(IContainerManager containerManager)
        {
            ContainerManager = containerManager;
        }

        public void ActivatePage(IScreen viewModel)
        {
            ActivateItem(viewModel);
        }

        public void CloseWindow(bool? dialogResult = null)
        {
            TryClose(dialogResult);
        }
    }
}
