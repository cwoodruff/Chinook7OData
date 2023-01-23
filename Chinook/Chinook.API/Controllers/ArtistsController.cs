using Chinook.Domain.ApiModels;
using Chinook.Domain.Supervisor;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Chinook.API.Controllers;

public class ArtistsController : ODataController
{
    private readonly IChinookSupervisor _chinookSupervisor;

    public ArtistsController(IChinookSupervisor chinookSupervisor) => _chinookSupervisor = chinookSupervisor;

    [EnableQuery]
    public async Task<ActionResult> Get()
    {
        try
        {
            var artists = await _chinookSupervisor.GetAllArtist();
            return Ok(artists);
        }
        catch (Exception)
        {
            return NotFound();
        }
    }
    
    [EnableQuery]
    public async Task<ActionResult> Get([FromRoute] int id)
    {
        try
        {
            var artist = await _chinookSupervisor.GetArtistById(id);
            return Ok(artist);
        }
        catch (Exception)
        {
            return NotFound();
        }
    }
    
    public async Task<ActionResult<ArtistApiModel>> Post([FromBody] ArtistApiModel input)
    {
        try
        {
            var artist = await _chinookSupervisor.AddArtist(input);
            return Ok(artist);
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Errors);
        }
    }
    
    public async Task<ActionResult<ArtistApiModel>> Put([FromRoute] int id, [FromBody]  ArtistApiModel input)
    {
        try
        {
            var artist = await _chinookSupervisor.UpdateArtist(input);
            return Ok(artist);
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Errors);
        }
    }
    
    public async Task<ActionResult> Patch([FromRoute] int id, [FromBody] Delta<AlbumApiModel> delta)
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
            await _chinookSupervisor.DeleteArtist(id);
            return Ok();
        }
        catch (Exception)
        {
            return NotFound();
        }
    }
}