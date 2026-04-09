using System.Text.Json;
using System.Text.Json.Serialization;

namespace OpenCodeClient.Models;

/// <summary>
/// OpenCode configuration settings.
/// </summary>
public class Config
{
    [JsonPropertyName("$schema")] public string? Schema { get; set; }
    [JsonPropertyName("logLevel")] public LogLevel? LogLevel { get; set; }
    [JsonPropertyName("server")] public ServerConfig? Server { get; set; }
    [JsonPropertyName("command")] public Dictionary<string, CommandConfig>? Commands { get; set; }
    [JsonPropertyName("skills")] public SkillsConfig? Skills { get; set; }
    [JsonPropertyName("watcher")] public WatcherConfig? Watcher { get; set; }
    [JsonPropertyName("plugin")] public List<string>? Plugin { get; set; }
    [JsonPropertyName("snapshot")] public bool? Snapshot { get; set; }
    [JsonPropertyName("share")] public string? Share { get; set; }
    [JsonPropertyName("autoshare")] public JsonElement? Autoshare { get; set; }
    [JsonPropertyName("autoupdate")] public JsonElement? Autoupdate { get; set; }
    [JsonPropertyName("disabled_providers")] public List<string>? DisabledProviders { get; set; }
    [JsonPropertyName("enabled_providers")] public List<string>? EnabledProviders { get; set; }
    [JsonPropertyName("model")] public string? Model { get; set; }
    [JsonPropertyName("small_model")] public string? SmallModel { get; set; }
    [JsonPropertyName("default_agent")] public string? DefaultAgent { get; set; }
    [JsonPropertyName("username")] public string? Username { get; set; }
    [JsonPropertyName("agent")] public Dictionary<string, JsonElement>? AgentConfigs { get; set; }
    [JsonPropertyName("provider")] public Dictionary<string, JsonElement>? ProviderConfigs { get; set; }
    [JsonPropertyName("mcp")] public Dictionary<string, JsonElement>? McpConfigs { get; set; }
    [JsonPropertyName("formatter")] public JsonElement? Formatter { get; set; }
    [JsonPropertyName("lsp")] public JsonElement? Lsp { get; set; }
    [JsonPropertyName("instructions")] public List<string>? Instructions { get; set; }
    [JsonPropertyName("layout")] public LayoutConfig? Layout { get; set; }
    [JsonPropertyName("permission")] public JsonElement? Permission { get; set; }
    [JsonPropertyName("tools")] public Dictionary<string, bool>? Tools { get; set; }
    [JsonPropertyName("enterprise")] public EnterpriseConfig? Enterprise { get; set; }
    [JsonPropertyName("compaction")] public CompactionConfig? Compaction { get; set; }
    [JsonPropertyName("experimental")] public ExperimentalConfig? Experimental { get; set; }
}

/// <summary>
/// Server network configuration.
/// </summary>
public class ServerConfig
{
    [JsonPropertyName("port")] public int? Port { get; set; }
    [JsonPropertyName("hostname")] public string? Hostname { get; set; }
    [JsonPropertyName("mdns")] public JsonElement? Mdns { get; set; }
    [JsonPropertyName("mdnsDomain")] public string? MdnsDomain { get; set; }
    [JsonPropertyName("cors")] public List<string>? Cors { get; set; }
}

/// <summary>
/// Configuration for a custom command.
/// </summary>
public class CommandConfig
{
    [JsonPropertyName("template")] public string Template { get; set; } = "";
    [JsonPropertyName("description")] public string? Description { get; set; }
    [JsonPropertyName("agent")] public string? Agent { get; set; }
    [JsonPropertyName("model")] public string? Model { get; set; }
    [JsonPropertyName("subtask")] public string? Subtask { get; set; }
}

/// <summary>
/// Configuration for agent skills sources.
/// </summary>
public class SkillsConfig
{
    [JsonPropertyName("paths")] public List<string>? Paths { get; set; }
    [JsonPropertyName("urls")] public List<string>? Urls { get; set; }
}

/// <summary>
/// File watcher configuration.
/// </summary>
public class WatcherConfig
{
    [JsonPropertyName("ignore")] public List<string>? Ignore { get; set; }
}

/// <summary>
/// Enterprise deployment configuration.
/// </summary>
public class EnterpriseConfig
{
    [JsonPropertyName("url")] public string? Url { get; set; }
}

/// <summary>
/// Session compaction configuration.
/// </summary>
public class CompactionConfig
{
    [JsonPropertyName("auto")] public JsonElement? Auto { get; set; }
    [JsonPropertyName("prune")] public JsonElement? Prune { get; set; }
    [JsonPropertyName("reserved")] public JsonElement? Reserved { get; set; }
}

/// <summary>
/// Experimental feature flags.
/// </summary>
public class ExperimentalConfig
{
    [JsonPropertyName("disable_paste_summary")] public bool? DisablePasteSummary { get; set; }
    [JsonPropertyName("batch_tool")] public JsonElement? BatchTool { get; set; }
    [JsonPropertyName("openTelemetry")] public JsonElement? OpenTelemetry { get; set; }
    [JsonPropertyName("primary_tools")] public List<string>? PrimaryTools { get; set; }
    [JsonPropertyName("continue_loop_on_deny")] public bool? ContinueLoopOnDeny { get; set; }
    [JsonPropertyName("mcp_timeout")] public JsonElement? McpTimeout { get; set; }
}
