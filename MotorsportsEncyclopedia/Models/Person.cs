using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MotorsportsEncyclopedia.Models
{
	public class Person
	{
		public int ID { get; set; }

		[StringLength(100, ErrorMessage = "Last name cannot be longer than 100 characters.")]
		[Display(Name = "Last Name")]
		[Required]
		public string LastName { get; set; }

		[StringLength(100, ErrorMessage = "First name cannot be longer than 100 characters.")]
		[Column("FirstName")]
		[Display(Name = "First Name")]
		[Required]
		public string FirstMidName { get; set; }

		[Display(Name = "Full Name")]
		public string FullName
		{
			get
			{
				return LastName + ", " + FirstMidName;
			}
		}
	}
}