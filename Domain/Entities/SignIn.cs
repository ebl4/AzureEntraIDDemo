namespace AzureEntraIDDemo.Domain.Entities;

public class SignIn
{
    public string Id { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public string UserDisplayName { get; set; }
    public string UserPrincipalName { get; set; }
    public string IpAddress { get; set; }
    public string Status { get; set; }
}