using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Entitys.Models;

namespace Entity.Models.Learning;
[Table("quizzes",Schema = "learning")]
public class Quiz : AuditableModelBase<long>
{
    public virtual CourseItem CourseItem { get; set; }

    [Column("total_score")]
    public decimal TotalScore { get; set; }
    [Column("passing_score")]
    public decimal? PassingScore { get; set; }
    [Column("duration")]
    public TimeSpan Duration { get; set; }
    [Column("heart")]
    public int Heart { get; set; } = 3;
}