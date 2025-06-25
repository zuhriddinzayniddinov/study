using DatabaseBroker.Context;
using DatabaseBroker.Context.Repositories;
using DatabaseBroker.DataContext;
using Entity.Models.Learning;
using Microsoft.EntityFrameworkCore;

namespace DatabaseBroker.Repositories.Learning;

public class ShortVideoRepository : RepositoryBase<ShortVideo, int>, IShortVideoRepository
{
    public ShortVideoRepository(PortalDataContext dbContext) : base(dbContext)
    {
    }
}
