using Chinook.Domain.Entities;
using Chinook.Domain.Supervisor;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Chinook.API.Controllers;

public class InvoiceLinesController : ODataController
{
    private readonly IChinookSupervisor _chinookSupervisor;

    public InvoiceLinesController(IChinookSupervisor chinookSupervisor) => _chinookSupervisor = chinookSupervisor;

    [EnableQuery]
    [HttpGet("odata/InvoiceLines")]
    public async Task<ActionResult<List<InvoiceLine>>> Get()
    {
        try
        {
            var invoiceLines = await _chinookSupervisor.GetAllInvoiceLine();
            return Ok(invoiceLines);
        }
        catch (Exception ex)
        {
            return NotFound(ex);
        }
    }

    [EnableQuery]
    [HttpGet("odata/InvoiceLines({id})")]
    public async Task<ActionResult<InvoiceLine>> Get([FromRoute] int id)
    {
        try
        {
            var invoiceLine = await _chinookSupervisor.GetInvoiceLineById(id);
            return Ok(invoiceLine);
        }
        catch (Exception ex)
        {
            return NotFound(ex);
        }
    }
    
    [HttpPost("odata/InvoiceLines")]
    public async Task<ActionResult<InvoiceLine>> Post([FromBody] InvoiceLine input)
    {
        try
        {
            var invoiceLine = await _chinookSupervisor.AddInvoiceLine(input);
            return Ok(invoiceLine);
        }
        catch (Exception ex)
        {
            return NotFound(ex);
        }
    }
    
    [HttpPut("odata/InvoiceLines({id})")]
    public async Task<ActionResult<InvoiceLine>> Put([FromRoute] int id, [FromBody] InvoiceLine input)
    {
        try
        {
            var invoiceLine = await _chinookSupervisor.UpdateInvoiceLine(input);
            return Ok(invoiceLine);
        }
        catch (Exception ex)
        {
            return NotFound(ex);
        }
    }

    [HttpDelete("odata/InvoiceLines({id})")]
    public async Task<ActionResult> Delete([FromRoute] int id)
    {
        try
        {
            await _chinookSupervisor.DeleteInvoiceLine(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return NotFound(ex);
        }
    }
}