namespace AzureEntraIDDemo.Infrastructure.Helpers;

public class FormatGraphInformation 
{
    public static string? ExtractTenantIdFromUpn(string upn)
    {
        if (string.IsNullOrEmpty(upn)) return null;

        var parts = upn.Split('@');
        if (parts.Length == 2)
        {
            return parts[1]; // Retorna o domínio (tenant.onmicrosoft.com)
        }

        return null;
    }
}