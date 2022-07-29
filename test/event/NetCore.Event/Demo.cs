using Microsoft.AspNetCore.Mvc;

namespace NetCore.Event;

[ApiController]
[Route("/demo")]
public class Demo : ControllerBase
{
    /// <summary>
    /// 获取
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult Get()
    {
        return Ok();
    }
}