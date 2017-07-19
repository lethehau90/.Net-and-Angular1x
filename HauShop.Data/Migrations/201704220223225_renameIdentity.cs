namespace HauShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class renameIdentity : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.IdentityUserClaims", newName: "ApplicationUserClaim");
            RenameTable(name: "dbo.IdentityUserLogins", newName: "ApplicationUserLogins");
            RenameTable(name: "dbo.IdentityUserRoles", newName: "ApplicationUserRoles");
            RenameTable(name: "dbo.IdentityRoles", newName: "ApplicationRoles");
            DropPrimaryKey("dbo.ApplicationUserClaim");
            AlterColumn("dbo.ApplicationUserClaim", "Id", c => c.Int(nullable: false));
            AlterColumn("dbo.ApplicationUserClaim", "UserId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.ApplicationUserClaim", "UserId");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.ApplicationUserClaim");
            AlterColumn("dbo.ApplicationUserClaim", "UserId", c => c.String());
            AlterColumn("dbo.ApplicationUserClaim", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.ApplicationUserClaim", "Id");
            RenameTable(name: "dbo.ApplicationRoles", newName: "IdentityRoles");
            RenameTable(name: "dbo.ApplicationUserRoles", newName: "IdentityUserRoles");
            RenameTable(name: "dbo.ApplicationUserLogins", newName: "IdentityUserLogins");
            RenameTable(name: "dbo.ApplicationUserClaim", newName: "IdentityUserClaims");
        }
    }
}
