using NewSmallProj.Interface;
using NewSmallProj.Models.dto;
using NewSmallProj.Models;
using Microsoft.EntityFrameworkCore;

namespace NewSmallProj.Repository
{
	public class ReservationRepository : IReservation
	{
		private readonly HotelDbContext _context;

		public ReservationRepository(HotelDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Reservation>> GetAllReservations()
		{
			try
			{
				return await _context.Reservations.ToListAsync();
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
				var reservationDetails = await _context.Reservations.FirstOrDefaultAsync(reservation => reservation.Reservation_Id == id);
				return reservationDetails;
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
				if (reservationdto == null)
				{
					return false;
				}
				var reservation = new Reservation
				{
					Email = reservationdto.Email,
					Address = reservationdto.Address,
					NoOfGuests = reservationdto.NoOfGuests,
					PerNightCharge = reservationdto.PerNightCharge,
					TotalCharges = reservationdto.TotalCharges,
					RoomNumber = reservationdto.RoomNumber
				};
				await _context.Reservations.AddAsync(reservation);
				await _context.SaveChangesAsync();
				return true;

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
				var reservation = await _context.Reservations.FirstOrDefaultAsync(u => (u.RoomNumber == reservationdto.RoomNumber));
				if (reservation != null)
				{
					reservation.NoOfGuests = reservationdto.NoOfGuests;
					reservation.Email = reservationdto.Email;
					reservation.Address = reservationdto.Address;
					reservation.PerNightCharge = reservationdto.PerNightCharge;
					reservation.TotalCharges = reservationdto.TotalCharges;
					reservation.RoomNumber = reservationdto.RoomNumber;

					await _context.SaveChangesAsync();
					return true;
				}
				return false;
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
				var reservationToDelete = await _context.Reservations.FirstOrDefaultAsync(u => u.Reservation_Id == reservationid);
				if (reservationToDelete != null)
				{
					_context.Reservations.Remove(reservationToDelete);
					await _context.SaveChangesAsync();
					return true;
				}
				return false;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				throw;
			}
		}
	}
}
