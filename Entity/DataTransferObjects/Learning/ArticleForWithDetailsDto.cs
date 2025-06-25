using Entity.Models.Learning;
using Entitys.Models;

namespace Entity.DataTransferObjects.Learning;

public class ArticleForWithDetailsDto
{
    public int id { get; set; }
    public MultiLanguageField title { get; set; }
    public MultiLanguageField content { get; set; }
    public string image { get; set; }
    public Author author { get; set; }
    public Category category { get; set; }
    public List<int> hashtagIds { get; set; }
}
