using AutoMapper;
using HauShop.Common;
using HauShop.Model.Models;
using HauShop.Service;
using HauShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HauShop.Web.Controllers
{
    public class HomeController : Controller
    {
        IProductCategoryService _produtCategoryService;
        IPageService _pageService;
        ICommonService _commonService;
        IProductService _productService;
        ILogoService _logoService;

        public HomeController(IProductCategoryService productCategoryService,
                              IProductService productService,
                              ICommonService commonService,
                              IPageService pageService,
                              ILogoService logoService
                              )
        {
            this._produtCategoryService = productCategoryService;
            this._commonService = commonService;
            this._productService = productService;
            this._pageService = pageService;
            this._logoService = logoService;
        }
        [OutputCache(Duration = 60, Location = System.Web.UI.OutputCacheLocation.Client)]
        public ActionResult Index()
        {
            var homeViewModel = new HomeViewModel();

            var slideModel = _commonService.GetSlide();
            var lastestProductModel = _productService.GetLastest(3);
            var topSalseProductModel = _productService.GetHotProduct(3);

            var slideViewModel = Mapper.Map<IEnumerable<Slide>, IEnumerable<SlideViewModel>>(slideModel);
            var lastestProductViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(lastestProductModel);
            var topSalseProductViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(topSalseProductModel);

            homeViewModel.Slides = slideViewModel;
            homeViewModel.LastestProducts = lastestProductViewModel;
            homeViewModel.TopSaleProducts = topSalseProductViewModel;

            try
            {
                homeViewModel.title = _commonService.GetSystemConfig(CommonConstants.HomeTitle).ValueString;
                homeViewModel.MetaKeyWord = _commonService.GetSystemConfig(CommonConstants.HomeMetaKeyword).ValueString;
                homeViewModel.MetaDescription = _commonService.GetSystemConfig(CommonConstants.HomeMetaDescription).ValueString;
            }
            catch
            {

            }

            return View(homeViewModel);
        }

        [ChildActionOnly]
        [OutputCache(Duration = 3600)]
        public ActionResult Footer()
        {
            var footerModel = _commonService.GetFooter();
            var footerViewModel = Mapper.Map<Footer, FooterViewModel>(footerModel);
            return PartialView(footerViewModel);
        }

        [ChildActionOnly]
        //[OutputCache(Duration = 3600)]
        public ActionResult Header()
        {
            var HeaderViewModel = new HeaderViewModel();

            var pageModel = _pageService.GetAll().Where(x=>x.Status);
            var pageViewModel = Mapper.Map<IEnumerable<Page>, IEnumerable<PageViewModel>>(pageModel);
            HeaderViewModel.Pages = pageViewModel;

            var logoModel = _logoService.GetCode("default");
            var logoViewModel = Mapper.Map<IEnumerable<Logo>, IEnumerable<LogoViewModel>>(logoModel);
            HeaderViewModel.Logos = logoViewModel;

            return PartialView(HeaderViewModel);
        }

        [ChildActionOnly]
        [OutputCache(Duration = 3600)]
        public ActionResult Category()
        {
            var model = _produtCategoryService.GetAll();
            var listProductCategoryViewModel = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(model);
            return PartialView(listProductCategoryViewModel);
        }
    }
}