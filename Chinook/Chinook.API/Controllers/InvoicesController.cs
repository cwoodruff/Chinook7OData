using Chinook.Domain.ApiModels;
using Chinook.Domain.Supervisor;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Chinook.API.Controllers;

public class InvoicesController : ODataController
{
    private readonly IChinookSupervisor _chinookSupervisor;

    public InvoicesController(IChinookSupervisor chinookSupervisor) => _chinookSupervisor = chinookSupervisor;

    [EnableQuery(PageSize = 1)]
    [HttpGet("odata/Invoices")]
    public async Task<ActionResult<List<InvoiceApiModel>>> Get()
    {
        try
        {
            var invoices = await _chinookSupervisor.GetAllInvoice();
            return Ok(invoices);
        }
        catch (Exception)
        {
            return NotFound();
        }
    }

    [EnableQuery]
    [HttpGet("odata/Invoices({id})")]
    public async Task<ActionResult<InvoiceApiModel>> Get([FromRoute] int id)
    {
        try
        {
            var invoice = await _chinookSupervisor.GetInvoiceById(id);
            return Ok(invoice);
        }
        catch (Exception)
        {
            return NotFound();
        }
    }
    
    [HttpPost("odata/Invoices")]
    public async Task<ActionResult<InvoiceApiModel>> Post([FromBody] InvoiceApiModel input)
    {
        try
        {
            var invoice = await _chinookSupervisor.AddInvoice(input);
            return Ok(invoice);
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Errors);
        }
    }
    
    [HttpPut("odata/Invoices({id})")]
    public async Task<ActionResult<InvoiceApiModel>> Put([FromRoute] int id, [FromBody] InvoiceApiModel input)
    {
        try
        {
            var invoice = await _chinookSupervisor.UpdateInvoice(input);
            return Ok(invoice);
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Errors);
        }
    }
    
    [HttpPatch("odata/Invoices({id})")]
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
    
    [HttpDelete("odata/Invoices({id})")]
    public async Task<ActionResult> Delete([FromRoute] int id)
    {
        try
        {
            await _chinookSupervisor.DeleteInvoice(id);
            return Ok();
        }
        catch (Exception)
        {
            return NotFound();
        }
    }
}