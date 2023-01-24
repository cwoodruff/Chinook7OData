﻿using Chinook.Domain.ApiModels;
using Chinook.Domain.Supervisor;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Chinook.API.Controllers;

public class TracksController : ODataController
{
    private readonly IChinookSupervisor _chinookSupervisor;

    public TracksController(IChinookSupervisor chinookSupervisor) => _chinookSupervisor = chinookSupervisor;

    [EnableQuery]
    [HttpGet("odata/Tracks")]
    public async Task<ActionResult<List<TrackApiModel>>> Get()
    {
        try
        {
            var tracks = await _chinookSupervisor.GetAllTrack();
            return Ok(tracks);
        }
        catch (Exception ex)
        {
            return NotFound(ex);
        }
    }

    [EnableQuery]
    [HttpGet("odata/Tracks({id})")]
    public async Task<ActionResult<TrackApiModel>> Get([FromRoute] int id)
    {
        try
        {
            var track = await _chinookSupervisor.GetTrackById(id);
            return Ok(track);
        }
        catch (Exception ex)
        {
            return NotFound(ex);
        }
    }
    
    [HttpPost("odata/Tracks")]
    public async Task<ActionResult<TrackApiModel>> Post([FromBody] TrackApiModel input)
    {
        try
        {
            var track = await _chinookSupervisor.AddTrack(input);
            return Ok(track);
        }
        catch (Exception ex)
        {
            return NotFound(ex);
        }
    }
    
    [HttpPut("odata/Tracks({id})")]
    public async Task<ActionResult<TrackApiModel>> Put([FromRoute] int id, [FromBody] TrackApiModel input)
    {
        try
        {
            var track = await _chinookSupervisor.UpdateTrack(input);
            return Ok(track);
        }
        catch (Exception ex)
        {
            return NotFound(ex);
        }
    }

    [HttpDelete("odata/Tracks({id})")]
    public async Task<ActionResult> Delete([FromRoute] int id)
    {
        try
        {
            await _chinookSupervisor.DeleteTrack(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return NotFound(ex);
        }
    }
}