using Entity.Models.Learning;

namespace Entity.DataTransferObjects.Learning;

public class GetByHashtagIdDto
{
    public int id { get; set; }
    public string name { get; set; }
    public List<ShortVideo> shortVideo { get; set; }
    public List<SeminarVideo> seminarVideo { get; set; }
    public List<Article> article { get; set; }
}
