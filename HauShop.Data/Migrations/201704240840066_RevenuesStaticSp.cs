namespace HauShop.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class RevenuesStaticSp : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure("GetRevenueStatistic",
                p => new
                {
                    fromDate = p.String(),
                    toDate = p.String()
                }
                ,
                    @"select o.CreatedDate as Date ,
                    sum(od.Quantity * od.Price) as Revenues,
                    sum((od.Quantity*od.Price)- (od.Quantity*p.OriginalPrice)) as Benefit
                    from Orders o
                    inner join OrderDetails od
                    on od.OrderID = o.ID
                    inner join Products p
                    on p.ID = od.ProductID
                    where o.CreatedDate <= CAST(@toDate as date) and o.CreatedDate >= CAST(@fromDate as date)
                    group by o.CreatedDate"
                );
        }

        public override void Down()
        {
            DropStoredProcedure("dbo.GetRevenueStatistic");
        }
    }
}