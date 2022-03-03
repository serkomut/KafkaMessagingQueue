using KafkaMessagingQueue.Domain;
using KafkaMessagingQueue.Messages.Models;
using System.Linq;

namespace KafkaMessagingQueue.Queries
{
    public static class MapperHelper
    {
        public static GuideModel GuideMap(this Guide x)
        {
            return new GuideModel
            {
                Id = x.Id,
                CreateBy = x.CreateBy,
                CreateDate = x.CreateDate,
                UpdateBy = x.UpdateBy,
                UpdateDate = x.UpdateDate,
                Company = x.Company,
                Name = x.Name,
                Surname = x.Surname,
                Contacts = x.Contacts != null ? x.Contacts.Select(y => new ContactDto
                {
                    Id = y.Id,
                    Value = y.Value,
                    ContactType = (ContactTypeEnum)y.ContactType,
                    GuideId = y.GuideId
                }).ToArray() : null
            };
        }

        public static ContactDto ContactMap(this Contact y)
        {
            return new ContactDto
            {
                Id = y.Id,
                Value = y.Value,
                ContactType = (ContactTypeEnum)y.ContactType,
                GuideId = y.GuideId
            };
        }
    }
}
