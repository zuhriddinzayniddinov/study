using DatabaseBroker.Context.Repositories;
using Entity.Models;

namespace DatabaseBroker.Repositories.Auth;

public interface IUserCertificateRepository : IRepositoryBase<UserCertificate, long>
{
}