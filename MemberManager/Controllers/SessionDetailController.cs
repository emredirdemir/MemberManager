using MemberManager.DataAccess;
using MemberManager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MemberManager.Controllers
{
    public class SessionMemberDetailController : Controller
    {
        SessionMemberCRUD _data = new SessionMemberCRUD();
        public ActionResult Index()
        {
            var response = _data.GetSessionMembers();
            return View(response);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SessionMember SessionMember)
        {
            _data.CreateSessionMembers(SessionMember);
            return RedirectToAction(nameof(Index));

        }

        public ActionResult Edit(int id)
        {
            var response = _data.GetSessionMembersById(id);
            return View(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SessionMember SessionMember)
        {
            _data.UpdateSessionMembers(SessionMember);
            return RedirectToAction(nameof(Index));
        }

        public ActionResult Delete(int id)
        {
            _data.Delete(id);
            return RedirectToAction(nameof(Index));

        }
    }
}
