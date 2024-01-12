using Entitys.Models;

namespace Entity.DataTransferObjects.Learning;

public record SeminarVideoDto(
    string videoLinc,
    MultiLanguageField title,
    int authorId,
    int categoryId,
    List<int> hashtagId);