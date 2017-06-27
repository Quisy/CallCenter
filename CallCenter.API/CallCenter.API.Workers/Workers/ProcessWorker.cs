using System.Threading.Tasks;
using CallCenter.API.Enums;
using CallCenter.API.Services.Interfaces.Services.Facebook;
using CallCenter.API.Utils.Helpers.Interfaces;
using CallCenter.API.Workers.Interfaces.Workers;

namespace CallCenter.API.Workers.Workers
{
    public class ProcessWorker : IProcessWorker
    {
        private readonly ISettingsManager _settingsManager;
        private readonly Services.Interfaces.Services.Facebook.IConversationService _facebookConversationService;
        private readonly Services.Interfaces.Services.Facebook.IMessageService _facebbokMessageService;
        private readonly Services.Interfaces.Services.Conversation.IConversationService _conversationService;
        private readonly IActivitiWorker _activitiWorker;

        public ProcessWorker(Services.Interfaces.Services.Facebook.IConversationService facebookConversationService, Services.Interfaces.Services.Conversation.IConversationService conversationService, ISettingsManager settingsManager, IMessageService messageService, IActivitiWorker activitiWorker)
        {
            _facebookConversationService = facebookConversationService;
            _conversationService = conversationService;
            _settingsManager = settingsManager;
            _facebbokMessageService = messageService;
            _activitiWorker = activitiWorker;
        }

        public async Task GetFacebookConversationsAndManage()
        {
            var facebookConversationsResult =
                await _facebookConversationService.GetPageConversationsWithUnreadMessages(
                    _settingsManager.Load(Constants.SettingsConstants.FacebookPageIdKeyName));

            if (facebookConversationsResult.IsError)
                return;

            var facebookConversations = facebookConversationsResult.Value;

            foreach (var fbConversation in facebookConversations)
            {
                var conversationResult = _conversationService.GetConversationByFacebookCnversationId(fbConversation.Id);

                // If new conversation
                if (conversationResult.IsWarning)
                {
                    var processInstanceResult = await _activitiWorker.StartInstanceOfProcessDefinitionAsync(_settingsManager.Load(Constants
                        .SettingsConstants.ActivitiTalkProcessNameKeyName));

                    if (processInstanceResult.IsError)
                        continue;

                    if (AddConversation(fbConversation.Id, processInstanceResult.Value.Id))
                    {
                        var mesageResult = await _facebbokMessageService.SendMessageToConversation(fbConversation.Id, "test message");

                        if (mesageResult.IsError)
                            return;

                        var message = mesageResult.Value;
                    }
                }

                var conversation = conversationResult.Value;
                var messagesResult = await _facebbokMessageService.GetConversationMessages(conversation.FacebookConversationId);

                var messages = messagesResult.Value;


            }
        }

        private bool AddConversation(string facebookConversationId, int processInstanceId)
        {
            var conversation = new Models.Conversation.ConversationModel
            {
                FacebookConversationId = facebookConversationId,
                ProcessInstanceId = processInstanceId,
                ProcessTask = TalkProcessTask.Manager,
                Status = ConversationStatus.None
            };

            var addResult = _conversationService.Add(conversation);

            if (addResult.IsError)
                return false;

            return true;
        }

        //private bool AddMessage(int conversationId, string message)
        //{

        //}
    }
}
