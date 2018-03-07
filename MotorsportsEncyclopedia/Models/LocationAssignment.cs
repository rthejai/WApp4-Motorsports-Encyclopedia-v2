using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MotorsportsEncyclopedia.Models
{
	public class LocationAssignment
	{
		[Key]
		[ForeignKey("Teacher")]
		public int? TeacherID { get; set; }

		[StringLength(100, ErrorMessage = "Location name cannot be longer than 100 characters.")]
		[Display(Name = "Teacher Location")]
		public string Location { get; set; }

		public virtual Teacher Teacher { get; set; }
	}
}