using Microsoft.EntityFrameworkCore;
using Phonebook.DAL.Entities;

namespace Phonebook.DAL.Data
{
	public class PhonebookDbContext : DbContext
	{
		public DbSet<Contact> Contacts { get; set; }
		public DbSet<PhoneNumber> PhoneNumbers { get; set; }
		public DbSet<PhoneType> PhoneTypes { get; set; }

		public PhonebookDbContext(DbContextOptions<PhonebookDbContext> options)
			: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Contact>()
				.HasMany(c => c.PhoneNumbers)
				.WithOne(pn => pn.Contact)
				.HasForeignKey(pn => pn.ContactId);

			modelBuilder.Entity<PhoneNumber>()
				.HasOne(pn => pn.PhoneType)
				.WithMany()
				.HasForeignKey(pn => pn.PhoneTypeId)
				.OnDelete(DeleteBehavior.SetNull);
			modelBuilder.Entity<PhoneType>()
				.HasIndex(pt => pt.TypeName)
				.IsUnique();

			// Dodavanje nekih početnih podataka (seed data)
			modelBuilder.Entity<PhoneType>().HasData(
				new PhoneType { Id = 1, TypeName = "Mobile" },
				new PhoneType { Id = 2, TypeName = "Home" },
				new PhoneType { Id = 3, TypeName = "Work" }
			);
		}
	}
}