using KafkaMessagingQueue.ReportApi.Application.Domain;
using System;

namespace KafkaMessagingQueue.ReportApi.Application.Models
{
    public class ReportModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Data { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateBy { get; set; }
        public string Status { get; set; }
        public static ReportModel Map(Report x)
        {
            return new ReportModel
            {
                Id = x.Id,
                Name = x.Name,
                Data = x.Data,
                CreateDate = x.CreateDate,
                CreateBy = x.CreateBy,
                Status = x.Status == Domain.Status.Preparing ? "Hazırlanıyor" : "Tamamlandı",
            };
        }
    }
}
