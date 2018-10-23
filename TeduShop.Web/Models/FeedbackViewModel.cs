using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeduShop.Web.Models
{
    public class FeedbackViewModel
    {
        public int ID { get; set; }

        [MaxLength(250,ErrorMessage ="Không dược vượt quá 250 ký tự.")]
        [Required(ErrorMessage = "Trường bắt buộc")]
        public string Name { get; set; }

        [MaxLength(250, ErrorMessage = "Không dược vượt quá 250 ký tự.")]
        public string Email { get; set; }

        [MaxLength(500, ErrorMessage = "Không dược vượt quá 500 ký tự.")]
        public string Message { get; set; }

        public DateTime CreatedDate { get; set; }

        [Required(ErrorMessage ="Trường bắt buộc")]
        public bool Status { get; set; }

        public ContactDetailViewModel ContactDetail { get; set; }
    }
}