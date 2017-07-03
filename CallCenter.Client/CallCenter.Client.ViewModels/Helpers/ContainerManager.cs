using Caliburn.Micro;
using CallCenter.Client.ViewModels.Helpers.Interfaces;

namespace CallCenter.Client.ViewModels.Helpers
{
    public class ContainerManager : BaseHelper, IContainerManager
    {
        public T GetInstance<T>()
        {
            return IoC.Get<T>();
        }
    }
}
