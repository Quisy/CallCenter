using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CallCenter.API.Workers.Interfaces.Workers;
using Quartz;
using Quartz.Impl;

namespace CallCenter.API.Web.Jobs
{
    public class JobScheduler
    {
        public static void Start(IProcessWorker processWorker)
        {
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();

            IJobDetail job = JobBuilder.Create<ProcessJob>().Build();
            job.JobDataMap["processWorker"] = processWorker;

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(10)
                    .RepeatForever())
                .Build();

            scheduler.ScheduleJob(job, trigger);
        }
    }
}