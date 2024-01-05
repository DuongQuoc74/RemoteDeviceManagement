using Jabil.Pico.Web.DAL.Models;
using System.Data.Entity;

namespace Jabil.Pico.Web.Models.Context
{
    public class ApplicationContext : DbContext
    {
        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<Machine> Machines { get; set; }

        public DbSet<Line> Lines { get; set; }

        public DbSet<Classification> Classifications { get; set; }

        public DbSet<Device> Devices { get; set; }

        public DbSet<Setting> Settings { get; set; }

        public DbSet<ApiLog> ApiLogs { get; set; }
        public DbSet<DeviceHistory> DeviceHistories { get; set; }

        public ApplicationContext() : base("ApplicationContext")
        {

        }
    }
}