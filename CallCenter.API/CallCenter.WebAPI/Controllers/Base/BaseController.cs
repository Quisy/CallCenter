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
        protected string GetUserId()
        {
            return User.Identity.GetUserId();
        }
    }
}