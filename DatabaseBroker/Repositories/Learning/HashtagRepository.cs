using DatabaseBroker.Context;
using DatabaseBroker.Context.Repositories;
using Entity.Models.Learning;

namespace DatabaseBroker.Repositories.Learning;

public class HashtagRepository : RepositoryBase<Hashtag, int>, IHashtagRepository
{
    public HashtagRepository(PortalDataContext dbContext) : base(dbContext)
    {
    }
}
