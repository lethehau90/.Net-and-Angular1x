using AutoMapper;
using HauShop.Common;
using HauShop.Model.Models;
using HauShop.Service;
using HauShop.Web.Infrastructure.Core;
using HauShop.Web.Infrastructure.Extensions;
using HauShop.Web.Models;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace HauShop.Web.Api
{
    [RoutePrefix("api/silde")]
    [Authorize]
    public class SlideController : ApiControllerBase
    {

        ISlideService _slideService;
        private SlideController(IErrorService  errorService, ISlideService slideService) : base(errorService)
        {
            _slideService = slideService;
        }

        [Route("getall")]
        [Authorize(Roles = "View")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            Func<HttpResponseMessage> func = () =>
            {
                var slideModel = _slideService.GetAll();
                var slideViewModel = Mapper.Map<IEnumerable<SlideViewModel>>(slideModel);
                var response = request.CreateResponse(HttpStatusCode.OK, slideViewModel);
                return response;
            };
            return CreateHttpResponse(request, func);
        }

        [HttpGet]
        [Authorize(Roles ="View")]
        [Route("getbyid/{id:int}")]
        public HttpResponseMessage GetById(HttpRequestMessage request , int Id)
        {
            Func<HttpResponseMessage> func = () =>
            {
                var slideModel = _slideService.GetByID(Id);
                var slideViewModel = Mapper.Map<SlideViewModel>(slideModel);
                var response = request.CreateResponse(HttpStatusCode.OK, slideViewModel);
                return response;
            };
            return CreateHttpResponse(request, func);
        }

        [HttpPost]
        [Route("create")]
        [Authorize(Roles ="Create")]
        public HttpResponseMessage Create(HttpRequestMessage request, SlideViewModel slideVM)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    Slide newSlide = new Slide();
                    newSlide.UpdateSlide(slideVM);
                    var slideModel = _slideService.Create(newSlide);
                    _slideService.Save();
                    response = request.CreateResponse(HttpStatusCode.Created, slideModel);
                }
                return response;
            });
        }

        [HttpPost]
        [Route("update")]
        [Authorize(Roles = "Update")]
        public HttpResponseMessage Update(HttpRequestMessage request, SlideViewModel slideVM)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var slideModel = _slideService.GetByID(slideVM.ID);
                    slideModel.UpdateSlide(slideVM);
                    _slideService.Update(slideModel);
                    _slideService.Save();
                    var responsedata = Mapper.Map<Slide,SlideViewModel>(slideModel);
                    response = request.CreateResponse(HttpStatusCode.Created, responsedata);
                }
                return response;
            });
        }

        public HttpResponseMessage Delete(HttpRequestMessage request, int Id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var sideModel = _slideService.Delete(Id);
                    _slideService.Save();
                    var slideViewModel = Mapper.Map<SlideViewModel>(sideModel);
                    response = request.CreateResponse(HttpStatusCode.OK, slideViewModel);
                }
                return response;
            });
        }




    }
}
