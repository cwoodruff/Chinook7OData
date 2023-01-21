using Chinook.Domain.ApiModels;
using Chinook.Domain.Supervisor;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;

namespace Chinook.API.Controllers;

public class TrackController : ControllerBase
{
    private readonly IChinookSupervisor _chinookSupervisor;
    private readonly ILogger<TrackController> _logger;

    public TrackController(IChinookSupervisor chinookSupervisor, ILogger<TrackController> logger)
    {
        _chinookSupervisor = chinookSupervisor;
        _logger = logger;
    }

    [EnableQuery]
    public async Task<ActionResult<List<TrackApiModel>>> Get()
    {
        try
        {
            var tracks = await _chinookSupervisor.GetAllTrack();
            return Ok(tracks);
        }
        catch (Exception)
        {
            return NotFound();
        }
    }

    [EnableQuery]
    public async Task<ActionResult<TrackApiModel>> Get([FromRoute] int id)
    {
        try
        {
            var track = await _chinookSupervisor.GetTrackById(id);
            return Ok(track);
        }
        catch (Exception)
        {
            return NotFound();
        }
    }
    
    public async Task<ActionResult<TrackApiModel>> Post([FromBody] TrackApiModel input)
    {
        try
        {
            var track = await _chinookSupervisor.AddTrack(input);
            return Ok(track);
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Errors);
        }
    }
    
    public async Task<ActionResult<TrackApiModel>> Put([FromRoute] int id, [FromBody] TrackApiModel input)
    {
        try
        {
            var track = await _chinookSupervisor.UpdateTrack(input);
            return Ok(track);
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
            await _chinookSupervisor.DeleteTrack(id);
            return Ok();
        }
        catch (Exception)
        {
            return NotFound();
        }
    }
}