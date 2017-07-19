using AutoMapper;
using HauShop.Model.Models;
using HauShop.Web.Models;
using System;
using System.Collections;
using System.Collections.Generic;

namespace HauShop.Web.Infrastructure.Extensions
{
    public static class EntityExtensions
    {
        //UpdatePostCategory
        public static void UpdatePostCategory(this PostCategory postCategory, PostCategoryViewModel postCategoryVM)
        {
            postCategory.ID = postCategoryVM.ID;
            postCategory.Name = postCategoryVM.Name;
            postCategory.Alias = postCategoryVM.Alias;
            postCategory.Description = postCategoryVM.Description;
            postCategory.ParentID = postCategoryVM.ParentID;
            postCategory.DisplayOrder = postCategoryVM.DisplayOrder;
            postCategory.Image = postCategoryVM.Image;
            postCategory.HomeFlag = postCategoryVM.HomeFlag;

            postCategory.CreatedBy = postCategoryVM.CreatedBy;
            postCategory.CreatedDate = postCategoryVM.CreatedDate;
            postCategory.UpdatedDate = postCategoryVM.UpdatedDate;
            postCategory.UpdatedBy = postCategoryVM.UpdatedBy;
            postCategory.MetaKeyword = postCategoryVM.MetaKeyword;
            postCategory.MetaDescription = postCategoryVM.MetaDescription;
            postCategory.Status = postCategoryVM.Status;
        }

        //UpdatePost
        public static void UpdatePost(this Post post, PostViewModel postVM)
        {
            post.ID = postVM.ID;
            post.Name = postVM.Name;
            post.Alias = postVM.Alias;
            post.CategoryID = postVM.CategoryID;
            post.Image = postVM.Image;
            post.Description = postVM.Description;
            post.Content = postVM.Content;
            post.HomeFlag = postVM.HomeFlag;
            post.HotFlag = postVM.HotFlag;
            post.ViewCount = postVM.ViewCount;
            post.Tags = postVM.Tags;

            post.CreatedBy = postVM.CreatedBy;
            post.CreatedDate = postVM.CreatedDate;
            post.UpdatedDate = postVM.UpdatedDate;
            post.UpdatedBy = postVM.UpdatedBy;
            post.MetaKeyword = postVM.MetaKeyword;
            post.MetaDescription = postVM.MetaDescription;
            post.Status = postVM.Status;
        }

        //UpdateProduct
        public static void UpdateProduct(this Product product, ProductViewModel productVM)
        {
            product.ID = productVM.ID;
            product.Name = productVM.Name;
            product.Alias = productVM.Alias;
            product.CategoryID = productVM.CategoryID;
            product.Image = productVM.Image;
            product.MoreImages = productVM.MoreImages;
            product.Price = productVM.Price;
            product.PromotionPrice = productVM.PromotionPrice;
            product.Warranty = productVM.Warranty;
            product.Description = productVM.Description;
            product.Content = productVM.Content;
            product.HomeFlag = productVM.HomeFlag;
            product.HotFlag = productVM.HotFlag;
            product.ViewCount = productVM.ViewCount;
            product.Tags = productVM.Tags;
            product.Quantity = productVM.Quantity;
            product.OriginalPrice = productVM.OriginalPrice;

            product.CreatedBy = productVM.CreatedBy;
            product.CreatedDate = productVM.CreatedDate;
            product.UpdatedDate = productVM.UpdatedDate;
            product.UpdatedBy = productVM.UpdatedBy;
            product.MetaKeyword = productVM.MetaKeyword;
            product.MetaDescription = productVM.MetaDescription;
            product.Status = productVM.Status;
        }

        //UpdateProductCategory
        public static void UpdateProductCategory(this ProductCategory productCategory, ProductCategoryViewModel productCategoryVM)
        {
           productCategory.ID = productCategoryVM.ID;
           productCategory.Name = productCategoryVM.Name;
           productCategory.Alias = productCategoryVM.Alias;
           productCategory.Description = productCategoryVM.Description;
           productCategory.ParentID = productCategoryVM.ParentID;
           productCategory.DisplayOrder = productCategoryVM.DisplayOrder;
           productCategory.Image = productCategoryVM.Image;
           productCategory.HomeFlag = productCategoryVM.HomeFlag;

           productCategory.CreatedBy = productCategoryVM.CreatedBy;
           productCategory.CreatedDate = productCategoryVM.CreatedDate;
           productCategory.UpdatedDate = productCategoryVM.UpdatedDate;
           productCategory.UpdatedBy = productCategoryVM.UpdatedBy;
           productCategory.MetaKeyword = productCategoryVM.MetaKeyword;
           productCategory.MetaDescription = productCategoryVM.MetaDescription;
           productCategory.Status = productCategoryVM.Status;
        }

