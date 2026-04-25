using Domain.Entities.Identity;

namespace Application.Interfaces
{
    public interface IFollowRepository
    {
        public Task<bool> IsFollowingAsync(Guid who, Guid whom);
        public Task<UserFollowEntity?> GetFollowAsync(Guid who, Guid whom);

        public Task<int> GetFollowersCountAsync(Guid userId);

        public Task<int> GetFollowingCountAsync(Guid userId);
    }
}
