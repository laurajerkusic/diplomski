using MessageMicroservice.Models;
using Microsoft.EntityFrameworkCore;

namespace MessageMicroservice.Data
{
	public class MessageDbContext : DbContext
	{
		public DbSet<Message> Messages { get; set; }

		public MessageDbContext(DbContextOptions<MessageDbContext> options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Message>().ToTable("Messages");
		}
	}
}