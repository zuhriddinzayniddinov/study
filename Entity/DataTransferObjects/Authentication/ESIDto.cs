namespace Entity.DataTransferObjects.Authentication;

public record ESIDto(
    string signature,
    string uid);