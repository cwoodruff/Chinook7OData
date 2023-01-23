using Chinook.Domain.ApiModels;
using Chinook.Domain.Supervisor;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;

namespace Chinook.API.Controllers;

public class MediaTypesController : ControllerBase
{
    private readonly IChinookSupervisor _chinookSupervisor;

    public MediaTypesController(IChinookSupervisor chinookSupervisor) => _chinookSupervisor = chinookSupervisor;

    [EnableQuery]
    public async Task<ActionResult<List<MediaTypeApiModel>>> Get()
    {
        try
        {
            var mediaTypes = await _chinookSupervisor.GetAllMediaType();
            return Ok(mediaTypes);
        }
        catch (Exception)
        {
            return NotFound();
        }
    }

    [EnableQuery]
    public async Task<ActionResult<MediaTypeApiModel>> Get([FromRoute] int id)
    {
        try
        {
            var mediaType = await _chinookSupervisor.GetMediaTypeById(id);
            return Ok(mediaType);
        }
        catch (Exception)
        {
            return NotFound();
        }
    }
    
    public async Task<ActionResult<MediaTypeApiModel>> Post([FromBody] MediaTypeApiModel input)
    {
        try
        {
            var mediaType = await _chinookSupervisor.AddMediaType(input);
            return Ok(mediaType);
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Errors);
        }
    }
    
    public async Task<ActionResult<MediaTypeApiModel>> Put([FromRoute] int id, [FromBody] MediaTypeApiModel input)
    {
        try
        {
            var mediaType = await _chinookSupervisor.UpdateMediaType(input);
            return Ok(mediaType);
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
            await _chinookSupervisor.DeleteMediaType(id);
            return Ok();
        }
        catch (Exception)
        {
            return NotFound();
        }
    }
}