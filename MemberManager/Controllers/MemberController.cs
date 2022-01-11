using MemberManagement.entity;
using MemberManager.DataAccess;
using MemberManager.Filter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MemberManager.Controllers
{
    public class MemberController : Controller
    {
        MemberCRUD _data = new MemberCRUD();
        public ActionResult Index()
        {
            var response = _data.GetMembers();
            return View(response);
        }

        [UserFilter]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Member member)
        {
            _data.CreateMembers(member);
            return RedirectToAction(nameof(Index));

        }

        [UserFilter]
        public ActionResult Edit(int id)
        {
            var response = _data.GetMembersById(id);
            return View(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Member member)
        {
            _data.UpdateMembers(member);
            return RedirectToAction(nameof(Index));
        }

        [UserFilter]
        public ActionResult Delete(int id)
        {
            _data.delete(id);
            return RedirectToAction(nameof(Index));

        }
    }
}
