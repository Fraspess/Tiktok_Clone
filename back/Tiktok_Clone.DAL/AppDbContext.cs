using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tiktok_Clone.DAL.Entities.Comment;
using Tiktok_Clone.DAL.Entities.HashTags;
using Tiktok_Clone.DAL.Entities.Identity;
using Tiktok_Clone.DAL.Entities.Like;
using Tiktok_Clone.DAL.Entities.Message;
using Tiktok_Clone.DAL.Entities.Report;
using Tiktok_Clone.DAL.Entities.Video;

namespace Tiktok_Clone.DAL;

public class AppDbContext : IdentityDbContext<
    UserEntity,
    RoleEntity,
    Guid,
    IdentityUserClaim<Guid>,
    UserRoleEntity,
    IdentityUserLogin<Guid>,
    IdentityRoleClaim<Guid>,
    IdentityUserToken<Guid>
    >
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<VideoEntity> Videos { get; set; }
    public DbSet<CommentEntity> Comments { get; set; }
    public DbSet<MessageEntity> Messages { get; set; }
    public DbSet<ReportEntity> Reports { get; set; }
    public DbSet<UserFollowEntity> UserFollows { get; set; }
    public DbSet<VideoHashTagEntity> VideoHashTags { get; set; }
    public DbSet<HashTagEntity> HashTags { get; set; }

    public DbSet<LikeEntity> Likes { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // ── User ────────────────────────────────────────────
        builder.Entity<UserEntity>()
            .HasIndex(u => u.UserName).IsUnique();

        builder.Entity<UserEntity>()
            .HasIndex(u => u.Email).IsUnique();

        // ── User Follows (self-referencing many-to-many) ────
        builder.Entity<UserFollowEntity>()
            .HasKey(uf => new { uf.FollowerId, uf.FollowingId });

        builder.Entity<UserFollowEntity>()
            .HasOne(uf => uf.Follower)
            .WithMany(u => u.Following)
            .HasForeignKey(uf => uf.FollowerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<UserFollowEntity>()
            .HasOne(uf => uf.Following)
            .WithMany(u => u.Followers)
            .HasForeignKey(uf => uf.FollowingId)
            .OnDelete(DeleteBehavior.Restrict);

        // ── Video ───────────────────────────────────────────
        builder.Entity<VideoEntity>()
            .HasOne(v => v.Author)
            .WithMany(u => u.Videos)
            .HasForeignKey(v => v.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // ── Comments ────────────────────────────────────────
        builder.Entity<CommentEntity>()
            .HasOne(c => c.Author)
            .WithMany(u => u.Comments)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<CommentEntity>()
            .HasOne(c => c.ParentComment)
            .WithMany(c => c.Replies)
            .HasForeignKey(c => c.ParentCommentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<CommentEntity>()
            .HasOne(c => c.Video)
            .WithMany(v => v.Comments)
            .HasForeignKey(c => c.VideoId)
            .OnDelete(DeleteBehavior.Cascade);

        // ── Messages ────────────────────────────────────────
        builder.Entity<MessageEntity>()
            .HasOne(m => m.Sender)
            .WithMany(u => u.SentMessages)
            .HasForeignKey(m => m.SenderId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<MessageEntity>()
            .HasOne(m => m.Receiver)
            .WithMany(u => u.ReceivedMessages)
            .HasForeignKey(m => m.ReceiverId)
            .OnDelete(DeleteBehavior.Restrict);

        // ── Reports ─────────────────────────────────────────
        builder.Entity<ReportEntity>()
            .HasOne(r => r.Sender)
            .WithMany()
            .HasForeignKey(r => r.SenderId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<ReportEntity>()
            .HasOne(r => r.Video)
            .WithMany()
            .HasForeignKey(r => r.VideoId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<ReportEntity>()
            .HasOne(r => r.ReportedUser)
            .WithMany()
            .HasForeignKey(r => r.ReportedUserId)
            .OnDelete(DeleteBehavior.Restrict);

        // ── HashTags (many-to-many) ──────────────────────────
        builder.Entity<VideoHashTagEntity>()
            .HasKey(vh => new { vh.VideoId, vh.HashTagId });

        builder.Entity<VideoHashTagEntity>()
            .HasOne(vh => vh.Video)
            .WithMany(v => v.HashTags)
            .HasForeignKey(vh => vh.VideoId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<VideoHashTagEntity>()
            .HasOne(vh => vh.HashTag)
            .WithMany(h => h.VideoHashTags)
            .HasForeignKey(vh => vh.HashTagId)
            .OnDelete(DeleteBehavior.Cascade);


        // ── Likes (many-to-many) ─────────────────────────────
        builder.Entity<LikeEntity>()
            .HasOne(l => l.User)
            .WithMany(u => u.Likes)
            .HasForeignKey(l => l.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<LikeEntity>()
            .HasOne(l => l.Video)
            .WithMany(v => v.Likes)
            .HasForeignKey(l => l.VideoId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<LikeEntity>()
            .HasIndex(l => new { l.UserId, l.VideoId })
            .IsUnique();


        // user to roles
        builder.Entity<UserRoleEntity>(ur =>
        {
            ur.HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(r => r.RoleId)
                .IsRequired();
            ur.HasOne(ur => ur.User)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(u => u.UserId)
                .IsRequired();
        });
    }
}