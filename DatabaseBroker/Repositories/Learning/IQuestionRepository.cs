using DatabaseBroker.Context.Repositories;
using Entity.Models.Learning;

namespace DatabaseBroker.Repositories.Learning;

public interface IQuestionRepository : IRepositoryBase<Question,long>
{
    
}