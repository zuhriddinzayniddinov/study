using Entitys.Models;
using System.ComponentModel.DataAnnotations.Schema;
using Entity.Models.Common;

namespace Entity.Models.Learning;

[Table("video_of_courses", Schema = "learning")]
public class VideoOfCourse : AuditableModelBase<int>
{
    public virtual CourseItem CourseItem { get; set; }

    [Column("link")]
    public string Link { get; set; }
}