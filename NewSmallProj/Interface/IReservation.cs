using NewSmallProj.Models.dto;
using NewSmallProj.Models;

namespace NewSmallProj.Interface
{
	public interface IReservation
	{
		Task<IEnumerable<Reservation>> GetAllReservations();
		Task<Reservation> GetReservationById(int reservationid);
		Task<bool> AddReservation(Reservationdto reservationdto);
		Task<bool> UpdateReservation(Reservationdto reservationdto);
		Task<bool> DeleteReservation(int reservationid);
	}
}
