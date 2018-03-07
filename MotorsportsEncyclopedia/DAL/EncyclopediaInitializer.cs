using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using MotorsportsEncyclopedia.Models;

namespace MotorsportsEncyclopedia.DAL
{
	public class EncyclopediaInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<EncyclopediaContext>
	{
		protected override void Seed(EncyclopediaContext context)
		{
			var cars = new List<Car>
			{
			new Car{CarYear=1994,CarMake="BMW",CarName="3 Series E36",CarDescription="A common generation of a BMW 3 Series used in motorsports frequently."},
			new Car{CarYear=1987,CarMake="Toyota",CarName="Corolla AE86",CarDescription="A car more common in Japan to modify for racing."},
			new Car{CarYear=1995,CarMake="Nissan",CarName="Silvia S14",CarDescription="Very popular car used frequently as a starting platform for drifting."},

			};

			cars.ForEach(c => context.Cars.Add(c));
			context.SaveChanges();
			var tracks = new List<Track>
			{
			new Track{TrackID=1000,TrackName="Mazda Raceway Laguna Seca",TrackLocation="California, United States"},
			new Track{TrackID=1010,TrackName="Mount Haruna",TrackLocation="Honshu, Japan"},
			new Track{TrackID=1020,TrackName="Tsukuba Circuit",TrackLocation="Tsukuba, Japan"},

			};
			tracks.ForEach(t => context.Tracks.Add(t));
			context.SaveChanges();
			var enrollments = new List<Enrollment>
			{
			new Enrollment{CarID=1,TrackID=1000,LapTime="1:58s"},
			new Enrollment{CarID=2,TrackID=1010,LapTime="3:10s"},
			new Enrollment{CarID=3,TrackID=1020,LapTime="1:10s"},

			};
			enrollments.ForEach(e => context.Enrollments.Add(e));
			context.SaveChanges();
		}
	}
}