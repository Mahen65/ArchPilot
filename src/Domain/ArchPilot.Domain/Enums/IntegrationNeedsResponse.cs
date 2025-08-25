namespace ArchPilot.Domain.Enums;

public enum IntegrationNeedsResponse
{
    RESTAPIs = 1,
    GraphQLAPIs = 2,
    WebhooksEventDriven = 3,
    MessageQueues = 4,
    NotNeeded = 5
}
