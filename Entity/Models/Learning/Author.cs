﻿using Entitys.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Models.Learning;

[Table("author", Schema = "learning")]
public class Author : ModelBase<int>
{
    [Column("name")]
    public MultiLanguageField Name { get; set; }
    [Column("content")]
    public MultiLanguageField? Content { get; set; }
    [Column("image_linc")] 
    public string ImageLinc { get; set; }

    [Column("user_id")]
    [ForeignKey(nameof(User))]
    public long UserId { get; set; }
    public User User { get; set; }
    [NotMapped]public virtual ICollection<Course> Courses { get; set; }
    [NotMapped]public virtual ICollection<Article> Articles { get; set; }
    [NotMapped]public virtual ICollection<ShortVideo> ShortVideos { get; set; }
    [NotMapped]public virtual ICollection<SeminarVideo> SeminarVideos { get; set; }
}
