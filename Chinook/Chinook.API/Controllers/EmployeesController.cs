using Chinook.Domain.ApiModels;
using Chinook.Domain.Supervisor;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Chinook.API.Controllers;

public class EmployeesController : ODataController
{
    private readonly IChinookSupervisor _chinookSupervisor;

    public EmployeesController(IChinookSupervisor chinookSupervisor) => _chinookSupervisor = chinookSupervisor;

    [EnableQuery]
    [HttpGet("odata/Employees")]
    public async Task<ActionResult<List<EmployeeApiModel>>> Get()
    {
        try
        {
            var employees = await _chinookSupervisor.GetAllEmployee();
            return Ok(employees);
        }
        catch (Exception ex)
        {
            return NotFound(ex);
        }
    }

    [EnableQuery]
    [HttpGet("odata/Employees({id})")]
    public async Task<ActionResult<EmployeeApiModel>> Get([FromRoute] int id)
    {
        try
        {
            var employee = await _chinookSupervisor.GetEmployeeById(id);
            return Ok(employee);
        }
        catch (Exception ex)
        {
            return NotFound(ex);
        }
    }
    
    [HttpPost("odata/Employees")]
    public async Task<ActionResult<EmployeeApiModel>> Post([FromBody] EmployeeApiModel input)
    {
        try
        {
            var employee = await _chinookSupervisor.AddEmployee(input);
            return Ok(employee);
        }
        catch (Exception ex)
        {
            return NotFound(ex);
        }
    }
    
    [HttpPut("odata/Employees({id})")]
    public async Task<ActionResult<EmployeeApiModel>> Put([FromRoute] int id, [FromBody] EmployeeApiModel input)
    {
        try
        {
            var employee = await _chinookSupervisor.UpdateEmployee(input);
            return Ok(employee);
        }
        catch (Exception ex)
        {
            return NotFound(ex);
        }
    }

    [HttpDelete("odata/Employees({id})")]
    public async Task<ActionResult> Delete([FromRoute] int id)
    {
        try
        {
            await _chinookSupervisor.DeleteEmployee(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return NotFound(ex);
        }
    }
}