using Entity.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseBroker.Context.Repositories;

public class StructurePermissionRepository : RepositoryBase<StructurePermission, long>, IStructurePermissionRepository
{
    public StructurePermissionRepository(PortalDataContext dbContext) : base(dbContext)
    {
    }
}