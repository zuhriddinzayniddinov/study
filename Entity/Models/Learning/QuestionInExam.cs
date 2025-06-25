using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entity.Enum;
using Entity.Models.Common;

namespace Entity.Models.Learning;
[Table("question_in_exams",Schema = "learning")]
public abstract class QuestionInExam : AuditableModelBase<long>
{
    [Column("exam_id"),ForeignKey(nameof(Exam))]
    public long ExamId { get; set; }
    [NotMapped]public virtual Exam Exam { get; set; }
    [Column("question_id"),ForeignKey(nameof(Question))]
    public long QuestionId { get; set; }
    public virtual Question Question { get; set; }
    [Column("question_type")]
    public QuestionTypes QuestionType { get; set; } = QuestionTypes.Simple;
}
public class SimpleQuestionInExam : QuestionInExam
{
    [Column("selected")]
    public Guid? Selected { get; set; }
    [Column("options"), DataType("jsonb")]
    public List<SimpleQuestionOption> Options { get; set; }
}

public class WrittenQuestionInExam : QuestionInExam
{
    [Column("written_answer"), DataType("jsonb")]
    public string? WrittenAnswer { get; set; }
    [Column("checked"), DataType("jsonb")]
    public bool Checked { get; set; } = false;
    [Column("accumulated_ball"), DataType("jsonb")]
    public decimal AccumulatedBall { get; set; } = 0;
}