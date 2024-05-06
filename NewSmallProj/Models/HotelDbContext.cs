using Microsoft.EntityFrameworkCore;

namespace NewSmallProj.Models
{
	public class HotelDbContext : DbContext
	{
		public HotelDbContext(DbContextOptions<HotelDbContext> options) : base(options)
		{
		}

		public DbSet<Room> Rooms { get; set; }
		public DbSet<Reservation> Reservations { get; set; }
		public DbSet<User> Users { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

			base.OnModelCreating(modelBuilder);

		}
	}
}
