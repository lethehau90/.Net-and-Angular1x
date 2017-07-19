using HauShop.Data.Infrastructure;
using HauShop.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HauShop.Data.Repositories
{
    public interface IFooterRepository : IRepository<Footer>
    {
    }
    public class FooterRepository : RepositoryBase<Footer>, IFooterRepository
    {
        public FooterRepository(IDbFactory dbFactory): base (dbFactory)
        {
        }
    }
}
