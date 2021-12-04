using System;

namespace SeturAssessment.Domain
{
    public interface IEntity
    {
        Guid Id { get; set; }
        string CreateBy { get; set; }
        string UpdateBy { get; set; }
        DateTime CreateDate { get; set; }
        DateTime? UpdateDate { get; set; }
    }

    public class BaseEntity : IEntity
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
        public string CreateBy { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
    }

}
