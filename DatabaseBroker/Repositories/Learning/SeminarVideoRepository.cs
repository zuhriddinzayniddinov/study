using DatabaseBroker.Context;
using DatabaseBroker.Context.Repositories;
using DatabaseBroker.DataContext;
using Entity.Models.Learning;
using Microsoft.EntityFrameworkCore;

namespace DatabaseBroker.Repositories.Learning;

public class SeminarVideoRepository : RepositoryBase<SeminarVideo, int>, ISeminarVideoRepository
{
    public SeminarVideoRepository(PortalDataContext dbContext) 
        : base(dbContext)
    {

    }
}