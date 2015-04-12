using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ServiceInterfaceFramework
{
    internal static class KeyCreator
    {
        private static string TRIGGER = "Trigger";

        public static JobKey GetJobKey(this string serviceName)
        {
            return new JobKey(serviceName);
        }

        public static TriggerKey GetTriggerKey(this string serviceName)
        {
            return new TriggerKey(serviceName + TRIGGER);
        }
    }

    public static class ServiceManager
    {
        private static ISchedulerFactory schedulerFactory;
        private static IScheduler scheduler;
        private static string TRIGGER = "Trigger";

        public static void StartWcfHost()
        {
            Host.HostService("some class type".GetType()); //寄宿
        }

        public static void StartAll()
        {
            if (schedulerFactory == null)
            {
                schedulerFactory = new StdSchedulerFactory();
                if (scheduler == null || !scheduler.IsStarted)
                {
                    scheduler = schedulerFactory.GetScheduler();
                    scheduler.Start();
                }
            }
        }

        public static void ReStart()
        {
            scheduler.Shutdown(true);

            int cnt = 0;
            while (!scheduler.IsShutdown && cnt++ < 60)
            {
                Thread.Sleep(2000);
            }

            scheduler = schedulerFactory.GetScheduler();
            scheduler.Start();
        }

        public static void Continue(string serviceName)
        {
            scheduler.ResumeTrigger(serviceName.GetTriggerKey());
        }

        public static EServiceStatus GetStatus(string serviceName)
        {
            foreach (var item in scheduler.GetCurrentlyExecutingJobs())
            {
                if (item.JobDetail.Key.Name == serviceName) return EServiceStatus.Running;
            }

            bool isPause = scheduler.IsTriggerGroupPaused(serviceName + TRIGGER);
            if (isPause) return EServiceStatus.Pause;

            return EServiceStatus.Normal;
        }

        public static string GetSchedule(string serviceName)
        {
            var triggerName = serviceName + TRIGGER;
            var trigger = scheduler.GetTrigger(serviceName.GetTriggerKey());
            if (trigger != null)
            {
                var cronBuilder = trigger as ICronTrigger;
                return JobCronExpressionConfig.GetCronExpression(triggerName);
            }
            return string.Empty;
        }

        public static void Pause(string serviceName)
        {
            scheduler.PauseTrigger(serviceName.GetTriggerKey());
        }

        public static void ModifySchedule(string serviceName, string cronExpresion)
        {
            var triggerName = serviceName + TRIGGER;
            var trigger = scheduler.GetTrigger(serviceName.GetTriggerKey());
            if (trigger != null)
            {
                var cronBuilder = trigger as ICronTrigger;
                JobCronExpressionConfig.SetCronExpression(triggerName, cronExpresion);
                cronBuilder.CronExpressionString = cronExpresion;
                var newTrigger = cronBuilder.GetTriggerBuilder().Build();

                scheduler.PauseTrigger(new TriggerKey(triggerName));
                scheduler.RescheduleJob(new TriggerKey(triggerName), newTrigger);
                scheduler.ResumeTrigger(new TriggerKey(triggerName));
            }
        }

        public static void StopAll()
        {
            if (scheduler != null)
            {
                scheduler.Shutdown(true);
            }
        }
    }

    public class JobListener : IJobListener
    {
        public void JobExecutionVetoed(IJobExecutionContext context)
        {
            throw new NotImplementedException();
        }

        public void JobToBeExecuted(IJobExecutionContext context)
        {
            throw new NotImplementedException();
        }

        public void JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException)
        {
            throw new NotImplementedException();
        }

        public string Name
        {
            get { throw new NotImplementedException(); }
        }
    }
}
