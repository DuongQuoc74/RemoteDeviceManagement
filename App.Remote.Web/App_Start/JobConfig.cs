using FluentScheduler;
using Jabil.Pico.Web.BLL.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jabil.Pico.Web.App_Start
{
    public class JobConfig : Registry
    {
        public static void RegisterJobs()
        {
            JobManager.Initialize(new JobConfig());
        }

        JobConfig()
        {
            // Schedule an IJob to run at an interval
            Schedule<UpdateDeviceStatusJob>().ToRunNow().AndEvery(60).Seconds();
        }
    }
}