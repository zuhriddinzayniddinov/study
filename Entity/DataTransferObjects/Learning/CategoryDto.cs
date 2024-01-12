using Entitys.Models;

namespace Entity.DataTransferObjects.Learning;

public record CategoryDto(
    MultiLanguageField title,
    MultiLanguageField description,
    string imageLinc
);