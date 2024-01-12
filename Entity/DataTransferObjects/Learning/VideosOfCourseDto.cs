using Entitys.Models;

namespace Entity.DataTransferObjects.Learning;

public record VideosOfCourseDto(
    string videoLinc,
    MultiLanguageField title,
    MultiLanguageField content,
    int courseId,
    int orderNumber,
    List<string> docsUrl);
