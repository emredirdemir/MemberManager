using MemberManagement.entity;
using MemberManager.DataAccess;
using MemberManager.Filter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MemberManager.Controllers
{
    [AdminFilter]
    public class InstructorController : Controller
    {
        InstructorCRUD _data = new InstructorCRUD();
        public ActionResult Index()
        {
            var response = _data.GetInstructors();
            return View(response);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Instructor Instructor)
        {
            _data.CreateInstructors(Instructor);
            return RedirectToAction(nameof(Index));

        }

        public ActionResult Edit(int id)
        {
            var response = _data.GetInstructorsById(id);
            return View(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Instructor Instructor)
        {
            _data.UpdateInstructors(Instructor);
            return RedirectToAction(nameof(Index));
        }

        public ActionResult Delete(int id)
        {
            _data.Delete(id);
            return RedirectToAction(nameof(Index));

        }

    }
}
