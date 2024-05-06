using NewSmallProj.Models.dto;
using NewSmallProj.Models;

namespace NewSmallProj.Interface
{
	public interface IRoom
	{
		Task<IEnumerable<Room>> GetAllRooms();
		Task<IEnumerable<Room>> GetRoomByStatus(string status);
		Task<IEnumerable<Room>> GetRoomByType(string type);
		Task<bool> AddRoom(Roomdto roomdto);
		Task<bool> UpdateRoom(Roomdto roomdto);
		Task<bool> DeleteRoom(int roomid);
	}
}
