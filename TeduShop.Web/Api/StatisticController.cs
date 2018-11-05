using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TeduShop.Service;
using TeduShop.Web.Infrastructure.Core;

namespace TeduShop.Web.Api
{
    //[Authorize]
    [RoutePrefix("api/statistic")]
    public class StatisticController : ApiControllerBase
    {
        private IStatisticService _statisticService;

        public StatisticController(IErrorService errorService, IStatisticService statisticService) : base(errorService)
        {
            _statisticService = statisticService;
        }

        [Route("getrevenue")]
        [HttpGet]
        public HttpResponseMessage GetRevenueStatistic(HttpRequestMessage requestMessage, string fromDate, string toDate)
        {
            return CreateHttpResponse(requestMessage, () =>
            {
                var model = _statisticService.GetRevenueStatistics(fromDate, toDate);
                HttpResponseMessage responseMessage = requestMessage.CreateResponse(HttpStatusCode.OK, model);
                return responseMessage;
            });
        }
    }
}
