using DatabaseBroker.Context;
using DatabaseBroker.Context.Repositories;
using DatabaseBroker.DataContext;
using Entity.Models.Learning;
using Microsoft.EntityFrameworkCore;

namespace DatabaseBroker.Repositories.Learning;

public class AuthorRepository : RepositoryBase<Author, int>, IAuthorRepository
{
    public AuthorRepository(PortalDataContext dbContext) : base(dbContext)
    {
    }
}
