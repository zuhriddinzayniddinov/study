using DatabaseBroker.Context;
using DatabaseBroker.Context.Repositories;
using Entity.Models.Learning;
using Microsoft.EntityFrameworkCore;

namespace DatabaseBroker.Repositories.Learning;

public class AuthorToCategoryRepository : RepositoryBase<AuthorToCategory,long>, IAuthorToCategoryRepository
{
    public AuthorToCategoryRepository(PortalDataContext dbContext) : base(dbContext)
    {
    }
}