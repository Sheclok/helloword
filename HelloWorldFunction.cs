using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace HelloWorldNet8;

public class HelloWorldFunction
{
    [Function("HelloWorld")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "hello")] HttpRequestData req)
    {
        var query = System.Web.HttpUtility.ParseQueryString(req.Url.Query);
        var name = query["name"];

        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(new
        {
            message = $"Hello, {string.IsNullOrWhiteSpace(name) ? "World" : name}!",
            framework = ".NET 8 Azure Function"
        });

        return response;
    }
}
