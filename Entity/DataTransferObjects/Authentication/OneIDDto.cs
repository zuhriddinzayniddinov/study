namespace Entity.DataTransferObjects.Authentication;

public record OneIDDto(
    string code,
    string frontUrl);