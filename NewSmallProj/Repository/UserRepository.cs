using NewSmallProj.Interface;
using NewSmallProj.Models.dto;
using NewSmallProj.Models;
using Microsoft.EntityFrameworkCore;

namespace NewSmallProj.Repository
{
	public class UserRepository : IUser
	{
		private readonly HotelDbContext _context;
		public UserRepository(HotelDbContext context)
		{
			_context = context;
		}

		//to get all the details of user
		public async Task<IEnumerable<User>> GetAllUsers()
		{
			try
			{
				return await _context.Users.ToListAsync();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				throw;
			}
		}

		//to login to the user's account
		public async Task<User> Login(string username, string password)
		{
			try
			{
				var userDetails = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username && u.Password == password);
				return userDetails;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				throw;
			}
		}

		//To register the details of user
		public async Task<bool> AddUser(Userdto userdto)
		{
			try
			{
				if (userdto == null)
				{
					return false;
				}

				if (await CheckIfUserExists(userdto) == true)
				{
					var user = new User
					{
						UserName = userdto.UserName,
						EmailId = userdto.EmailId,
						Password = userdto.Password,
						UserType = userdto.UserType
					};
					await _context.Users.AddAsync(user);
					await _context.SaveChangesAsync();
					return true;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				throw;
			}
			return false;
		}

		//to update the details of user
		public async Task<bool> UpdateUser(Userdto userdto)
		{
			try
			{
				var user = await _context.Users.FirstOrDefaultAsync(u => (u.UserName == userdto.UserName));
				if (user != null)
				{
					user.UserName = userdto.UserName;
					user.EmailId = userdto.EmailId;
					user.Password = userdto.Password;

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


		//to delete user account
		public async Task<bool> DeleteUser(string username)
		{
			try
			{
				var userToDelete = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
				if (userToDelete != null)
				{
					_context.Users.Remove(userToDelete);
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


		//to check if username or email already exists
		public async Task<bool> CheckIfUserExists(Userdto userdto)
		{
			try
			{
				if (await _context.Users.AnyAsync(u => (u.UserName == userdto.UserName) || (u.EmailId == userdto.EmailId)))
				{
					return false;
				}
				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				throw;
			}
			//return false;
		}
	}
	}
