using NewSmallProj.Interface;
using NewSmallProj.Models.dto;
using NewSmallProj.Models;
using Microsoft.EntityFrameworkCore;

namespace NewSmallProj.Repository
{
	public class RoomRepository: IRoom
	{
		
			private readonly HotelDbContext _context;

			public RoomRepository(HotelDbContext context)
			{
				_context = context;
			}

			public async Task<IEnumerable<Room>> GetAllRooms()
			{
				try
				{
					return await _context.Rooms.ToListAsync();
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
					var roomDetails = await _context.Rooms.Where(room => room.RoomStatus == status).ToListAsync();
					return roomDetails;
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
					if (string.IsNullOrEmpty(type))
					{
						return new List<Room>();
					}
					return await _context.Rooms.Where(r => r.RoomType.ToUpper().Contains(type.ToUpper())).ToListAsync();

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
					if (roomdto == null)
					{
						return false;
					}
					var room = new Room
					{
						RoomType = roomdto.RoomType,
						RoomStatus = roomdto.RoomStatus,
						RoomNumber = roomdto.RoomNumber
					};
					await _context.Rooms.AddAsync(room);
					await _context.SaveChangesAsync();
					return true;

				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
					throw;
				}
				//return false;
			}

			public async Task<bool> UpdateRoom(Roomdto roomdto)
			{
				try
				{
					var room = await _context.Rooms.FirstOrDefaultAsync(u => (u.RoomType == roomdto.RoomType));
					if (room != null)
					{
						room.RoomNumber = roomdto.RoomNumber;
						room.RoomType = roomdto.RoomType;
						room.RoomStatus = roomdto.RoomStatus;

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

			public async Task<bool> DeleteRoom(int roomid)
			{
				try
				{
					var roomToDelete = await _context.Rooms.FirstOrDefaultAsync(u => u.RoomID == roomid);
					if (roomToDelete != null)
					{
						_context.Rooms.Remove(roomToDelete);
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
