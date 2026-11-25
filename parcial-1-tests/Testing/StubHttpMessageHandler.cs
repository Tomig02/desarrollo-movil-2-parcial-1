using System.Net;
using System.Text;

namespace parcial_1_tests.Testing;

internal sealed class StubHttpMessageHandler : HttpMessageHandler
{
    private readonly Func<HttpRequestMessage, HttpResponseMessage> _respond;

    public StubHttpMessageHandler(string json)
        : this(_ => new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(json, Encoding.UTF8, "application/json")
        })
    {
    }

    public StubHttpMessageHandler(HttpStatusCode statusCode)
        : this(_ => new HttpResponseMessage(statusCode))
    {
    }

    public StubHttpMessageHandler(Func<HttpRequestMessage, HttpResponseMessage> respond)
    {
        _respond = respond;
    }

    public IList<Uri> Requests { get; } = new List<Uri>();

    public HttpClient CreateClient() => new(this)
    {
        BaseAddress = new Uri("https://dogapi.dog/")
    };

    protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        Requests.Add(request.RequestUri!);
        return Task.FromResult(_respond(request));
    }
}
