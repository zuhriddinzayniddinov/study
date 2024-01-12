using DatabaseBroker.Context;
using DatabaseBroker.Context.Repositories;
using Entity.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseBroker.Repositories.Auth;

public class UserCertificateRepository : RepositoryBase<UserCertificate, long>, IUserCertificateRepository
{
    public UserCertificateRepository(PortalDataContext dbContext) : base(dbContext)
    {
    }
}