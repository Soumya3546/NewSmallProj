using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NewSmallProj.Models.dto;
using NewSmallProj.Models;
using NewSmallProj.Service;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace NewSmallProj.Controllers
{

	[ApiController]
	[Route("api/[controller]")]
	public class UserController : Controller
	{

		private readonly UserService _userService;
		private readonly IConfiguration _configuration;

		public UserController(UserService userService, IConfiguration configuration)
		{
			_userService = userService;
			_configuration = configuration;
		}

		[HttpGet]
		[Route("UserDetails")]
		public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
		{
			try
			{
				var details = await _userService.GetAllUsers();
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


		[HttpPost]
		[Route("RegisterUser")]
		public async Task<ActionResult> AddUser(Userdto userdto)
		{
			try
			{
				var functionCall = await _userService.AddUser(userdto);
				if (functionCall == true)
				{

					//Sending email to the user when he/she registers
					MailMessage mailMessage = new MailMessage("superstar221919@gmail.com", userdto.EmailId);
					mailMessage.Subject = "Welcome to MyHotel";
					mailMessage.Body = "Thank you for registering with MyHotel...You are successfully registered with the role of " + userdto.UserType;
					mailMessage.IsBodyHtml = false;
					SmtpClient smtp = new SmtpClient();
					smtp.Host = "smtp.gmail.com";
					smtp.Port = 587;
					smtp.EnableSsl = true;
					NetworkCredential networkCredential = new NetworkCredential("superstar221919@gmail.com", "tdmxrtadbiwobeoo");
					smtp.UseDefaultCredentials = false;
					smtp.Credentials = networkCredential;
					smtp.Send(mailMessage);

					return Ok("User added succesfully");
				}
				return BadRequest("User already exists");
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				throw;
			}

		}


		[HttpPost]
		[Route("LoginUser")]
		public async Task<ActionResult<User>> Login(string username, string password)
		{
			try
			{
				var user = await _userService.Login(username, password);

				if (user == null)
				{
					return NotFound();
				}
				//Geenerating JWT Token
				var tokenHandler = new JwtSecurityTokenHandler();

				var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

				var tokenDescriptor = new SecurityTokenDescriptor
				{
					Audience = "YourAudience",
					Issuer = "YourIssuer",
					Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, user.UserName), new Claim(ClaimTypes.Role, user.UserType) }),
					Expires = DateTime.UtcNow.AddMinutes(30),
					SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
				};
				var token = tokenHandler.CreateToken(tokenDescriptor);
				var tokenString = tokenHandler.WriteToken(token);

				return Ok(new { Token = tokenString });
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				throw;
			}
		}

		[HttpPut]
		[Route("UpdateUser")]

		public async Task<ActionResult> UpdateUser(Userdto userdto)
		{
			try
			{
				if (await _userService.UpdateUser(userdto) == false)
				{
					return BadRequest("UserName, UserEmail or Phone number already exists.");
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
		[Route("DeleteUser")]
		public async Task<ActionResult> DeleteUser(string username)
		{
			try
			{
				if (await _userService.DeleteUser(username) == false)
				{
					return BadRequest("UserName doesn't exists");
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
