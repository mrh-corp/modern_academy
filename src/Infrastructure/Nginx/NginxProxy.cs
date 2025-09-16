using System.Net.Http.Json;
using System.Text.Json;
using Application.Abstractions.Cache;
using Application.Abstractions.Nginx;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Nginx;

public class NginxProxy(
    IConfiguration configuration,
    ICacheManager cacheManager,
    IHttpClientFactory httpFactory) : INginxProxy
{
    private const string _tokenCacheKey = "nginx-proxy-token";
    private IConfigurationSection NpmSection => configuration.GetSection("npm");
    
    public async Task EncryptDomainName(string domainName)
    {
        var requestBody = new NginxSaveDomain(
            "0",
            "",
            false,
            false,
            false,
            true,
            "new",
            [domainName],
            NpmSection.GetValue<string>("host")!,
            NpmSection.GetValue<int>("port")!,
            "http",
            new(NpmSection.GetValue<string>("letsencryptEmail")!, true)
        );

        string token = await GetToken();
        HttpClient client = httpFactory.CreateClient("nginx-client");
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        HttpResponseMessage response = await client.PostAsJsonAsync("nginx/proxy-hosts", requestBody);
        string serializer = JsonSerializer.Serialize(requestBody);
        Console.WriteLine(serializer);
        if (!response.IsSuccessStatusCode)
        {
            throw new NginxException("Error while encrypting domain name");
        }
    }

    public Task<bool> RemoveDomainName(string domainName)
    {
        throw new NotImplementedException();
    }

    private async Task<string> GetToken()
    {
        string? token = await cacheManager.GetValueAsync(_tokenCacheKey);
        if (token is null)
        {
            string identity = NpmSection.GetValue<string>("identity");
            string secret = NpmSection.GetValue<string>("secret");
            var requestBody = new NginxTokenRequest(identity!, secret!);
            HttpClient client = httpFactory.CreateClient("nginx-client");
            HttpResponseMessage response = await client.PostAsJsonAsync("tokens", requestBody);
            
            response.EnsureSuccessStatusCode();
            NginxTokenResponse tokenResponse = await response.Content.ReadFromJsonAsync<NginxTokenResponse>();
            await cacheManager.SetValueWithExpirationAsync(_tokenCacheKey, tokenResponse!.Token, tokenResponse.Expires);
            token = tokenResponse.Token;
        }

        return token;
    }
}
