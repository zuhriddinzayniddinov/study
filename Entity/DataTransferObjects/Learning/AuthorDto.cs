using Entity.Models.Learning;
using Entitys.Models;

namespace Entity.DataTransferObjects.Learning;

public record AuthorDto(
    MultiLanguageField name,
    MultiLanguageField? content,
    string? imageLink = "",
    int id = 0,
    List<int>? categorieIds = null,
    List<Category>? categories = null,
    int? courseCount = 0,
    int? shortVideoCount = 0,
    int? seminarVideoCount = 0,
    int? articleCount = 0);