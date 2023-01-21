using Chinook.Domain.ApiModels;
using Chinook.Domain.Supervisor;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;

namespace Chinook.API.Controllers;

public class EmployeeController : ControllerBase
{
    private readonly IChinookSupervisor _chinookSupervisor;
    private readonly ILogger<EmployeeController> _logger;

    public EmployeeController(IChinookSupervisor chinookSupervisor, ILogger<EmployeeController> logger)
    {
        _chinookSupervisor = chinookSupervisor;
        _logger = logger;
    }

    [EnableQuery]
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