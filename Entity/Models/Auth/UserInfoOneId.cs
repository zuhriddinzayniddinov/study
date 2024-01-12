using System.Text.Json.Serialization;

namespace Entity.Models;

public class UserInfoOneId
{
    [JsonPropertyName("legal_info")]
    public LegalInfo[] LegalInfos { get; set; }
    [JsonPropertyName("first_name")]
    public string FirstName { get; set; }
    [JsonPropertyName("birth_date")]
    public string BirthDate { get; set; }
    [JsonPropertyName("birth_place")]
    public string BirthPlace { get; set; }
    [JsonPropertyName("ctzn")]
    public string NationalityCity { get; set; }
    [JsonPropertyName("email")]
    public string Email { get; set; }
    [JsonPropertyName("full_name")]
    public string FullName { get; set; }
    [JsonPropertyName("gd")]
    public string Gender { get; set; }
    [JsonPropertyName("mid_name")]
    public string LastName { get; set; }
    [JsonPropertyName("mob_phone_no")]
    public string PhoneNumber { get; set; }
    [JsonPropertyName("natn")]
    public string Nationality { get; set; }
    [JsonPropertyName("per_adr")]
    public string Propiska { get; set; }
    [JsonPropertyName("pin")]
    public string JSHSHIR { get; set; }
    [JsonPropertyName("pport_expr_date")]
    public string PassportExp { get; set; }
    [JsonPropertyName("pport_issue_date")]
    public string PassportOld { get; set; }
    [JsonPropertyName("pport_issue_place")]
    public string PassportIIB { get; set; }
    [JsonPropertyName("pport_no")]
    public string PassportSeriaNumber { get; set; }
    [JsonPropertyName("sur_name")]
    public string MiddleName { get; set; }
    [JsonPropertyName("pkcs_legal_tin")]
    public long INN { get; set; }
    [JsonPropertyName("user_type")]
    public string Type { get; set; }
    [JsonPropertyName("valid")]
    public string Valid { get; set; }
    [JsonPropertyName("sess_id")]
    public string SessID { get; set; }
}
public class LegalInfo
{
    [JsonPropertyName("le_tin")]
    public string LeTIN { get; set; }
    [JsonPropertyName("tin")]
    public string TIN { get; set; }
    [JsonPropertyName("le_name")]
    public string LeName { get; set; }
    [JsonPropertyName("acron_UZ")]
    public string Acron { get; set; }
    [JsonPropertyName("is_basic")]
    public bool IsBasic { get; set; }
}