using Entity.DataTransferObjects.Learning;
using Entity.Models.Learning;

namespace LearningService.Service;

public interface IAuthorService
{
    Task<Author> CreateAuthorAsync(AuthorDto article, int userId);
    Task<Author> UpdateAuthorAsync(AuthorDto article, int userId);
    Task<Author> DeleteAuthorAsync(int articleId,  int userId);
    Task<AuthorDto> GetAuthorByIdAsync(int id);
    Task<IList<AuthorDto>> GetAllAuthorAsync();
}
