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
using MotorsportsEncyclopedia.ViewModels;

namespace MotorsportsEncyclopedia.Controllers
{
    public class TeacherController : Controller
    {
        private EncyclopediaContext db = new EncyclopediaContext();

		// GET: Teacher
		public ActionResult Index(int? id, int? trackID)
		{
			var viewModel = new TeacherIndexData();
			viewModel.Teachers = db.Teachers
				.Include(i => i.LocationAssignment)
				.Include(i => i.Tracks.Select(c => c.Company))
				.OrderBy(i => i.LastName);

			if (id != null)
			{
				ViewBag.TeacherID = id.Value;
				viewModel.Tracks = viewModel.Teachers.Where(
					i => i.ID == id.Value).Single().Tracks;
			}

			if (trackID != null)
			{
				ViewBag.TrackID = trackID.Value;
				// Lazy loading
				//viewModel.Enrollments = viewModel.Courses.Where(
				//    x => x.CourseID == courseID).Single().Enrollments;
				// Explicit loading
				var selectedTrack = viewModel.Tracks.Where(x => x.TrackID == trackID).Single();
				db.Entry(selectedTrack).Collection(x => x.Enrollments).Load();
				foreach (Enrollment enrollment in selectedTrack.Enrollments)
				{
					db.Entry(enrollment).Reference(x => x.Car).Load();
				}

				viewModel.Enrollments = selectedTrack.Enrollments;
			}

			return View(viewModel);
		}

		// GET: Teacher/Details/5
		public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // GET: Teacher/Create
        public ActionResult Create()
        {
            ViewBag.ID = new SelectList(db.LocationAssignments, "TeacherID", "Location");
            return View();
        }

        // POST: Teacher/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,LastName,FirstMidName,HireDate")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                db.Teachers.Add(teacher);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID = new SelectList(db.LocationAssignments, "TeacherID", "Location", teacher.ID);
            return View(teacher);
        }

        // GET: Teacher/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID = new SelectList(db.LocationAssignments, "TeacherID", "Location", teacher.ID);
            return View(teacher);
        }

        // POST: Teacher/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,LastName,FirstMidName,HireDate")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teacher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID = new SelectList(db.LocationAssignments, "TeacherID", "Location", teacher.ID);
            return View(teacher);
        }

        // GET: Teacher/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // POST: Teacher/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Teacher teacher = db.Teachers.Find(id);
            db.Teachers.Remove(teacher);
            db.SaveChanges();
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
