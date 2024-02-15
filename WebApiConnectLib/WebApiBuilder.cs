using System.Text;
using Newtonsoft.Json;
using RestSharp;
using WebApiConnectLib.Models;

namespace WebApiConnectLib;

public class WebApiBuilder : IWebApiBuilder
{
    private readonly RestClient _client;
    private readonly ApiConfig _config;

    public WebApiBuilder(ApiConfig config)
    {
        _config = config;
        
        var options = new RestClientOptions(config.EndpointBaseUrl) {
            ThrowOnAnyError = false,
            MaxTimeout = 5000,
            Encoding = Encoding.UTF8
        };

        _client = new RestClient(options);
        //If the API requires an API key, this would be passed in
        //from the client application, or hard-coded in this package, 
        //or retrieved from a key vault from this package.
        //_client.AddDefaultHeader("x-api-key", config.ApiKey);
    }

    public async Task<string> GetHelloWorld()
    {
        var result = await ExecuteRequest<string>(RequestType.GET, "/hello-world");
        return result;
    }

    public async Task<string> GetHelloWorld(GetHelloWorldRequest requestBody)
    {
        var result = await ExecuteRequest<string>(
            RequestType.POST,
            "/hello-world",
            requestBody);

        return result;
    }
    
    private async Task<T> ExecuteRequest<T>(RequestType requestType, string requestPath, IJsonRequestBody jsonBody = null)
    {
        var request = new RestRequest(requestPath);

        if (jsonBody != null)
        {
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(jsonBody);
        }
            
        RestResponse resp;

        switch (requestType)
        {
            case RequestType.POST:
                resp = await _client.ExecutePostAsync<T>(request);
                break;
            default:
                resp = await _client.ExecuteGetAsync<T>(request);
                break;
        }

        if(resp.IsSuccessful && resp.Content != null)
            return JsonConvert.DeserializeObject<T>(resp.Content);

        return default(T);
    }

    private async Task ExecuteRequest(RequestType requestType, string requestPath, IJsonRequestBody jsonBody = null)
    {
        await ExecuteRequest<object>(requestType, requestPath, jsonBody);
    }
}