using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MotorsportsEncyclopedia.Models
{
	public class Teacher : Person
	{

		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		[Display(Name = "Hire Date")]
		public DateTime? HireDate { get; set; }

		public virtual ICollection<Track> Tracks { get; set; }
		public virtual LocationAssignment LocationAssignment { get; set; }
	}
}