var builder = DistributedApplication.CreateBuilder(args);

    

builder.AddProject("BackEnd", @"..\src\Presentation\ArchPilot.API\ArchPilot.API.csproj", "https");
builder.AddProject("FrontEnd", @"..\src\Presentation\ArchPilot.BlazorWasm\ArchPilot.BlazorWasm.csproj", "https");
builder.Build().Run();
