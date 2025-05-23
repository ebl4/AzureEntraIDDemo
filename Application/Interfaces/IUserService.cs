using AzureEntraIDDemo.Domain.Entities;

public interface IUserService
{
    Task<User> GetUserInfoAsync();
}