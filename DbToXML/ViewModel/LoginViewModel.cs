using System.ComponentModel.DataAnnotations;

namespace DbToXML.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessageResourceName = "NameError", ErrorMessageResourceType = typeof(Resources.Resource))]
        public string Name { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessageResourceName = "PasswordError", ErrorMessageResourceType = typeof(Resources.Resource))]
        public string Password { get; set; }
    }
}