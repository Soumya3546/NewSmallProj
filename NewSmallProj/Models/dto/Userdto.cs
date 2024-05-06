using System.ComponentModel.DataAnnotations;

namespace NewSmallProj.Models.dto
{
	public class Userdto
	{
		[Required(ErrorMessage = "This field is required")]
		[StringLength(50)]
		public string UserName { get; set; }

		[Required(ErrorMessage = "This field is required")]
		[StringLength(100)]
		public string EmailId { get; set; }


		[Required(ErrorMessage = "This field is required")]
		[StringLength(50)]
		public string Password { get; set; }

		[Required(ErrorMessage = "This field is required")]
		public string UserType { get; set; }
	}
}
