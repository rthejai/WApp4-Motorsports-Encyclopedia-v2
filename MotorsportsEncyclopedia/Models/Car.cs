using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MotorsportsEncyclopedia.Models
{
	public class Car
	{
		public int? CarID { get; set; }
		public int CarYear { get; set; }
		[Required]
		[StringLength(100, ErrorMessage = "Car make cannot be longer than 100 characters.")]
		public string CarMake { get; set; }
		[Required]
		[StringLength(100, ErrorMessage = "Car name cannot be longer than 100 characters.")]
		public string CarName { get; set; }
		public string CarDescription { get; set; }

		public virtual ICollection<Enrollment> Enrollments { get; set; }
	}
}