using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MotorsportsEncyclopedia.DAL;
using MotorsportsEncyclopedia.Models;
using PagedList;

namespace MotorsportsEncyclopedia.Controllers
{
    public class CarController : Controller
    {
        private EncyclopediaContext db = new EncyclopediaContext();

		// GET: Car
		public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
		{
			ViewBag.CurrentSort = sortOrder;
			ViewBag.intSortParm = sortOrder == "Year" ? "year_desc" : "Year";
			ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "make_desc" : "";
			ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

			if (searchString != null)
			{
				page = 1;
			}
			else
			{
				searchString = currentFilter;
			}

			ViewBag.CurrentFilter = searchString;

			var cars = from c in db.Cars
						   select c;
			if (!String.IsNullOrEmpty(searchString))
			{
				cars = cars.Where(c => c.CarName.Contains(searchString)
									   || c.CarMake.Contains(searchString));
			}
			switch (sortOrder)
			{
				case "Year":
					cars = cars.OrderBy(c => c.CarYear);
					break;
				case "year_desc":
					cars = cars.OrderByDescending(c => c.CarYear);
					break;
				case "make_desc":
					cars = cars.OrderByDescending(c => c.CarMake);
					break;
				case "name_desc":
					cars = cars.OrderByDescending(c => c.CarName);
					break;
				default:
					cars = cars.OrderBy(c => c.CarYear);
					break;
			}
			int pageSize = 10;
			int pageNumber = (page ?? 1);
			return View(cars.ToPagedList(pageNumber, pageSize));
		}

		public void Create(int v1, string v2, string v3, string v4)
		{
			throw new NotImplementedException();
		}

		// GET: Car/Details/5
		public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // GET: Car/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Car/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "CarYear, CarMake, CarName, CarDescription")]Car car)
		{
			try
			{
				if (ModelState.IsValid)
				{
					db.Cars.Add(car);
					db.SaveChanges();
					return RedirectToAction("Index");
				}
			}
			catch (DataException /* dex */)
			{
				//Log the error (uncomment dex variable name and add a line here to write a log.
				ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
			}
			return View(car);
        }

        // GET: Car/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

		// POST: Car/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost, ActionName("Edit")]
		[ValidateAntiForgeryToken]
		public ActionResult EditPost(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			var carToUpdate = db.Cars.Find(id);
			if (TryUpdateModel(carToUpdate, "",
			   new string[] { "CarYear", "CarMake", "CarName", "CarDescription" }))
			{
				try
				{
					db.SaveChanges();

					return RedirectToAction("Index");
				}
				catch (DataException /* dex */)
				{
					//Log the error (uncomment dex variable name and add a line here to write a log.
					ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
				}
			}
			return View(carToUpdate);
		}

		// GET: Car/Delete/5
		public ActionResult Delete(int? id, bool? saveChangesError = false)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			if (saveChangesError.GetValueOrDefault())
			{
				ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
			}
			Car car = db.Cars.Find(id);
			if (car == null)
			{
				return HttpNotFound();
			}
			return View(car);
		}

		// POST: Car/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id)
		{
			try
			{
				Car car = db.Cars.Find(id);
				db.Cars.Remove(car);
				db.SaveChanges();
			}
			catch (DataException/* dex */)
			{
				//Log the error (uncomment dex variable name and add a line here to write a log.
				return RedirectToAction("Delete", new { id = id, saveChangesError = true });
			}
			return RedirectToAction("Index");
		}

		protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
