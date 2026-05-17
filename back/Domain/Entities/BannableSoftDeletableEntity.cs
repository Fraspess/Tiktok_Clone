using Domain.Entities.Interfaces;

namespace Domain.Entities;

public class BannableSoftDeletableEntity : SoftDeletableEntity, IBannable
{
    public Guid? BannedBy { get; set; }
    public DateTime? BannedAt { get; set; }
    public BanReasons? BanReason { get; set; }
    
    public void Ban(Guid by, BanReasons reason)
    {
        throw new NotImplementedException();
    }

    public void Unban()
    {
        BannedBy = null;
        BannedAt = null;
        BanReason = null;
    }
}