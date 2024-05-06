using NewSmallProj.Interface;
using NewSmallProj.Models.dto;
using NewSmallProj.Models;

namespace NewSmallProj.Service
{
	public class UserService
	{
		private readonly IUser _userRepository;

		public UserService(IUser userRepository)
		{
			_userRepository = userRepository;
		}

		public async Task<IEnumerable<User>> GetAllUsers()
		{
			try
			{
				return await _userRepository.GetAllUsers();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				throw;
			}
		}

		public async Task<User> Login(string username, string password)
		{
			try
			{
				return await _userRepository.Login(username, password);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				throw;
			}
		}

		public async Task<bool> AddUser(Userdto userdto)
		{
			try
			{
				return await _userRepository.AddUser(userdto);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				throw;
			}
		}

		public async Task<bool> UpdateUser(Userdto userdto)
		{
			try
			{
				return await _userRepository.UpdateUser(userdto);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				throw;
			}
		}

		public async Task<bool> DeleteUser(string username)
		{
			try
			{
				return await _userRepository.DeleteUser(username);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				throw;
			}
		}
	}
}
