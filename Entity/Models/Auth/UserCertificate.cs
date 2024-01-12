using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Models;

[Table("user_certificates", Schema = "auth")]
public class UserCertificate : AuditableModelBase<long>
{
    [Column("cn")] public string CN { get; set; }
    [Column("name")] public string Name { get; set; }
    [Column("surname")] public string Surname { get; set; }
    [Column("location")] public string Location { get; set; }
    [Column("street")] public string Street { get; set; }
    [Column("country")] public string Country { get; set; }
    [Column("o")] public string O { get; set; }
    [Column("uid")] public string UID { get; set; }
    [Column("pinfl")] public string PINFL { get; set; }
    [Column("tin")] public string TIN { get; set; }
    [Column("ou")] public string OU { get; set; }
    [Column("type")] public string Type { get; set; }
    [Column("business_category")] public string BusinessCategory { get; set; }
    [Column("serial_number")] public string SerialNumber { get; set; }
    [Column("valid_from")] public DateTime ValidFrom { get; set; }
    [Column("valid_to")] public DateTime ValidTo { get; set; }

    [Column("owner_id"), ForeignKey(nameof(Owner))]
    public long OwnerId { get; set; }

    [NotMapped] public virtual User Owner { get; set; }
}