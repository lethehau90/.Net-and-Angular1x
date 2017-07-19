using AutoMapper;
using HauShop.Model.Models;
using HauShop.Service;
using HauShop.Web.Infrastructure.Core;
using HauShop.Web.Infrastructure.Extensions;
using HauShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HauShop.Web.Api
{
    [RoutePrefix("api/postcategory")]
    [Authorize]

    public class PostCategoryController : ApiControllerBase
    {
        private IPostCategoryService _postCategoryService;
        private IPostService _postService;

        public PostCategoryController(IErrorService errorService, IPostCategoryService postCategoryService, IPostService postService) : base(errorService)
        {
            this._postCategoryService = postCategoryService;
            this._postService = postService;
        }

        [Route("getall")]
        [HttpGet]
        [Authorize(Roles ="View")]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var listCategory = _postCategoryService.GetAll();

                var listCategoryVm = Mapper.Map<List<PostCategoryViewModel>>(listCategory);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listCategoryVm);
                return response;
            });
        }

        [Route("getbyid/{id:int}")]
        [HttpGet]
        [Authorize(Roles = "View")]
        public HttpResponseMessage GetById(HttpRequestMessage request, int Id)
        {
            return CreateHttpResponse(request, () =>
            {
                var listCategory = _postCategoryService.GetById(Id);

                var listCategoryVm = Mapper.Map<PostCategoryViewModel>(listCategory);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listCategoryVm);
                return response;
            });
        }

        [Route("create")]
        [HttpPost]
        public HttpResponseMessage Post(HttpRequestMessage request, PostCategoryViewModel postCategoryVM)
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
                    PostCategory newPostCategory = new PostCategory();
                    newPostCategory.UpdatePostCategory(postCategoryVM);
                    newPostCategory.CreatedDate = DateTime.Now;
                    newPostCategory.CreatedBy = User.Identity.Name;
                    var category = _postCategoryService.Add(newPostCategory);
                    _postCategoryService.Save();

                    response = request.CreateResponse(HttpStatusCode.Created, category);
                }
                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        public HttpResponseMessage Put(HttpRequestMessage request, PostCategoryViewModel postCategoryVm)
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
                    var postCategoryDb = _postCategoryService.GetById(postCategoryVm.ID);
                    postCategoryDb.UpdatePostCategory(postCategoryVm);
                    postCategoryDb.UpdatedDate = DateTime.Now;
                    postCategoryDb.UpdatedBy = User.Identity.Name;
                    _postCategoryService.Update(postCategoryDb);
                    _postCategoryService.Save();

                    response = request.CreateResponse(HttpStatusCode.OK, postCategoryDb);
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

                    var listCategory = _postCategoryService.GetAll();
                    foreach(var item in listCategory)
                    {
                        if(item.ParentID == id)
                        {
                            return response = request.CreateResponse(HttpStatusCode.OK, "children");
                        }
                    }

                    var listCategoryByID = _postService.GetListPostByCategoryId(id);

                    if (listCategoryByID != null)
                    {
                        return response = request.CreateResponse(HttpStatusCode.OK, "post");
                    }

                    var oldPostCategory = _postCategoryService.Delete(id);
                    _postCategoryService.Save();

                    var responeDate = Mapper.Map<PostCategory, PostCategoryViewModel>(oldPostCategory);

                    response = request.CreateResponse(HttpStatusCode.OK, responeDate);
                }
                return response;
            });
        }
    }
}