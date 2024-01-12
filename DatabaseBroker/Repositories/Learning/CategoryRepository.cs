using DatabaseBroker.Context;
using DatabaseBroker.Context.Repositories;
using Entity.Models.Learning;
using Microsoft.EntityFrameworkCore;

namespace DatabaseBroker.Repositories.Learning;


public class CategoryRepository : RepositoryBase<Category, int>, ICategoryRepository
{
    public CategoryRepository(PortalDataContext dbContext) 
        : base(dbContext)
    {
    }
}