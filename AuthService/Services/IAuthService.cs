using Entity.DataTransferObjects.Authentication;
using Entity.Models;

namespace AuthService.Services;

public interface IAuthService
{
    TokenDto DeleteToken(string accesstoken);
    Task<bool> RegisterByEsi(ESIDto esi);
    Task<bool> Register(UserRegisterDto userRegisterDto);
    Task<TokenDto> SignByPassword(AuthenticationDto authenticationDto);
    Task<TokenDto> SignByESI(ESIDto esiDto);
    Task<TokenDto> RefreshTokenAsync(TokenDto tokenDto);
    Task<bool> DeleteTokenAsync(TokenDto tokenDto);
    Task<TokenDto> SignByOneID(string code,string frontUrl);
    Task<bool> RegisterByOneID(string code,string frontUrl);
    Task<Uri?> GetOneIDUrlAsync(string frontUrl);
}