using HauShop.Service;
using HauShop.Web.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HauShop.Web.Api
{
    //[Authorize]
    [RoutePrefix("api/statistic")]
    public class StatisticController : ApiControllerBase
    {
        private IStatisticService _statisticService;

        public StatisticController(IErrorService errorService, IStatisticService statisticService) : base(errorService)
        {
            this._statisticService = statisticService;
        }

        [HttpGet]
        [Route("getrevenue")]
        public HttpResponseMessage GetRevenueStatistic(HttpRequestMessage request, string fromDate, string toDate)
        {
            return CreateHttpResponse(request, () =>
            {
                var mode = _statisticService.GetRevenueStatistic(fromDate, toDate);
                var response = request.CreateResponse(HttpStatusCode.OK, mode);
                return response;
            });
        }
    }
}
