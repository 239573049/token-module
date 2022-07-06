using Microsoft.AspNetCore.Mvc;
using NetCore.Application.Contracts.Services;

namespace NetCoreTest.Controllers;

[ApiController]
[Route("api/demo")]
public class DemoController : ControllerBase
{
    private readonly IDemoService _demoService;

    public DemoController(IDemoService demoService)
    {
        _demoService = demoService;
    }

    [HttpGet]
    public async Task<string> GetAsync()
    {
        return await _demoService.GetAsync();
    }
    
}