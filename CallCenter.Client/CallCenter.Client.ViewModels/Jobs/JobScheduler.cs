using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;

namespace CallCenter.Client.ViewModels.Jobs
{
    public static class JobScheduler
    {
        private static IScheduler scheduler;
        private static IJobDetail conversationjob;
        private static IJobDetail messageJob;

        public static void StartConversationJob(Action action)
        {
            scheduler = StdSchedulerFactory.GetDefaultScheduler();
            if (!scheduler.IsStarted)
                scheduler.Start();

            conversationjob = JobBuilder.Create<ConversationJob>().Build();
            conversationjob.JobDataMap["action"] = action;


            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(5)
                    .RepeatForever())
                .Build();

            scheduler.ScheduleJob(conversationjob, trigger);
        }

        public static void StopConversationJob()
        {
            scheduler.DeleteJob(conversationjob.Key);
        }

        public static void StartMessageJob(Action action)
        {
            scheduler = StdSchedulerFactory.GetDefaultScheduler();
            if(!scheduler.IsStarted)
                scheduler.Start();

            messageJob = JobBuilder.Create<MessageJob>().Build();
            messageJob.JobDataMap["action"] = action;


            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("trigger2", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(5)
                    .RepeatForever())
                .Build();

            scheduler.ScheduleJob(messageJob, trigger);
        }

        public static void StopMessageJob()
        {
            scheduler.DeleteJob(messageJob.Key);
        }

        public static void StopAllJobs()
        {
            scheduler.Clear();
        }
    }
}
