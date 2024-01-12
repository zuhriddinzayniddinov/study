using Entity.Models;

namespace DatabaseBroker.Context.Repositories.Users;

public interface IUserRepository : IRepositoryBase<User,long>
{
    Task<User> GetByUsernameWithDetails(string username);
    Task<User> GetByUIdWithDetails(string uid);
}
