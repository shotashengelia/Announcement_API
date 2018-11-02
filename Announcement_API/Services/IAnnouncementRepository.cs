using AnnouncementDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Announcement_API.Services
{
    public interface IAnnouncementRepository
    {

        IEnumerable<Announcement> GetAnnouncements();
        Announcement GetAnnouncement(int ID);
        void AddAnnouncement(Announcement announcement);
        void DeleteAnnouncement(Announcement announcement);
        void UpdateAnnouncement(Announcement announcement);
        bool AnnouncementExists(int ID);
        bool Save();
    }
}
