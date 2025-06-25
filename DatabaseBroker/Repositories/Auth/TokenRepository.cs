using DatabaseBroker.Context;
using DatabaseBroker.Context.Repositories;
using DatabaseBroker.DataContext;
using Entity.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseBroker.Repositories.Token;

public class TokenRepository : RepositoryBase<TokenModel, long>, ITokenRepository
{
    public TokenRepository(PortalDataContext dbContext) : base(dbContext)
    {
    }
}