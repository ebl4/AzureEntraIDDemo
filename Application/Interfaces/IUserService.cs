using AzureEntraIDDemo.Domain.Entities;

namespace AzureEntraIDDemo.Application.Interfaces;

public interface IUserService
{
    Task<User> GetUserAsync();
    Task<IEnumerable<SignIn>> GetRecentSignInsAsync();
    Task<IEnumerable<User>> GetUsersAsync();
    Task<IEnumerable<Group>> GetGroupsAsync();
}