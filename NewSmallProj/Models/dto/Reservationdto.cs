using System.ComponentModel.DataAnnotations;

namespace NewSmallProj.Models.dto
{
	public class Reservationdto
	{
		[Required(ErrorMessage = "This field is required")]
		[StringLength(50)]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }

		[Required(ErrorMessage = "This field is required")]
		[StringLength(50)]
		public string Address { get; set; }

		public int RoomNumber { get; set; }

		[Required(ErrorMessage = "This field is required")]
		public int NoOfGuests { get; set; }

		[Required(ErrorMessage = "This field is required")]
		public decimal PerNightCharge { get; set; }

		[Required(ErrorMessage = "This field is required")]
		[StringLength(50)]
		public decimal TotalCharges { get; set; }
	}
}
