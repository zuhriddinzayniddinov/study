using DatabaseBroker.Context;
using DatabaseBroker.Context.Repositories;
using Entity.Models.Learning;

namespace DatabaseBroker.Repositories.Learning;

public class ArticleRepository : RepositoryBase<Article, int>, IArticleRepository
{
    public ArticleRepository(PortalDataContext dbContext) : base(dbContext)
    {
    }
}