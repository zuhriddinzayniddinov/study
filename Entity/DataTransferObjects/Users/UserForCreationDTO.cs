namespace Entity.DataTransferObjects;

public record UserForCreationDTO(
    string userName,
    string password,
    string firstName,
    string midleName,
    string lastName,
    string inn,
    long stuructureId
    );