using Entity.DataTransferObjects.Learning;
using LearningService.Service;
using Microsoft.AspNetCore.Mvc;
using WebCore.Controllers;
using WebCore.Models;

namespace LearningApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class AuthorController(IAuthorService _authorService) : ApiControllerBase
{
    [HttpPost]
    public async ValueTask<ResponseModel> CreateAuthor(AuthorDto author)
    {
        return ResponseModel
            .ResultFromContent(await _authorService.CreateAuthorAsync(author, UserId));
    }

    [HttpGet]
    public async ValueTask<ResponseModel> GetAllAuthor()
    {
        return ResponseModel
            .ResultFromContent(await _authorService.GetAllAuthorAsync());
    }

    [HttpGet]
    public async Task<ResponseModel> GetAuthorById(int id)
    {
        return ResponseModel
            .ResultFromContent(await _authorService.GetAuthorByIdAsync(id));
    }

    [HttpPut]
    public async Task<ResponseModel> UpdateAuthor(AuthorDto author)
    {
        return ResponseModel
            .ResultFromContent(await _authorService.UpdateAuthorAsync(author, UserId));
    }

    [HttpDelete]
    public async Task<ResponseModel> DeleteAuthor(int id)
    {
        return ResponseModel
            .ResultFromContent(await _authorService.DeleteAuthorAsync(id, UserId));
    }
}