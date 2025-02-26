using AzureEntraIDDemo.Domain.Entities;
using AzureEntraIDDemo.Domain.Interfaces;
using AzureEntraIDDemo.Infrastructure.Helpers;
using Microsoft.Graph;

namespace AzureEntraIDDemo.Infrastructure.Repositories;

public class GraphRepository : IGraphRepository
{
    private readonly GraphServiceClient _graphServiceClient;

    public GraphRepository(GraphServiceClient graphServiceClient)
    {
        _graphServiceClient = graphServiceClient;
    }

    public async Task<User> GetUserAsync()
    {
        var user = await _graphServiceClient.Me.GetAsync();
        return new User
        {
            Id = user.Id,
            DisplayName = user.DisplayName,
            Email = user.Mail,
            TenantId = FormatGraphInformation.ExtractTenantIdFromUpn(user.UserPrincipalName)
        };
    }

    public async Task<IEnumerable<SignIn>> GetRecentSignInsAsync()
    {
        var signIns = await _graphServiceClient.AuditLogs.SignIns.GetAsync();
        var result = new List<SignIn>();

        foreach (var signIn in signIns.Value)
        {
            result.Add(new SignIn
            {
                Id = signIn.Id,
                CreatedDateTime = signIn.CreatedDateTime?.DateTime ?? DateTime.MinValue,
                UserDisplayName = signIn.UserDisplayName,
                UserPrincipalName = signIn.UserPrincipalName,
                IpAddress = signIn.IpAddress,
                Status = signIn.Status?.ErrorCode?.ToString() ?? "Success"
            });
        }

        return result;
    }

    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        var users = await _graphServiceClient.Users.GetAsync();
        var result = new List<User>();

        foreach (var user in users.Value)
        {
            result.Add(new User
            {
                Id = user.Id,
                DisplayName = user.DisplayName,
                Email = user.Mail,
                TenantId = FormatGraphInformation.ExtractTenantIdFromUpn(user.UserPrincipalName)
            });
        }

        return result;
    }

    public async Task<IEnumerable<Group>> GetGroupsAsync()
    {
        var groups = await _graphServiceClient.Groups.GetAsync();
        var result = new List<Group>();

        foreach (var group in groups.Value)
        {
            result.Add(new Group
            {
                Id = group.Id,
                DisplayName = group.DisplayName
            });
        }

        return result;
    }
}