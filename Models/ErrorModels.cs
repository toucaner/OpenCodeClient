using System.Text.Json;
using System.Text.Json.Serialization;

namespace OpenCodeClient.Models;

/// <summary>Error returned for invalid requests (HTTP 400).</summary>
public class BadRequestError
{
    [JsonPropertyName("data")] public JsonElement? Data { get; set; }
    [JsonPropertyName("errors")] public List<Dictionary<string, JsonElement>>? Errors { get; set; }
    [JsonPropertyName("success")] public bool Success { get; set; }
}

/// <summary>Error returned when a resource is not found (HTTP 404).</summary>
public class NotFoundError
{
    [JsonPropertyName("name")] public string Name { get; set; } = "NotFoundError";
    [JsonPropertyName("data")] public NotFoundErrorData Data { get; set; } = new();
}

/// <summary>Data payload for a not-found error.</summary>
public class NotFoundErrorData
{
    [JsonPropertyName("message")] public string Message { get; set; } = "";
}

/// <summary>Base class for message processing error variants.</summary>
[JsonPolymorphic(TypeDiscriminatorPropertyName = "name")]
[JsonDerivedType(typeof(ProviderAuthErrorInfo), "ProviderAuthError")]
[JsonDerivedType(typeof(UnknownErrorInfo), "UnknownError")]
[JsonDerivedType(typeof(MessageOutputLengthErrorInfo), "MessageOutputLengthError")]
[JsonDerivedType(typeof(MessageAbortedErrorInfo), "MessageAbortedError")]
[JsonDerivedType(typeof(StructuredOutputErrorInfo), "StructuredOutputError")]
[JsonDerivedType(typeof(ContextOverflowErrorInfo), "ContextOverflowError")]
[JsonDerivedType(typeof(ApiErrorInfo), "APIError")]
public abstract class MessageError
{
    [JsonPropertyName("name")] public abstract string Name { get; }
}

/// <summary>Provider authentication error.</summary>
public class ProviderAuthErrorInfo : MessageError
{
    public override string Name => "ProviderAuthError";
    [JsonPropertyName("data")] public ProviderAuthErrorData Data { get; set; } = new();
}

/// <summary>Data for a provider auth error.</summary>
public class ProviderAuthErrorData
{
    [JsonPropertyName("providerID")] public string ProviderId { get; set; } = "";
    [JsonPropertyName("message")] public string Message { get; set; } = "";
}

/// <summary>An unknown or unexpected error.</summary>
public class UnknownErrorInfo : MessageError
{
    public override string Name => "UnknownError";
    [JsonPropertyName("data")] public UnknownErrorData Data { get; set; } = new();
}

/// <summary>Data for an unknown error.</summary>
public class UnknownErrorData
{
    [JsonPropertyName("message")] public string Message { get; set; } = "";
}

/// <summary>Error when message output exceeds length limits.</summary>
public class MessageOutputLengthErrorInfo : MessageError
{
    public override string Name => "MessageOutputLengthError";
    [JsonPropertyName("data")] public JsonElement? Data { get; set; }
}

/// <summary>Error when a message was aborted.</summary>
public class MessageAbortedErrorInfo : MessageError
{
    public override string Name => "MessageAbortedError";
    [JsonPropertyName("data")] public MessageAbortedErrorData Data { get; set; } = new();
}

/// <summary>Data for an aborted message error.</summary>
public class MessageAbortedErrorData
{
    [JsonPropertyName("message")] public string Message { get; set; } = "";
}

/// <summary>Error in structured output parsing.</summary>
public class StructuredOutputErrorInfo : MessageError
{
    public override string Name => "StructuredOutputError";
    [JsonPropertyName("data")] public StructuredOutputErrorData Data { get; set; } = new();
}

/// <summary>Data for a structured output error.</summary>
public class StructuredOutputErrorData
{
    [JsonPropertyName("message")] public string Message { get; set; } = "";
    [JsonPropertyName("retries")] public double Retries { get; set; }
}

/// <summary>Error when context window overflows.</summary>
public class ContextOverflowErrorInfo : MessageError
{
    public override string Name => "ContextOverflowError";
    [JsonPropertyName("data")] public ContextOverflowErrorData Data { get; set; } = new();
}

/// <summary>Data for a context overflow error.</summary>
public class ContextOverflowErrorData
{
    [JsonPropertyName("message")] public string Message { get; set; } = "";
    [JsonPropertyName("responseBody")] public string? ResponseBody { get; set; }
}

/// <summary>API-level error from the provider.</summary>
public class ApiErrorInfo : MessageError
{
    public override string Name => "APIError";
    [JsonPropertyName("data")] public ApiErrorData Data { get; set; } = new();
}

/// <summary>Detailed data for an API error.</summary>
public class ApiErrorData
{
    [JsonPropertyName("message")] public string Message { get; set; } = "";
    [JsonPropertyName("isRetryable")] public bool IsRetryable { get; set; }
    [JsonPropertyName("statusCode")] public double? StatusCode { get; set; }
    [JsonPropertyName("responseHeaders")] public Dictionary<string, string>? ResponseHeaders { get; set; }
    [JsonPropertyName("responseBody")] public string? ResponseBody { get; set; }
    [JsonPropertyName("metadata")] public Dictionary<string, string>? Metadata { get; set; }
}
