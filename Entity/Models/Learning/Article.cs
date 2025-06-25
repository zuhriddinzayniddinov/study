using Entitys.Models;
using System.ComponentModel.DataAnnotations.Schema;
using Entity.Models.Common;

namespace Entity.Models.Learning;

[Table("article", Schema = "learning")]
public class Article : AuditableModelBase<int>
{
    [Column("title")]
    public string Title { get; set; }

    [Column("description")]
    public string Description { get; set; }

    [Column("content")]
    public string Content { get; set; }

    [Column("image")]
    public string  Image { get; set; }

    [Column("author_id")]
    [ForeignKey(nameof(Author))]
    public int AuthorId { get; set; }
    public virtual Author? Author { get; set; }

    [Column("category_id")]
    [ForeignKey(nameof(Category))]
    public int CategoryId { get; set; }
    public virtual Category? Category { get; set; }
}