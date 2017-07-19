using AutoMapper;
using HauShop.Model.Models;
using HauShop.Web.Models;

namespace HauShop.Web.Mappings
{
    public class AutoMapperConfiguraion
    {
        public static void Configure()
        {
            Mapper.Initialize(config => {

                config.CreateMap<Post, PostViewModel>();
                config.CreateMap<PostTag, PostTagViewModel>();
                config.CreateMap<PostCategory, PostCategoryViewModel>();
                config.CreateMap<Tag, TagViewModel>();
                config.CreateMap<ProductCategory, ProductCategoryViewModel>();
                config.CreateMap<Product, ProductViewModel>();
                config.CreateMap<ProductTag, ProductTagViewModel>();
                config.CreateMap<Footer, FooterViewModel>();
                config.CreateMap<Slide, SlideViewModel>();
                config.CreateMap<Page, PageViewModel>();
                config.CreateMap<ContactDetail, ContactDetailViewModel>();
                config.CreateMap<Feedback, FeedbackViewModel>();
                config.CreateMap<ApplicationGroup, ApplicationGroupViewModel>();
                config.CreateMap<ApplicationRole, ApplicationRoleViewModel>();
                config.CreateMap<ApplicationUser, ApplicationUserViewModel>();
                config.CreateMap<Logo, LogoViewModel>();
            });

            
        }
    }
}