using Entitys.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Models.Learning;

[Table("short_video", Schema = "learning")]
public class ShortVideo : ModelBase<int>
{
    [Column("video_linc")]
    public string VideoLinc { get; set; }

    [Column("title")]
    public MultiLanguageField Title { get; set; }

    [Column("author_id")]
    [ForeignKey(nameof(Author))]
    public int AuthorId { get; set; }
    public virtual Author? Author { get; set; }

    [Column("category_id")]
    [ForeignKey(nameof(Category))]
    public int CategoryId { get; set; }
    public virtual Category? Category { get; set; }

    [Column("hashtag_id")]
    public List<int> HashtagId { get; set; }
    [NotMapped] public List<Hashtag>? Hashtags { get; set; } = new List<Hashtag>();
}