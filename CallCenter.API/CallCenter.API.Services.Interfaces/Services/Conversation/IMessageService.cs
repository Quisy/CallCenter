using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CallCenter.API.Models.Conversation;
using CallCenter.API.Services.Interfaces.Base;

namespace CallCenter.API.Services.Interfaces.Services.Conversation
{
    public interface IMessageService : ICrudService<MessageModel>
    {
    }
}
