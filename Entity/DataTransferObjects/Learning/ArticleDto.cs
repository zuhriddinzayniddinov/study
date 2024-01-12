using Entitys.Models;

namespace Entity.DataTransferObjects.Learning;

public record ArticleDto(
    MultiLanguageField title,
    MultiLanguageField description,
    MultiLanguageField content,
    int authorId,
    int categoryId,
    List<int> hashtagId,
    string image);