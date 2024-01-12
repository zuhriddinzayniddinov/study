namespace Entity.DataTransferObjects.Authentication;

public record UserRegisterDto(
    string firstname,
    string? middlename,
    string lastname,
    string login,
    string password
);