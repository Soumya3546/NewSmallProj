using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NewSmallProj.Filter;
using NewSmallProj.Interface;
using NewSmallProj.Models;
using NewSmallProj.Repository;
using NewSmallProj.Service;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<HotelDbContext>(
	options => options.UseSqlServer(
		builder.Configuration.GetConnectionString("hotelConnectionString")
		)
	);

// Add services to the container.
builder.Services.AddScoped<IUser, UserRepository>();
builder.Services.AddScoped<UserService>();

builder.Services.AddScoped<IRoom, RoomRepository>();
builder.Services.AddScoped<RoomService>();

builder.Services.AddScoped<IReservation, ReservationRepository>();
builder.Services.AddScoped<ReservationService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
	//options.SwaggerDoc("v1",new OpenApiInfo { Title= "MyHotel API", Version="v1" });
	options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		Description = "Authorization header using the Bearer scheme",
		Name = "Authorization",
		In = ParameterLocation.Header,
		Type = SecuritySchemeType.ApiKey
	});

	options.OperationFilter<AddAuthorizationHeaderOperationFilter>();
});

//JWT Configuration
var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]);

builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
	options.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuer = true,
		ValidateAudience = true,
		ValidateLifetime = true,
		ValidateIssuerSigningKey = true,
		ValidIssuer = builder.Configuration["Jwt:Issuer"],
		ValidAudience = builder.Configuration["Jwt:Audience"],
		IssuerSigningKey = new SymmetricSecurityKey(key)
	};
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
