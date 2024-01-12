using DatabaseBroker.Context;
using DatabaseBroker.Context.Repositories;
using Entity.Models.Learning;

namespace DatabaseBroker.Repositories.Learning;

public class VideoOfCourseRepository : RepositoryBase<VideoOfCourse, int>, IVideoOfCourseRepository
{
    public VideoOfCourseRepository(PortalDataContext dbContext) : base(dbContext)
    {
    }
}
