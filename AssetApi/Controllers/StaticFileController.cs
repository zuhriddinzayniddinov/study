using Entity.DataTransferObjects.StaticFiles;
using Microsoft.AspNetCore.Mvc;
using StaticFileService.Service;
using WebCore.Models;

namespace AssetApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
[RequestFormLimits(MultipartBodyLengthLimit = (long)1024 * 1024 * 1024 * 1024)]
public class StaticFileController : ControllerBase       
{
    private readonly IStaticFileService _staticFileService;

    public StaticFileController(IStaticFileService staticFileService)
    {
        _staticFileService = staticFileService;
    }

    [HttpPost]
    public async Task<ResponseModel> Add([FromForm]FileDto fileDto)
    {
        return ResponseModel
            .ResultFromContent(
                await _staticFileService.AddFileAsync(fileDto));
    }
    
    [HttpPut]
    public async Task<ResponseModel> UpdateFileName(string url, string newName)
    {
        return ResponseModel
            .ResultFromContent(
                await _staticFileService.UpdateFileNameAsync(url, newName));
    }

    [HttpGet]
    public async ValueTask<ResponseModel> GetFolder()
    {
        return ResponseModel
            .ResultFromContent(
                await _staticFileService.GetFolderAsync());
    }
    
    [HttpPost]
    public async ValueTask<ResponseModel> GetFilesInfo(List<string> filePaths)
    {
        return ResponseModel
            .ResultFromContent(
                await _staticFileService.GetFilesInfoAsync(filePaths));
    }

    [HttpGet]
    public async ValueTask<ResponseModel> GetImagesByFolderPath(string folderPath)
    {
        return ResponseModel
            .ResultFromContent(
                await _staticFileService.GetImagesByFolderPathAsync(folderPath));
    }

    [HttpGet]
    public async ValueTask<ResponseModel> GetImagesNiceBeck()
    {
        return ResponseModel
            .ResultFromContent(
                await _staticFileService.GetImagesNiceBeckAsync());
    }

    [HttpDelete]
    public async Task<ResponseModel> Remove([FromBody]RemoveFileDto removeFileDto)
    {
        return ResponseModel
            .ResultFromContent(
                await _staticFileService.RemoveAsync(removeFileDto));
    }
}