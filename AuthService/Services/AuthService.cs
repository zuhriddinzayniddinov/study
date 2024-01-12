using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Web;
using AuthenticationBroker.TokenHandler;
using DatabaseBroker.Context.Repositories.Auth;
using DatabaseBroker.Context.Repositories.Structures;
using DatabaseBroker.Context.Repositories.Users;
using DatabaseBroker.Repositories.Auth;
using DatabaseBroker.Repositories.Token;
using EimzoApi.Models;
using Entity.DataTransferObjects.Authentication;
using Entity.Enum;
using Entity.Exeptions;
using Entity.Exeptions.Eimzo;
using Entity.Extensions;
using Entity.Models;
using Entity.Models.ExternalAPIs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace AuthService.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenHandler _jwtTokenHandler;
    private readonly ITokenRepository _tokenRepository;
    private readonly IConfiguration _configuration;
    private readonly ISignMethodsRepository _signMethodsRepository;
    private readonly IStructureRepository _structureRepository;
    private readonly IUserCertificateRepository _userCertificateRepository;
    private readonly HttpClient _httpClient;
    private readonly OneIDOptions _oneIdOptions;

    public AuthService(
        IJwtTokenHandler jwtTokenHandler,
        IUserRepository userRepository,
        ITokenRepository tokenRepository,
        IConfiguration configuration,
        ISignMethodsRepository signMethodsRepository,
        IStructureRepository structureRepository,
        IUserCertificateRepository userCertificateRepository,
        HttpClient httpClient,
        IOptions<OneIDOptions> oneIdOptions)
    {
        _jwtTokenHandler = jwtTokenHandler;
        _userRepository = userRepository;
        _tokenRepository = tokenRepository;
        _configuration = configuration;
        _signMethodsRepository = signMethodsRepository;
        _structureRepository = structureRepository;
        _userCertificateRepository = userCertificateRepository;
        _httpClient = httpClient;
        _oneIdOptions = oneIdOptions.Value;
    }

    public TokenDto DeleteToken(string accesstoken)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> RegisterByEsi(ESIDto esi)
    {
        string? eImzoApiPath = _configuration.GetSection("Params:EimzoApi")?.Value;
        if (eImzoApiPath is null)
            throw new NotFoundException("Eimzo Api Path not found");
        SignaturePost postData = new SignaturePost()
        {
            Signature = esi.signature
        };
        PkcsResponse? result = await HttpClientHelpers.GetInstance(eImzoApiPath)
            .PostJsonAsync<SignaturePost, PkcsResponse>("eimzo/GetVerifyPkcs", postData);

        if (!result.Success)
            throw new InvalidOperationException(result.Reason);
        var defaultSigner = result.Pkcs7Info.Signers[0];
        CertificateModel defaultCertificate = defaultSigner.Certificate[0];

        if (defaultCertificate.ValidTo < DateTime.Now)
            throw new ExpiredException("This certificate is expired");

        //subjectName template is "cn=bobokalonov abdusalom xxx,
        //name=abdusalom,surname=bobokalonov,l=yunusobod tumani,
        //st=toshkent shahri,c=uz,o=business-terra mchj,uid=506156966,1.2.860.3.16.1.2=32301940190066,
        //ou=кўрсатилмаган,t=direktor,1.2.860.3.16.1.1=300777132,
        //businesscategory=masʼuliyati cheklangan jamiyat,
        //serialnumber=77f634b1,validfrom=2023.03.29 14:27:14,validto=2025.03.29 23:59:59"
        string subjectName = defaultCertificate.SubjectName;

        Dictionary<string, string> subjectData = subjectName.Split(",")
            .Select(x => x.Split("=").Select(y => y.Trim()))
            .Where(x => x.Count() > 1)
            .ToDictionary(x => x.ElementAt(0), x => x.ElementAt(1));

        subjectData.Add("INN", subjectData["1.2.860.3.16.1.1"]);
        subjectData.Add("PINFL", subjectData["1.2.860.3.16.1.2"]);

        var esiId = defaultCertificate.SerialNumber;

        var hasCertificate =
            await _signMethodsRepository.OfType<ESISignMethod>().AnyAsync(x => x.CertSerialNumber == esiId);
        if (hasCertificate)
            throw new AlreadyExistsException("This certificate already exists to another user");

        User newUser = new User()
        {
            FirstName = subjectData["Name"],
            LastName = subjectData["SURNAME"],
            MiddleName = null,
            StructureId = (await _structureRepository.FirstOrDefaultAsync(x => x.IsDefault))?.Id,
            SignMethods = new List<SignMethod>(),
        };
        User storedUser = await _userRepository.AddAsync(newUser);
        UserCertificate certificate = new UserCertificate()
        {
            SerialNumber = esiId,
            Country = subjectData["C"],
            CN = subjectData["CN"],
            Name = subjectData["Name"],
            Surname = subjectData["SURNAME"],
            Location = subjectData["L"],
            Street = subjectData["ST"],
            O = subjectData["O"],
            UID = subjectData["UID"],
            PINFL = subjectData["PINFL"],
            TIN = subjectData["INN"],
            OU = subjectData["OU"],
            Type = subjectData["T"],
            BusinessCategory = subjectData["BusinessCategory"],
            ValidTo = defaultCertificate.ValidTo,
            ValidFrom = defaultCertificate.ValidFrom,
            OwnerId = storedUser.Id
        };


        await _signMethodsRepository.AddAsync(new ESISignMethod()
        {
            UserId = storedUser.Id,
            CertSerialNumber = esiId,
        });

        await _userCertificateRepository.AddAsync(certificate);
        return true;
    }

    public async Task<bool> Register(UserRegisterDto userRegisterDto)
    {
        var hasStoredUser = await _signMethodsRepository.OfType<DefaultSignMethod>()
            .AnyAsync(x => x.Username == userRegisterDto.login);
        if (hasStoredUser)
            throw new AlreadyExistsException("User name or login already exists");

        User newUser = new User()
        {
            FirstName = userRegisterDto.firstname,
            LastName = userRegisterDto.lastname,
            MiddleName = userRegisterDto.middlename,
            SignMethods = new List<SignMethod>(),
            StructureId = (await _structureRepository.FirstOrDefaultAsync(x => x.IsDefault))?.Id,
        };

        var storedUser = await _userRepository.AddAsync(newUser);
        await _signMethodsRepository.AddAsync(new DefaultSignMethod()
        {
            Username = userRegisterDto.login,
            PasswordHash = PasswordHelper.Encrypt(userRegisterDto.password),
            UserId = storedUser.Id
        });
        return true;
    }

    public async Task<TokenDto> SignByPassword(AuthenticationDto authenticationDto)
    {
        var signMethod = await _signMethodsRepository.OfType<DefaultSignMethod>()
            .FirstOrDefaultAsync(x => x.Username == authenticationDto.username);

        if (signMethod is null)
            throw new NotFoundException("That credentials not found");

        if (!PasswordHelper.Verify(signMethod.PasswordHash, authenticationDto.password))
            throw new NotFoundException("User not found");

        var user = signMethod.User;

        // user.UserCerifiticate = 
        //     await _userCertificateRepository
        //     .FirstOrDefaultAsync(us =>
        //         us.OwnerId == user.Id)
        //     ?? throw new NotFoundException("That Certificate not found");;

        var refreshToken = _jwtTokenHandler.GenerateRefreshToken();

        var token = new TokenModel()
        {
            UserId = user.Id,
            TokenType = TokenTypes.Normal,
            AccessToken = new JwtSecurityTokenHandler()
                .WriteToken(_jwtTokenHandler.GenerateAccessToken(user)),
            RefreshToken = refreshToken.refreshToken,
            ExpireRefreshToken = refreshToken.expireDate
        };

        token = await _tokenRepository.AddAsync(token);

        var tokenDto = new TokenDto(
            token.AccessToken,
            token.RefreshToken,
            token.ExpireRefreshToken);

        return tokenDto;
    }

    public async Task<TokenDto> SignByESI(ESIDto esiDto)
    {
        string eImzoApiPath = _configuration.GetSection("Params:EimzoApi").Value;
        if (eImzoApiPath is null)
            throw new NotFoundException("Eimzo Api Path not found");
        SignaturePost postData = new SignaturePost()
        {
            Signature = esiDto.signature
        };
        PkcsResponse? result = await HttpClientHelpers.GetInstance(eImzoApiPath)
            .PostJsonAsync<SignaturePost, PkcsResponse>("eimzo/GetVerifyPkcs", postData);

        if (!result.Success)
            throw new InvalidOperationException(result.Reason);
        var defaultSigner = result.Pkcs7Info.Signers[0];
        CertificateModel defaultCertificate = defaultSigner.Certificate[0];

        if (defaultCertificate.ValidTo < DateTime.Now)
            throw new ExpiredException("This certificate is expired");
        
        string subjectName = defaultCertificate.SubjectName;

        Dictionary<string, string> subjectData = subjectName.Split(",")
            .Select(x => x.Split("=").Select(y => y.Trim()))
            .Where(x => x.Count() > 1)
            .ToDictionary(x => x.ElementAt(0), x => x.ElementAt(1));

        subjectData.Add("INN", subjectData["1.2.860.3.16.1.1"]);
        subjectData.Add("PINFL", subjectData["1.2.860.3.16.1.2"]);

        var esiId = defaultCertificate.SerialNumber;

        var signMethod = await _signMethodsRepository
            .OfType<ESISignMethod>()
            .FirstOrDefaultAsync(x => x.CertSerialNumber == esiId);

        if (signMethod is null)
            throw new NotFoundException("This Certificate not found");

        var user = signMethod.User;
        user.UserCerifiticate = 
            await _userCertificateRepository.FirstOrDefaultAsync(s => s.OwnerId == user.Id);
        
        var refreshToken = _jwtTokenHandler.GenerateRefreshToken();
        
        var token = new TokenModel()
        {
            UserId = user.Id,
            TokenType = TokenTypes.ESI,
            AccessToken = new JwtSecurityTokenHandler()
                .WriteToken(_jwtTokenHandler.GenerateAccessToken(user)),
            RefreshToken = refreshToken.refreshToken,
            ExpireRefreshToken = refreshToken.expireDate
        };

        token = await _tokenRepository.AddAsync(token);

        var tokenDto = new TokenDto(
            token.AccessToken,
            token.RefreshToken,
            token.ExpireRefreshToken);

        return tokenDto;
    }

    public async Task<TokenDto> RefreshTokenAsync(TokenDto tokenDto)
    {
        var token = await _tokenRepository
            .GetAllAsQueryable()
            .FirstOrDefaultAsync(t => t.AccessToken == tokenDto.accessToken
                && t.RefreshToken == tokenDto.refreshToken)
            ?? throw new NotFoundException("Not Found Token");

        if (token.ExpireRefreshToken < DateTime.UtcNow)
        {
            var deleteToken = await _tokenRepository.RemoveAsync(token);
            throw new AlreadyExistsException("Refresh token timed out");
        }

        token.User = await _userRepository.GetByIdAsync(token.UserId);

        token.AccessToken = new JwtSecurityTokenHandler()
            .WriteToken(_jwtTokenHandler.GenerateAccessToken(token.User));
        token.UpdatedAt = DateTime.UtcNow;

        token = await _tokenRepository.UpdateAsync(token);

        var newTokenDto = new TokenDto(
            token.AccessToken,
            token.RefreshToken,
            token.ExpireRefreshToken);

        return newTokenDto;
    }

    public async Task<bool> DeleteTokenAsync(TokenDto tokenDto)
    {
        var token = await _tokenRepository
                        .GetAllAsQueryable()
                        .FirstOrDefaultAsync(t => t.AccessToken == tokenDto.accessToken
                                                  && t.RefreshToken == tokenDto.refreshToken)
                    ?? throw new NotFoundException("Not Found Token");

        var deleteToken = await _tokenRepository.RemoveAsync(token);

        return deleteToken.Id == token.Id;
    }

    public async Task<TokenDto> SignByOneID(string code,string frontUrl)
    {
        var (signMethod, accessToken) = await GetOneIDTokenAsync(code,frontUrl);
        var userInfoOneId = await GetUserInfoByOneIdTokenAsync(accessToken,frontUrl);
        signMethod.PINFL = userInfoOneId.JSHSHIR;
        
        var signMethodBase = await _signMethodsRepository
            .OfType<OneIDSignMethod>()
            .FirstOrDefaultAsync(x => x.PINFL == signMethod.PINFL);

        if (signMethodBase is null)
        {
            if(userInfoOneId?.LegalInfos is null || userInfoOneId.LegalInfos.Length is 0)
                throw new AlreadyExistsException("User OneID is not legal");
            
            var newUser = new User()
            {
                FirstName = userInfoOneId.FirstName,
                LastName = userInfoOneId.LastName,
                MiddleName = userInfoOneId.MiddleName,
                SignMethods = new List<SignMethod>(),
                StructureId = (await _structureRepository.FirstOrDefaultAsync(x => x.IsDefault))?.Id,
            };

            var storedUser = await _userRepository.AddAsync(newUser);
        
            DateTime.TryParseExact(userInfoOneId.PassportExp,
                "yyyy-MM-dd",CultureInfo.InvariantCulture,
                DateTimeStyles.None,out var dateTo);
            DateTime.TryParseExact(userInfoOneId.PassportOld,
                "yyyy-MM-dd",CultureInfo.InvariantCulture,
                DateTimeStyles.None,out var dateFrom);

            var certificate = new UserCertificate()
            {
                SerialNumber = userInfoOneId.JSHSHIR,
                Country = userInfoOneId.BirthPlace,
                CN = userInfoOneId.FullName,
                Name = userInfoOneId.FirstName,
                Surname = userInfoOneId.LastName,
                Location = userInfoOneId.PassportIIB,
                Street = userInfoOneId.BirthPlace,
                O = userInfoOneId?.LegalInfos?.FirstOrDefault()?.LeName,
                UID = userInfoOneId.INN.ToString(),
                PINFL = userInfoOneId.JSHSHIR,
                TIN = userInfoOneId.INN.ToString(),
                OU = "",
                Type = "",
                BusinessCategory = userInfoOneId?.LegalInfos?.FirstOrDefault()?.Acron,
                ValidTo = dateTo,
                ValidFrom = dateFrom,
                OwnerId = storedUser.Id
            };

            certificate = await _userCertificateRepository.AddAsync(certificate);
        
            signMethod.UserId = storedUser.Id;
            await _signMethodsRepository.AddAsync(signMethod);
        }

        var user = signMethodBase.User;

        user.UserCerifiticate = 
            await _userCertificateRepository.FirstOrDefaultAsync(s => s.OwnerId == user.Id);
        
        var refreshToken = _jwtTokenHandler.GenerateRefreshToken();
        
        var token = new TokenModel()
        {
            UserId = user.Id,
            TokenType = TokenTypes.OneId,
            AccessToken = new JwtSecurityTokenHandler().WriteToken(_jwtTokenHandler.GenerateAccessToken(user)),
            RefreshToken = refreshToken.refreshToken,
            ExpireRefreshToken = refreshToken.expireDate
        };
        
        token = await _tokenRepository.AddAsync(token);

        var tokenDto = new TokenDto(
            token.AccessToken,
            token.RefreshToken,
            token.ExpireRefreshToken);

        return tokenDto;
    }

    public async Task<bool> RegisterByOneID(string code,string frontUrl)
    {
        var (signMethod, accessToken) = await GetOneIDTokenAsync(code,frontUrl);
        var userInfoOneId = await GetUserInfoByOneIdTokenAsync(accessToken,frontUrl);
        signMethod.PINFL = userInfoOneId.JSHSHIR;
        
        var hasStoredUser = await _signMethodsRepository.OfType<OneIDSignMethod>()
            .AnyAsync(x => x.PINFL == signMethod.PINFL);
        
        if (hasStoredUser)
            throw new AlreadyExistsException("User OneID already exists");
        
        if(userInfoOneId?.LegalInfos is null || userInfoOneId.LegalInfos.Length is 0)
            throw new AlreadyExistsException("User OneID is not legal");

        var newUser = new User()
        {
            FirstName = userInfoOneId.FirstName,
            LastName = userInfoOneId.LastName,
            MiddleName = userInfoOneId.MiddleName,
            SignMethods = new List<SignMethod>(),
            StructureId = (await _structureRepository.FirstOrDefaultAsync(x => x.IsDefault))?.Id,
        };

        var storedUser = await _userRepository.AddAsync(newUser);
        
        DateTime.TryParseExact(userInfoOneId.PassportExp,
            "yyyy-MM-dd",CultureInfo.InvariantCulture,
            DateTimeStyles.None,out var dateTo);
        DateTime.TryParseExact(userInfoOneId.PassportOld,
            "yyyy-MM-dd",CultureInfo.InvariantCulture,
            DateTimeStyles.None,out var dateFrom);

        var certificate = new UserCertificate()
        {
            SerialNumber = userInfoOneId.JSHSHIR,
            Country = userInfoOneId.BirthPlace,
            CN = userInfoOneId.FullName,
            Name = userInfoOneId.FirstName,
            Surname = userInfoOneId.LastName,
            Location = userInfoOneId.PassportIIB,
            Street = userInfoOneId.BirthPlace,
            O = userInfoOneId?.LegalInfos?.FirstOrDefault()?.LeName,
            UID = userInfoOneId.INN.ToString(),
            PINFL = userInfoOneId.JSHSHIR,
            TIN = userInfoOneId.INN.ToString(),
            OU = "",
            Type = "",
            BusinessCategory = userInfoOneId?.LegalInfos?.FirstOrDefault()?.Acron,
            ValidTo = dateTo,
            ValidFrom = dateFrom,
            OwnerId = storedUser.Id
        };

        certificate = await _userCertificateRepository.AddAsync(certificate);
        
        signMethod.UserId = storedUser.Id;
        await _signMethodsRepository.AddAsync(signMethod);
        return true;
    }

    public async Task<Uri?> GetOneIDUrlAsync(string frontUrl)
    {
        var values = new Dictionary<string, string>
        {
            { "response_type", "one_code" },
            { "client_id", _oneIdOptions.ClientId },
            { "Scope", _oneIdOptions.ClientSecret },
            { "redirect_uri", frontUrl },
            { "state", "OneId" }
        };
        
        var builder = new UriBuilder(ExternalAPI.OneID)
        {
            Port = -1,
            Query = string
                .Join("&",values.Select(
                    x => $"{HttpUtility.UrlEncode(x.Key)}={HttpUtility.UrlEncode(x.Value)}"))
        };

        var response = await _httpClient.GetAsync(builder.Uri);

        return response.RequestMessage.RequestUri;
    }

    private async Task<(OneIDSignMethod, string)> GetOneIDTokenAsync(string code,string frontUrl)
    {
        var values = new Dictionary<string, string>
        {
            { "grant_type", "one_authorization_code" },
            { "client_id", _oneIdOptions.ClientId },
            { "client_secret", _oneIdOptions.ClientSecret },
            { "redirect_uri", frontUrl },
            { "code", code }
        };
        
        var builder = new UriBuilder(ExternalAPI.OneID);
        builder.Query = string
            .Join("&", values.Select(
                x => $"{HttpUtility.UrlEncode(x.Key)}={HttpUtility.UrlEncode(x.Value)}"));

        var response = await _httpClient.PostAsync(builder.Uri,null);
        
        var responseContent = await response.Content.ReadAsStringAsync();

        var subjectData = JsonNode.Parse(responseContent);

        return new ValueTuple<OneIDSignMethod, string>
            (new OneIDSignMethod(){ Type = SignMethods.OneId },subjectData?["access_token"]?.ToString());
    }

    private async ValueTask<UserInfoOneId> GetUserInfoByOneIdTokenAsync(string accessToken,string frontUrl)
    {
        var values = new Dictionary<string, string>
        {
            { "grant_type", "one_access_token_identify" },
            { "client_id", _oneIdOptions.ClientId },
            { "client_secret", _oneIdOptions.ClientSecret },
            { "redirect_uri",  frontUrl},
            { "scope", _oneIdOptions.ClientSecret },
            { "access_token", accessToken }
        };
        
        var builder = new UriBuilder(ExternalAPI.OneID);
        builder.Query = string
            .Join("&", values.Select(
                x => $"{HttpUtility.UrlEncode(x.Key)}={HttpUtility.UrlEncode(x.Value)}"));

        var response = await _httpClient.PostAsync(builder.Uri,null);
        
        var responseContent = await response.Content.ReadAsStringAsync();
        try
        {
            var user = JsonSerializer.Deserialize<UserInfoOneId>(responseContent);

            return user;
        }
        catch (Exception e)
        {
            throw new AlreadyExistsException("User OneID is not legal");
        }
    }
}