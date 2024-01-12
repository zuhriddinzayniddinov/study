using Entitys.Models;

namespace Entity.DataTransferObjects.Learning;

public record CourseDto(
    MultiLanguageField title,
    MultiLanguageField description,
    int authorId,
    int categoryId,
    int orderNumber,
    List<string> docsUrl,
    string image,
    List<int> hashtagId);
