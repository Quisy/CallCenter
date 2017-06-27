using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using CallCenter.API.Workers.Interfaces.Workers;

namespace CallCenter.API.Web.Controllers
{
    public class TestController : ApiController
    {
        private readonly IProcessWorker _processWorker;

        public TestController(IProcessWorker processWorker)
        {
            _processWorker = processWorker;
        }

        [Route("test")]
        [HttpGet]
        public async Task<IHttpActionResult> Test()
        {
            await _processWorker.GetFacebookConversationsAndManage();

            return Ok();
        }
    }
}
