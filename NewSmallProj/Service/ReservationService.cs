using NewSmallProj.Interface;
using NewSmallProj.Models.dto;
using NewSmallProj.Models;

namespace NewSmallProj.Service
{
	public class ReservationService
	{
		private readonly IReservation _reservationRepository;

		public ReservationService(IReservation reservationRepository)
		{
			_reservationRepository = reservationRepository;
		}

		public async Task<IEnumerable<Reservation>> GetAllReservations()
		{
			try
			{
				return await _reservationRepository.GetAllReservations();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				throw;
			}
		}

		public async Task<Reservation> GetReservationById(int id)
		{
			try
			{
				return await _reservationRepository.GetReservationById(id);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				throw;
			}
		}

		public async Task<bool> AddReservation(Reservationdto reservationdto)
		{
			try
			{
				return await _reservationRepository.AddReservation(reservationdto);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				throw;
			}
		}

		public async Task<bool> UpdateReservation(Reservationdto reservationdto)
		{
			try
			{
				return await _reservationRepository.UpdateReservation(reservationdto);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				throw;
			}
		}

		public async Task<bool> DeleteReservation(int reservationid)
		{
			try
			{
				return await _reservationRepository.DeleteReservation(reservationid);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				throw;
			}
		}
	}
}
