using MemberManager.DataAccess;
using MemberManager.Filter;
using MemberManager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MemberManager.Controllers
{

    [AdminFilter]
    public class SessionController : Controller
    {
        SessionCRUD _data = new SessionCRUD();
        public ActionResult Index()
        {
            var response = _data.GetSessions();
            return View(response);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Session session)
        {
            _data.CreateSessions(session);
            return RedirectToAction(nameof(Index));

        }

        public ActionResult Edit(int id)
        {
            var response = _data.GetSessionsById(id);
            return View(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Session session)
        {
            _data.UpdateSessions(session);
            return RedirectToAction(nameof(Index));
        }

        public ActionResult Delete(int id)
        {
            _data.Delete(id);
            return RedirectToAction(nameof(Index));

        }

    }
}
