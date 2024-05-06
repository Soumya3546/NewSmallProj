using NewSmallProj.Models.dto;
using NewSmallProj.Models;

namespace NewSmallProj.Interface
{
	public interface IUser
	{
		Task<IEnumerable<User>> GetAllUsers();
		Task<User> Login(string username, string password);
		Task<bool> AddUser(Userdto userdto);
		Task<bool> UpdateUser(Userdto userdto);
		Task<bool> DeleteUser(string username);
		Task<bool> CheckIfUserExists(Userdto userdto);
	}
}
