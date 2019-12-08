using MTS.Data;
using MTS.Helper;
using MTS.Model.Models.Account;
using MTS.Model.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MTS.Controllers
{
    public class AccountController : Controller
    {
        #region Global
        private UserManager<ApplicationUser> _userManager;
        private UserStore<ApplicationUser> _store;
        private readonly Message _message = new Message();
        #endregion
        public AccountController()
        {

        }

        private IAuthenticationManager _authnManager;
        public IAuthenticationManager AuthenticationManager
        {
            get
            {
                if (_authnManager == null)
                    _authnManager = HttpContext.GetOwinContext().Authentication;
                return _authnManager;
            }
            set { _authnManager = value; }
        }

        // GET: Account
        public ActionResult Register()
        {
            return View();
        }


        private IEnumerable<IdentityRole> _GetRoleList(EasyContext db)
        {
            var roleStore = new RoleStore<IdentityRole>(db);
            var roleMngr = new RoleManager<IdentityRole>(roleStore);
            var roles = roleMngr.Roles.ToList();
            return roles;
        }

        #region Login Get
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login(string returnUrl, string id)
        {
            AuthenticationManager.SignOut();
            //SmartSession.Clear();
            ViewBag.ReturnUrl = returnUrl;

            return View();
        }
        #endregion

        #region Login Post
        public async Task<ActionResult> Login(VMLogin model, string returnUrl)
        {
            if (!ModelState.IsValid) return View(model);
            EasyContext db = new EasyContext();
            _store = new UserStore<ApplicationUser>(db);
            _userManager = new UserManager<ApplicationUser>(this._store);
            var user = _userManager.Find(model.UserName, model.Password);

            if (user != null)
            {
                await SignInAsync(user, model.RememberMe, db);
                var role = _userManager.FindById(user.Id).Roles.Select(r => r.RoleId).FirstOrDefault();
                role.ToString();

                var userRole = db.Database.SqlQuery<UserRole>("SELECT TOP(1) ur.RoleId,r.Name FROM UserRoles AS ur INNER JOIN Roles AS r on r.Id=ur.RoleId WHERE UserId='" + user.Id + "'").FirstOrDefault();
                EasySession.RoleId = userRole.RoleId;
                EasySession.RoleName = userRole.Name;
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", @"Invalid username or password.");
            return View(model);

        }
        #endregion

        #region Log off
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();

            EasySession.Clear();
            return RedirectToAction("Login", "Account");
        }
        #endregion

        private async Task SignInAsync(ApplicationUser user, bool isPersistent, EasyContext db)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            _store = new UserStore<ApplicationUser>(db);
            _userManager = new UserManager<ApplicationUser>(this._store);
            var identity = await _userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);

        }

    }
}