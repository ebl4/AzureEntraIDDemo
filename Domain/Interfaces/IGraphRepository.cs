using AzureEntraIDDemo.Domain.Entities;

namespace AzureEntraIDDemo.Domain.Interfaces;

public interface IGraphRepository
{
    Task<User> GetUserAsync();
    Task<IEnumerable<SignIn>> GetRecentSignInsAsync();
    Task<IEnumerable<User>> GetUsersAsync();
    Task<IEnumerable<Group>> GetGroupsAsync();
}