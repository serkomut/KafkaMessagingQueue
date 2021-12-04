using System;

namespace SeturAssessment.Messages.Models
{
    public class GuideModel : BaseEntityModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }
        public ContactModel[] Contacts { get; set; }
    }

    public partial class ContactModel
    {
        public Guid Id { get; set; }
    }
}
