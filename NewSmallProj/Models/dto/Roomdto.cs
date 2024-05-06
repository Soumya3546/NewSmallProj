using System.ComponentModel.DataAnnotations;

namespace NewSmallProj.Models.dto
{
	public class Roomdto
	{
		[Required(ErrorMessage = "This field is required")]
		public int RoomNumber { get; set; }
		[Required(ErrorMessage = "This field is required")]
		public string RoomType { get; set; }
		[Required(ErrorMessage = "This field is required")]
		public string RoomStatus { get; set; }
	}
}
