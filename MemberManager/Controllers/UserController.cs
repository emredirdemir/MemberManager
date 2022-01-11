using MemberManagement.entity;
using MemberManager.DataAccess;
using MemberManager.Filter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MemberManager.Controllers
{
    public class UserController : Controller
    {
        UserCRUD _dao = new UserCRUD();

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User user)
        {

            var users = _dao.GetUsers();
            var controll = users.FirstOrDefault(x => x.UserName == user.UserName && x.Password == user.Password);
            if (controll != null)
            {
                HttpContext.Session.SetString("username", user.UserName);
                HttpContext.Session.SetString("role", controll.Role);
                return RedirectToAction("Index", "Home");
            }
            return View();

        }
        [AdminFilter]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User user)
        {
            try
            {
                user.Role = "User";
                _dao.CreateUsers(user);
                return RedirectToAction(nameof(Login));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction(nameof(Login));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteUser(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
