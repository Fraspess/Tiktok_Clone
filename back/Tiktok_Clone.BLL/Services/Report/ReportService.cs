using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Tiktok_Clone.BLL.Dtos.Report;
using Tiktok_Clone.DAL.Entities.Report;
using Tiktok_Clone.DAL.Repositories;

namespace Tiktok_Clone.BLL.Services.Report
{
    public class ReportService : GenericService<ReportEntity, String, ReportDTO, CreateReportDTO, UpdateReportDTO>, IReportService
    {
        public ReportService(IGenericRepository<ReportEntity, string> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
