using Domain.Entities.Comment;
using Domain.Entities.Conversation;
using Domain.Entities.Favorite;
using Domain.Entities.HashTags;
using Domain.Entities.Identity;
using Domain.Entities.Like;
using Domain.Entities.Message;
using Domain.Entities.Report;
using Domain.Entities.Video;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

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
    public DbSet<ConversationEntity> Conversations { get; set; }

    public DbSet<FavoriteEntity> Favorites { get; set; }

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
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<CommentEntity>()
            .HasOne(c => c.ParentComment)
            .WithMany(c => c.Replies)
            .HasForeignKey(c => c.ParentCommentId)
            .OnDelete(DeleteBehavior.Cascade);

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

        // favorites many-to-many
        builder.Entity<FavoriteEntity>()
            .HasOne(f => f.Video)
            .WithMany(v => v.Favorites)
            .HasForeignKey(f => f.VideoId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<FavoriteEntity>()
            .HasOne(f => f.User)
            .WithMany(u => u.Favorites)
            .HasForeignKey(f => f.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<FavoriteEntity>()
            .HasIndex(f => new { f.UserId, f.VideoId })
            .IsUnique();


        builder.Entity<ConversationEntity>()
            .HasMany(c => c.Participants)
            .WithOne(p => p.Conversation)
            .HasForeignKey(p => p.ConversationId);

        builder.Entity<ConversationEntity>()
            .HasMany(c => c.Messages)
            .WithOne(m => m.Conversation)
            .HasForeignKey(m => m.ConversationId);

        builder.Entity<ConversationParticipant>()
            .HasKey(p => new { p.ConversationId, p.UserId });


        builder.Entity<ConversationParticipant>()
            .HasOne(p => p.User)
            .WithMany(u => u.ConversationParticipants)
            .HasForeignKey(p => p.UserId);

        builder.Entity<ConversationParticipant>()
            .HasOne(p => p.Conversation)
            .WithMany(c => c.Participants)
            .HasForeignKey(p => p.ConversationId);




        builder.Entity<CommentLikeEntity>()
            .HasKey(c => new { c.UserId, c.CommentId });

        builder.Entity<CommentLikeEntity>()
            .HasOne(p => p.Comment)
            .WithMany(c => c.CommentLikes)
            .HasForeignKey(p => p.CommentId);

        builder.Entity<CommentLikeEntity>()
            .HasOne(p => p.User)
            .WithMany(u => u.LikedComments)
            .HasForeignKey(u => u.UserId);

        builder.Entity<CommentLikeEntity>()
           .HasIndex(x => new { x.UserId, x.CommentId })
           .IsUnique();

    }
}