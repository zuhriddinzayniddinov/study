using Entitys.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Models.Learning;

[Table("categories", Schema = "learning")]
public class Category : ModelBase<int>
{
    [Column("title")]
    public MultiLanguageField Title { get; set; }

    [Column("description")] 
    public MultiLanguageField Description { get; set; }

    [Column("image_link")]
    public string ImageLink { get; set; }
}