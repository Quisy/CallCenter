using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CallCenter.Client.Models;

namespace CallCenter.Client.Services.Interfaces.Services
{
    public interface IConversationService : IService
    {
        Task<ConversationModel> FindConversationForEmployee(int employeeId);
    }
}
