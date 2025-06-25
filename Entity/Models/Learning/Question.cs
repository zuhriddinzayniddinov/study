using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entity.Enum;
using Entity.Models.Common;
using Entitys.Models;

namespace Entity.Models.Learning;
[Table("questions",Schema = "learning")]
public abstract class Question : AuditableModelBase<long>
{
    [Column("quiz_id")]
    [ForeignKey(nameof(Quiz))]
    public long QuizId { get; set; }
    public virtual Quiz Quiz { get; set; }
    [Column("order_number")]
    public int OrderNumber { get; set; }
    [Column("question_type")]
    public QuestionTypes QuestionType { get; set; } = QuestionTypes.Simple;
    [Column("question_content")]
    public MultiLanguageField? QuestionContent { get; set; }
    [Column("image_link")]
    public string? ImageLink { get; set; }
    [Column("doc_link")]
    public string? DocLink { get; set; }
    [Column("total_ball")]
    public decimal? TotalBall { get; set; }
}

public class SimpleQuestion : Question
{
    [Column("options"), DataType("jsonb")]
    public List<SimpleQuestionOption> Options { get; set; }
}

public class WrittenQuestion : Question
{
    [Column("possible_answer"), DataType("jsonb")]
    public string PossibleAnswer { get; set; }
}