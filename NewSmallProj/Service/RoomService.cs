using NewSmallProj.Interface;
using NewSmallProj.Models.dto;
using NewSmallProj.Models;

namespace NewSmallProj.Service
{
	public class RoomService
	{
		private readonly IRoom _roomRepository;

		public RoomService(IRoom roomRepository)
		{
			_roomRepository = roomRepository;
		}

		public async Task<IEnumerable<Room>> GetAllRooms()
		{
			try
			{
				return await _roomRepository.GetAllRooms();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				throw;
			}
		}

		public async Task<IEnumerable<Room>> GetRoomByStatus(string status)
		{
			try
			{
				return await _roomRepository.GetRoomByStatus(status);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				throw;
			}
		}

		public async Task<IEnumerable<Room>> GetRoomByType(string type)
		{
			try
			{
				return await _roomRepository.GetRoomByType(type);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				throw;
			}
		}

		public async Task<bool> AddRoom(Roomdto roomdto)
		{
			try
			{
				return await _roomRepository.AddRoom(roomdto);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				throw;
			}
		}

		public async Task<bool> UpdateRoom(Roomdto roomdto)
		{
			try
			{
				return await _roomRepository.UpdateRoom(roomdto);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				throw;
			}
		}

		public async Task<bool> DeleteRoom(int roomid)
		{
			try
			{
				return await _roomRepository.DeleteRoom(roomid);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				throw;
			}
		}
	}
}
