using Data.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DbToXML.ViewModel
{
    public class YearsViewModel
    {
        public List<Year> YersView { get; set; }

        [Display(Name = "YearOfRelease", ResourceType = typeof(Resources.Resource))]
        public int NewYear { get; set; }

        [Display(Name = "Имя")]
        public string Name { get; set; }
    }
}