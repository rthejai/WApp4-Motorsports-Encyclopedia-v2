using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MotorsportsEncyclopedia.DAL;
using MotorsportsEncyclopedia.ViewModels;

namespace MotorsportsEncyclopedia.Controllers
{
	public class HomeController : Controller
	{
		private EncyclopediaContext db = new EncyclopediaContext();

		public ActionResult Index()
		{
			return View();
		}

		public ActionResult About()
		{
			IQueryable<EnrollmentYearGroup> data = from car in db.Cars
												   group car by car.CarYear into timeGroup
												   select new EnrollmentYearGroup()
												   {
													   CarYear = timeGroup.Key,
													   CarCount = timeGroup.Count()
												   };
			return View(data.ToList());
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}

		public ActionResult Chat()
		{
			return View();
		}

	}
}