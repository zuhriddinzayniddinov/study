namespace Entity.DataTransferObjects.StaticFiles;

public record FileInfoDto(
    long id,
    string url,
    long? size,
    string name,
    string? type,
    string? fileExtension);