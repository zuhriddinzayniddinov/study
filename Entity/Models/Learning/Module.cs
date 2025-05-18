using Entitys.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Models.Learning;

[Table("modules", Schema = "learning")]
public class Module : AuditableModelBase<int>
{
    [Column("title")]
    public string Title { get; set; }

    [Column("description")]
    public string Description { get; set; }

    [Column("order_number")]
    public int OrderNumber { get; set; }


    [Column("lesson_count")]
    public int LessonCount { get; set; }

    [Column("duration")]
    public TimeSpan Duration { get; set; }

    [Column("course_id")]
    [ForeignKey(nameof(Course))]
    public int CourseId { get; set; }

    #region Navigation Properties
    public virtual Course? Course { get; set; }

    #endregion
}