using Entity.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseBroker.Context.Repositories.Auth;

public class SignMethodsRepository : RepositoryBase<SignMethod, long>, ISignMethodsRepository
{
    public SignMethodsRepository(PortalDataContext dbContext) : base(dbContext)
    {
    }
}