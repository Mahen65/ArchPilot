////var builder = DistributedApplication.CreateBuilder(args);
////builder.AddProject("BackEnd", @"..\src\Presentation\ArchPilot.API\ArchPilot.API.csproj", "https");
////builder.AddProject("FrontEnd", @"..\src\Presentation\ArchPilot.BlazorWasm\ArchPilot.BlazorWasm.csproj", "https");
//var apiService = builder.AddProject<Projects.ArchPilot_API>("BackEnd", "https");

//var blazorApp = builder.AddProject<Projects.ArchPilot_BlazorWasm>("blazor-app")
//    .WithEnvironment("ApiBaseUrl", apiService.GetEndpoint("http")); 
//builder.Build().Run();

var builder = DistributedApplication.CreateBuilder(args);
var apiService = builder.AddProject<Projects.ArchPilot_API>("BackEnd", "https");
var blazorApp = builder.AddProject<Projects.ArchPilot_BlazorWasm>("blazorwasm", "https")
    .WithReference(apiService);


var app = builder.Build();
app.Run();