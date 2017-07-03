using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using CallCenter.API.Services.Interfaces.Services.Membership;
using CallCenter.API.Web.Controllers.Base;

namespace CallCenter.API.Web.Controllers
{
    [Authorize]
    [RoutePrefix("employee")]
    public class EmployeeController : BaseController
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        [Route("id")]
        public IHttpActionResult GetEmployeId()
        {
            var employeesResult = _employeeService.GetAll();

            if (employeesResult.IsError)
                return NotFound();

            var employees = employeesResult.Value;

            var userEmployeeData = employees.SingleOrDefault(e => e.ApplicationUserId.Equals(base.GetUserId()));

            if (userEmployeeData == null)
                return NotFound();

            return Ok(userEmployeeData.Id);
        }
    }
}