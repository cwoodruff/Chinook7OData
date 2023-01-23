using Chinook.Domain.ApiModels;
using Chinook.Domain.Supervisor;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Chinook.API.Controllers;

public class GenresController : ODataController
{
    private readonly IChinookSupervisor _chinookSupervisor;

    public GenresController(IChinookSupervisor chinookSupervisor) => _chinookSupervisor = chinookSupervisor;

    [EnableQuery]
    [HttpGet("odata/Genres")]
    public async Task<ActionResult<List<GenreApiModel>>> Get()
    {
        try
        {
            var genres = await _chinookSupervisor.GetAllGenre();
            return Ok(genres);
        }
        catch (Exception)
        {
            return NotFound();
        }
    }

    [EnableQuery]
    [HttpGet("odata/Genres({id})")]
    public async Task<ActionResult<GenreApiModel>> Get([FromRoute] int id)
    {
        try
        {
            var album = await _chinookSupervisor.GetGenreById(id);
            return Ok(album);
        }
        catch (Exception)
        {
            return NotFound();
        }
    }
    
    [HttpPost("odata/Genres")]
    public async Task<ActionResult<GenreApiModel>> Post([FromBody] GenreApiModel input)
    {
        try
        {
            var album = await _chinookSupervisor.AddGenre(input);
            return Ok(album);
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Errors);
        }
    }
    
    [HttpPut("odata/Genres({id})")]
    public async Task<ActionResult<GenreApiModel>> Put([FromRoute] int id, [FromBody] GenreApiModel input)
    {
        try
        {
            var album = await _chinookSupervisor.UpdateGenre(input);
            return Ok(album);
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Errors);
        }
    }
    
    [HttpPatch("odata/Genres({id})")]
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
    
    [HttpDelete("odata/Genres({id})")]
    public async Task<ActionResult> Delete([FromRoute] int id)
    {
        try
        {
            await _chinookSupervisor.DeleteGenre(id);
            return Ok();
        }
        catch (Exception)
        {
            return NotFound();
        }
    }
}