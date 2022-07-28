using System;
using System.Threading;
using System.Threading.Tasks;
using EGM.GQL.Abstractions.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EGM.GraphQL.API.Controllers;

[ApiController]
[Route("[controller]")]
public class PeopleController : ControllerBase
{
    private readonly ILogger<PeopleController> _logger;
    private readonly IPersonService _personService;
    
    public PeopleController(IPersonService personService, ILogger<PeopleController> logger)
    {
        _personService = personService ?? throw new ArgumentNullException(nameof(personService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var result = await _personService.GetAllPeopleAsync(cancellationToken: cancellationToken);
        return result.Match(Ok, error => StatusCode(500, error));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _personService.GetPersonByIdAsync(id, cancellationToken);
        return result.Match<IActionResult>(Ok, NotFound);
    }
}