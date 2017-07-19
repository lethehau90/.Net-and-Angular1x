using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HauShop.Data.Infrastructure;
using HauShop.Model.Models;


namespace HauShop.Data.Repositories
{
    public interface ILogoRepository: IRepository<Logo>
    {

    }
    public class LogoRepository : RepositoryBase<Logo>, ILogoRepository
    {
        public LogoRepository(IDbFactory factory) : base(factory)
        {

        }
    }
}
