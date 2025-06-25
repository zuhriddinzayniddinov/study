using Entitys.Models;

namespace Entity.DataTransferObjects.Learning;

public record ArticleDto(
    string title,
    string description,
    string content,
    int authorId,
    int categoryId,
    List<int> hashtagId,
    string image);