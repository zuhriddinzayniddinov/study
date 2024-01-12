using Entitys.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Models.Learning;

[Table("course", Schema = "learning")]
public class Course : AuditableModelBase<int>
{
    [Column("title")]
    public MultiLanguageField Title { get; set; }

    [Column("description")]
    public MultiLanguageField Description { get; set; }

    [Column("image")]
    public string Image { get; set; }

    [Column("order_number")]
    public int OrderNumber { get; set; }

    [Column("author_id")]
    [ForeignKey(nameof(Author))]
    public int AuthorId { get; set; }

    public virtual Author? Author { get; set; }

    [Column("category_id")]
    [ForeignKey(nameof(Category))]
    public int CategoryId { get; set; }
    public virtual Category? Category { get; set; }
    public virtual Quiz? Quiz { get; set; }

    [Column("docs_url")]
    public List<string> DocsUrl { get; set; }
    [Column("hashtag_id")]
    public List<int>? HashtagId { get; set; }
    [NotMapped] public List<Hashtag>? Hashtags { get; set; } = new List<Hashtag>();
}