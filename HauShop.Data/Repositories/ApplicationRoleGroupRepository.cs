using HauShop.Common.ViewModels;
using HauShop.Data.Infrastructure;
using HauShop.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HauShop.Data.Repositories
{
    public interface IApplicationRoleGroupRepository : IRepository<ApplicationRoleGroup>
    {
        IEnumerable<GroupRoleViewModel> GetGroupRoleByUserName(string userName);
    }
    public class ApplicationRoleGroupRepository : RepositoryBase<ApplicationRoleGroup>, IApplicationRoleGroupRepository
    {
        public ApplicationRoleGroupRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public IEnumerable<GroupRoleViewModel> GetGroupRoleByUserName(string userName)
        {
            var parameters = new object[]
            {
                new SqlParameter("@userName", userName)
            };
            return DbContext.Database.SqlQuery<GroupRoleViewModel>("GetGroupRoleByUserName @userName", parameters);
        }
    }
}
