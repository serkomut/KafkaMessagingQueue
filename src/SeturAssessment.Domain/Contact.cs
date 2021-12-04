using System;

namespace SeturAssessment.Domain
{
    public class Contact : BaseEntity
    {
        public Guid GuideId { get; set; }
        public Guide Guide { get; set; }
        public ContactType ContactType { get; set; }
        public string Value { get; set; }
    }
}
