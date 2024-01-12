using DatabaseBroker.Context.Repositories;
using Entity.Models;

namespace DatabaseBroker.Repositories.Token;

public interface ITokenRepository : IRepositoryBase<TokenModel,long>
{
    
}