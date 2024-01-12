using Entity.Models.Learning;
using Entitys.Models;

namespace Entity.DataTransferObjects.Learning;

public class ShortVideoForWithDetailsDto
{
    public int id { get; set; }
    public string videoLinc { get; set; }
    public MultiLanguageField title { get; set; }
    public Author author { get; set; }
    public Category category { get; set; }
    public List<int> hashtagId { get; set; }
    public List<Hashtag> hashtags { get; set; }
}
