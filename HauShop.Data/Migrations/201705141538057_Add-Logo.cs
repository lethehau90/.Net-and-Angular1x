namespace HauShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLogo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Logos",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        Image = c.String(),
                        Code = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Logos");
        }
    }
}
