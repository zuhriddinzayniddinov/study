using DatabaseBroker.Repositories.Learning;
using Entity.DataTransferObjects.Learning;
using Entity.Exeptions;
using Entity.Models.ApiModels;
using Entity.Models.Learning;
using Microsoft.EntityFrameworkCore;

namespace LearningService.Service;

public class CourseService(
    ICourseRepository courseRepository)
    : ICourseService
{
    public async Task<Course> CreateCourseAsync(CourseDto courseDto, int userId)
    {
        var newCourse = new Course
        {
            Title = courseDto.title,
            Description = courseDto.description,
            Image = courseDto.image,
            LanguageCode = courseDto.languageCode,
            AuthorId = courseDto.authorId,
            CategoryId = courseDto.categoryId,
            CreatedBy = userId,
        };

        return await courseRepository.AddAsync(newCourse);
    }
    public async Task<Course> DeleteCourseAsync(int courseId, int userId)
    {
        var courseResult = await courseRepository.GetByIdAsync(courseId)
            ?? throw new NotFoundException("Not found");
        
        courseResult.UpdatedBy = userId;

        return await courseRepository.RemoveAsync(courseResult);
    }
    public async Task<IList<Course>> GetAllCourseAsync(MetaQueryModel metaQuery)
    {
        var courses = await courseRepository
            .GetAllAsQueryable(deleted:metaQuery.IsDeleted)
            .Skip(metaQuery.Skip)
            .Take(metaQuery.Take)
            .Select(c => new Course()
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                Image = c.Image,
                LanguageCode = c.LanguageCode,
                AuthorId = c.AuthorId,
                Author = new Author(){Id = c.Author.Id,Name = c.Author.Name,Content = c.Author.Content,ImageLink = c.Author.ImageLink},
                CategoryId = c.CategoryId,
                Category = new Category(){Id = c.Category.Id,Title = c.Category.Title,Description = c.Category.Description,ImageLink = c.Category.ImageLink},
            })
            .ToListAsync();

        return courses;
    }
    public async Task<IList<Course>> GetAllCourseByCategoryIdAsync(MetaQueryModel metaQuery,int categoryId)
    {
        var courses = await courseRepository
            .GetAllAsQueryable(deleted:metaQuery.IsDeleted)
            .Where(c => c.CategoryId == categoryId)
            .Skip(metaQuery.Skip)
            .Take(metaQuery.Take)
            .Select(c => new Course()
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                Image = c.Image,
                LanguageCode = c.LanguageCode,
                AuthorId = c.AuthorId,
                Author = new Author(){Id = c.Author.Id,Name = c.Author.Name,Content = c.Author.Content,ImageLink = c.Author.ImageLink},
                CategoryId = c.CategoryId,
                Category = new Category(){Id = c.Category.Id,Title = c.Category.Title,Description = c.Category.Description,ImageLink = c.Category.ImageLink},
            })
            .ToListAsync();

        return courses;
    }
    public async Task<IList<Course>> GetAllCourseByAuthorIdAsync(MetaQueryModel metaQuery, int authorId)
    {
        var newCourse = await courseRepository
           .GetAllAsQueryable(deleted:metaQuery.IsDeleted)
           .Where(x => x.AuthorId == authorId)
           .Skip(metaQuery.Skip)
           .Select(c => new Course()
           {
               Id = c.Id,
               Title = c.Title,
               Description = c.Description,
               Image = c.Image,
               AuthorId = c.AuthorId,
               Author = new Author() { Id = c.Author.Id, Name = c.Author.Name, ImageLink = c.Author.ImageLink },
               CategoryId = c.CategoryId,
               Category = new Category() { Id = c.Category.Id, Title = c.Category.Title, ImageLink = c.Category.ImageLink },
           })
           .Take(metaQuery.Take)
           .ToListAsync();
        
        return newCourse;
    }
    public async Task<IList<Course>> GetAllCourseByHashtagIdAsync(MetaQueryModel metaQuery, int hashtagId)
    {
        var newCourse = await courseRepository
            .GetAllAsQueryable(deleted:metaQuery.IsDeleted)
            .Skip(metaQuery.Skip)
            .Take(metaQuery.Take)
            .Select(c => new Course()
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                Image = c.Image,
                LanguageCode = c.LanguageCode,
                AuthorId = c.AuthorId,
                Author = new Author() { Id = c.Author.Id, Name = c.Author.Name,  ImageLink = c.Author.ImageLink },
                CategoryId = c.CategoryId,
                Category = new Category() { Id = c.Category.Id, Title = c.Category.Title, ImageLink = c.Category.ImageLink },
            })
            .ToListAsync();

        return newCourse;
    }
    public async Task<Course> GetCourseByIdAsync(int id)
    {
        var courseResult = await courseRepository
                .GetAllAsQueryable(true)
                .Select(c => new Course()
                {
                    Id = c.Id,
                    Title = c.Title,
                    Description = c.Description,
                    Image = c.Image,
                    LanguageCode = c.LanguageCode,
                    AuthorId = c.AuthorId,
                    Author = new Author(){Id = c.Author.Id,Name = c.Author.Name,Content = c.Author.Content,ImageLink = c.Author.ImageLink},
                    CategoryId = c.CategoryId,
                    Category = new Category(){Id = c.Category.Id,Title = c.Category.Title,Description = c.Category.Description,ImageLink = c.Category.ImageLink},
                })
                .FirstOrDefaultAsync(c => c.Id == id)
            ?? throw new NotFoundException("Not found");

         return courseResult;
    }
    public async Task<Course> UpdateCourseAsync(Course course, int userId)
    {
        var courseResult = await courseRepository.GetByIdAsync(course.Id)
            ?? throw new NotFoundException("Not found");

        courseResult.Title = course.Title ?? courseResult.Title;
        courseResult.Description = course.Description ?? courseResult.Description;
        courseResult.Image = course.Image ?? courseResult.Image;
        courseResult.LanguageCode = course.LanguageCode ?? courseResult.LanguageCode;
        courseResult.AuthorId = (course.AuthorId != 0) ? course.AuthorId : courseResult.AuthorId;
        courseResult.CategoryId = (course.CategoryId != 0) ? course.CategoryId : courseResult.CategoryId;
        courseResult.UpdatedBy = userId;
        
        return await courseRepository.UpdateAsync(courseResult);
    }
}
