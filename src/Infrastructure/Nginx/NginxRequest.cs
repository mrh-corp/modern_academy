using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Infrastructure.Nginx;

public record NginxTokenRequest(
    string Identity,
    string Secret);

public sealed record NginxTokenResponse(
    [JsonProperty("token")]
    string Token,
    [JsonProperty("expires")]
    DateTime Expires);

public sealed class NginxSaveDomain
{
    [JsonPropertyName("access_list_id")]
    public string AccessList { get; set; } = string.Empty;

    [JsonPropertyName("advanced_config")]
    public string AdvancedConfig { get; set; } = string.Empty;

    [JsonPropertyName("allow_websocket_upgrade")]
    public bool AllowWebsocketUpgrade { get; set; }

    [JsonPropertyName("block_exploits")]
    public bool BlockExploits { get; set; }

    [JsonPropertyName("ssl_forced")]
    public bool SslForced { get; set; }

    [JsonPropertyName("caching_enabled")]
    public bool CachingEnabled { get; set; }

    [JsonPropertyName("certificate_id")]
    public string CertificateId { get; set; } = string.Empty;

    [JsonPropertyName("domain_names")]
    public string[] DomainNames { get; set; } = Array.Empty<string>();

    [JsonPropertyName("forward_host")]
    public string ForwardHost { get; set; } = string.Empty;

    [JsonPropertyName("forward_port")]
    public int ForwardPort { get; set; }

    [JsonPropertyName("forward_scheme")]
    public string ForwardScheme { get; set; } = string.Empty;

    [JsonPropertyName("meta")]
    public NginxMeta Meta { get; set; }

    public NginxSaveDomain() { }

    public NginxSaveDomain(
        string accessList,
        string advancedConfig,
        bool allowWebsocketUpgrade,
        bool blockExploits,
        bool sslForced,
        bool cachingEnabled,
        string certificateId,
        string[] domainNames,
        string forwardHost,
        int forwardPort,
        string forwardScheme,
        NginxMeta meta)
    {
        AccessList = accessList;
        AdvancedConfig = advancedConfig;
        AllowWebsocketUpgrade = allowWebsocketUpgrade;
        BlockExploits = blockExploits;
        SslForced = sslForced;
        CachingEnabled = cachingEnabled;
        CertificateId = certificateId;
        DomainNames = domainNames;
        ForwardHost = forwardHost;
        ForwardPort = forwardPort;
        ForwardScheme = forwardScheme;
        Meta = meta;
    }
}

public sealed class NginxMeta
{
    [JsonPropertyName("letsencrypt_email")]
    public string LetsEncryptEmail { get; set; } = string.Empty;

    [JsonPropertyName("letsencrypt_agree")]
    public bool LetsEncryptAgree { get; set; }

    // ✅ Default constructor
    public NginxMeta() { }

    // ✅ Parameterized constructor
    public NginxMeta(string letsEncryptEmail, bool letsEncryptAgree)
    {
        LetsEncryptEmail = letsEncryptEmail;
        LetsEncryptAgree = letsEncryptAgree;
    }
}

