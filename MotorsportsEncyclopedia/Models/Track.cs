using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MotorsportsEncyclopedia.Models
{
	public class Track
	{
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int? TrackID { get; set; }

		[Display(Name = "Track Name")]
		[Required]
		[StringLength(100, ErrorMessage = "Track name cannot be longer than 100 characters.")]
		public string TrackName { get; set; }

		[Display(Name = "Track Location")]
		[Required]
		[StringLength(100, ErrorMessage = "Track location cannot be longer than 100 characters.")]
		public string TrackLocation { get; set; }

		public virtual Company Company { get; set; }
		public virtual ICollection<Enrollment> Enrollments { get; set; }
		public virtual ICollection<Teacher> Teachers { get; set; }
	}
}