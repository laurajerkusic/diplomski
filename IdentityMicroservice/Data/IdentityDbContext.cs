using Microsoft.EntityFrameworkCore;
using IdentityMicroservice.Models;

namespace IdentityMicroservice.Data
{
	public class IdentityDbContext : DbContext
	{
		public DbSet<User> Users { get; set; }

		public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options) { }

	}
}