using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnnouncementDatabase.Context
{
    public class AnnouncementDbContext : DbContext
    {
        public AnnouncementDbContext(DbContextOptions<AnnouncementDbContext> options) : base(options) { }
        
        private DbSet<Announcement> Announcements { get; set; }             //must be public!!! 

    }
}