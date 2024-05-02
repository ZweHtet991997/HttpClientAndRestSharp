using Consume_Backend_Api_Project.ApiConsumeServices.RestSharpService;
using Microsoft.AspNetCore.Mvc;
using ShareEntities;

namespace Consume_Backend_Api_Project.Controllers
{
    public class RestSharpController : Controller
    {
        private EmployeeApiService_2 _service;

        public RestSharpController(EmployeeApiService_2 employeeService)
        {
            _service = employeeService;
        }

        [HttpGet]
        [Route("api/employeelist")]
        public async Task<IActionResult> Index()
        {
            var dataResult = await _service.GetEmployee();
            return Json(dataResult);
        }

        [HttpPost]
        [Route("api/create")]
        public async Task<IActionResult> Create([FromBody] EmployeeEntities model)
        {
            var isCreateEmployee = await _service.CreateEmployee(model);
            return isCreateEmployee ? Ok("Success") : BadRequest();
        }

        [HttpPost]
        [Route("api/getemployee")]
        public async Task<IActionResult> GetEmployeeById(int Id)
        {
            var employee = await _service.GetEmployeeById(Id);
            return Json(employee);
        }

        [HttpPut]
        [Route("api/update")]
        public async Task<IActionResult> Update([FromBody] EmployeeEntities model)
        {
            var isUpdateEmployee = await _service.UpdateEmployee(model);
            return isUpdateEmployee ? Ok("Success") : BadRequest();
        }

        [HttpDelete]
        [Route("api/delete")]
        public async Task<IActionResult> Delete(int Id)
        {
            var isDeleteEmployee = await _service.DeleteEmployee(Id);
            return isDeleteEmployee ? Ok("Success") : BadRequest();
        }

        [HttpGet]
        [Route("api/getemployeebytoken")]
        public async Task<IActionResult> GetEmployeeListByAuthentication()
        {
            string authorizationHeader = Request.Headers["Authorization"];
            if (authorizationHeader != null)
            {
                var dataResult = await _service.GetEmployeeListByAuthentication(authorizationHeader);
                if (dataResult.Count > 0)
                {
                    return Json(dataResult);
                }
                else if (dataResult == null)
                {
                    return Unauthorized();
                }
            }
            return BadRequest();
        }
    }
}
