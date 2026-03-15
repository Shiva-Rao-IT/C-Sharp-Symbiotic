using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;

namespace AAzureFunctionApp1;

public class RegisterUserFunction
{
    private readonly ILogger _logger;

    public RegisterUserFunction(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<RegisterUserFunction>();
    }

    [Function("RegisterUser")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
    {
        _logger.LogInformation("RegisterUser function triggered.");

        string name = null;
        string email = null;

        var query = System.Web.HttpUtility.ParseQueryString(req.Url.Query);
        name = query["name"];
        email = query["email"];

        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email))
        {
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            if (!string.IsNullOrEmpty(requestBody))
            {
                var data = JsonSerializer.Deserialize<UserRequest>(requestBody);
                name = data?.Name;
                email = data?.Email;
            }
        }

        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email))
        {
            var badResponse = req.CreateResponse(HttpStatusCode.BadRequest);
            await badResponse.WriteAsJsonAsync(new
            {
                success = false,
                message = "Name and Email are required."
            });
            return badResponse;
        }


        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(new
        {
            success = true,
            message = $"User {name} registered successfully!",
            email = email,
            timestamp = DateTime.UtcNow
        });

        return response;
    }
}


public class UserRequest
{
    public string Name { get; set; }
    public string Email { get; set; }
}