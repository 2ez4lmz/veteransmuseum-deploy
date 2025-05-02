using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VeteransMuseum.Application.News.AddNews;
using VeteransMuseum.Application.News.DeleteNews;
using VeteransMuseum.Application.News.GetNews;
using VeteransMuseum.Application.News.GetNewsById;
using VeteransMuseum.Application.News.UpdateNews;
using VeteransMuseum.Domain.Abstractions;

namespace VeteransMuseum.WebApi.Controllers.News;

[ApiController]
[Route("api/news")]
public class NewsController : ControllerBase
{
    private readonly ISender _sender;
 
    public NewsController(ISender sender)
    {
        _sender = sender;
    }
    
    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetNewsById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetNewsByIdQuery(id);
 
        Result<NewsResponse> result = await _sender.Send(query, cancellationToken);
 
        return result.IsSuccess ? Ok(result.Value) : NotFound();
    }
    
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetNews(CancellationToken cancellationToken)
    {
        var query = new GetNewsQuery();
 
        Result<IReadOnlyList<NewsResponse>> result = await _sender.Send(query, cancellationToken);
 
        return Ok(result.Value);
    }
    
    [HttpPost]
    //[Authorize(Roles = Roles.Admin)]
    public async Task<IActionResult> CreateNews(
        AddNewsRequest request,
        CancellationToken cancellationToken)
    {
        var command = new AddNewsCommand(
            request.Title,
            request.Content,
            request.ImageUrl);

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpPut("{id}")]
    //[Authorize(Roles = Roles.Admin)]
    public async Task<IActionResult> UpdateNews(
        Guid id,
        UpdateNewsRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateNewsCommand(
            id,
            request.Title,
            request.Content,
            request.ImageUrl);

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok();
    }

    [HttpDelete("{id}")]
    //[Authorize(Roles = Roles.Admin)]
    public async Task<IActionResult> DeleteNews(
        Guid id,
        CancellationToken cancellationToken)
    {
        var command = new DeleteNewsCommand(id);

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return NoContent();
    }
}