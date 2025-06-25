using DatabaseBroker.DataContext;
using Entity.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseBroker.Context.Repositories.Users;

public class UserRepository : RepositoryBase<User, long>, IUserRepository
{
    public UserRepository(PortalDataContext dbContext) : base(dbContext)
    {
    }

    public async Task<User> GetByUsernameWithDetails(string username)
    {
        // return await this._dbContext.Set<User>()
        //            .AsNoTracking()
        //            .Include(u => u.Structure)
        //            .Include(u => u.Structure.StructurePermissions)
        //            .ThenInclude(sp => sp.Permission)
        //            .FirstOrDefaultAsync(x => x.UserName!.Equals(username))
        //        ?? new User();

        return null;
    }

    public async Task<User> GetByUIdWithDetails(string uid)
    {
        // return await this._dbContext.Set<User>()
        //            .AsNoTracking()
        //            .Include(u => u.Structure)
        //            .Include(u => u.Structure.StructurePermissions)
        //            .FirstOrDefaultAsync(x => x.UId!.Equals(uid))
        //        ?? new User();
        return null;
    }
}