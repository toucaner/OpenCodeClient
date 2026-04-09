using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace OpenCodeClient.Events;

internal static class SseClient
{
    internal static async IAsyncEnumerable<T> StreamAsync<T>(
        HttpClient httpClient,
        string requestUri,
        JsonSerializerOptions jsonOptions,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("text/event-stream"));

        using var response = await httpClient.SendAsync(
            request,
            HttpCompletionOption.ResponseHeadersRead,
            cancellationToken).ConfigureAwait(false);

        response.EnsureSuccessStatusCode();

        await using var stream = await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);
        using var reader = new StreamReader(stream);

        var dataBuffer = new List<string>();

        while (!cancellationToken.IsCancellationRequested)
        {
            var line = await reader.ReadLineAsync(cancellationToken).ConfigureAwait(false);

            if (line is null)
                yield break;

            if (line.StartsWith("data:", StringComparison.Ordinal))
            {
                dataBuffer.Add(line[5..].TrimStart());
            }
            else if (line.Length == 0 && dataBuffer.Count > 0)
            {
                var json = string.Join("\n", dataBuffer);
                dataBuffer.Clear();

                if (!string.IsNullOrWhiteSpace(json))
                {
                    var item = JsonSerializer.Deserialize<T>(json, jsonOptions);
                    if (item is not null)
                        yield return item;
                }
            }
        }
    }
}
