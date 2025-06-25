namespace Entity.DataTransferObjects.Learning;

public record CourseDto(
    string title,
    string description,
    string languageCode,
    int authorId,
    int categoryId,
    string image);
