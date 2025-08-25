namespace ArchPilot.Domain.Enums;

public enum DataSensitivityResponse
{
    EncryptionAtRest = 1,
    EncryptionInTransit = 2,
    AccessControls = 3,
    ComplianceCertification = 4,
    NotAConcern = 5
}
