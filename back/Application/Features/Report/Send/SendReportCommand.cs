using Application.Dtos.Report;
using MediatR;

namespace Application.Features.Report.Send;

public record SendReportCommand(Guid UserId, ReportDTO Dto) : IRequest<Unit>;