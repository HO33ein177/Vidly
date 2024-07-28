using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class Customer
    {
        [Required(ErrorMessage = "Please Enter Your Name")]
        [StringLength(255)]
        public string Name { get; set; }

        [Min18YearsIfMember]
        public DateTime? Birthdate { get; set; }

        public int Id { get; set; }

        public bool IsSubscribedToNewsLetter { get; set; }

        public MemberShipType MemberShipType { get; set; }

        [Display(Name="MemberShip")]
        public byte MemberShipTypeId { get; set; }

        
    }
}