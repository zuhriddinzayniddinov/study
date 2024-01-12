using Entitys.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Models.Learning;

[Table("video_of_course", Schema = "learning")]
public class VideoOfCourse : AuditableModelBase<int>
{
    [Column("video_linc")]
    public string VideoLinc { get; set; }

    [Column("title")]
    public MultiLanguageField Title { get; set; }
    [Column("content")]
    public MultiLanguageField? Content { get; set; }

    [Column("order_number")]
    public int? OrderNumber { get; set; }

    [Column("course_id")]
    [ForeignKey(nameof(Course))]
    public int CourseId { get; set; }

    public virtual Course? Course { get; set; }

    [Column("docs_url")]
    public List<string> DocsUrl { get; set; }
}