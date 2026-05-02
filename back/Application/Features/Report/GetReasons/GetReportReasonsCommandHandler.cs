using System.ComponentModel;
using System.Reflection;
using Domain;
using MediatR;

namespace Application.Features.Report.GetReasons;

internal class GetReportReasonsCommandHandler : IRequestHandler<GetReportReasonsCommand, List<string>>
{
    public Task<List<string>> Handle(GetReportReasonsCommand request, CancellationToken cancellationToken)
    {
        var reasons = Enum.GetValues<ReportReasons>()
            .Select(r => r.GetType()
                .GetField(r.ToString())!
                .GetCustomAttribute<DescriptionAttribute>()?.Description ?? r.ToString())
            .ToList();
        return Task.FromResult(reasons);
    }
}