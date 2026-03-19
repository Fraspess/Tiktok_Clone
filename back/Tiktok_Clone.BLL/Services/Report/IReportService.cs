using System;
using System.Collections.Generic;
using System.Text;
using Tiktok_Clone.BLL.Dtos.Report;
using Tiktok_Clone.DAL.Entities.Report;

namespace Tiktok_Clone.BLL.Services.Report
{
    public interface IReportService : IGenericService<ReportEntity,String, ReportDTO, CreateReportDTO, UpdateReportDTO>
    {
    }
}
