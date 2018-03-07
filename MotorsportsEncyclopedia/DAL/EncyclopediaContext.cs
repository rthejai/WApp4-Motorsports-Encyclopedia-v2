using MotorsportsEncyclopedia.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace MotorsportsEncyclopedia.DAL
{
	public class EncyclopediaContext : DbContext
	{

		public EncyclopediaContext() : base("EncyclopediaContext")
		{
		}

		public DbSet<Car> Cars { get; set; }
		public DbSet<Enrollment> Enrollments { get; set; }
		public DbSet<Track> Tracks { get; set; }
		public DbSet<Company> Companies { get; set; }
		public DbSet<Teacher> Teachers { get; set; }
		public DbSet<Person> People { get; set; }
		public DbSet<Driver> Drivers { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

			modelBuilder.Entity<Track>()
				.HasMany(c => c.Teachers).WithMany(i => i.Tracks)
				.Map(t => t.MapLeftKey("TrackID")
					.MapRightKey("TeacherID")
					.ToTable("TrackTeacher"));
		}

		public System.Data.Entity.DbSet<MotorsportsEncyclopedia.Models.LocationAssignment> LocationAssignments { get; set; }
	}
}