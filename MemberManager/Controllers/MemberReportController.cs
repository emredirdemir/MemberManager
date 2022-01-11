using MemberManagement.entity;
using MemberManager.DataAccess;
using MemberManager.Filter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MemberManager.Controllers
{
    public class MemberReportController : Controller
    {
        MemberReportCRUD _dao = new MemberReportCRUD();

        public ActionResult Index()
        {
            var data = new MemberReportCRUD();
            var response = data.GetMemberReports();
            return View(response);
        }

        [UserFilter]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MemberReport report)
        {
            try
            {
                _dao.CreateMemberReports(report);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        [UserFilter]
        public ActionResult Edit(int id)
        {
            MemberReport data = _dao.GetMemberReportsById(id);
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MemberReport report)
        {
            _dao.UpdateMemberReports(report);
            return RedirectToAction(nameof(Index));
        }

        [UserFilter]
        public ActionResult Delete(int id)
        {
            _dao.delete(id);
            return RedirectToAction(nameof(Index));

        }
    }
}
