using DatabaseBroker.DataContext;
using Entity.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseBroker.Context.Repositories;

public class PermissionRepository : RepositoryBase<Permission, long>,IPermissionRepository
{
    public PermissionRepository(PortalDataContext dbContext) : base(dbContext)
    {
    }
}
