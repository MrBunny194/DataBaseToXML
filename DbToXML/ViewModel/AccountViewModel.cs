using System.Collections.Generic;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DbToXML.ViewModel
{
    public class AccountViewModel
    {
        public IEnumerable<SelectListItem> Roles { get; set; }

        [Required]
        [Display(Name = "Логин")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [DataType(DataType.Password)]
        [Display(Name = "Повторите")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessageResourceName = "CheckPassword", ErrorMessageResourceType = typeof(Resources.Resource))]
        public string ConfirmPassword { get; set; }
        
        [Required]
        [Display(Name = "Привилегии")]
        public int RoleId { get; set; }
    }
}