using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Models.Learning;

[Table("hashtag", Schema = "learning")]
public class Hashtag : ModelBase<int>
{
    [Column("name")]
    public string Name { get; set; }
}
