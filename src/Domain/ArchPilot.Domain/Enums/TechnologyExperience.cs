namespace ArchPilot.Domain.Enums;

[Flags]
public enum TechnologyExperience
{
    None = 0,
    DotNetCSharp = 1,
    JavaScriptTypeScript = 2,
    Python = 4,
    Java = 8,
    CloudAwsAzure = 16,
    DatabaseDesign = 32,
    NewToDevelopment = 64
}
