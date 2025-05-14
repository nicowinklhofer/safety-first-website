﻿using System;
using System.ComponentModel.DataAnnotations;

namespace SPL_Diplom_Winki_Trippi_Sabi.Models
{
    public class ContactMessage
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Message { get; set; }

        public DateTime SentAt { get; set; } = DateTime.Now;
    }
}
