using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MotorsportsEncyclopedia.Models
{
	public class Enrollment
	{
		public int? EnrollmentID { get; set; }
		public int? TrackID { get; set; }
		public int? CarID { get; set; }
		[DisplayFormat(NullDisplayText = "No lap time available.")]
		public string LapTime { get; set; }

		public virtual Track Track { get; set; }
		public virtual Car Car { get; set; }
	}
}