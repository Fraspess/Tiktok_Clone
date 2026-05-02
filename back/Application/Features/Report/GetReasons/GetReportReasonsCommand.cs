using MediatR;

namespace Application.Features.Report.GetReasons;

public record GetReportReasonsCommand() : IRequest<List<string>>;