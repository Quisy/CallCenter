using System.Threading.Tasks;

namespace CallCenter.API.Workers.Interfaces.Workers
{
    public interface IProcessWorker
    {
        Task GetFacebookConversationsAndManage();
    }
}
