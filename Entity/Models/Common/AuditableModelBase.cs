using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Models.Common;

public abstract class AuditableModelBase<TId> : ModelBase<TId>
{
    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    [Column("created_by"), ForeignKey(nameof(CreatedByUser))]
    public int? CreatedBy { get; set; }
    public virtual User CreatedByUser { get; set; }
    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    [Column("updated_by"), ForeignKey(nameof(UpdatedByUser))]
    public int UpdatedBy { get; set; }
    public virtual User UpdatedByUser { get; set; }
    
}