using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Entity.Models.Learning;
[Table("author_to_category", Schema = "learning")]
public class AuthorToCategory : AuditableModelBase<long>
{
    [Column("author_id"),ForeignKey(nameof(Author))]
    public int AuthorId { get; set; }
    [NotMapped,JsonIgnore]
    public virtual Author Author { get; set; }
    [Column("category_id"),ForeignKey(nameof(Category))]
    public int CategoryId { get; set; }
    public virtual Category Category { get; set; }
}