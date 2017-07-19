using AutoMapper;
using HauShop.Model.Models;
using HauShop.Service;
using HauShop.Web.Infrastructure.Core;
using HauShop.Web.Infrastructure.Extensions;
using HauShop.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace HauShop.Web.Api
{
    [RoutePrefix("api/footer")]
    [Authorize]
    public class FooterController : ApiControllerBase
    {
        private IFooterService _footerService;

        public FooterController(IErrorService errorService, IFooterService footerService) : base(errorService)
        {
            this._footerService = footerService;
        }

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _footerService.GetAll();

                var responseData = Mapper.Map<List<FooterViewModel>>(model.ToList());

                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

        [Route("getbyid/{id}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, string id)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _footerService.GetById(id);
                var responseData = Mapper.Map<Footer, FooterViewModel>(model);
                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

        [Route("create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, FooterViewModel FooterVM)
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
                    if (_footerService.GetById(FooterVM.ID) != null)
                    {
                        response = request.CreateResponse(HttpStatusCode.Created, "doubleID");
                    }
                    else
                    {
                        Footer newFooter = new Footer();
                        newFooter.UpdateFooter(FooterVM);
                        var page = _footerService.Add(newFooter);
                        _footerService.Save();
                        response = request.CreateResponse(HttpStatusCode.Created, page);
                    }
                }
                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        public HttpResponseMessage Put(HttpRequestMessage request, FooterViewModel FooterVm)
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
                    var FooterDb = _footerService.GetById(FooterVm.ID);
                    FooterDb.UpdateFooter(FooterVm);
                    _footerService.Update(FooterDb);
                    _footerService.Save();

                    var responseData = Mapper.Map<Footer, FooterViewModel>(FooterDb);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }
                return response;
            });
        }

        [Route("delete")]
        [HttpDelete]
        public HttpResponseMessage Delete(HttpRequestMessage request, string id)
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
                    var getByid = _footerService.GetById(id);
                    _footerService.Delete(id);
                    _footerService.Save();
                    var responseData = Mapper.Map<Footer, FooterViewModel>(getByid);
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
                    var ids = new JavaScriptSerializer().Deserialize<List<string>>(listId);
                    foreach (var id in ids)
                    {
                        _footerService.Delete(id);
                    }
                    _footerService.Save();
                    response = request.CreateResponse(HttpStatusCode.OK, ids.Count);
                }
                return response;
            });
        }
    }
}