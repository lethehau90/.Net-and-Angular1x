namespace HauShop.Data.Migrations
{
    using Model.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Collections.Generic;
    using Common;

    internal sealed class Configuration : DbMigrationsConfiguration<HauShop.Data.HauShopDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(HauShop.Data.HauShopDbContext context)
        {
            CreateUser(context);
            CreateProductCategorySimple(context);
            CreatSide(context);
            CreatePage(context);
            CreateContactDetail(context);
            CreateConfigTitle(context);
            CreateLogo(context);
            //  This method will be called after migrating to the latest version.
        }

        private void CreateLogo(HauShopDbContext context)
        {
            if (context.Logos.Count() == 0)
            {
                List<Logo> logo = new List<Logo>()
                {
                    new Logo {Name = CommonConstants.DefaultLogoId, Code = CommonConstants.DefaultLogoId, Image = "/Assets/client/images/logo.png" }
                };
                context.Logos.AddRange(logo);
                context.SaveChanges();
            }
        } 
        private void CreateUser(HauShopDbContext context)
        {
            if (!context.Users.Any())
            {
                var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new HauShopDbContext()));

                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new HauShopDbContext()));

                var user = new ApplicationUser()
                {
                    UserName = "Hau",
                    Email = "lethehau90.vn@gmail.com",
                    EmailConfirmed = true,
                    BirthDay = DateTime.Now,
                    FullName = "HauLe"
                };

                manager.Create(user, "123456$");

                if (!roleManager.Roles.Any())
                {
                    roleManager.Create(new IdentityRole { Name = "Admin" });
                    roleManager.Create(new IdentityRole { Name = "User" });
                }

                var adminUser = manager.FindByEmail("lethehau90.vn@gmail.com");

                manager.AddToRoles(adminUser.Id, new string[] { "Admin", "User" });
            }
        }

        private void CreateProductCategorySimple(HauShop.Data.HauShopDbContext context)
        {
            if (context.ProductCategories.Count() == 0)
            {
                List<ProductCategory> listProductCategory = new List<ProductCategory>()
                {
                    new ProductCategory() { Name ="Điện lạnh", Alias = "dien-lanh", Status  = true },
                    new ProductCategory() { Name ="Viễn thông", Alias = "vien-thong", Status  = true },
                    new ProductCategory() { Name ="Đồ gia dụng", Alias = "do-gia-dung", Status  = true },
                    new ProductCategory() { Name ="Mỹ phẩm", Alias = "my-pham", Status  = true }
                };
                context.ProductCategories.AddRange(listProductCategory);
                context.SaveChanges();
            }
        }

        private void CreatFooter(HauShopDbContext context)
        {
            if (context.Footers.Count(x => x.ID == CommonConstants.DefaultFooterId) == 0)
            {
                string content = @"";
            }
        }

        private void CreatSide(HauShopDbContext context)
        {
            if (context.Slides.Count() == 0)
            {
                List<Slide> listSlide = new List<Slide>()
                {
                    new Slide() { Name = "Slide 1", DisplayOrder = 1, Status = true, Url = "#" , Image ="/Assets/client/images/bag.jpg", Content=@"<h2>FLAT 50% 0FF</h2>
                                <label>FOR ALL PURCHASE <b>VALUE</b></label>
                                <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et </p>
                                <span class=""on-get"">GET NOW</span>" },
                    new Slide() { Name = "Slide 2", DisplayOrder = 2, Status = true, Url = "#" , Image ="/Assets/client/images/bag1.jpg", Content =@"<h2>FLAT 50% 0FF</h2>
                                <label>FOR ALL PURCHASE <b>VALUE</b></label>
                                <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et </p>
                                <span class=""on-get"">GET NOW</span>" }
                };
                context.Slides.AddRange(listSlide);
                context.SaveChanges();
            }
        }

        private void CreatePage(HauShopDbContext context)
        {
            if (context.Pages.Count() == 0)
            {
                var page = new Page()
                {
                    Name = "tư vấn",
                    Alias = "tu-van",
                    Content = @"+ Etc: bao gồm các file xml dùng để config cho module. Tùy theo mỗi module mà có những file xml khác nhau.
                                – Config.xml: dùng để khai báo model,helper,block…
                                – System.xml: config tạo ra 1 số field, hiển thị trên menu bên trái khi click vào systemconfig.
                                – Adminhtml.xml: dùng để config hiển thị trên menu chính của phần quản trị.
                                + Helper: trong này dùng để viết các function được sử dụng ở nhiều nơi khác nhau trong hệ thống. Cách gọi 1 helper: Mage::helper(‘tenmodule/tenhelper’)->helperTenfunction();
                                + Model: Dùng để viết các câu lệnh truy vấn trực tiếp với cơ sở dữ liệu.
                                + Sql: dùng để tạo bảng, cập nhật bảng dữ liệu, tương tác thay đổi dữ liệu…",
                    Status = true
                };
                context.Pages.Add(page);
                context.SaveChanges();
            }
        }

        private void CreateContactDetail(HauShopDbContext context)
        {
            if (context.ContactDetails.Count() == 0)
            {
                var ContactDetail = new HauShop.Model.Models.ContactDetail()
                {
                    Name = "Shop thời trang HauShop",
                    Address = "Q7 TpHCM",
                    Email = "lethehau90.vn@gmail.com",
                    Lat =  10.7473298,
                    Lng = 106.704277,
                    Phone = "0902671917",
                    Website = "Sharefree24h.com",
                    Status = true
                };
                context.ContactDetails.Add(ContactDetail);
                context.SaveChanges();
            }
        }
        private void CreateConfigTitle(HauShopDbContext context)
        {
            if(!context.SystemConfigs.Any(x=>x.Code == "HomeTitle"))
            {
                context.SystemConfigs.Add(new SystemConfig()
                {
                    Code = "HomeTitle",
                    ValueString = "Trang chủ HauShop"
                });
            }
            if (!context.SystemConfigs.Any(x => x.Code == "HomeMetaKeyWord"))
            {
                context.SystemConfigs.Add(new SystemConfig()
                {
                    Code = "HomeMetaKeyWord",
                    ValueString = "Trang chủ HauShop"
                });
            }
            if (!context.SystemConfigs.Any(x => x.Code == "HomeMetaDescription"))
            {
                context.SystemConfigs.Add(new SystemConfig()
                {
                    Code = "HomeMetaDescription",
                    ValueString = "Trang chủ HauShop"
                });
            }
        }
    }
}