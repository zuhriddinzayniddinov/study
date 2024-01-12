using DatabaseBroker.Context;
using DatabaseBroker.Context.Repositories;
using Entity.Models.Learning;
using Microsoft.EntityFrameworkCore;

namespace DatabaseBroker.Repositories.Learning;

public class QuizRepository : RepositoryBase<Quiz,long>, IQuizRepository
{
    public QuizRepository(PortalDataContext dbContext) : base(dbContext)
    {
    }
}