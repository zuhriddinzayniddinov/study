using DatabaseBroker.Context;
using DatabaseBroker.Context.Repositories;
using Entity.Models.Learning;
using Microsoft.EntityFrameworkCore;

namespace DatabaseBroker.Repositories.Learning;

public class QuestionInExamRepository : RepositoryBase<QuestionInExam,long>, IQuestionInExamRepository
{
    public QuestionInExamRepository(PortalDataContext dbContext) : base(dbContext)
    {
    }
}