using System.Text.Json;
using System.Text.Json.Serialization;

namespace OpenCodeClient.Models;

/// <summary>An AI provider with its models.</summary>
public class Provider
{
    [JsonPropertyName("id")] public string Id { get; set; } = "";
    [JsonPropertyName("name")] public string Name { get; set; } = "";
    [JsonPropertyName("source")] public ProviderSource Source { get; set; }
    [JsonPropertyName("env")] public List<string> Env { get; set; } = [];
    [JsonPropertyName("options")] public JsonElement? Options { get; set; }
    [JsonPropertyName("models")] public Dictionary<string, Model>? Models { get; set; }
    [JsonPropertyName("key")] public string? Key { get; set; }
}

/// <summary>An AI model available through a provider.</summary>
public class Model
{
    [JsonPropertyName("id")] public string Id { get; set; } = "";
    [JsonPropertyName("providerID")] public string ProviderId { get; set; } = "";
    [JsonPropertyName("api")] public ModelApi Api { get; set; } = new();
    [JsonPropertyName("name")] public string Name { get; set; } = "";
    [JsonPropertyName("capabilities")] public JsonElement? Capabilities { get; set; }
    [JsonPropertyName("cost")] public JsonElement? Cost { get; set; }
    [JsonPropertyName("limit")] public JsonElement? Limit { get; set; }
    [JsonPropertyName("status")] public ModelStatusValue Status { get; set; }
    [JsonPropertyName("options")] public JsonElement? Options { get; set; }
    [JsonPropertyName("headers")] public Dictionary<string, string>? Headers { get; set; }
    [JsonPropertyName("release_date")] public string ReleaseDate { get; set; } = "";
    [JsonPropertyName("variants")] public Dictionary<string, JsonElement>? Variants { get; set; }
}

/// <summary>API endpoint information for a model.</summary>
public class ModelApi
{
    [JsonPropertyName("id")] public string? Id { get; set; }
    [JsonPropertyName("url")] public string? Url { get; set; }
    [JsonPropertyName("npm")] public string? Npm { get; set; }
}

/// <summary>Base class for authentication credential variants.</summary>
[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(OAuthAuth), "oauth")]
[JsonDerivedType(typeof(ApiAuth), "api")]
[JsonDerivedType(typeof(WellKnownAuth), "wellknown")]
public abstract class Auth
{
    [JsonPropertyName("type")] public abstract string Type { get; }
}

/// <summary>OAuth authentication credentials.</summary>
public class OAuthAuth : Auth
{
    public override string Type => "oauth";
    [JsonPropertyName("refresh")] public string Refresh { get; set; } = "";
    [JsonPropertyName("access")] public string Access { get; set; } = "";
    [JsonPropertyName("expires")] public double Expires { get; set; }
    [JsonPropertyName("accountId")] public string? AccountId { get; set; }
    [JsonPropertyName("enterpriseUrl")] public string? EnterpriseUrl { get; set; }
}

/// <summary>API key authentication credentials.</summary>
public class ApiAuth : Auth
{
    public override string Type => "api";
    [JsonPropertyName("key")] public string Key { get; set; } = "";
}

/// <summary>Well-known authentication credentials.</summary>
public class WellKnownAuth : Auth
{
    public override string Type => "wellknown";
    [JsonPropertyName("key")] public string Key { get; set; } = "";
    [JsonPropertyName("token")] public string Token { get; set; } = "";
}

/// <summary>An available authentication method for a provider.</summary>
public class ProviderAuthMethod
{
    [JsonPropertyName("type")] public string Type { get; set; } = "";
    [JsonPropertyName("label")] public string Label { get; set; } = "";
}

/// <summary>OAuth authorization response with URL and instructions.</summary>
public class ProviderAuthAuthorization
{
    [JsonPropertyName("url")] public string Url { get; set; } = "";
    [JsonPropertyName("method")] public string Method { get; set; } = "";
    [JsonPropertyName("instructions")] public string Instructions { get; set; } = "";
}

/// <summary>Response containing providers list and defaults.</summary>
public class ConfigProvidersResponse
{
    [JsonPropertyName("providers")] public List<Provider> Providers { get; set; } = [];
    [JsonPropertyName("default")] public Dictionary<string, string> Default { get; set; } = new();
}
