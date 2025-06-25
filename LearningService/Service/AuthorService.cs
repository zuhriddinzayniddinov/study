using DatabaseBroker.Repositories.Learning;
using Entity.DataTransferObjects.Learning;
using Entity.Exeptions;
using Entity.Models.Learning;
using Microsoft.EntityFrameworkCore;

namespace LearningService.Service;

public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _repository;

    public AuthorService(IAuthorRepository repository)
    {
        _repository = repository;
    }
    public async Task<Author> CreateAuthorAsync(AuthorDto article, int userId)
    {
        var result = new Author
        {
            Name = article.name,
            Content = article.content,
            ImageLink = article.imageLink ?? "",
            UserId = userId
        };
        
        return await _repository.AddAsync(result);
    }
    public async Task<Author> DeleteAuthorAsync(int articleId, int userId)
    {
        var articleResult = await _repository.GetByIdAsync(articleId)
           ?? throw new NotFoundException("Author not found");
        
        if(articleResult.UserId != userId)
            throw new NotFoundException("Author not found");

        return await _repository.RemoveAsync(articleResult);
    }
    public async Task<AuthorDto> GetAuthorByIdAsync(int id)
    {
        var article = await _repository.GetByIdAsync(id)
            ?? throw new NotFoundException("Not Found");

        return new AuthorDto(article.Name,article.Content,article.ImageLink,article.Id,
            article.Courses.Count,
            article.ShortVideos.Count,
            article.SeminarVideos.Count,
            article.Articles.Count);
    }
    public async Task<IList<AuthorDto>> GetAllAuthorAsync()
    {
        var articles = await _repository
           .GetAllAsQueryable()
           .Select(author => new AuthorDto(author.Name,author.Content,author.ImageLink,author.Id,0,0,0,0))
           .ToListAsync();

        return articles;
    }
    public async Task<Author> UpdateAuthorAsync(AuthorDto article, int userId)
    {
        var articleResult = await _repository.GetByIdAsync(article.id)
            ?? throw new NotFoundException("Not found");
        
        if(articleResult.UserId != userId)
            throw new NotFoundException("Not found");
        
        articleResult.Name = article.name ?? articleResult.Name;
        articleResult.Content = article.content ?? articleResult.Content;
        articleResult.ImageLink = article.imageLink ?? articleResult.ImageLink;

        return await _repository.UpdateAsync(articleResult);
    }
}
