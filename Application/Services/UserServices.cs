using AzureEntraIDDemo.Application.Interfaces;
using AzureEntraIDDemo.Domain.Entities;
using AzureEntraIDDemo.Domain.Interfaces;

namespace AzureEntraIDDemo.Application.Services;

public class UserService : IUserService
{
    private readonly IGraphRepository _graphRepository;

    public UserService(IGraphRepository graphRepository)
    {
        _graphRepository = graphRepository;
    }

    public async Task<User> GetUserAsync()
    {
        return await _graphRepository.GetUserAsync();
    }

    public async Task<IEnumerable<SignIn>> GetRecentSignInsAsync()
    {
        return await _graphRepository.GetRecentSignInsAsync();
    }

    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        return await _graphRepository.GetUsersAsync();
    }

    public async Task<IEnumerable<Group>> GetGroupsAsync()
    {
        return await _graphRepository.GetGroupsAsync();
    }
}