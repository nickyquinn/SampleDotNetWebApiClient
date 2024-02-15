using WebApiConnectLib;
using WebApiConnectSampleApp;

var builder = Host.CreateApplicationBuilder(args);

var apiConfig = new ApiConfig
{
    EndpointBaseUrl = "https://www.example-sample.com/api/"
};

builder.Services.AddSingleton<IWebApiBuilder>(new WebApiBuilder(apiConfig));

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();