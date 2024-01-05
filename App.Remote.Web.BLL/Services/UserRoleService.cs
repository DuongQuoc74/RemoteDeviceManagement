using Jabil.Pico.Common.ViewModels;
using Jabil.Pico.Web.Models;
using Jabil.Pico.Web.Models.Context;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Jabil.Pico.Web.BLL.Services
{
    public class UserRoleService : IUserRoleService
    {
        private readonly ApplicationContext _ctx;

        public UserRoleService(ApplicationContext ctx)
        {
            this._ctx = ctx;
        }

        public async Task<UserRoleVM[]> GetAllAsync()
        {
            var result = await _ctx.Database.SqlQuery<UserRoleVM>("EXEC pi_GetUserRols").ToArrayAsync();
            return result;
        }

        public async Task<UserRoleVM> GetByIdAsync(int userId)
        {
            var sqlParams = new SqlParameter[]
                  {
                        new SqlParameter("@UserId", SqlDbType.UniqueIdentifier)
                            {
                                Value = userId
                            }
                  };
            var user = await _ctx.Database.SqlQuery<UserRoleVM>("EXEC pi_GetUserRolsById @UserId", sqlParams).FirstOrDefaultAsync();
            return user;
        }

        public async Task<UserRoleVM> GetByNameAsync(string userName)
        {
            if (userName.Contains("\\"))
            {
                var name = userName.Split(new string[] { "\\" }, StringSplitOptions.None)[1];
                var sqlParams = new SqlParameter[]
                  {
                        new SqlParameter("@UserName", SqlDbType.NVarChar)
                            {
                                Value = name
                            }
                  };
                var user = await _ctx.Database.SqlQuery<UserRoleVM>("EXEC pi_GetUserRolsByName @UserName", sqlParams).FirstOrDefaultAsync();
                return user;
            }

            var sqlParams1 = new SqlParameter[]
             {
                        new SqlParameter("@UserName", SqlDbType.NVarChar)
                        {
                                Value = userName
                            }
             };
            var user1 = await _ctx.Database.SqlQuery<UserRoleVM>("EXEC pi_GetUserRolsByName @UserName", sqlParams1).FirstOrDefaultAsync();
            return user1;
        }

    

        public async Task UpdateAsync (UserRoleVM user)
        {
            var sqlParams = new SqlParameter[]
               {
                     new SqlParameter("@EmployeeNumber", SqlDbType.NVarChar)
                         {
                             Value = user.EmployeeNumber
                         },
                     new SqlParameter("@WindowsNT", SqlDbType.NVarChar)
                         {
                             Value = user.WindowsNT
                         },
                     new SqlParameter("@RolID", SqlDbType.Int)
                         {
                             Value = user.RoleId
                         },
                     new SqlParameter("@Available", SqlDbType.Bit)
                         {
                             Value = user.Available
                         },
                     new SqlParameter("@Id", SqlDbType.Int)
                         {
                             Value = user.Id
                         }
               };
            var result = await _ctx.Database.ExecuteSqlCommandAsync("EXEC pi_UpdateUserRols @EmployeeNumber, @WindowsNT, @RolID, @Available, @Id", sqlParams);
            if (result <= 0)
            {
                throw new InvalidOperationException($"User {user.Id} does not exist");
            }     
        }

        public async Task ActivateAsync(int userId)
        {
            var result = await _ctx.Database.ExecuteSqlCommandAsync("EXEC pi_ActivateUserRols @Id", new SqlParameter("Id", userId));
            if (result <= 0)
            {
                throw new InvalidOperationException($"User {userId} does not exist");
            }
        }

        public async Task DeactivateAsync(int userId)
        {
            var result = await _ctx.Database.ExecuteSqlCommandAsync("EXEC pi_DeactivateUserRols @Id", new SqlParameter("Id", userId));
            if (result <= 0)
            {
                throw new InvalidOperationException($"User {userId} does not exist");
            }
        }

        public async Task PromoteAsync(int userId)
        {
            var result = await _ctx.Database.ExecuteSqlCommandAsync("EXEC pi_PromoteUserRols @Id", new SqlParameter("Id", userId));
            if (result <= 0)
            {
                throw new InvalidOperationException($"User {userId} does not exist");
            }
        }

        public async Task DemoteAsync(int userId)
        {
            var result = await _ctx.Database.ExecuteSqlCommandAsync("EXEC pi_DemoteUserRols @Id", new SqlParameter("Id", userId));
            if (result <= 0)
            {
                throw new InvalidOperationException($"User {userId} does not exist");
            }
        }

        public async Task DeleteAsync(int userId)
        {
            var sqlParams = new SqlParameter[]
                 {
                        new SqlParameter("@Id", SqlDbType.Int)
                            {
                                Value = userId
                            }                     
                 };
            var result = await _ctx.Database.ExecuteSqlCommandAsync("EXEC pi_DeleteUserRols @Id", sqlParams);
            if (result <= 0)
            {
                throw new InvalidOperationException($"User {userId} does not exist");
            }
        }

        public async Task<bool> AddAsync(UserRoleVM user)
        {
            var sqlParams = new SqlParameter[]
               {
                     new SqlParameter("@EmployeeNumber", SqlDbType.NVarChar)
                         {
                             Value = user.EmployeeNumber
                         },
                     new SqlParameter("@WindowsNT", SqlDbType.NVarChar)
                         {
                             Value = user.WindowsNT
                         },
                     new SqlParameter("@RolID", SqlDbType.Int)
                         {
                             Value = user.RoleId
                         },
                     new SqlParameter("@Available", SqlDbType.Bit)
                         {
                             Value = user.Available
                         }
               };
                var result = await _ctx.Database.ExecuteSqlCommandAsync("EXEC pi_AddUserRols @EmployeeNumber, @WindowsNT, @RolID, @Available", sqlParams);
                if (result <= 0)
                {
                    return false;
                }
            return true;
        }

        public UserRoleVM GetByName(string userName)
        {
            if (userName.Contains("\\"))
            {
                var name = userName.Split(new string[] { "\\" }, StringSplitOptions.None)[1];
                var sqlParams = new SqlParameter[]
                  {
                        new SqlParameter("@UserName", SqlDbType.NVarChar)
                            {
                                Value = name
                            }
                  };
                var user =  _ctx.Database.SqlQuery<UserRoleVM>("EXEC pi_GetUserRolsByName @UserName", sqlParams).FirstOrDefault();
                return user;
            }

            var sqlParams1 = new SqlParameter[]
             {
                        new SqlParameter("@UserName", SqlDbType.NVarChar)
                        {
                                Value = userName
                        }
             };
            var user1 =  _ctx.Database.SqlQuery<UserRoleVM>("EXEC pi_GetUserRolsByName @UserName", sqlParams1).FirstOrDefault();
            return user1;
        }
    }
}
