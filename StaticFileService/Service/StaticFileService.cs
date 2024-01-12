using DatabaseBroker.Context.Repositories.StaticFiles;
using Entity.DataTransferObjects.StaticFiles;
using Entity.Enum;
using Entity.Exeptions;
using Entity.Models.StaticFiles;
using Microsoft.EntityFrameworkCore;

namespace StaticFileService.Service;

public class StaticFileService : IStaticFileService
{
    private readonly IStaticFileRepository _staticFileRepository;

    public StaticFileService(IStaticFileRepository staticFileRepository)
    {
        _staticFileRepository = staticFileRepository;
    }

    public async ValueTask<StaticFileDto> AddFileAsync(FileDto fileDto)
    {
        var filePath = Guid.NewGuid() + Path.GetExtension(fileDto.file.FileName);
        var fieldName = fileDto.fieldName;
        if(fieldName.Length == 0)
            fieldName = "temp";
        
        var path = Path.Combine(
            Directory.GetCurrentDirectory(),
            "wwwroot", fieldName);
        
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        path = Path.Combine(path, filePath);

        var staticFile = new StaticFile()
        {
            Path = path,
            FileExtension = Path.GetExtension(fileDto.file.FileName),
            Url = $"{fieldName}/{filePath}",
            Size = fileDto.file.Length,
            OldName = fileDto.fileName ?? Path.GetFileName(fileDto.file.FileName)
        };

        await using Stream fileStream = new FileStream(path, FileMode.Create);
        await  fileDto.file.CopyToAsync(fileStream);

        staticFile = await _staticFileRepository.AddAsync(staticFile);

        return new StaticFileDto(staticFile.Id,staticFile.Url,staticFile.OldName);
    }

    public async ValueTask<List<FileInfoDto>> GetFilesInfoAsync(List<string> filePaths)
    {
        return await _staticFileRepository.GetAllAsQueryable(true)
            .Where(sf => filePaths.Contains(sf.Url))
            .Select(sf => new FileInfoDto(
                sf.Id,
                sf.Url,
                sf.Size,
                sf.OldName ?? "",
                sf.Type,
                sf.FileExtension))
            .ToListAsync();
    }

    public async ValueTask<IList<string>> GetFolderAsync()
    {
        var result = new List<string>();
        string path = Path.Combine(
            Directory.GetCurrentDirectory(),
            "wwwroot");
         var subDirectories = Directory.GetDirectories(path);
         var splitSubDirectories = subDirectories.Select(x => x.Split("/")).ToList();

         foreach( var item in splitSubDirectories)
         {
            result.Add(item[item.Length - 1]);                        
         }
         return result;
    }

    public async ValueTask<IList<StaticFileResponsDto>> GetImagesByFolderPathAsync(string folderPath)
    {
        var result = new List<StaticFileResponsDto>();
        string path = Path.Combine(
            Directory.GetCurrentDirectory(),
            "wwwroot", folderPath);
        var files = Directory.GetFiles(path);

        foreach(var item in files)
        {
            var sfCheck = _staticFileRepository
                .GetAllAsQueryable()
                .FirstOrDefault(x => x.Path == item);

            if (sfCheck == null)
                result.Add(
                    new StaticFileResponsDto(
                        item,
                        StaticFileStatus.Beck
                    ));            
            else
                result.Add(
                    new StaticFileResponsDto(
                        item,
                        StaticFileStatus.BaseAndBeck
                    ));
        }

        return result;
    }

    public async ValueTask<IList<StaticFileResponsDto>> GetImagesNiceBeckAsync()
    {
        var result = new List<StaticFileResponsDto>();
        var path = Path.Combine(
            Directory.GetCurrentDirectory(),
            "wwwroot");
        var filesInBeck = GetImageFiles(path);
        var filesInBase = _staticFileRepository.GetAllAsQueryable().ToList();
        foreach( var item in filesInBase)
        {
            var checkFiles = filesInBeck.FirstOrDefault(x => x == item.Path);

            if(checkFiles==null)
                result.Add(
                   new StaticFileResponsDto(
                       item.Url,
                       StaticFileStatus.Base
                   ));
            else
                result.Add(
                    new StaticFileResponsDto(
                        item.Url,
                        StaticFileStatus.BaseAndBeck
                    ));
        }
        return result;
    }

    public async ValueTask<FileInfoDto> UpdateFileNameAsync(string url, string newName)
    {
        var staticFile = await _staticFileRepository.GetAllAsQueryable()
            .FirstOrDefaultAsync(sf => sf.Url == url)
            ?? throw new NotFoundException("Not found file");

        staticFile.OldName = newName;

        staticFile = await _staticFileRepository.UpdateAsync(staticFile);

        return new FileInfoDto(
            staticFile.Id,
            staticFile.Url,
            staticFile.Size,
            staticFile.OldName,
            staticFile.Type,
            staticFile.FileExtension);
    }

    static List<string> GetImageFiles(string directory)
    {
        List<string> imageFiles = new List<string>();

        string[] files = Directory.GetFiles(directory);
        foreach (string file in files)
        {
            imageFiles.Add(file);
        }

        string[] subDirectories = Directory.GetDirectories(directory);
        foreach (var item in subDirectories)
        {
            Console.WriteLine(item);
        }
        foreach (string subDirectory in subDirectories)
        {
            List<string> subDirectoryImageFiles = GetImageFiles(subDirectory);
            imageFiles.AddRange(subDirectoryImageFiles);
        }

        return imageFiles;
    }
    public async ValueTask<StaticFileDto> RemoveAsync(RemoveFileDto removeFileDto)
    {
        var staticFile = await _staticFileRepository.GetAllAsQueryable()
            .FirstOrDefaultAsync(sf => sf.Url == removeFileDto.filePath);
        
        var path = Path.Combine(
            Directory.GetCurrentDirectory(),
            staticFile.Path);
        
        if (staticFile == null || staticFile.Id == 0)
        {
            throw new NotFoundException($"Static File Not found by url: {removeFileDto.filePath}");
        }

        await _staticFileRepository.RemoveAsync(staticFile);

        return new StaticFileDto(staticFile.Id, staticFile.Url,staticFile.OldName);
    }
}