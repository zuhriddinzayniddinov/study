namespace Entity.DataTransferObjects;

public record UserForModificationDTO(
    long id,
    string userName,
    string firstname,
    string midlename,
    string lastname,
    string inn,
    string password
    );