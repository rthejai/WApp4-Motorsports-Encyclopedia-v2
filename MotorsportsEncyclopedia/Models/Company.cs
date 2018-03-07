using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MotorsportsEncyclopedia.Models
{
	public class Company
	{
		public int? CompanyID { get; set; }

		[StringLength(100, ErrorMessage = "Company name cannot be longer than 100 characters.")]
		[Display(Name = "Company Name")]
		[Required]
		public string CompanyName { get; set; }

		[StringLength(100, ErrorMessage = "Company location cannot be longer than 100 characters.")]
		[Display(Name = "Company Location")]
		public string CompanyLocation { get; set; }

		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		[Display(Name = "Established Date")]
		public DateTime? StartDate { get; set; }

		public int? TeacherID { get; set; }

		public virtual Teacher Administrator { get; set; }
		public virtual ICollection<Track> Tracks { get; set; }
	}
}