using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeduShop.Web.Models
{
    public class ContactDetailViewModel
    {
        public int ID { get; set; }

        [MaxLength(250,ErrorMessage ="Không được vượt quá 250 ký tự.")]
        [Required(ErrorMessage ="Trường này không được trống")]
        public string Name { get; set; }

        [MaxLength(50, ErrorMessage = "Không được vượt quá 50 ký tự.")]
        public string Phone { get; set; }

        [MaxLength(250, ErrorMessage = "Không được vượt quá 250 ký tự.")]
        public string Email { get; set; }

        [MaxLength(250, ErrorMessage = "Không được vượt quá 250 ký tự.")]
        public string Website { get; set; }

        [MaxLength(250, ErrorMessage = "Không được vượt quá 250 ký tự.")]
        public string Address { get; set; }

        public string Other { get; set; }


        public double? Lat { get; set; }//toa do google map


        public double? Lng { get; set; }//toa do google map

        public bool Status { get; set; }
    }
}