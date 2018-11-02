using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnnouncementDatabase;
using AnnouncementDatabase.Context;

namespace Announcement_API.Services
{
    public class AnnouncementRepository : IAnnouncementRepository
    {
        private AnnouncementDbContext _Context;

        public void AddAnnouncement(Announcement announcement)
        {
            _Context.Announcements.Add(announcement);
        }
        
        public bool AnnouncementExists(int ID)
        {
            return _Context.Announcements.Any(a => a.ID == ID);
        }

        public void DeleteAnnouncement(Announcement announcement)
        {
            _Context.Announcements.Remove(announcement);
        }

        public Announcement GetAnnouncement(int ID)
        {
            return _Context.Announcements.FirstOrDefault(a => a.ID == ID);
        }

        public IEnumerable<Announcement> GetAnnouncements()
        {
            return _Context.Announcements.OrderBy(a => a.Title).ToList();
        }

        public bool Save()
        {
            return (_Context.SaveChanges() >= 0);
        }

        public void UpdateAnnouncement(Announcement announcement)
        {
            throw new NotImplementedException();
        }
    }
}
