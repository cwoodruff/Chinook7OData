using Chinook.Domain.ApiModels;
using Chinook.Domain.Supervisor;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Chinook.API.Controllers;

public class InvoiceLinesController : ODataController
{
    private readonly IChinookSupervisor _chinookSupervisor;

    public InvoiceLinesController(IChinookSupervisor chinookSupervisor) => _chinookSupervisor = chinookSupervisor;

    [EnableQuery]
    [HttpGet("odata/InvoiceLines")]
    public async Task<ActionResult<List<InvoiceLineApiModel>>> Get()
    {
        try
        {
            var invoiceLines = await _chinookSupervisor.GetAllInvoiceLine();
            return Ok(invoiceLines);
        }
        catch (Exception)
        {
            return NotFound();
        }
    }

    [EnableQuery]
    [HttpGet("odata/InvoiceLines({id})")]
    public async Task<ActionResult<InvoiceLineApiModel>> Get([FromRoute] int id)
    {
        try
        {
            var invoiceLine = await _chinookSupervisor.GetInvoiceLineById(id);
            return Ok(invoiceLine);
        }
        catch (Exception)
        {
            return NotFound();
        }
    }
    
    [HttpPost("odata/InvoiceLines")]
    public async Task<ActionResult<InvoiceLineApiModel>> Post([FromBody] InvoiceLineApiModel input)
    {
        try
        {
            var invoiceLine = await _chinookSupervisor.AddInvoiceLine(input);
            return Ok(invoiceLine);
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Errors);
        }
    }
    
    [HttpPut("odata/InvoiceLines({id})")]
    public async Task<ActionResult<InvoiceLineApiModel>> Put([FromRoute] int id, [FromBody] InvoiceLineApiModel input)
    {
        try
        {
            var invoiceLine = await _chinookSupervisor.UpdateInvoiceLine(input);
            return Ok(invoiceLine);
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Errors);
        }
    }
    
    [HttpPatch("odata/InvoiceLines({id})")]
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
    
    [HttpDelete("odata/InvoiceLines({id})")]
    public async Task<ActionResult> Delete([FromRoute] int id)
    {
        try
        {
            await _chinookSupervisor.DeleteInvoiceLine(id);
            return Ok();
        }
        catch (Exception)
        {
            return NotFound();
        }
    }
}