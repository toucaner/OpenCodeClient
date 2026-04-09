# OpenCodeClient

A .NET client library for interacting with the OpenCode API.

## Installation

Install the NuGet package:

```bash
dotnet add package OpenCodeClient
```

## Quick Start

### 1. Register services

```csharp
using OpenCodeClient.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenCodeClient(options =>
{
    options.BaseUrl = "http://localhost:4096";
});
```

### 2. Inject and use services

```csharp
using OpenCodeClient.Abstractions;

public class MyService(ISessionService sessions, IGlobalService global)
{
    public async Task RunAsync()
    {
        // Check server health
        var health = await global.GetHealthAsync();
        Console.WriteLine($"Server v{health.Version}, healthy: {health.Healthy}");

        // List sessions
        var allSessions = await sessions.ListAsync();
        foreach (var session in allSessions)
        {
            Console.WriteLine($"  [{session.Id}] {session.Title}");
        }

        // Create a new session
        var newSession = await sessions.CreateAsync(new() { Title = "My Session" });

        // Send a prompt
        var response = await sessions.PromptAsync(newSession.Id, new()
        {
            Parts = [new TextPartInput { Text = "Hello, world!" }]
        });

        Console.WriteLine($"Response cost: {response.Info.Cost}");
    }
}
```

### 3. Subscribe to events (SSE)

```csharp
using OpenCodeClient.Abstractions;

public class EventListener(IGlobalService global)
{
    public async Task ListenAsync(CancellationToken ct)
    {
        await foreach (var globalEvent in global.SubscribeToEventsAsync(ct))
        {
            Console.WriteLine($"[{globalEvent.Directory}] Event: {globalEvent.Payload.Type}");
        }
    }
}
```

## Available Services

| Interface | Description |
|---|---|
| `IGlobalService` | Server health, global config, SSE events, dispose |
| `IAuthService` | Authentication credential management |
| `IProjectService` | Project listing, creation, and updates |
| `IPtyService` | Pseudo-terminal session management |
| `IConfigService` | Project-level configuration |
| `ISessionService` | AI session management, messaging, prompts |
| `IPermissionService` | Permission request management |
| `IQuestionService` | Question request management |
| `IProviderService` | AI provider and OAuth management |
| `IFindService` | Text, file, and symbol search |
| `IFileService` | File listing, reading, git status |
| `IMcpService` | MCP server management |
| `ITuiService` | Terminal UI interactions |
| `IExperimentalService` | Tools, workspaces, worktrees |
| `IAppService` | Logging, agents, skills, commands |
| `IInfraService` | Paths, VCS, LSP, formatter, event subscription |

## Configuration

The `OpenCodeClientOptions` class provides the following settings:

| Property | Type | Default | Description |
|---|---|---|---|
| `BaseUrl` | `string` | `http://localhost:4096` | Base URL of the OpenCode server |
| `Username` | `string` | `opencode` | Username for HTTP Basic authentication |
| `Password` | `string?` | `null` | Password for HTTP Basic authentication. When set, every request includes an `Authorization: Basic` header |

### Authentication

If the server is protected with `OPENCODE_SERVER_PASSWORD`, configure credentials:

```csharp
builder.Services.AddOpenCodeClient(options =>
{
    options.BaseUrl = "http://localhost:4096";
    options.Password = "your-password";
    // options.Username = "opencode"; // default, override if OPENCODE_SERVER_USERNAME is set
});
```

### Advanced HTTP client configuration

The `AddOpenCodeClient` method returns an `IHttpClientBuilder`, allowing further customization:

```csharp
builder.Services.AddOpenCodeClient(o => o.BaseUrl = "https://my-server.example.com")
    .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
    {
        // Custom handler settings
    })
    .AddStandardResilienceHandler(); // Requires Microsoft.Extensions.Http.Resilience
```

## Project Structure

```
OpenCodeClient/
  Models/                  -- DTO models (records, classes, enums)
  Abstractions/            -- Service interfaces with XML docs
  Services/                -- HTTP service implementations
  Events/                  -- SSE client for streaming endpoints
  Extensions/              -- IServiceCollection extensions
  OpenCodeClientOptions.cs -- Configuration options
```

## Requirements

- .NET 9.0 or later
- `Microsoft.Extensions.DependencyInjection.Abstractions` 9.0.0
- `Microsoft.Extensions.Http` 9.0.0
- `Microsoft.Extensions.Options` 9.0.0

## License

Apache-2.0
