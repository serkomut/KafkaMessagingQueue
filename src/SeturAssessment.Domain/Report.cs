using System;

namespace SeturAssessment.Domain
{
    public class Report
    {
        private DateTime? createdDate;
        private Guid? id;
        public Guid Id
        {
            get => id ?? Guid.NewGuid();
            set => id = value;
        }
        public DateTime CreateDate
        {
            get => createdDate ?? DateTime.UtcNow;
            set => createdDate = value;
        }
        public string Name { get; set; }
        public string Data { get; set; }
        public string CreateBy { get; set; }
    }
}
