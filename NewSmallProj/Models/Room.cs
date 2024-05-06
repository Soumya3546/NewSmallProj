using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NewSmallProj.Models
{
	public class Room
	{

		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int RoomID { get; set; }

		[Required(ErrorMessage = "This field is required")]
		public int RoomNumber { get; set; }

		[Required(ErrorMessage = "This field is required")]
		public string RoomType { get; set; }

		[Required(ErrorMessage = "This field is required")]
		public string RoomStatus { get; set; }

	}
}
