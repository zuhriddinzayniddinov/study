using DatabaseBroker.Context;
using DatabaseBroker.Context.Repositories;
using DatabaseBroker.DataContext;
using Entity.Models.Learning;

namespace DatabaseBroker.Repositories.Learning;

public class CourseRepository : RepositoryBase<Course, int>, ICourseRepository
{
    public CourseRepository(PortalDataContext dbContext) : base(dbContext)
    {
    }
}
