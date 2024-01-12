using Entity.Enum;

namespace Entity.DataTransferObjects.StaticFiles
{
    public record StaticFileResponsDto(
        string url,
        StaticFileStatus status);
}
