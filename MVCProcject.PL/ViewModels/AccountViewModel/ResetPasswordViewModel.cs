using System.ComponentModel.DataAnnotations;

namespace MVCProcject.PL.ViewModels.AccountViewModel
{
    public class ResetPasswordViewModel
    {
        [DataType(DataType.Password)]
        public string newPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare(nameof(newPassword))]
        public string confirmNewPassword { get; set; }
    }
}
