using System;
using System.Collections.Generic;
using System.Text;
using Tiktok_Clone.DAL.Entities.User;
using Tiktok_Clone.DAL.Entities.Video;

namespace Tiktok_Clone.DAL.Entities.Report
{
    public class ReportEntity : BaseEntity<String>
    {
        public override String Id { get; set; } = Guid.NewGuid().ToString();


        public required String SenderId { get; set; }
        public UserEntity Sender { get; set; } = null!;

        public String? VideoId { get; set; }
        public VideoEntity? Video { get; set; }

        public String? ReportedUserId { get; set; }
        public UserEntity? ReportedUser { get; set; }

        public required String Reason { get; set; }

    }
}
