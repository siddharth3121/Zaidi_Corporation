using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Zaidi_Corporation.Models
{
    public class CrudModel
    {
        [Required(ErrorMessage = "Batch Name is required")]
        public string  Batch_Name { get; set; }
        [Required(ErrorMessage = "Batch Location is required")]
        public string Batch_Location { get; set; }
        [Required(ErrorMessage = "Start date is required")]
        public string Start_Date { get; set; }
        [Required(ErrorMessage = "End date is required")]
        public string End_Date { get; set; }
    }
}