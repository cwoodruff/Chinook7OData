using Chinook.Domain.ApiModels;
using Chinook.Domain.Supervisor;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;

namespace Chinook.API.Controllers;

public class CustomerController : ControllerBase
{
    private readonly IChinookSupervisor _chinookSupervisor;
    private readonly ILogger<CustomerController> _logger;

    public CustomerController(IChinookSupervisor chinookSupervisor, ILogger<CustomerController> logger)
    {
        _chinookSupervisor = chinookSupervisor;
        _logger = logger;
    }
    
    [EnableQuery]
    public async Task<ActionResult<List<CustomerApiModel>>> Get()
    {
        try
        {
            var customers = await _chinookSupervisor.GetAllCustomer();
            return Ok(customers);
        }
        catch (Exception)
        {
            return NotFound();
        }
    }

    [EnableQuery]
    public async Task<ActionResult<CustomerApiModel>> Get([FromRoute] int id)
    {
        try
        {
            var customer = await _chinookSupervisor.GetCustomerById(id);
            return Ok(customer);
        }
        catch (Exception)
        {
            return NotFound();
        }
    }
    
    public async Task<ActionResult<CustomerApiModel>> Post([FromBody] CustomerApiModel input)
    {
        try
        {
            var customer = await _chinookSupervisor.AddCustomer(input);
            return Ok(customer);
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Errors);
        }
    }
    
    public async Task<ActionResult<CustomerApiModel>> Put([FromRoute] int id, [FromBody] CustomerApiModel input)
    {
        try
        {
            var customer = await _chinookSupervisor.UpdateCustomer(input);
            return Ok(customer);
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
            await _chinookSupervisor.DeleteCustomer(id);
            return Ok();
        }
        catch (Exception)
        {
            return NotFound();
        }
    }
}