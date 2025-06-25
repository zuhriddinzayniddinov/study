using DatabaseBroker.Repositories.Learning;
using Entity.DataTransferObjects.Learning;
using Entity.Exeptions;
using Entity.Models.ApiModels;
using Entity.Models.Learning;
using Microsoft.EntityFrameworkCore;

namespace LearningService.Service;

public class SeminarVideoService(
    ICategoryRepository categoryRepository,
    ISeminarVideoRepository seminarVideoRepository)
    : ISeminarVideoService
{
    public async ValueTask<SeminarVideo> CreateSeminarVideoAsync(SeminarVideoDto seminarVideo)
    {
        var nwCourse = new SeminarVideo()
        {
            Title = seminarVideo.title,
            VideoLinc = seminarVideo.videoLinc,
            AuthorId =seminarVideo.authorId,
            CategoryId = seminarVideo.categoryId,
        };

        return await seminarVideoRepository.AddAsync(nwCourse);
    }

    public async ValueTask<Category> CreateSeminarVideoCategoryAsync(CategoryDto seminarVideoCategory)
    {
        var nwCategory = new Category() 
        {        
            Title = seminarVideoCategory.title,
            Description =seminarVideoCategory.description,
            ImageLink = seminarVideoCategory.imageLink
        };
        return await categoryRepository.AddAsync(nwCategory);
    }

    public async ValueTask<SeminarVideo> DeleteSeminarVideoAsync(int seminarVideoId)
    {
        var articleResult = await seminarVideoRepository.GetByIdAsync(seminarVideoId)
            ?? throw new NotFoundException("Logotype not found");

        return await seminarVideoRepository.RemoveAsync(articleResult);
    }

    public async ValueTask<Category> DeleteSeminarVideoCategoryAsync(int categoryId)
    {
        var articleResult = await categoryRepository.GetByIdAsync(categoryId)
                            ?? throw new NotFoundException("Logotype not found");

        return await categoryRepository.RemoveAsync(articleResult);
    }

    public async ValueTask<IList<SeminarVideo>> GetAllSeminarVideoAsync(MetaQueryModel metaQuery)
    {
        var seminarVideos = await seminarVideoRepository
           .GetAllAsQueryable(deleted:metaQuery.IsDeleted)
           .Skip(metaQuery.Skip)
           .Take(metaQuery.Take)
           .Select(sv => new SeminarVideo()
           {
               Id = sv.Id,
               CategoryId = sv.CategoryId,
               VideoLinc = sv.VideoLinc,
               Title = sv.Title,
               AuthorId = sv.AuthorId,
               Category = new Category(){Id = sv.Category!.Id,Title = sv.Category.Title,ImageLink = sv.Category.ImageLink,Description = sv.Category.Description},
               Author = new Author(){Id = sv.Author!.Id,Name = sv.Author.Name,ImageLink = sv.Author.ImageLink}
           })
           .ToListAsync();

        return seminarVideos;
    }

    public async ValueTask<IList<SeminarVideo>> GetAllSeminarVideoByAuthorIdAsync(MetaQueryModel metaQuery, int authorId)
    {
        var newSeminars = await seminarVideoRepository
            .GetAllAsQueryable(deleted:metaQuery.IsDeleted)
            .Where(x => x.AuthorId == authorId)
            .Skip(metaQuery.Skip)
            .Take(metaQuery.Take)
            .Select(seminar => new SeminarVideo()
            {
                Id = seminar.Id,
                VideoLinc = seminar.VideoLinc,
                Title = seminar.Title,
                AuthorId = seminar.AuthorId,
                Author = new Author() { Id = seminar.Author.Id, Name = seminar.Author.Name, ImageLink = seminar.Author.ImageLink  },
                CategoryId = seminar.CategoryId,
                Category = new Category() { Id = seminar.Category.Id, Title = seminar.Category.Title, ImageLink = seminar.Category.ImageLink },
            })
            .ToListAsync();
        
        return newSeminars;
    }

    public async ValueTask<IList<SeminarVideo>> GetAllSeminarVideoByHashtagIdAsync(MetaQueryModel metaQuery, int hashtagId)
    {
        var newSeminars = await seminarVideoRepository
            .GetAllAsQueryable(deleted:metaQuery.IsDeleted)
            .Skip(metaQuery.Skip)
            .Take(metaQuery.Take)
            .Select(seminar => new SeminarVideo()
            {
                Id = seminar.Id,
                VideoLinc = seminar.VideoLinc,
                Title = seminar.Title,
                AuthorId = seminar.AuthorId,
                Author = new Author() { Id = seminar.Author.Id, Name = seminar.Author.Name, ImageLink = seminar.Author.ImageLink },
                CategoryId = seminar.CategoryId,
                Category = new Category() { Id = seminar.Category.Id, Title = seminar.Category.Title, ImageLink = seminar.Category.ImageLink },

            })
            .ToListAsync();

        return newSeminars;
    }

    public async ValueTask<IList<Category>> GetAllSeminarVideoCategoryAsync(MetaQueryModel metaQuery)
    {
        var articles = await categoryRepository
            .GetAllAsQueryable(deleted:metaQuery.IsDeleted)
            .Skip(metaQuery.Skip)
            .Take(metaQuery.Take)
            .ToListAsync();
        return articles;
    }

    public async ValueTask<Category> GetCategoryById(int id)
    {
        return await categoryRepository.GetByIdAsync(id);
    }

    public async ValueTask<IList<SeminarVideo>> GetSeminarVideoByCategoryIdAsync(MetaQueryModel metaQuery, int categoryId)
    {
        var seminars = await seminarVideoRepository
             .GetAllAsQueryable(deleted:metaQuery.IsDeleted)
             .Where(vc => vc.CategoryId == categoryId)
             .Skip(metaQuery.Skip)
             .Take(metaQuery.Take)
             .Select(seminar => new SeminarVideo()
             {
                 Id = seminar.Id,
                 VideoLinc = seminar.VideoLinc,
                 Title = seminar.Title,
                 AuthorId = seminar.AuthorId,
                 Author = new Author() { Id = seminar.Author.Id, Name = seminar.Author.Name, Content = seminar.Author.Content, ImageLink = seminar.Author.ImageLink },
                 CategoryId = seminar.CategoryId,
                 Category = new Category() { Id = seminar.Category.Id,  Title = seminar.Category.Title, Description = seminar.Category.Description, ImageLink = seminar.Category.ImageLink },
             })
             .ToListAsync();

        return seminars;
    }

    public async ValueTask<IList<SeminarVideoForWhithDetaileDto>> GetSeminarVideoWithDetailsAsync(MetaQueryModel metaQuery)
    {
        var articles = seminarVideoRepository
                .GetAllAsQueryable(deleted:metaQuery.IsDeleted)
                .Skip(metaQuery.Skip)
                .Take(metaQuery.Take)
                .ToList();

        var result = articles.Select(x => new SeminarVideoForWhithDetaileDto()
        {
            id = x.Id,
            title = x.Title,
            author = x.Author,
            category = x.Category,
            videoLinc = x.VideoLinc
        }).ToList();

        return result;
    }

    public async ValueTask<SeminarVideo> UpdateSeminarVideoAsync(SeminarVideo seminarVideo)
    {
        var result = await seminarVideoRepository.GetByIdAsync(seminarVideo.Id)
           ?? throw new NotFoundException("Logotype not found");
        result.Title = seminarVideo.Title ?? result.Title;
        result.VideoLinc = seminarVideo.VideoLinc ?? result.VideoLinc;
        result.CategoryId = seminarVideo.CategoryId != 0 ? seminarVideo.CategoryId : result.CategoryId;
        result.AuthorId = seminarVideo.AuthorId != 0 ? seminarVideo.AuthorId : result.AuthorId;

        return await seminarVideoRepository .UpdateAsync(result);
    }

    public async ValueTask<Category> UpdateSeminarVideoCategoryAsync(Category seminarVideoCategory)
    {
        var result = await categoryRepository.GetByIdAsync(seminarVideoCategory.Id)
                           ?? throw new NotFoundException("Logotype not found");

        result.Title = seminarVideoCategory?.Title ?? result.Title;
        result.Description = seminarVideoCategory.Description ?? result.Description;

        return await categoryRepository .UpdateAsync(result);
    }
}
