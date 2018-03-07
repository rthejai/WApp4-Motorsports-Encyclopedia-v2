using System.Collections.Generic;
using MotorsportsEncyclopedia.Models;
using MotorsportsEncyclopedia.Controllers;

namespace MotorsportsEncyclopedia.ViewModels
{
	public class TeacherIndexData
	{
		public IEnumerable<Teacher> Teachers { get; set; }
		public IEnumerable<Track> Tracks { get; set; }
		public IEnumerable<Enrollment> Enrollments { get; set; }
	}
}