using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;

namespace CallCenter.API.Web.Controllers.Base
{
    public class BaseController : ApiController
    {
        public string UserId => HttpContext.Current.User.Identity.GetUserId();

    }
}