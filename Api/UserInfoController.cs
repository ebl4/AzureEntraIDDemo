using AuthDemo.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UserInfoController : ControllerBase
{
    private readonly IUserService _userService;

    public UserInfoController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var user = await _userService.GetUserInfoAsync();

        return Ok(new
        {
            User = user
        });
    }
}