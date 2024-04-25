using System.ComponentModel.DataAnnotations;

namespace BadrMahmoud.PL.Models.User
{
    public class ResetPasswordViewModel
    {
        [DataType(DataType.Password)]
        [Required]
        public string Passoword { get; set; }
        [DataType(DataType.Password)]
        [Required]
        [Compare(nameof(Passoword), ErrorMessage = "Password Not Matched")]
        public string ConfirmPassword { get; set; }
    }
}
