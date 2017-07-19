using AutoMapper;
using HauShop.Model.Models;
using HauShop.Service;
using HauShop.Web.Infrastructure.Core;
using HauShop.Web.Infrastructure.Extensions;
using HauShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace HauShop.Web.Api
{
    [RoutePrefix("api/page")]
    [Authorize]
    public class PageController : ApiControllerBase
    {
        private IPageService _pageService;

        public PageController(IErrorService errorService, IPageService pageService) : base(errorService)
        {
            this._pageService = pageService;
        }

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _pageService.GetAll();

                var responseData = Mapper.Map<List<PageViewModel>>(model.ToList());

                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }


        [Route("getbyid/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _pageService.GetByID(id);
                var responseData = Mapper.Map<Page, PageViewModel>(model);
                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

        [Route("create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, PageViewModel PageVM)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if (!ModelState.IsValid)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    if(_pageService.GetByAlias(PageVM.Alias) != null)
                    {
                        response = request.CreateResponse(HttpStatusCode.Created, "doubleAlias");
                    }
                    else
                    {
                        Page newPage = new Page();
                        newPage.UpdatePage(PageVM);
                        newPage.CreatedDate = DateTime.Now;
                        newPage.CreatedBy = User.Identity.Name;
                        var page = _pageService.Add(newPage);
                        _pageService.Save();
                        response = request.CreateResponse(HttpStatusCode.Created, page);
                    }                 
                }
                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        public HttpResponseMessage Put(HttpRequestMessage request, PageViewModel PageVm)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if (!ModelState.IsValid)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var PageDb = _pageService.GetByID(PageVm.ID);
                    PageDb.UpdatePage(PageVm);
                    PageDb.UpdatedDate = DateTime.Now;
                    PageDb.UpdatedBy = User.Identity.Name;
                    _pageService.Update(PageDb);
                    _pageService.Save();

                    var responseData = Mapper.Map<Page, PageViewModel>(PageDb);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }
                return response;
            });
        }

        [Route("delete")]
        [HttpDelete]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if (!ModelState.IsValid)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var oldPage = _pageService.Delete(id);
                    _pageService.Save();
                    var responseData = Mapper.Map<Page, PageViewModel>(oldPage);

                    response = request.CreateResponse(HttpStatusCode.OK, responseData);
                }
                return response;
            });
        }

        [Route("deletemulti")]
        [HttpDelete]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string listId)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if (!ModelState.IsValid)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var ids = new JavaScriptSerializer().Deserialize<List<int>>(listId);
                    foreach (var id in ids)
                    {
                        _pageService.Delete(id);
                    }
                    _pageService.Save();
                    response = request.CreateResponse(HttpStatusCode.OK, ids.Count);
                }
                return response;
            });
        }

    }
}
