using System;
using Quartz;

namespace CallCenter.Client.ViewModels.Jobs
{
    public class ConversationJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            JobDataMap dataMap = context.JobDetail.JobDataMap;

            Action action = (Action)dataMap["action"];

            action.Invoke();
        }
    }
}
