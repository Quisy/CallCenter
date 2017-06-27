using CallCenter.API.Enums;
using CallCenter.API.Models.Facebook;
using CallCenter.API.Services.Interfaces.Services.Facebook;
using CallCenter.API.Utils.Helpers.Interfaces;
using CallCenter.API.Workers.Interfaces.Workers;

namespace CallCenter.API.Workers.Workerks
{
    public class ProcessWorker : IProcessWorker
    {
        private readonly ISettingsManager _settingsManager;
        private readonly Services.Interfaces.Services.Facebook.IConversationService _facebookConversationService;
        private readonly Services.Interfaces.Services.Facebook.IMessageService _messageService;
        private readonly Services.Interfaces.Services.Conversation.IConversationService _conversationService;

        public ProcessWorker(Services.Interfaces.Services.Facebook.IConversationService facebookConversationService, Services.Interfaces.Services.Conversation.IConversationService conversationService, ISettingsManager settingsManager, IMessageService messageService)
        {
            _facebookConversationService = facebookConversationService;
            _conversationService = conversationService;
            _settingsManager = settingsManager;
            _messageService = messageService;
        }

        public async void GetFacebookConversationsAndManage()
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

                if (conversationResult.IsWarning) // not exist
                {
                    if (AddConversation(fbConversation.Id))
                    {
                        await _messageService.SendMessageToConversation(fbConversation.Id, "test message");
                    }
                }

                var conversation = conversationResult.Value;


            }
        }

        private bool AddConversation(string facebookConversationId)
        {
            var conversation = new Models.Conversation.ConversationModel
            {
                FacebookConversationId = facebookConversationId,
                ProcessTask = TalkProcessTask.None,
                Status = ConversationStatus.None
            };

            var addResult = _conversationService.Add(conversation);

            if (addResult.IsError)
                return false;

            return true;
        }
    }
}
