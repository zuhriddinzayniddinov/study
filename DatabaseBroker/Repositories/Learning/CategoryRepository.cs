using DatabaseBroker.Context.Repositories;
using DatabaseBroker.DataContext;
using Entity.Models.Learning;

namespace DatabaseBroker.Repositories.Learning;


public class CategoryRepository : RepositoryBase<Category, int>, ICategoryRepository
{
    public CategoryRepository(PortalDataContext dbContext) 
        : base(dbContext)
    {
    }
}