namespace CallCenter.Client.ViewModels.Helpers.Interfaces
{
    public interface IContainerManager
    {
        T GetInstance<T>();
    }
}
