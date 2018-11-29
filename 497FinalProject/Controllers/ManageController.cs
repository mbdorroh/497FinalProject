using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using _497FinalProject.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Web.Security;

namespace _497FinalProject.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ManageController()
        {
        }

        public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }


        //
        //GET: /Manage/Users/
        //[Authorize(Roles = "admin, IT")]
        public ActionResult Users()
        {
            var db = new ApplicationDbContext();
            var users = db.Users.ToList().OrderBy(u => u.UserName);

            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            Dictionary<string, string> roles = RoleManager.Roles.ToDictionary(r => r.Id, r => r.Name);

            var roleId = roles.Where(r => r.Value == "Professor").FirstOrDefault().Key;
            var professorUsers = users.Where(u => u.Roles.Any(r => r.RoleId == roleId)).ToList();

            //roleId = roles.Where(r => r.Value == "admin").FirstOrDefault().Key;
            //var adminUsers = users.Where(u => u.Roles.Any(r => r.RoleId == roleId)).ToList();

            //roleId = roles.Where(r => r.Value == "IT").FirstOrDefault().Key;
            //var itUsers = users.Where(u => u.Roles.Any(r => r.RoleId == roleId)).ToList();
            if (!RoleManager.RoleExists("Student"))
            {
                var createRoleResult = RoleManager.Create(new IdentityRole("Student"));
            }
            roleId = roles.Where(r => r.Value == "Student").FirstOrDefault().Key;
            var studentUsers = users.Where(u => u.Roles.Any(r => r.RoleId == roleId)).ToList();

            if (!RoleManager.RoleExists("TA"))
            {
                var createRoleResult = RoleManager.Create(new IdentityRole("TA"));
            }
            roleId = roles.Where(r => r.Value == "TA").FirstOrDefault().Key;
            var taUsers = users.Where(u => u.Roles.Any(r => r.RoleId == roleId)).ToList();

            
            var usersWithoutRole = users.Where(u => u.Roles.Count == 0).ToList();

            ViewBag.professorUsers = professorUsers;
            //ViewBag.adminUsers = adminUsers;
            //ViewBag.itUsers = itUsers;
            ViewBag.taUsers = taUsers;
            ViewBag.studentUsers = studentUsers;
            ViewBag.usersWithoutRole = usersWithoutRole;

            return View();
        }

        //
        //GET: /Manage/ResetUserPassword/userid
        //[Authorize(Roles = "IT")]
        public ActionResult ResetUserPassword(string Id)
        {
            UserManager<IdentityUser> userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>());

            userManager.RemovePassword(Id);

            userManager.AddPassword(Id, "Alabama2018");

            return RedirectToAction("Users");
        }


        //GET: /Manage/CreateNewUser
        //[Authorize(Roles = "IT")]
        public ActionResult CreateNewUser()
        {

            return View();
        }

        //
        ////POST: /Manage/CreateNewUser
        //[Authorize(Roles = "IT")]
        [HttpPost]
        public ActionResult CreateNewUser(FormCollection formData)
        {
            //Initialize DB
            var db = new ApplicationDbContext();

            //Initialize user/role managers
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            //Make sure role exists
            var roleName = formData["role"];
            if (!RoleManager.RoleExists(roleName))
            {
                var createRoleResult = RoleManager.Create(new IdentityRole(roleName));
            }

            //Build username
            var username = formData["FirstName"].ToLower()[0] + formData["LastName"].ToLower();

            if (db.Users.Any(u => u.UserName == username))
            {
                //increment username
                int i = 1;
                while (db.Users.Any(u => u.UserName == username + i))
                {
                    i++;
                }

                username = username + i;
            }

            //Generate password
            //string password = "Alabama2018";
            string password = Membership.GeneratePassword(12, 1);

            //Create admin
            var adminUser = new ApplicationUser { UserName = username, Email = formData["Email"], FirstName = formData["FirstName"], LastName = formData["LastName"]};
            
            var createUserResult = UserManager.Create(adminUser, password);

            //Add to admin role
            if (createUserResult.Succeeded)
            {
                var result = UserManager.AddToRole(adminUser.Id, roleName);
            }

            try
            {
                //Send Email
                var message = new MailMessage();
                message.To.Add(new MailAddress(adminUser.Email));  // replace with valid value 
                message.From = new MailAddress("mbdorroh@crimson.ua.edu");  // replace with valid value
                message.Subject = "IMPORTANT: MIS Forum Account Information";
                message.Body = string.Format("<p>You can access your account on our website with the following credentials:</p><ul><li><strong>username: </strong>" + adminUser.UserName + "</li><li><strong>password: </strong>" + password + "</li><br /><br />This is an automated email. PLEASE DO NOT REPLY");
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "",  // replace with valid value
                        Password = ""          // replace with valid value
                    };
                    smtp.Credentials = credential;
                    smtp.Host = "smtp-mail.outlook.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    smtp.Send(message);
                }
            }
            catch
            {

            }


            return RedirectToAction("Users");
        }

        // GET: Manage/DeleteUser/5
        //[Authorize(Roles = "IT")]
        public ActionResult DeleteUser(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Initialize db
            var db = new ApplicationDbContext();

            //Initialize user/role managers
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            //Get the user
            var user = UserManager.FindById(id);

            Dictionary<string, string> roles = RoleManager.Roles.ToDictionary(r => r.Id, r => r.Name);
            var roleId = roles.Where(r => r.Value == "Student").FirstOrDefault().Key;

         
            var rolesForUser = UserManager.GetRoles(id);

            if (rolesForUser.Count() > 0)
            {
                foreach (var item in rolesForUser.ToList())
                {
                    // item should be the name of the role
                    var result = UserManager.RemoveFromRole(user.Id, item);
                }
            }

            var logins = user.Logins;

            foreach (var login in logins.ToList())
            {
                UserManager.RemoveLogin(login.UserId, new UserLoginInfo(login.LoginProvider, login.ProviderKey));
            }

            UserManager.Delete(user);

            return RedirectToAction("Users");
        }

        //[Authorize(Roles = "IT")]
        public ActionResult AssignRoles(string id)
        {
            var db = new ApplicationDbContext();

            var user = db.Users.Find(id);

            ViewBag.FirstName = user.FirstName;
            ViewBag.LastName = user.LastName;

            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            Dictionary<string, string> roles = RoleManager.Roles.ToDictionary(r => r.Id, r => r.Name);

            bool isInProfessor = false;
            bool isInStudent = false;
            bool isInTA = false;
            

            //var roleId = roles.Where(r => r.Value == "admin").FirstOrDefault().Key;
            //if (user.Roles.Any(r => r.RoleId == roleId))
            //{
            //    isInAdmin = true;
            //}

           var roleId = roles.Where(r => r.Value == "Student").FirstOrDefault().Key;
            if (user.Roles.Any(r => r.RoleId == roleId))
            {
                isInStudent = true;
            }

            roleId = roles.Where(r => r.Value == "TA").FirstOrDefault().Key;
            if (user.Roles.Any(r => r.RoleId == roleId))
            {
                isInTA = true;
            }

            //roleId = roles.Where(r => r.Value == "IT").FirstOrDefault().Key;
            //if (user.Roles.Any(r => r.RoleId == roleId))
            //{
            //    isInIT = true;
            //}

            roleId = roles.Where(r => r.Value == "Professor").FirstOrDefault().Key;
            if (user.Roles.Any(r => r.RoleId == roleId))
            {
                isInProfessor = true;
            }

          

            ViewBag.IsInProfessor = isInProfessor;
            //ViewBag.IsInAdmin = isInAdmin;
            ViewBag.IsInStudent = isInStudent;
            ViewBag.IsInTA = isInTA;
            //ViewBag.IsInIT = isInIT;

            return View(user);
        }

        //[Authorize(Roles = "IT")]
        [HttpPost]
        public ActionResult AssignRoles(string id, FormCollection form)
        {
            var db = new ApplicationDbContext();

            var user = db.Users.Find(id);

            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            //if (form["isInIT"] != null)
            //{
            //    if (!UserManager.IsInRole(id, "IT"))
            //    {
            //        UserManager.AddToRole(id, "IT");
            //    }
            //}
            //else
            //{
            //    if (UserManager.IsInRole(id, "IT"))
            //    {
            //        UserManager.RemoveFromRole(id, "IT");
            //    }
            //}

            //if (form["isInAdmin"] != null)
            //{
            //    if (!UserManager.IsInRole(id, "admin"))
            //    {
            //        UserManager.AddToRole(id, "admin");
            //    }
            //}
            //else
            //{
            //    if (UserManager.IsInRole(id, "admin"))
            //    {
            //        UserManager.RemoveFromRole(id, "admin");
            //    }
            //}

            if (form["isInStudent"] != null)
            {
                if (!UserManager.IsInRole(id, "student"))
                {
                    UserManager.AddToRole(id, "student");
                }
            }
            else
            {
                if (UserManager.IsInRole(id, "student"))
                {
                    UserManager.RemoveFromRole(id, "student");
                }
            }

            if (form["isInTA"] != null)
            {
                if (!UserManager.IsInRole(id, "ta"))
                {
                    UserManager.AddToRole(id, "ta");
                }
            }
            else
            {
                if (UserManager.IsInRole(id, "ta"))
                {
                    UserManager.RemoveFromRole(id, "ta");
                }
            }

            if (form["isInProfessor"] != null)
            {
                if (!UserManager.IsInRole(id, "professor"))
                {
                    UserManager.AddToRole(id, "professor");
                }
            }
            else
            {
                if (UserManager.IsInRole(id, "professor"))
                {
                    UserManager.RemoveFromRole(id, "professor");
                }
            }

            return RedirectToAction("Users", "Manage");
        }



        //
        // GET: /Manage/Index
        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.SetTwoFactorSuccess ? "Your two-factor authentication provider has been set."
                : message == ManageMessageId.Error ? "An error has occurred."
                : message == ManageMessageId.AddPhoneSuccess ? "Your phone number was added."
                : message == ManageMessageId.RemovePhoneSuccess ? "Your phone number was removed."
                : "";

            var userId = User.Identity.GetUserId();
            var model = new IndexViewModel
            {
                HasPassword = HasPassword(),
                PhoneNumber = await UserManager.GetPhoneNumberAsync(userId),
                TwoFactor = await UserManager.GetTwoFactorEnabledAsync(userId),
                Logins = await UserManager.GetLoginsAsync(userId),
                BrowserRemembered = await AuthenticationManager.TwoFactorBrowserRememberedAsync(userId)
            };
            return View(model);
        }

        //
        // POST: /Manage/RemoveLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveLogin(string loginProvider, string providerKey)
        {
            ManageMessageId? message;
            var result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("ManageLogins", new { Message = message });
        }

        //
        // GET: /Manage/AddPhoneNumber
        public ActionResult AddPhoneNumber()
        {
            return View();
        }

        //
        // POST: /Manage/AddPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddPhoneNumber(AddPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            // Generate the token and send it
            var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), model.Number);
            if (UserManager.SmsService != null)
            {
                var message = new IdentityMessage
                {
                    Destination = model.Number,
                    Body = "Your security code is: " + code
                };
                await UserManager.SmsService.SendAsync(message);
            }
            return RedirectToAction("VerifyPhoneNumber", new { PhoneNumber = model.Number });
        }

        //
        // POST: /Manage/EnableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EnableTwoFactorAuthentication()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), true);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", "Manage");
        }

        //
        // POST: /Manage/DisableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DisableTwoFactorAuthentication()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), false);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", "Manage");
        }

        //
        // GET: /Manage/VerifyPhoneNumber
        public async Task<ActionResult> VerifyPhoneNumber(string phoneNumber)
        {
            var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), phoneNumber);
            // Send an SMS through the SMS provider to verify the phone number
            return phoneNumber == null ? View("Error") : View(new VerifyPhoneNumberViewModel { PhoneNumber = phoneNumber });
        }

        //
        // POST: /Manage/VerifyPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyPhoneNumber(VerifyPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePhoneNumberAsync(User.Identity.GetUserId(), model.PhoneNumber, model.Code);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.AddPhoneSuccess });
            }
            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "Failed to verify phone");
            return View(model);
        }

        //
        // POST: /Manage/RemovePhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemovePhoneNumber()
        {
            var result = await UserManager.SetPhoneNumberAsync(User.Identity.GetUserId(), null);
            if (!result.Succeeded)
            {
                return RedirectToAction("Index", new { Message = ManageMessageId.Error });
            }
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", new { Message = ManageMessageId.RemovePhoneSuccess });
        }

        //
        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);
            return View(model);
        }

        //
        // GET: /Manage/SetPassword
        public ActionResult SetPassword()
        {
            return View();
        }

        //
        // POST: /Manage/SetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                    if (user != null)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    }
                    return RedirectToAction("Index", new { Message = ManageMessageId.SetPasswordSuccess });
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Manage/ManageLogins
        public async Task<ActionResult> ManageLogins(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return View("Error");
            }
            var userLogins = await UserManager.GetLoginsAsync(User.Identity.GetUserId());
            var otherLogins = AuthenticationManager.GetExternalAuthenticationTypes().Where(auth => userLogins.All(ul => auth.AuthenticationType != ul.LoginProvider)).ToList();
            ViewBag.ShowRemoveButton = user.PasswordHash != null || userLogins.Count > 1;
            return View(new ManageLoginsViewModel
            {
                CurrentLogins = userLogins,
                OtherLogins = otherLogins
            });
        }

        //
        // POST: /Manage/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new AccountController.ChallengeResult(provider, Url.Action("LinkLoginCallback", "Manage"), User.Identity.GetUserId());
        }

        //
        // GET: /Manage/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
            }
            var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
            return result.Succeeded ? RedirectToAction("ManageLogins") : RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

#region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private bool HasPhoneNumber()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PhoneNumber != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }

#endregion
    }
}