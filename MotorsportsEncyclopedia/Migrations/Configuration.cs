namespace MotorsportsEncyclopedia.Migrations
{
	using MotorsportsEncyclopedia.DAL;
	using MotorsportsEncyclopedia.Models;
	using System;
	using System.Collections.Generic;
	using System.Data.Entity;
	using System.Data.Entity.Migrations;
	using System.Linq;

	internal sealed class Configuration : DbMigrationsConfiguration<MotorsportsEncyclopedia.DAL.EncyclopediaContext>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = false;
		}

		protected override void Seed(MotorsportsEncyclopedia.DAL.EncyclopediaContext context)
		{
			var cars = new List<Car>
			{

			new Car{CarYear=1994,CarMake="BMW",CarName="3 Series E36",CarDescription="A common generation of a BMW 3 Series used in motorsports frequently."},
			new Car{CarYear=1987,CarMake="Toyota",CarName="Corolla AE86",CarDescription="A car more common in Japan to modify for racing."},
			new Car{CarYear=1995,CarMake="Nissan",CarName="Silvia S14",CarDescription="Very popular car used frequently as a starting platform for drifting."},

			};
			cars.ForEach(s => context.Cars.AddOrUpdate(c => c.CarName, s));
			context.SaveChanges();

			var teachers = new List<Teacher>
			{
				new Teacher { FirstMidName = "Kim", LastName = "Abercrombie", HireDate = DateTime.Parse("1995-03-11") },
				new Teacher { FirstMidName = "Fadi", LastName = "Fakhouri", HireDate = DateTime.Parse("1987-10-25") },
				new Teacher { FirstMidName = "Roger", LastName = "Harui", HireDate = DateTime.Parse("2002-08-15") },
			};
			teachers.ForEach(s => context.Teachers.AddOrUpdate(p => p.LastName, s));
			context.SaveChanges();

			var drivers = new List<Driver>
			{
				new Driver { FirstMidName = "John", LastName = "Smith", HireDate = DateTime.Parse("1995-03-11") },
				new Driver { FirstMidName = "Thejai", LastName = "Riem", HireDate = DateTime.Parse("1987-10-25") },
				new Driver { FirstMidName = "James", LastName = "Brown", HireDate = DateTime.Parse("2002-08-15") },
			};
			drivers.ForEach(s => context.Drivers.AddOrUpdate(p => p.LastName, s));
			context.SaveChanges();

			var companies = new List<Company>
			{
				new Company { CompanyName = "Top Secret", CompanyLocation = "Tsukuba, Japan",
					StartDate = DateTime.Parse("2007-09-01"),
					TeacherID  = teachers.Single( i => i.LastName == "Abercrombie").ID },
				new Company { CompanyName = "Envyus", CompanyLocation = "Seattle, United States of America",
					StartDate = DateTime.Parse("2007-09-01"),
					TeacherID  = teachers.Single( i => i.LastName == "Fakhouri").ID },
				new Company { CompanyName = "Juicebox", CompanyLocation = "London, United Kingdom",
					StartDate = DateTime.Parse("2007-09-01"),
					TeacherID  = teachers.Single( i => i.LastName == "Harui").ID },
			};
			companies.ForEach(s => context.Companies.AddOrUpdate(p => p.CompanyName, s));
			context.SaveChanges();

			var tracks = new List<Track>
			{

			new Track{TrackID=1000,TrackName="Mazda Raceway Laguna Seca",TrackLocation="California, United States", Teachers = new List<Teacher>()},
			new Track{TrackID=1010,TrackName="Mount Haruna",TrackLocation="Honshu, Japan", Teachers = new List<Teacher>()},
			new Track{TrackID=1020,TrackName="Tsukuba Circuit",TrackLocation="Tsukuba, Japan", Teachers = new List<Teacher>()},

			};
			tracks.ForEach(s => context.Tracks.AddOrUpdate(p => p.TrackName, s));
			context.SaveChanges();

			AddOrUpdateTeacher(context, "Mount Haruna", "Abercrombie");
			AddOrUpdateTeacher(context, "Tsukuba Circuit", "Fakhouri");
			AddOrUpdateTeacher(context, "Mazda Raceway Laguna Seca", "Harui");

			context.SaveChanges();

			var enrollments = new List<Enrollment>
			{

			new Enrollment{CarID=1,TrackID=1000,LapTime="1:58s"},
			new Enrollment{CarID=2,TrackID=1010,LapTime="3:10s"},
			new Enrollment{CarID=3,TrackID=1020,LapTime="1:10s"},

			};

			foreach (Enrollment e in enrollments)
			{
				var enrollmentInDataBase = context.Enrollments.Where(
					s =>
						 s.CarID == e.CarID &&
						 s.Track.TrackID == e.TrackID).SingleOrDefault();
				if (enrollmentInDataBase == null)
				{
					context.Enrollments.Add(e);
				}
			}
			context.SaveChanges();
		}

		void AddOrUpdateTeacher(EncyclopediaContext context, string TrackName, string TeacherName)
		{
			var crs = context.Tracks.SingleOrDefault(c => c.TrackName == TrackName);
			var inst = crs.Teachers.SingleOrDefault(i => i.LastName == TeacherName);
			if (inst == null)
				crs.Teachers.Add(context.Teachers.Single(i => i.LastName == TeacherName));
		}

	}
}