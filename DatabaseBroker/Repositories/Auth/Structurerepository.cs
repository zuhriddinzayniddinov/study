using Entity.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseBroker.Context.Repositories.Structures;

public class Structurepository : RepositoryBase<Structure, long>, IStructureRepository
{
    public Structurepository(PortalDataContext dbContext) : base(dbContext)
    {
    }
    public async Task<Structure> SelectByNameAsync(string name) =>
        await this._dbContext.Set<Structure>().FindAsync(name);
}