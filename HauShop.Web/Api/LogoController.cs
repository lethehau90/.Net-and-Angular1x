using HauShop.Web.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HauShop.Service;
using AutoMapper;
using HauShop.Model.Models;
using HauShop.Web.Models;
using HauShop.Web.Infrastructure.Extensions;

namespace HauShop.Web.Api
{
    [RoutePrefix("api/logo")]
    [Authorize]
    public class LogoController : ApiControllerBase
    {
        ILogoService _logoService;
        public LogoController(IErrorService errorService, ILogoService logoService) : base(errorService)
        {
            _logoService = logoService;
        }


        [Route("getall")]
        [HttpGet]
        [Authorize(Roles = "View")]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var query = _logoService.GetAll();

                var responseData = Mapper.Map<IEnumerable<Logo>, IEnumerable<LogoViewModel>>(query.ToList());

                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

        [Route("getbyid")]
        [HttpGet]
        [Authorize(Roles = "View")]
        public HttpResponseMessage GetByID(HttpRequestMessage request, int Id)
        {
            Func<HttpResponseMessage> Func = () => {
                var query = _logoService.GetId(Id);
                var responseData = Mapper.Map<Logo, LogoViewModel>(query);
                var response = request.CreateResponse(responseData);
                return response;
            };
            return CreateHttpResponse(request, Func);
        }

        [Route("create")]
        [HttpPost]
        [Authorize(Roles = "Create")]
        public HttpResponseMessage Create(HttpRequestMessage request , LogoViewModel logoVM)
        {
            Func<HttpResponseMessage> Func = () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.OK, ModelState);
                }
                else
                {
                    var requestCode = _logoService.GetCode(logoVM.Code);
                    if(requestCode.Count() > 0)
                    {
                        response = request.CreateResponse(HttpStatusCode.Created, "doubleCode");
                    }
                    else
                    {
                        Logo newLogo = new Logo();
                        newLogo.UpdateLogo(logoVM);
                        var logo = _logoService.Create(newLogo);
                        _logoService.Save();
                        response = request.CreateResponse(HttpStatusCode.OK, logo);
                    }
                    
                }
                return response;
            };

            return CreateHttpResponse(request, Func);
        }

        [Route("update")]
        [HttpPut]
        [Authorize(Roles ="Update")]
        public HttpResponseMessage Update(HttpRequestMessage request , LogoViewModel logoVM)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.OK, ModelState);
                }
                else
                {
                    var requestData = _logoService.GetId(logoVM.ID);
                    requestData.UpdateLogo(logoVM);
                    _logoService.Update(requestData);
                    _logoService.Save();

                    var responseData = Mapper.Map<Logo, LogoViewModel>(requestData);

                   response = request.CreateResponse(HttpStatusCode.OK, responseData);

                }
                return response;
            });
        }

        [HttpDelete]
        [Route("delete")]
        [Authorize(Roles ="Delete")]
        public HttpResponseMessage Delete(HttpRequestMessage request, int Id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage respone = null;

                if (!ModelState.IsValid)
                {
                    respone = request.CreateResponse(HttpStatusCode.OK, ModelState);
                }
                else
                {
                    var logoOld = _logoService.Delete(Id);
                    _logoService.Save();
                    var responeData = Mapper.Map<Logo, LogoViewModel>(logoOld);
                    respone = request.CreateResponse(HttpStatusCode.OK, responeData);
                }
                return respone;
            });
        }

    }
}
