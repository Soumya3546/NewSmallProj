using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewSmallProj.Models.dto;
using NewSmallProj.Models;
using NewSmallProj.Service;

namespace NewSmallProj.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ReservationController : Controller
	{
		private readonly ReservationService _reservationService;

		public ReservationController(ReservationService reservationService)
		{
			_reservationService = reservationService;
		}

		[HttpGet]
		[Route("ReservationDetails")]
		public async Task<ActionResult<IEnumerable<Reservation>>> GetAllReservations()
		{
			try
			{
				var details = await _reservationService.GetAllReservations();
				if (details == null)
				{
					return BadRequest();
				}
				return Ok(details);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				throw;
			}
		}

		[HttpGet]
		[Route("ReservationById")]
		public async Task<IActionResult> GetReservationById(int id)
		{
			try
			{
				var reservation = await _reservationService.GetReservationById(id);
				if (reservation != null)
				{
					return Ok(reservation);
				}
				else
				{
					return NotFound($"Reservation with ReservationID {id} not found.");
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				throw;
			}
		}

		[HttpPost]
		[Route("AddReservation")]
		[Authorize(Roles = "Receptionist")]
		public async Task<ActionResult> AddReservation(Reservationdto reservationdto)
		{
			try
			{
				var functionCall = await _reservationService.AddReservation(reservationdto);
				if (functionCall == true)
				{
					return Ok("Reservation added succesfully");
				}
				return BadRequest("Reservation already exists");
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				throw;
			}
		}

		[HttpPut]
		[Route("UpdateReservation")]
		[Authorize(Roles = "Receptionist")]
		public async Task<ActionResult> UpdateReservation(Reservationdto reservationdto)
		{
			try
			{
				if (await _reservationService.UpdateReservation(reservationdto) == false)
				{
					return BadRequest("Reservation already exists.");
				}
				return Ok("Successfully updated.");
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				throw;
			}
		}

		[HttpDelete]
		[Route("DeleteReservation")]
		[Authorize(Roles = "Receptionist")]
		public async Task<ActionResult> DeleteReservation(int reservationid)
		{
			try
			{
				if (await _reservationService.DeleteReservation(reservationid) == false)
				{
					return BadRequest($"Reservation with ReservationID as {reservationid} doesn't exists");
				}
				return Ok("Successfully deleted");
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				throw;
			}
		}

	}
}
