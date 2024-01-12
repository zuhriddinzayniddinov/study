using Entity.DataTransferObjects.StaticFiles;
using Microsoft.AspNetCore.Http;

namespace StaticFileService.Service;

public interface IStaticFileService
{
    ValueTask<StaticFileDto> AddFileAsync(FileDto fileDto);
    ValueTask<StaticFileDto> RemoveAsync(RemoveFileDto removeFileDto);
    ValueTask<List<FileInfoDto>> GetFilesInfoAsync(List<string> filePaths);
    ValueTask<IList<string>> GetFolderAsync();
    ValueTask<IList<StaticFileResponsDto>> GetImagesByFolderPathAsync(string folderPath);
    ValueTask<IList<StaticFileResponsDto>> GetImagesNiceBeckAsync();
    ValueTask<FileInfoDto> UpdateFileNameAsync(string url, string newName);
}