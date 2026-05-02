using Application;
using Application.Dtos.Report;
using Application.Extensions;
using Application.Features.Report.GetReasons;
using Application.Features.Report.Send;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Reports;

[Route("api/reports")]
[ApiController]
[Authorize]
public class ReportController(IMediator mediator) : ControllerBase
{
    [HttpGet("reasons")]
    public async Task<IActionResult> GetReportReasons()
    {
        var reasons = await mediator.Send(new GetReportReasonsCommand());
        return Ok(ApiResponse<object>.Success(reasons));
    }

    [HttpPost]
    public async Task<IActionResult> SendReport(ReportDTO dto)
    {
        await mediator.Send(new SendReportCommand(User.GetUserId(), dto));
        return Ok(ApiResponse<object>.Success(null!, "Успішно відправлено скаргу!"));
    }
}