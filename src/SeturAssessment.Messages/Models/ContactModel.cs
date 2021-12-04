using System;

namespace SeturAssessment.Messages.Models
{
    public partial class ContactModel
    {
        public Guid GuideId { get; set; }
        public ContactTypeEnum ContactType { get; set; }
        public string Value { get; set; }
    }

    public enum ContactTypeEnum : byte
    {
        EMAIL = 1,
        PHONE = 2,
        LOCATION = 3
    }
}
