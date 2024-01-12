using Entity.Models;

namespace DatabaseBroker.Context.Repositories.Structures;

public interface IStructureRepository : IRepositoryBase<Structure,long>
{
    public  Task<Structure> SelectByNameAsync(string name);
}
