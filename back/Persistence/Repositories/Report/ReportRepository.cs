using Application.Interfaces;
using Domain;
using Domain.Entities.Report;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories.Report;

internal class ReportRepository(AppDbContext context)
    : GenericRepository<ReportEntity, Guid>(context), IReportRepository
{
    public async Task<bool> ExistsAsync(Guid senderId, Guid contentId, ContentTypes contentType)
    {
        return contentType switch
        {
            ContentTypes.Video => await _context.Set<VideoReportEntity>()
                .AnyAsync(r => r.SenderId == senderId && r.VideoId == contentId),
            ContentTypes.Comment => await _context.Set<CommentReportEntity>()
                .AnyAsync(r => r.SenderId == senderId && r.CommentId == contentId),
            ContentTypes.User => await _context.Set<UserReportEntity>()
                .AnyAsync(r => r.SenderId == senderId && r.UserId == contentId),
            _ => false
        };
    }
}