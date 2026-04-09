using OpenCodeClient.Abstractions;
using OpenCodeClient.Models;

namespace OpenCodeClient.Services;

internal sealed class QuestionService(HttpClient httpClient) : OpenCodeServiceBase(httpClient), IQuestionService
{
    public Task<List<QuestionRequest>> ListAsync(string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
    {
        var url = BuildQuery("/question", DirWs(directory, workspace));
        return GetAsync<List<QuestionRequest>>(url, cancellationToken);
    }

    public Task<bool> ReplyAsync(string requestId, QuestionReplyRequest request, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
    {
        var url = BuildQuery($"/question/{requestId}/reply", DirWs(directory, workspace));
        return PostAsync<bool>(url, request, cancellationToken);
    }

    public Task<bool> RejectAsync(string requestId, string? directory = null, string? workspace = null, CancellationToken cancellationToken = default)
    {
        var url = BuildQuery($"/question/{requestId}/reject", DirWs(directory, workspace));
        return PostAsync<bool>(url, cancellationToken);
    }
}