        //UpdateFooter
        public static void UpdateFooter(this Footer footer, FooterViewModel footerVM)
        {
            footer.ID = footerVM.ID;
            footer.Content = footerVM.Content;
        }

        //UpdateSlide
        public static void UpdateSlide(this Slide slide, SlideViewModel slideVM)
        {
            slide.ID = slideVM.ID;
            slide.Name = slideVM.Name;
            slide.Description = slideVM.Description;
            slide.Image = slideVM.Image;
            slide.Url = slideVM.Url;
            slide.DisplayOrder = slideVM.DisplayOrder;
            slide.Status = slideVM.Status;
            slide.Content = slideVM.Content;
        }

        //Page
        public static void UpdatePage(this Page page, PageViewModel pageVM)
        {
            page.ID = pageVM.ID;
            page.Name = pageVM.Name;
            page.Alias = pageVM.Alias;
            page.Content = pageVM.Content;
            page.Ord = pageVM.Ord;

            page.CreatedBy = pageVM.CreatedBy;
            page.CreatedDate = pageVM.CreatedDate;
            page.UpdatedDate = pageVM.UpdatedDate;
            page.UpdatedBy = pageVM.UpdatedBy;
            page.MetaKeyword = pageVM.MetaKeyword;
            page.MetaDescription = pageVM.MetaDescription;
            page.Status = pageVM.Status;
        }

        public static void UpdateFeedback(this Feedback feedback, FeedbackViewModel feedbackVM)
        {
            feedback.ID = feedbackVM.ID;
            feedback.Name = feedbackVM.Name;
            feedback.Email = feedbackVM.Email;
            feedback.Message = feedbackVM.Message;
            feedback.CreateDate = feedbackVM.CreateDate;
            feedback.Status = feedbackVM.Status;
        }

        public static void UpdateOrder(this Order order, OrderViewModel orderVm)
        {
            order.CustomerName = orderVm.CustomerName;
            order.CustomerAddress = orderVm.CustomerAddress;
            order.CustomerEmail = orderVm.CustomerEmail;
            order.CustomerMobile = orderVm.CustomerMobile;
            order.CustomerMessage = orderVm.CustomerMessage;
            order.PaymentMethod = orderVm.PaymentMethod;
            order.CreatedDate = DateTime.Now;
            order.CreatedBy = orderVm.CreatedBy;
            order.Status = orderVm.Status;
            order.CustomerId = orderVm.CustomerId;
        }
        public static void UpdateLogo(this Logo logo, LogoViewModel logoVm)
        {
            logo.ID = logoVm.ID;
            logo.Name = logoVm.Name;
            logo.Image = logoVm.Image;
            logo.Code = logoVm.Code;
        }

        public static void UpdateApplicationGroup(this ApplicationGroup appGroup, ApplicationGroupViewModel appGroupViewModel)
        {
            appGroup.ID = appGroupViewModel.ID;
            appGroup.Name = appGroupViewModel.Name;
            appGroup.Description = appGroupViewModel.Description;
        }

        public static void UpdateApplicationRole(this ApplicationRole appRole, ApplicationRoleViewModel appRoleViewModel, string action = "add")
        {
            if (action == "update")
                appRole.Id = appRoleViewModel.Id;
            else
                appRole.Id = Guid.NewGuid().ToString();
            appRole.Name = appRoleViewModel.Name;
            appRole.Description = appRoleViewModel.Description;
        }
        public static void UpdateUser(this ApplicationUser appUser, ApplicationUserViewModel appUserViewModel, string action = "add")
        {

            appUser.Id = appUserViewModel.Id;
            appUser.FullName = appUserViewModel.FullName;
            appUser.BirthDay = appUserViewModel.BirthDay;
            appUser.Email = appUserViewModel.Email;
            appUser.UserName = appUserViewModel.UserName;
            appUser.PhoneNumber = appUserViewModel.PhoneNumber;
        }

    }
}