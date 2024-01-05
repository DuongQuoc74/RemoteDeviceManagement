using Jabil.Pico.Web.BLL.Services;
using Jabil.Pico.Web.DAL.Models;
using Jabil.Pico.Web.Models;
using Jabil.Pico.Web.Models.Context;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PicoTestDataCreator
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start creating test data");
            using (var dbContext = new ApplicationContext())
            {
                var devicesv = new DeviceService(dbContext);
                var apilogsv = new ApiLogService(dbContext);
                
                foreach( var machine in  dbContext.Machines.ToArray() )
                {
                    Task.Run(async () => await devicesv.AddAsync(RandomDevice(machine.Id))).Wait();
                    Console.WriteLine("Device created:" + dbContext.Devices.Count() );
                }

                foreach (var machine in dbContext.Machines.ToArray())
                {
                    var rand = new Random();
                    int logcount = rand.Next(20, 40);
                    do
                    {
                        Task.Run(async () => await apilogsv.AddAsync(RandomApiLog(machine.Id))).Wait();
                        Console.WriteLine("Apilog created:" + dbContext.ApiLogs.Count());
                    }
                    while (--logcount > 0);
                }
            }
            Console.WriteLine("Done. Enter to exit.");
            Console.ReadLine();
        }

        internal static Device RandomDevice(int machineId)
        {
            var rand = new Random();
            string[] statusList = {"active", "deactivated", "disabled"};
            return new Device
            {
                MachineId = machineId,
                Guid = Guid.NewGuid(),
                Remark = "randomized test data",
                Status = statusList[rand.Next(0,2)],
                Available = true
            };
        }

        internal static ApiLog RandomApiLog(int machineId)
        {
            var rand = new Random();
            string[] apiList = { "ping", "getopenticket" };
            return new ApiLog
            {
                MachineId = machineId,
                Api = apiList[rand.Next(2)],
                Created = DateTime.Now,
                CreatedBy = "test",
                Request = "request",
                Response = "response"
            };
        }
    }
}
