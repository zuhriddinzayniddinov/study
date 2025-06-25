using System.ComponentModel.DataAnnotations.Schema;
using Entity.Enum;
using Entity.Models.Common;

namespace Entity.Models.Learning;

[Table("course_items", Schema = "learning")]
public class CourseItem : AuditableModelBase<int>
{
    [Column("title")]
    public string Title { get; set; }

    [Column("description")]
    public string Description { get; set; }

    [Column("image")]
    public string Image { get; set; }

    [Column("order_number")]
    public int OrderNumber { get; set; }

    [Column("type")]
    public CourseItemType Type { get; set; }

    [Column("docs_url")]
    public List<string> DocsUrl { get; set; }

    [Column("module_id")]
    [ForeignKey(nameof(Module))]
    public int ModuleId { get; set; }

    public virtual Module Module { get; set; }

    [Column("quiz_id")]
    [ForeignKey(nameof(Quiz))]
    public int? QuizId { get; set; }

    public virtual Quiz? Quiz { get; set; }

    [Column("video_of_course_id")]
    [ForeignKey(nameof(VideoOfCourse))]
    public int? VideoOfCourseId { get; set; }
    public virtual VideoOfCourse? VideoOfCourse { get; set; }

    [Column("text_content_id")]
    [ForeignKey(nameof(TextContent))]
    public int? TextContentId { get; set; }

    public virtual TextContent? TextContent { get; set; }
}