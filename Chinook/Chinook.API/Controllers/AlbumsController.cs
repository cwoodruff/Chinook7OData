﻿using Chinook.Domain.ApiModels;
using Chinook.Domain.Supervisor;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Chinook.API.Controllers;

public class AlbumsController : ODataController
{
    private readonly IChinookSupervisor _chinookSupervisor;

    public AlbumsController(IChinookSupervisor chinookSupervisor) => _chinookSupervisor = chinookSupervisor;

    [EnableQuery]
    [HttpGet("odata/Albums")]
    public async Task<ActionResult<List<AlbumApiModel>>> Get()
    {
        try
        {
            var albums = await _chinookSupervisor.GetAllAlbum();
            return Ok(albums);
        }
        catch (Exception)
        {
            return NotFound();
        }
    }

    [EnableQuery]
    [HttpGet("odata/Albums({id})")]
    public async Task<ActionResult<AlbumApiModel>> Get([FromRoute] int id)
    {
        try
        {
            var album = await _chinookSupervisor.GetAlbumById(id);
            return Ok(album);
        }
        catch (Exception)
        {
            return NotFound();
        }
    }

    [HttpPost("odata/Albums")]
    public async Task<ActionResult> Post([FromBody] AlbumApiModel input)
    {
        try
        {
            var album = await _chinookSupervisor.AddAlbum(input);
            return Ok(album);
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Errors);
        }
    }
    
    [HttpPut("odata/Albums({id})")]
    public async Task<ActionResult> Put([FromRoute] int id, [FromBody] AlbumApiModel input)
    {
        try
        {
            var album = await _chinookSupervisor.UpdateAlbum(input);
            return Ok(album);
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Errors);
        }
    }
    
    [HttpPatch("odata/Albums({id})")]
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
    
    [HttpDelete("odata/Albums({id})")]
    public async Task<ActionResult> Delete([FromRoute] int id)
    {
        try
        {
            await _chinookSupervisor.DeleteAlbum(id);
            return Ok();
        }
        catch (Exception)
        {
            return NotFound();
        }
    }
}