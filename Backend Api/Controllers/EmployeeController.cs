using Backend_Api.ApiServices;
using Microsoft.AspNetCore.Mvc;
using ShareEntities;

namespace Backend_Api.Controllers
{
    public class EmployeeController : Controller
    {
        private EmployeeServices _employeeService;

        public EmployeeController(EmployeeServices employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        [Route("api/employee")]
        public IActionResult EmployeeList()
        {
            var dataResult = _employeeService.EmployeeList();
            return dataResult.Count > 0 ? Ok(dataResult) : NoContent();
        }

        [HttpGet]
        [Route("api/getemployee/{Id}")]
        public IActionResult GetEmployeeById(int Id)
        {
            var dataResult = _employeeService.GetEmployeeById(Id);
            return dataResult != null ? Ok(dataResult) : NoContent();
        }

        [HttpPost]
        [Route("api/employee")]
        public IActionResult Add([FromBody] EmployeeEntities model)
        {
            var dataResult = _employeeService.Add(model);
            return dataResult > 0 ? Ok("Success") : BadRequest();
        }

        [HttpPut]
        [Route("api/employee")]
        public IActionResult Update([FromBody] EmployeeEntities model)
        {
            var dataResult = _employeeService.Update(model);
            return dataResult > 0 ? Ok("Success") : BadRequest();
        }

        [HttpDelete]
        [Route("api/employee/{Id}")]
        public IActionResult Delete(int Id)
        {
            var dataResult = _employeeService.Delete(Id);
            return dataResult > 0 ? Ok("Success") : BadRequest();
        }
    }
}
