namespace Domain.Entities.Interfaces;

public interface IBannable
{
    public Guid? BannedBy { get; set; }
    public DateTime? BannedAt { get; set; }
    BanReasons? BanReason { get; set; }
    public bool IsBanned => BannedBy.HasValue;
    public void Ban(Guid by, BanReasons reason);
    public void Unban();
}