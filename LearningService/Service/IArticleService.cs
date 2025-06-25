using Entity.DataTransferObjects.Learning;
using Entity.Models.ApiModels;
using Entity.Models.Learning;

namespace LearningService.Service;

public interface IArticleService
{ 
    Task<Article> CreateArticleAsync(ArticleDto article);
    Task<Article> UpdateArticleAsync(Article article);
    Task<Article> DeleteArticleAsync(int articleId);
    Task<Article> GetArticleByIdAsync(int id);
    Task<IList<Article>> GetAllArticleAsync(MetaQueryModel metaQuery);
    Task<IList<Article>> GetAllArticleByHashtagIdAsync(MetaQueryModel metaQuery,int hashtagId);
    Task<IList<Article>> GetAllArticleByAuthorIdAsync(MetaQueryModel metaQuery,int authorId);
    Task<IList<Article>> GetAllArticleByCategoryIdAsync(MetaQueryModel metaQuery,int categoryId);
    Task<IList<ArticleForWithDetailsDto>> GetArticleWithDetailsAsync(MetaQueryModel metaQuery);
}