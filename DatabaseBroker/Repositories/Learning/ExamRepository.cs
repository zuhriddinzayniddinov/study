using DatabaseBroker.Context;
using DatabaseBroker.Context.Repositories;
using Entity.Models.Learning;
using Microsoft.EntityFrameworkCore;

namespace DatabaseBroker.Repositories.Learning;

public class ExamRepository : RepositoryBase<Exam,long>, IExamRepository
{
    public ExamRepository(PortalDataContext dbContext) : base(dbContext)
    {
    }
}