namespace Domain.Entities;

public class BaseEntity<TId> : IBaseEntity<TId>
{
    public virtual TId Id { get; set; } = default!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? DeletedAt { get; set; }
    public bool IsDeleted => DeletedAt.HasValue;

    public Guid? DeletedBy { get; set; }
}