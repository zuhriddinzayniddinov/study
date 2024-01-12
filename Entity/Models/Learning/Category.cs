using Entitys.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Models.Learning;

[Table("category", Schema = "learning")]
public class Category : ModelBase<int>
{
    [Column("title")]
    public MultiLanguageField Title { get; set; }

    [Column("description")] 
    public MultiLanguageField Description { get; set; }

    [Column("image_linc")] 
    public string ImageLinc { get; set; }
}