using WebApiConnectLib.Models;

namespace WebApiConnectLib;

public interface IWebApiBuilder
{
    Task<string> GetHelloWorld();

    Task<string> GetHelloWorld(GetHelloWorldRequest requestBody);
}