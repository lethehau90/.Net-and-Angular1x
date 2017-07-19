namespace HauShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateproducreGroupRole : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure("GetGroupRoleByUserName",

                p => new
                {
                    userName = p.String()
                    
                }
                ,
                @"SELECT dbo.ApplicationUsers.UserName, dbo.ApplicationGroups.Name AS GroupName , dbo.ApplicationRoles.Name AS Role
                    FROM  dbo.ApplicationUsers INNER JOIN
                                             dbo.ApplicationUserGroups ON dbo.ApplicationUsers.Id = dbo.ApplicationUserGroups.UserId INNER JOIN
                                             dbo.ApplicationGroups ON dbo.ApplicationUserGroups.GroupId = dbo.ApplicationGroups.ID INNER JOIN
                                             dbo.ApplicationRoleGroups ON dbo.ApplicationGroups.ID = dbo.ApplicationRoleGroups.GroupId INNER JOIN
                                             dbo.ApplicationRoles ON dbo.ApplicationRoleGroups.RoleId = dbo.ApplicationRoles.Id
                    GROUP BY dbo.ApplicationUsers.UserName, dbo.ApplicationGroups.Name, dbo.ApplicationRoles.Name HAVING  (dbo.ApplicationUsers.UserName =  @userName)"
                );
        }
        
        public override void Down()
        {
            DropStoredProcedure("dbo.GetGroupRoleByUserName");
        }
    }
}
