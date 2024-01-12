using Entity.DataTransferObjects.Role;
using Entity.Models;
using Microsoft.AspNetCore.Mvc;
using RoleService.Service;
using Entity.Enum;
using Microsoft.EntityFrameworkCore;
using WebCore.Attributes;
using WebCore.Controllers;
using WebCore.Models;

namespace AuthApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class RoleController : ApiControllerBase
{
    private readonly IRoleService _structureService;

    public RoleController(IRoleService structureService)
    {
        _structureService = structureService;
    }

    [HttpPost, Authorize(UserPermissions.StructureCreate)]
    public async Task<ResponseModel> CreateStructure(StructureForCreationDTO structureDto)
    {
        return ResponseModel
            .ResultFromContent(
                await _structureService.CreateStructureAsync(structureDto));
    }

    [HttpGet("{name}"), Authorize(UserPermissions.StructureView)]
    public async ValueTask<ResponseModel> GetStructureByName(string name)
    {
        return ResponseModel
            .ResultFromContent(
                _structureService.RetrieveStructureByName(name));
    }

    /*[HttpDelete, Authorize(UserPermissions.StructureDelete)]
    public async ValueTask<ResponseModel> DeleteStructure(long structureId)
    {
        return ResponseModel
            .ResultFromContent(
                await _structureService.RemoveStructureAsync(structureId));
    }*/

    [HttpPut, Authorize(UserPermissions.StructureEdit)]
    public async ValueTask<ResponseModel> PutStructure(StructureDTO structure)
    {
        return ResponseModel
            .ResultFromContent(
                await _structureService.ModifyStructureAsync(structure));
    }

    [HttpGet, Authorize(UserPermissions.StructureView)]
    public async ValueTask<ResponseModel> GetAllStructures()
    {
        return ResponseModel
            .ResultFromContent(
                await _structureService.RetrieveStructureAsync());
    }

    [HttpPut, Authorize(UserPermissions.PermissionNameEdit)]
    public async ValueTask<ResponseModel> UpdatePermissionName(Permission permissionName)
    {
        return ResponseModel
            .ResultFromContent(
                await _structureService.ModifyPermission(permissionName));
    }

    [HttpGet, Authorize(UserPermissions.PermissionView)]
    public async ValueTask<ResponseModel> GetPermissions()
    {
        return ResponseModel
            .ResultFromContent(
                await _structureService.RetrievePermissionAsync().ToListAsync());
    }


    [HttpGet, Authorize(UserPermissions.PermissionView)]
    public IActionResult GetPermissionByName(string permissionName)
    {
        return Ok(_structureService.RetrieveStructureByName(permissionName));
    }


    [HttpPost, Authorize(UserPermissions.StructureEdit)]
    public async ValueTask<ResponseModel> CreateStructurePermission(
        StructurePermissionForCreationDTO structurePermission)
    {
        return ResponseModel
            .ResultFromContent(
                await _structureService.CreateStructurePermission(structurePermission));
    }

    [HttpGet, Authorize(UserPermissions.PermissionView)]
    public IActionResult GetAllStructurePermission()
    {
        return Ok(_structureService.RetriveStructurePermission().ToList());
    }

    [HttpGet("/api/structures/{structureId:long}/permissions"), Authorize(UserPermissions.PermissionView)]
    public async ValueTask<ResponseModel> GetStructurePermissionByStructureId(long structureId)
    {
        return ResponseModel
            .ResultFromContent(
                await _structureService.RetriveStructurePermissionByStructureId(structureId).ToListAsync());
    }

    [HttpDelete, Authorize(UserPermissions.StructureEdit)]
    public async ValueTask<ActionResult<ResponseModel>> DeleteStructurePermission(
        StructurePermissionForCreationDTO structurePermission)
    {
        return ResponseModel.ResultFromContent(
            await _structureService
                .RemoveStructurePermissionAsync(structurePermission));
    }

    [HttpPost, Authorize(UserPermissions.StructureEdit)]
    public async ValueTask<ResponseModel> AssignPermissionsToStructureById(
        [FromBody] AssignPermissionToStructureDto dto)
    {
        await _structureService
            .AssignPermissionsToStructureById(dto.StructureId, 
                this.UserId, dto.PermissionIds);
        return ResponseModel.ResultFromContent(true);
    }
}