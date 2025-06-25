using DatabaseBroker.Context;
using DatabaseBroker.Context.Repositories;
using DatabaseBroker.DataContext;
using Entity.Models.Learning;
using Microsoft.EntityFrameworkCore;

namespace DatabaseBroker.Repositories.Learning;

public class QuestionRepository : RepositoryBase<Question,long>, IQuestionRepository
{
    public QuestionRepository(PortalDataContext dbContext) : base(dbContext)
    {
    }
}