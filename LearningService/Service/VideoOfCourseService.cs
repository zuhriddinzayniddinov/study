using DatabaseBroker.Repositories.Learning;
using Entity.DataTransferObjects.Learning;
using Entity.Exeptions;
using Entity.Models.ApiModels;
using Entity.Models.Learning;
using Microsoft.EntityFrameworkCore;

namespace LearningService.Service;

public class VideoOfCourseService(IVideoOfCourseRepository videoOfCourseRepository) : IVideoOfCourseService
{
    public async ValueTask<VideoOfCourse> CreateVideoOfCourseAsync(VideosOfCourseDto videoOfCourse)
    {
        var  VideoOfCourse = new VideoOfCourse()
        {
            Link =  videoOfCourse.videoLinc
        };

        return await videoOfCourseRepository.AddAsync(VideoOfCourse);
    }

    public async ValueTask<VideoOfCourse> DeleteVideoOfCourseAsync(int id)
    {
        var articleResult = await videoOfCourseRepository.GetByIdAsync(id)
            ?? throw new NotFoundException("Logotype not found");

        return await videoOfCourseRepository.RemoveAsync(articleResult);
    }

    public async ValueTask<IList<VideoOfCourse>> GetAllVideoOfCourseAsync(MetaQueryModel metaQuery)
    {
        var articles = await videoOfCourseRepository
           .GetAllAsQueryable(deleted:metaQuery.IsDeleted)
           .Skip(metaQuery.Skip)
           .Take(metaQuery.Take)
           .Select(voc => new VideoOfCourse()
           {
               Id = voc.Id,
           })
           .ToListAsync();

        return articles;
    }

    public async ValueTask<IList<VideoOfCourse>> GetVideoOfCourseByCourseIdAsync(MetaQueryModel metaQuery, int courseId)
    {
        var result = await videoOfCourseRepository
            .GetAllAsQueryable(deleted:metaQuery.IsDeleted)
            .Skip(metaQuery.Skip)
            .Take(metaQuery.Take)
            .Select(voc => new VideoOfCourse()
            {
                Id = voc.Id,
            })
            .ToListAsync();

        return result;
    }

    public async ValueTask<VideoOfCourse> GetVideoOfCourseByIdAsync(int id)
    {
        var result = await videoOfCourseRepository
            .GetByIdAsync(id);

        return result;
    }

    public async ValueTask<VideoOfCourse> UpdateVideoOfCourseAsync(VideoOfCourse videoOfCourse)
    {
        var result = await videoOfCourseRepository.GetByIdAsync(videoOfCourse.Id)
            ?? throw new NotFoundException("Logotype not found");
        

        return await videoOfCourseRepository.UpdateAsync(result);
    }
}
