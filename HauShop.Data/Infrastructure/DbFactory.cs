
namespace HauShop.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private HauShopDbContext dbContext;

        public HauShopDbContext Init()
        {
            return dbContext ?? (dbContext = new HauShopDbContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}