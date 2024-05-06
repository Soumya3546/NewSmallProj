using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewSmallProj.Models.dto;
using NewSmallProj.Models;
using NewSmallProj.Service;

namespace NewSmallProj.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RoomController : Controller
	{
		private readonly RoomService _roomService;

		public RoomController(RoomService roomService)
		{
			_roomService = roomService;
		}

		[HttpGet]
		[Route("RoomDetails")]
		public async Task<ActionResult<IEnumerable<Room>>> GetAllRooms()
		{
			try
			{
				var details = await _roomService.GetAllRooms();
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
		[Route("SearchRoomByStatus")]
		[Authorize(Roles = "Receptionist, Manager")]
		public async Task<ActionResult<IEnumerable<Room>>> GetRoomByStatus(string status)
		{
			try
			{
				var room = await _roomService.GetRoomByStatus(status);
				if (room != null)
				{
					return Ok(room);
				}
				else
				{
					return NotFound($"Room with status {status} not found.");
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				throw;
			}
		}

		[HttpGet]
		[Route("SearchRoomByType")]
		[Authorize(Roles = "Receptionist, Manager")]
		public async Task<ActionResult<IEnumerable<Room>>> GetRoomByType(string type)
		{
			try
			{
				var room = await _roomService.GetRoomByType(type);
				if (room != null)
				{
					return Ok(room);
				}
				else
				{
					return NotFound($"Room with type {type} not found.");
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				throw;
			}
		}

		[HttpPost]
		[Route("AddRoom")]
		[Authorize(Roles = "Manager")]
		public async Task<ActionResult> AddRoom(Roomdto roomdto)
		{
			try
			{
				var functionCall = await _roomService.AddRoom(roomdto);
				if (functionCall == true)
				{
					return Ok("Room added succesfully");
				}
				return BadRequest("Room already exists");
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				throw;
			}
		}

		[HttpPut]
		[Route("UpdateRoom")]
		[Authorize(Roles = "Manager")]
		public async Task<ActionResult> UpdateRoom(Roomdto roomdto)
		{
			try
			{
				if (await _roomService.UpdateRoom(roomdto) == false)
				{
					return BadRequest("RoomType, or Room Status already exists.");
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
		[Route("DeleteRoom")]
		[Authorize(Roles = "Manager")]
		public async Task<ActionResult> DeleteRoom(int roomid)
		{
			try
			{
				if (await _roomService.DeleteRoom(roomid) == false)
				{
					return BadRequest("Room with this roomid doesn't exists");
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
