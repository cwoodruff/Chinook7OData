﻿using Chinook.Domain.ApiModels;
using Chinook.Domain.Supervisor;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Chinook.API.Controllers;

public class CustomersController : ODataController
{
    private readonly IChinookSupervisor _chinookSupervisor;

    public CustomersController(IChinookSupervisor chinookSupervisor) => _chinookSupervisor = chinookSupervisor;
    
    [EnableQuery]
    [HttpGet("odata/Customers")]
    public async Task<ActionResult<List<CustomerApiModel>>> Get()
    {
        try
        {
            var customers = await _chinookSupervisor.GetAllCustomer();
            return Ok(customers);
        }
        catch (Exception ex)
        {
            return NotFound(ex);
        }
    }

    [EnableQuery]
    [HttpGet("odata/Customers({id})")]
    public async Task<ActionResult<CustomerApiModel>> Get([FromRoute] int id)
    {
        try
        {
            var customer = await _chinookSupervisor.GetCustomerById(id);
            return Ok(customer);
        }
        catch (Exception ex)
        {
            return NotFound(ex);
        }
    }
    
    [HttpPost("odata/Customers")]
    public async Task<ActionResult<CustomerApiModel>> Post([FromBody] CustomerApiModel input)
    {
        try
        {
            var customer = await _chinookSupervisor.AddCustomer(input);
            return Ok(customer);
        }
        catch (Exception ex)
        {
            return NotFound(ex);
        }
    }
    
    [HttpPut("odata/Customers({id})")]
    public async Task<ActionResult<CustomerApiModel>> Put([FromRoute] int id, [FromBody] CustomerApiModel input)
    {
        try
        {
            var customer = await _chinookSupervisor.UpdateCustomer(input);
            return Ok(customer);
        }
        catch (Exception ex)
        {
            return NotFound(ex);
        }
    }

    [HttpDelete("odata/Customers({id})")]
    public async Task<ActionResult> Delete([FromRoute] int id)
    {
        try
        {
            await _chinookSupervisor.DeleteCustomer(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return NotFound(ex);
        }
    }
}