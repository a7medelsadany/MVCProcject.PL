using System.ComponentModel.DataAnnotations;

namespace MVCProcject.PL.ViewModels.AccountViewModel
{
    public class ForgetPasswordViewModel
    {
        [Required(ErrorMessage ="email field is empty!!")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
