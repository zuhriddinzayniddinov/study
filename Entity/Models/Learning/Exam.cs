using System.ComponentModel.DataAnnotations.Schema;
using Entity.Enum;
using Entity.Models.Common;
using Microsoft.EntityFrameworkCore;

namespace Entity.Models.Learning;
[Table("exams",Schema = "learning")]
public class Exam : AuditableModelBase<long>
{
    [Column("close_at")]
    public DateTime? ClosedAt { get; set; }
    [Column("used_heart")]
    public int UsedHeart { get; set; } = 1;
    [Column("quiz_id"),ForeignKey(nameof(Quiz))]
    public long QuizId { get; set; }
    public virtual Quiz Quiz { get; set; }
    [Column("status")]
    public ExamStatus Status { get; set; } = ExamStatus.Progress;
    [Column("user_id"),ForeignKey(nameof(User))]
    public long UserId { get; set; }
    public virtual User User { get; set; }
    public virtual List<QuestionInExam> Questions { get; set; }
}