using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace join_loginSignup.Models
{
    public class Model
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string name { get; set; }

        [Display(Name = "E-mail")]
        public string email { get; set; }

        [Display(Name = "Phone Number")]
        public long phoneNumber { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "password is required")]
        public string password { get; set; }
        public List<Model> Enrollsinfo { get; set; }
    }

    public class Model1 
    {

        [Display(Name = "Name")]
        public string name { get; set; }

        [Display(Name = "E-mail")]
        public string email { get; set; }

        [Display(Name = "Phone Number")]
        public long phoneNumber { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "password is required")]
        public string password { get; set; }
        public List<Model1> Enrollsinfo { get; set; }
    }

    public class Model2
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string name { get; set; }

        [Display(Name = "Car name")]
        public string CarName { get; set; }

        [Display(Name = "Description")]
        public string description { get; set; }

        [Display(Name = "Image")]
        public string images { get; set; }

        [Display(Name = "Order")]
        public bool OrderCar { get; set; }
        public HttpPostedFileBase File { get; set; }
    }
}