using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Announcement_API.Model
{
    public class Get_Announcement
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageName { get; set; }

        public string PhoneNumber { get; set; }

    }
}
