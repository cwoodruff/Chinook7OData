﻿using Chinook.Domain.ApiModels;
using Chinook.Domain.Supervisor;
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
        catch (Exception ex)
        {
            return NotFound(ex);
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
        catch (Exception ex)
        {
            return NotFound(ex);
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
        catch (Exception ex)
        {
            return NotFound(ex);
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
        catch (Exception ex)
        {
            return NotFound(ex);
        }
    }

    [HttpDelete("odata/Genres({id})")]
    public async Task<ActionResult> Delete([FromRoute] int id)
    {
        try
        {
            await _chinookSupervisor.DeleteGenre(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return NotFound(ex);
        }
    }
}