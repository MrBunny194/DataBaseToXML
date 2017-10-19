using Data.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DbToXML.ViewModel
{
    public class CarsViewModel
    {
        public List<Car> CarsView { get; set; }
        public IEnumerable<SelectListItem> SelectYear { get; set; }

        [Required]
        [Display(Name = "Brand", ResourceType = typeof(Resources.Resource))]
        public string Model { get; set; }

        [Required]
        [Display(Name = "Model", ResourceType = typeof(Resources.Resource))]
        public string Name { get; set; }

        [Required]
        [Display(Name = "YearOfRelease", ResourceType = typeof(Resources.Resource))]
        public int Year { get; set; }
    }
}