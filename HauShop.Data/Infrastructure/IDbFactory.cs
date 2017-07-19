using System;

namespace HauShop.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        HauShopDbContext Init();
    }
}