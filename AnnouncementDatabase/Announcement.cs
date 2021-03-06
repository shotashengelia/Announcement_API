﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AnnouncementDatabase
{
    public class Announcement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [MaxLength(100)]
        [Required]
        public string Title { get; set; }

        [MaxLength(300)]
        [Required]
        public string Description { get; set; }

        [MaxLength(32)]
        public string ImageName { get; set; }

        [MaxLength(20)]
        public string PhoneNumber { get; set; }
    }
}
