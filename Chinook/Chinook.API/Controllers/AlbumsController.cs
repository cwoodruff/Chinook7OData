using Chinook.Domain.Entities;
using Chinook.Domain.Supervisor;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Chinook.API.Controllers;

public class AlbumsController : ODataController
{
    private readonly IChinookSupervisor _chinookSupervisor;

    public AlbumsController(IChinookSupervisor chinookSupervisor) => _chinookSupervisor = chinookSupervisor;

    [EnableQuery]
    [HttpGet("odata/Albums")]
    public async Task<ActionResult<List<Album>>> Get()
    {
        try
        {
            var albums = await _chinookSupervisor.GetAllAlbum();
            return Ok(albums);
        }
        catch (Exception ex)
        {
            return NotFound(ex);
        }
    }

    [EnableQuery]
    [HttpGet("odata/Albums({id})")]
    public async Task<ActionResult<Album>> Get([FromRoute] int id)
    {
        try
        {
            var album = await _chinookSupervisor.GetAlbumById(id);
            return Ok(album);
        }
        catch (Exception ex)
        {
            return NotFound(ex);
        }
    }

    [HttpPost("odata/Albums")]
    public async Task<ActionResult> Post([FromBody] Album input)
    {
        try
        {
            var album = await _chinookSupervisor.AddAlbum(input);
            return Ok(album);
        }
        catch (Exception ex)
        {
            return NotFound(ex);
        }
    }
    
    [HttpPut("odata/Albums({id})")]
    public async Task<ActionResult> Put([FromRoute] int id, [FromBody] Album input)
    {
        try
        {
            var album = await _chinookSupervisor.UpdateAlbum(input);
            return Ok(album);
        }
        catch (Exception ex)
        {
            return NotFound(ex);
        }
    }

    [HttpDelete("odata/Albums({id})")]
    public async Task<ActionResult> Delete([FromRoute] int id)
    {
        try
        {
            await _chinookSupervisor.DeleteAlbum(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return NotFound(ex);
        }
    }
}