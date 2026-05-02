namespace Domain.Entities;

public interface IBaseEntity<TId>
{
    TId Id { get; set; }
    DateTime CreatedAt { get; set; }
}