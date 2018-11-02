using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Announcement_API.Model
{
    public class Update_Announcement
    {
        [MaxLength(100)]
        [Required]
        public string Title { get; set; }

        [MaxLength(300)]
        [Required]
        public string Description { get; set; }

        [MaxLength(32)]
        public string ImageName { get; set; }

        [MaxLength(20)]
        //    don't forget regular expression
        public string PhoneNumber { get; set; }
    }
}
