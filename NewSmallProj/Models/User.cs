using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NewSmallProj.Models
{
	public class User
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int UserID { get; set; }
		public string UserName { get; set; }

		public string EmailId { get; set; }

		public string Password { get; set; }

		public string UserType { get; set; }
	}
}
