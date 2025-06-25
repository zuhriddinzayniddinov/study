using Entity.DataTransferObjects.Learning;
using Entity.Models.ApiModels;
using Entity.Models.Learning;

namespace LearningService.Service;

public interface ICourseService
{
    Task<Course> CreateCourseAsync(CourseDto courseDto, int userId);
    Task<Course> UpdateCourseAsync(Course course, int userId);
    Task<Course> DeleteCourseAsync(int courseId, int userId);
    Task<Course> GetCourseByIdAsync(int id);
    Task<IList<Course>> GetAllCourseAsync(MetaQueryModel metaQuery);
    Task<IList<Course>> GetAllCourseByAuthorIdAsync(MetaQueryModel metaQuery,int authorId);
    Task<IList<Course>> GetAllCourseByHashtagIdAsync(MetaQueryModel metaQuery, int hashtagId);
    Task<IList<Course>> GetAllCourseByCategoryIdAsync(MetaQueryModel metaQuery, int categoryId);
}