using DatabaseBroker.DataContext;
using Entity.Models.StaticFiles;

namespace DatabaseBroker.Context.Repositories.StaticFiles;

public class StaticFileRepository : RepositoryBase<StaticFile, long> , IStaticFileRepository
{
    public StaticFileRepository(PortalDataContext dbContext) : base(dbContext)
    {
    }
}