using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VeteransMuseum.Application.Veterans.AddVeteran;
using VeteransMuseum.Application.Veterans.DeleteVeteran;
using VeteransMuseum.Application.Veterans.GetVeteran;
using VeteransMuseum.Application.Veterans.GetVeterans;
using VeteransMuseum.Application.Veterans.UpdateVeteran;
using VeteransMuseum.Domain.Abstractions;

namespace VeteransMuseum.WebApi.Controllers.Veterans;

[ApiController]
[Route("api/veterans")]
public class VeteransController : ControllerBase
{
    private readonly ISender _sender;
 
    public VeteransController(ISender sender)
    {
        _sender = sender;
    }
    
    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetVeteran(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetVeteranQuery(id);
 
        Result<VeteranResponse> result = await _sender.Send(query, cancellationToken);
 
        return result.IsSuccess ? Ok(result.Value) : NotFound();
    }
    
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetVeterans(CancellationToken cancellationToken)
    {
        var query = new GetVeteransQuery();
 
        Result<IReadOnlyList<VeteranResponse>> result = await _sender.Send(query, cancellationToken);
 
        return Ok(result.Value);
    }
    
    [HttpPost]
    //[Authorize(Roles = Roles.Admin)]
    public async Task<IActionResult> CreateVeteran(
        AddVeteranRequest request,
        CancellationToken cancellationToken)
    {
        var command = new AddVeteranCommand(
            request.FirstName,
            request.LastName,
            request.MiddleName,
            request.BirthDate,
            request.DeathDate,
            request.Biography,
            request.Rank,
            request.Awards,
            request.MilitaryUnit,
            request.Battles,
            request.ImageUrl
            );
 
        var result = await _sender.Send(command, cancellationToken);
 
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }
             
        return Ok(result.Value);
    }
    
    [HttpPut("{id}")]
    [Authorize(Roles = Roles.Admin)]
    public async Task<IActionResult> UpdateVeteran(
        Guid id,
        UpdateVeteranRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateVeteranCommand(
            id,
            request.FirstName,
            request.LastName,
            request.MiddleName,
            request.BirthDate,
            request.DeathDate,
            request.Biography,
            request.Rank,
            request.Awards,
            request.MilitaryUnit,
            request.Battles,
            request.ImageUrl);

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = Roles.Admin)]
    public async Task<IActionResult> DeleteVeteran(
        Guid id,
        CancellationToken cancellationToken)
    {
        var command = new DeleteVeteranCommand(id);

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return NoContent();
    }
}