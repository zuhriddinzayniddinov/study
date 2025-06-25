using Entity.DataTransferObjects.Learning;
using Entity.Models.ApiModels;
using Entity.Models.Learning;
using LearningService.Service;
using Microsoft.AspNetCore.Mvc;
using WebCore.Controllers;
using WebCore.Models;

namespace LearningApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class CourseController(ICourseService courseService) : ApiControllerBase
{
    [HttpPost]
    public async Task<ResponseModel> CreateCourse(CourseDto course)
    {
        return ResponseModel
            .ResultFromContent(await courseService.CreateCourseAsync(course, UserId));
    }

    [HttpGet]
    public async Task<ResponseModel> GetAllCourse([FromQuery] MetaQueryModel metaQuery)
    {
        return ResponseModel
            .ResultFromContent(await courseService.GetAllCourseAsync(metaQuery));
    }
    [HttpGet]
    public async Task<ResponseModel> GetAllCourseByCategoryId([FromQuery] MetaQueryModel metaQuery,[FromQuery]int categoryId)
    {
        return ResponseModel
            .ResultFromContent(await courseService.GetAllCourseByCategoryIdAsync(metaQuery,categoryId));
    }

    [HttpGet]
    public async Task<ResponseModel> GetCourseById(int id)
    {
        return ResponseModel
            .ResultFromContent(await courseService.GetCourseByIdAsync(id));
    }

    [HttpPut]
    public async Task<ResponseModel> UpdateCourse(Course course)
    {
        return ResponseModel
            .ResultFromContent(await courseService.UpdateCourseAsync(course, UserId));
    }

    [HttpDelete]
    public async Task<ResponseModel> DeleteCourse(int id)
    {
        return ResponseModel
            .ResultFromContent(await courseService.DeleteCourseAsync(id, UserId));
    }
}
