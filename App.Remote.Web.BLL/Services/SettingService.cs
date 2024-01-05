using Jabil.Pico.Common.ViewModels;
using Jabil.Pico.Web.Models;
using Jabil.Pico.Web.Models.Context;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Caching;
using System.Threading.Tasks;

namespace Jabil.Pico.Web.BLL.Services
{
    public class SettingService : ISettingService
    {
        private readonly ApplicationContext _ctx;
        private readonly MemoryCache _cache;
        public SettingService(ApplicationContext ctx)
        {
            this._ctx = ctx;
            _cache = MemoryCache.Default;
        }

        public async Task<SettingVM[]> GetAllAsync()
        {
            string cacheKey = "settingData";
            if (_cache.Contains(cacheKey))
            {
                // Nếu có dữ liệu trong cache, trả về dữ liệu từ cache
                return (SettingVM[])_cache.Get(cacheKey);
            }
            var result = await _ctx.Database.SqlQuery<SettingVM>("EXEC pi_GetSettings").ToArrayAsync();
            var time = ConfigurationManager.AppSettings.Get("cacheSetting");
            DateTimeOffset expiration = DateTimeOffset.Now.AddMinutes(Int64.Parse(time));
            _cache.Set(cacheKey, result, expiration);
            return result;
        }

        public async Task<SettingVM[]> GetAllNowAsync()
        {
            var result = await _ctx.Database.SqlQuery<SettingVM>("EXEC pi_GetSettings").ToArrayAsync();
            return result;
        }

        public async Task<SettingVM> GetByNameAsync( string name)
        {
            var result = await _ctx.Database.SqlQuery<SettingVM>("EXEC pi_GetSettingByName @Name", new SqlParameter("Name", name)).FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> UpdateAsync(SettingVM request)
        {

            var parameterSql = new SqlParameter[]
            {
                new SqlParameter("@Name", SqlDbType.NVarChar)
                {
                    Value = request.Name
                },
                new SqlParameter("@Value", SqlDbType.NVarChar)
                {
                    Value = request.Value
                },
                new SqlParameter("@Available", SqlDbType.Bit)
                {
                    Value = true
                },
            };
            var result = await _ctx.Database.ExecuteSqlCommandAsync("EXEC pi_UpdateSetting @Name, @Value, @Available", parameterSql);
            if(result !=1)
                return false;
            return true;
        }
    }
}
