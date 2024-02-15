namespace WebApiConnectLib.Models;

public class GetHelloWorldRequest : IJsonRequestBody
{
    public string Name { get; set; }
}