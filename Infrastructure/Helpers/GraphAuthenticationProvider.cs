using Microsoft.Graph;
using Microsoft.Identity.Web;
using System.Net.Http.Headers;

public class GraphAuthenticationProvider : IAuthenticationProvider
{
    private readonly ITokenAcquisition _tokenAcquisition;
    private readonly string[] _scopes;

    public GraphAuthenticationProvider(ITokenAcquisition tokenAcquisition, string[] scopes)
    {
        _tokenAcquisition = tokenAcquisition;
        _scopes = scopes;
    }

    public async Task AuthenticateRequestAsync(HttpRequestMessage request)
    {
        var token = await _tokenAcquisition.GetAccessTokenForUserAsync(_scopes);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }
}