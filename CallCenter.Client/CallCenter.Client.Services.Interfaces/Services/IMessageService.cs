using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CallCenter.Client.Models;

namespace CallCenter.Client.Services.Interfaces.Services
{
    public interface IMessageService : IService
    {
        Task<IList<MessageModel>> GetAllMessagesForConversation(int conversationId);
        Task<MessageModel> SendMessage(int conversationId, string content);
    }
}
