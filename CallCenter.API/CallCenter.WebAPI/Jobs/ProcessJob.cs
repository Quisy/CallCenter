using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CallCenter.API.Workers.Interfaces.Workers;
using Quartz;

namespace CallCenter.API.Web.Jobs
{
    public class ProcessJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            JobDataMap dataMap = context.JobDetail.JobDataMap;

            IProcessWorker processWorker =  (IProcessWorker)dataMap["processWorker"];

            processWorker.GetFacebookConversationsAndManage();
        }
    }
}