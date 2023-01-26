using Chinook.Domain.Entities;
using Chinook.Domain.Supervisor;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Chinook.API.Controllers;

public class ArtistsController : ODataController
{
    private readonly IChinookSupervisor _chinookSupervisor;

    public ArtistsController(IChinookSupervisor chinookSupervisor) => _chinookSupervisor = chinookSupervisor;

    [EnableQuery]
    [HttpGet("odata/Artists")]
    public async Task<ActionResult> Get()
    {
        try
        {
            var artists = await _chinookSupervisor.GetAllArtist();
            return Ok(artists);
        }
        catch (Exception ex)
        {
            return NotFound(ex);
        }
    }
    
    [EnableQuery]
    [HttpGet("odata/Artists({id})")]
    public async Task<ActionResult> Get([FromRoute] int id)
    {
        try
        {
            var artist = await _chinookSupervisor.GetArtistById(id);
            return Ok(artist);
        }
        catch (Exception ex)
        {
            return NotFound(ex);
        }
    }
    
    [HttpPost("odata/Artists")]
    public async Task<ActionResult<Artist>> Post([FromBody] Artist input)
    {
        try
        {
            var artist = await _chinookSupervisor.AddArtist(input);
            return Ok(artist);
        }
        catch (Exception ex)
        {
            return NotFound(ex);
        }
    }
    
    [HttpPut("odata/Artists({id})")]
    public async Task<ActionResult<Artist>> Put([FromRoute] int id, [FromBody]  Artist input)
    {
        try
        {
            var artist = await _chinookSupervisor.UpdateArtist(input);
            return Ok(artist);
        }
        catch (Exception ex)
        {
            return NotFound(ex);
        }
    }

    [HttpDelete("odata/Artists({id})")]
    public async Task<ActionResult> Delete([FromRoute] int id)
    {
        try
        {
            await _chinookSupervisor.DeleteArtist(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return NotFound(ex);
        }
    }
}