namespace AzureEntraIDDemo.Domain.Entities;

public class User
{
    public string Id { get; set; }
    public string DisplayName { get; set; }
    public string Email { get; set; }
    public string TenantId { get; set; }
}