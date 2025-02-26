using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AzureEntraIDDemo.Application.Interfaces;

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
        var user = await _userService.GetUserAsync();
        var recentSignIns = await _userService.GetRecentSignInsAsync();
        var users = await _userService.GetUsersAsync();
        var groups = await _userService.GetGroupsAsync();

        return Ok(new
        {
            User = user,
            RecentSignIns = recentSignIns,
            Users = users,
            Groups = groups
        });
    }
}