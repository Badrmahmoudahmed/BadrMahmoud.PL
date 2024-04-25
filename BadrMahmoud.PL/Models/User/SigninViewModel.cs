using System.ComponentModel.DataAnnotations;

namespace BadrMahmoud.PL.Models.User
{
	public class SigninViewModel
	{
		[Required (ErrorMessage ="Email Is Requierd")]
        public string Email { get; set; }
        [DataType(DataType.Password)]
		[Required]
		public string Passoword { get; set; }

		public bool RememberMe { get; set; }
	}
}
