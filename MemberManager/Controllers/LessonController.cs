using MemberManagement.entity;
using MemberManager.DataAccess;
using MemberManager.Filter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MemberManager.Controllers
{
    [AdminFilter]
    public class LessonController : Controller
    {
        LessonCRUD _data = new LessonCRUD();
        public ActionResult Index()
        {
            var response = _data.GetLessons();
            return View(response);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Lesson Lesson)
        {
            _data.CreateLessons(Lesson);
            return RedirectToAction(nameof(Index));

        }

        public ActionResult Edit(int id)
        {
            var response = _data.GetLessonsById(id);
            return View(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Lesson Lesson)
        {
            _data.UpdateLessons(Lesson);
            return RedirectToAction(nameof(Index));
        }

        public ActionResult Delete(int id)
        {
            _data.Delete(id);
            return RedirectToAction(nameof(Index));

        }

    }
}
