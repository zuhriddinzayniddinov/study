using System.Security.Claims;
using AuthService.Services;
using Entity.DataTransferObjects.Authentication;
using Entity.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserService.Service;
using WebCore.Attributes;
using WebCore.Controllers;
using WebCore.Models;

namespace AuthApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class AuthController : ApiControllerBase
{
    private readonly IAuthService _authService;
    private readonly IUserService _userService;

    public AuthController(IAuthService authService,
        IUserService userService)
    {
        _authService = authService;
        _userService = userService;
    }
    
    [HttpPost]
    public async Task<ResponseModel> Sign(
        [FromBody] AuthenticationDto authenticationDto)
    {
        return ResponseModel
            .ResultFromContent(
                await _authService.SignByPassword(authenticationDto));
    }

    [HttpPost]
    public async Task<ResponseModel> Register(
        [FromBody] UserRegisterDto userRegisterDto)
    {
        return (await _authService
            .Register(userRegisterDto), 
            StatusCodes.Status200OK);
    }


    [HttpPost]
    public async Task<ResponseModel> SignByESI([FromBody] ESIDto esiDto)
    {
        return ResponseModel
            .ResultFromContent(
                await _authService.SignByESI(esiDto));
    }

    [HttpPost]
    public async Task<ResponseModel> RegisterByESI([FromBody] ESIDto esiDto)
    {
        return ResponseModel
            .ResultFromContent(
                await _authService.RegisterByEsi(esiDto));
    }

    [HttpPost]
    public async Task<ResponseModel> SignByOneID([FromBody] OneIDDto oneIdDto)
    {
        return ResponseModel
            .ResultFromContent(
                await _authService.SignByOneID(oneIdDto.code, oneIdDto.frontUrl));
    }

    [HttpPost]
    public async Task<ResponseModel> RegisterByOneID([FromBody] OneIDDto oneIdDto)
    {
        return ResponseModel
            .ResultFromContent(
                await _authService.RegisterByOneID(oneIdDto.code, oneIdDto.frontUrl));
    }
    
    [HttpPost]
    public async Task<ResponseModel> RefreshToken([FromBody]TokenDto tokenDto)
    {
        return ResponseModel
            .ResultFromContent(
                await _authService.RefreshTokenAsync(tokenDto));
    }
    
    [HttpDelete]
    public async Task<ResponseModel> LogOut([FromBody]TokenDto tokenDto)
    {
        return ResponseModel
            .ResultFromContent(
                await _authService.DeleteTokenAsync(tokenDto));
    }

    [HttpGet]
    public async Task<ResponseModel> OneIDUrl([FromQuery] string frontUrl)
    {
        return ResponseModel
            .ResultFromContent(
                await _authService.GetOneIDUrlAsync(frontUrl));
    }

    [HttpGet, Authorize(UserPermissions.UserInfoView)]
    public async Task<ResponseModel> GetUser()
    {
        return (await this._userService
            .RetrieveUserByIdAsync(this.UserId), 200);
    }

    [HttpGet, Authorize(UserPermissions.UsersView)]
    public async Task<ResponseModel> GetAllUsers()
    {
        return (await this._userService.RetrieveUsersAsync(), 200);
    }

    [HttpPut, Authorize(UserPermissions.ChangeUserStructure)]
    public async Task<ResponseModel> ChangeUserStructure(
        [FromBody] ChangeUserStructureDto dto)
    {
        return (await _userService.ChangeUserStructure(dto), 200);
    }
}