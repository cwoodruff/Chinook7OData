using Chinook.Domain.ApiModels;
using Chinook.Domain.Supervisor;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Chinook.API.Controllers;

public class PlaylistsController : ODataController
{
    private readonly IChinookSupervisor _chinookSupervisor;

    public PlaylistsController(IChinookSupervisor chinookSupervisor) => _chinookSupervisor = chinookSupervisor;

    [EnableQuery]
    [HttpGet("odata/Playlists")]
    public async Task<ActionResult<List<PlaylistApiModel>>> Get()
    {
        try
        {
            var playlists = await _chinookSupervisor.GetAllPlaylist();
            return Ok(playlists);
        }
        catch (Exception)
        {
            return NotFound();
        }
    }

    [EnableQuery]
    [HttpGet("odata/Playlists({id})")]
    public async Task<ActionResult<PlaylistApiModel>> Get([FromRoute] int id)
    {
        try
        {
            var playlist = await _chinookSupervisor.GetPlaylistById(id);
            return Ok(playlist);
        }
        catch (Exception)
        {
            return NotFound();
        }
    }
    
    [HttpPost("odata/Playlists")]
    public async Task<ActionResult<PlaylistApiModel>> Post([FromBody] PlaylistApiModel input)
    {
        try
        {
            var playlist = await _chinookSupervisor.AddPlaylist(input);
            return Ok(playlist);
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Errors);
        }
    }
    
    [HttpPut("odata/Playlists({id})")]
    public async Task<ActionResult<PlaylistApiModel>> Put([FromRoute] int id, [FromBody] PlaylistApiModel input)
    {
        try
        {
            var playlist = await _chinookSupervisor.UpdatePlaylist(input);
            return Ok(playlist);
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Errors);
        }
    }
    
    [HttpPatch("odata/Playlists({id})")]
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

    [HttpDelete("odata/Playlists({id})")]
    public async Task<ActionResult> Delete([FromRoute] int id)
    {
        try
        {
            await _chinookSupervisor.DeletePlaylist(id);
            return Ok();
        }
        catch (Exception)
        {
            return NotFound();
        }
    }
}