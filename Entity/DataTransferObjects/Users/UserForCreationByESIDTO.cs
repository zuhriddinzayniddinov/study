namespace Entity.DataTransferObjects;

public record UserForCreationByESIDTO(
    string uid,
    string lastName,
    string firstName,
    string middleName,
    string inn,
    long stuructureId,
    string signature);