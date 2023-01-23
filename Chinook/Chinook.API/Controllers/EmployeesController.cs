using Chinook.Domain.ApiModels;
using Chinook.Domain.Supervisor;
using FluentValidation;
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
        catch (Exception)
        {
            return NotFound();
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
        catch (Exception)
        {
            return NotFound();
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
        catch (ValidationException ex)
        {
            return BadRequest(ex.Errors);
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
        catch (ValidationException ex)
        {
            return BadRequest(ex.Errors);
        }
    }
    
    [HttpPatch("odata/Employees({id})")]
    public async Task<ActionResult> Patch([FromRoute] int id, [FromBody] Delta<CustomerApiModel> delta)
    {
        // var customer = db.Customers.SingleOrDefault(d => d.Id == key);
        //
        // if (customer == null)
        // {
        //     return NotFound();
        // }
        //
        // delta.Patch(customer);
        //
        // db.SaveChanges();
        //
        // return Updated(customer);
        
        return Ok();
    }
    
    [HttpDelete("odata/Employees({id})")]
    public async Task<ActionResult> Delete([FromRoute] int id)
    {
        try
        {
            await _chinookSupervisor.DeleteEmployee(id);
            return Ok();
        }
        catch (Exception)
        {
            return NotFound();
        }
    }
}