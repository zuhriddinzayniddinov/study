using Entitys.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Models.Learning;

[Table("course", Schema = "learning")]
public class Course : AuditableModelBase<int>
{
    [Column("title")]
    public string Title { get; set; }

    [Column("description")]
    public string Description { get; set; }

    [Column("image")]
    public string Image { get; set; }

    [Column("language_code")]
    public string LanguageCode { get; set; } = "uz";

    [Column("author_id")]
    [ForeignKey(nameof(Author))]
    public int AuthorId { get; set; }

    public virtual Author? Author { get; set; }

    [Column("category_id")]
    [ForeignKey(nameof(Category))]
    public int CategoryId { get; set; }
    public virtual Category? Category { get; set; }

    [Column("hashtag_id")]
    public List<int>? HashtagId { get; set; }
    [NotMapped] public List<Hashtag>? Hashtags { get; set; } = new List<Hashtag>();

    public virtual ICollection<Module> Modules { get; set; } = new List<Module>();
}