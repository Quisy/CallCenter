using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;

namespace CallCenter.Client.ViewModels.Jobs
{
    public class MessageJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            JobDataMap dataMap = context.JobDetail.JobDataMap;

            Action action = (Action)dataMap["action"];

            action.Invoke();
        }
    }
}
