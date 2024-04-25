using System.ComponentModel.DataAnnotations;

namespace BadrMahmoud.PL.Models.User
{
    public class SendResetPasswordEmilViewModel
    {
        [Required]
        public string Email { get; set; }
    }
}
