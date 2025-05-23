using System.Security.Claims;
using AzureEntraIDDemo.Domain.Entities;

public class UserService : IUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Task<User> GetUserInfoAsync()
    {
        var user = _httpContextAccessor.HttpContext?.User;
        
        if (user == null)
        {
            throw new InvalidOperationException("User not authenticated");
        }

        var userInfo = new User
        {
            Name = user.FindFirstValue("name") ?? user.FindFirstValue(ClaimTypes.Name) ?? "Unknown",
            Email = user.FindFirstValue("preferred_username") ?? user.FindFirstValue(ClaimTypes.Email) ?? "Unknown",
            TenantId = user.FindFirstValue("tid") ?? "Unknown"
        };

        return Task.FromResult(userInfo);
    }
}