using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using MultiPART.Models;
using System.Net.Mail;
using MultiPART.Models.ViewModel;
using MultiPART.Models.LinkTable;
using System.Net;
using MultiPART.Authorization;

namespace MultiPART.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private MultipartContext db = new MultipartContext();

        //
        // GET: /Account/Users
        [Authorize(Roles = "Administrator, Superuser")]
        [Restrict("Disabled")]
        public ActionResult Users(string filterRole = "", string sortOrder = "UserName")
        {
            SelectList allroles = new SelectList(Roles.GetAllRoles());
            ViewBag.Roles = allroles;
            ViewBag.filterRole = filterRole;
            var userlist = (from u in db.UserProfiles
                            where u.Status == "Current"
                            orderby u.UserName
                            select u).AsEnumerable()
                           .Select(u => new UserViewModel
                           {
                               UserId = u.UserId,
                               UserName = u.UserName,
                               ForeName = u.ForeName,
                               SurName = u.SurName,
                               Email = u.Email,
                               Details = u.Details,
                               CreatedOn = u.CreatedOn.LocalDateTime,
                               Roles = String.Join(", ", Roles.GetRolesForUser(u.UserName))
                           });

            if (filterRole == "") filterRole = "norole";
            var searchRecord = from s in userlist
                               where filterRole == "norole" || Roles.IsUserInRole(s.UserName, filterRole)
                               select s;

            var result = searchRecord;

            switch (sortOrder)
            {
                case "UserName":
                    result = result.OrderBy(s => s.UserName);
                    break;
                case "UserName desc":
                    result = result.OrderByDescending(s => s.UserName);
                    break;
                case "ForeName":
                    result = result.OrderBy(s => s.ForeName);
                    break;
                case "ForeName desc":
                    result = result.OrderByDescending(s => s.ForeName);
                    break;
                case "SurName":
                    result = result.OrderBy(s => s.SurName);
                    break;
                case "SurName desc":
                    result = result.OrderByDescending(s => s.SurName);
                    break;
                case "Email":
                    result = result.OrderBy(s => s.Email);
                    break;
                case "Email desc":
                    result = result.OrderByDescending(s => s.Email);
                    break;
                case "Details":
                    result = result.OrderBy(s => s.Email);
                    break;
                case "Details desc":
                    result = result.OrderByDescending(s => s.Email);
                    break;
                case "CreatedOn":
                    result = result.OrderBy(s => s.CreatedOn);
                    break;
                case "CreatedOn desc":
                    result = result.OrderByDescending(s => s.CreatedOn);
                    break;
                case "Roles":
                    result = result.OrderBy(s => s.CreatedOn);
                    break;
                case "Roles desc":
                    result = result.OrderByDescending(s => s.CreatedOn);
                    break;
                default:
                    result = result.OrderBy(s => s.UserName);
                    break;
            }

            ViewBag.sortOrder = sortOrder;

            return View(result.ToList());
        }

        //
        // GET: /Account/Profile
        [Authorize]
        [Restrict("Disabled")]
        public ActionResult ViewProfile(string username = null)
        {
            if (username == null) username = User.Identity.Name;

            UserProfile userprofile = db.UserProfiles.Where(u => u.UserName == username && u.Status == "Current").FirstOrDefault();

            var roles = Roles.GetRolesForUser(username).ToList();
            ViewBag.RolesForThisUser = "";
            foreach (var role in roles)
            {
                ViewBag.RolesForThisUser = ViewBag.RolesForThisUser + role;
                if (role != roles.Last())
                {
                    ViewBag.RolesForThisUser = ViewBag.RolesForThisUser + ", ";
                }
            }

            ViewBag.ResearchHistory = from lui in db.Careerhistories.Include("Institutions")
                                      where lui.UserProfile.UserName == username && lui.Status == "Current"
                                      orderby lui.StartTime descending
                                      select new CareerhistoryViewModel
                                      {
                                          CareerhistoryID = lui.CareerhistoryID,
                                          InstitutionName = lui.Institutions.InstitutionName,
                                          Position = lui.Position,
                                          StartTime = lui.StartTime,
                                          EndTime = lui.EndTime,
                                          UserName = lui.UserProfile.UserName,
                                          UserId = lui.UserProfileUserId
                                      };

            ViewBag.UserProject = from up in db.UserProjectAssignments
                                  join ur in db.UserInResearchgroups.Include("Researchgroup").Include("Institution") on up.UserInResearchgroupUserInResearchgroupID equals ur.UserInResearchgroupID
                                  join rp in db.ResearchgroupInMultiPARTProjects.Include("MultiPARTProjects") on up.ResearchgroupInMultiPARTProjectResearchgroupInMultiPARTProjectID equals rp.ResearchgroupInMultiPARTProjectID
                                  where up.Status == "Current" && ur.Status == "Current" && rp.Status == "Current" &&  rp.MultiPartProject.Status == "Current"
                                  && ur.UserProfiles.UserName == User.Identity.Name && rp.ResearchgroupResearchgroupID == ur.ResearchgroupResearchgroupID
                                  orderby up.CreatedOn descending
                                  select new UserProjectViewModel
                                  {
                                      Project = rp.MultiPartProject.MultiPARTProjectName,
                                      Researchgroup = ur.Researchgroups.ResearchgroupName,
                                      Role = up.Options.OptionValue,
                                      ProjectStatus = (rp.MultiPartProject.ProjectComletionDate == null) ? "Current":"Completed",
                                      ProjectID = rp.MultiPARTProjectMultiPARTProjectID,
                                  };

            if (db.Careerhistories.Where(c => c.Status == "Current" && c.UserProfile.UserName == username).Count() == 0) ViewBag.actionneeded = true;

            if (userprofile == null)
            {
                return HttpNotFound();
            }

            if (userprofile.Status != "Current")
            {
                return HttpNotFound();
            }

            return View(userprofile);
        }

        //
        //GET: /Account/Edit?username = username
            [Authorize]
            [Restrict("Disabled")]
        public ActionResult Edit(string username)
        {
            UserProfile userprofile;
            if (User.IsInRole("Administrator, Superuser"))
            {
                userprofile = db.UserProfiles.Find((int)Membership.GetUser(username).ProviderUserKey);
            }
            else
            {
                if (username == User.Identity.Name)
                {
                    userprofile = db.UserProfiles.Find((int)Membership.GetUser().ProviderUserKey);
                }
                else
                {
                    return HttpNotFound();
                }
            }

            if (userprofile == null)
            {
                return HttpNotFound();
            }

            if (userprofile.Status != "Current")
            {
                return HttpNotFound();
            }

            return View(userprofile);
        }

        //
        // POST: /Review/Edit/?username = username
            [Authorize]
            [Restrict("Disabled")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserProfile userprofile)
        {
            UserProfile newprofile = db.UserProfiles.Find(userprofile.UserId);

            if (ModelState.IsValid)
            {
                newprofile.ForeName = userprofile.ForeName;
                newprofile.SurName = userprofile.SurName;
                newprofile.Email = userprofile.Email;
                newprofile.Details = userprofile.Details;
                newprofile.LastUpdatedBy = userprofile.UserId;
                //newprofile.LastUpdatedOn = DateTimeOffset.Now;
                //db.Entry(userprofile).State = EntityState.Modified;

                db.SaveChanges();
                if (User.IsInRole("Administrator"))
                {
                    return RedirectToAction("Users");
                }
                return RedirectToAction("ViewProfile");
            }

            return View(userprofile);
        }

        //
        // GET: /Institution/Create
            [Authorize]
            [Restrict("Disabled")]
        public ActionResult CreateResearchHistory(string username)
        {
            ViewBag.InstitutionList = new SelectList(db.Institutions.Where(i => i.Status == "Current").OrderBy(i => i.InstitutionName).AsEnumerable(), "InstitutionID", "InstitutionName");
            ViewBag.username = username;

            return View();
        }

        //
        // POST: /Institution/Create       
            [Authorize]
            [Restrict("Disabled")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateResearchHistory(CareerhistoryViewModel careerhistoryVM)
        {
            if (ModelState.IsValid)
            {
                if (careerhistoryVM.StartTime > careerhistoryVM.EndTime)
                {
                    ModelState.AddModelError(string.Empty, "Start date must be before end date");
                    ViewBag.InstitutionList = new SelectList(db.Institutions.Where(i => i.Status == "Current").OrderBy(i => i.InstitutionName).AsEnumerable(), "InstitutionID", "InstitutionName", careerhistoryVM.InstitutionID);
                    return View(careerhistoryVM);
                }

                Careerhistory careerhistory = new Careerhistory();
                careerhistory.InstitutionInstitutionID = careerhistoryVM.InstitutionID;
                careerhistory.UserProfileUserId = (int)Membership.GetUser(careerhistoryVM.UserName).ProviderUserKey;
                careerhistory.StartTime = careerhistoryVM.StartTime;
                careerhistory.EndTime = careerhistoryVM.EndTime;
                careerhistory.Position = careerhistoryVM.Position;

                careerhistory.Status = "Current";
                careerhistory.LastUpdatedBy = (int)Membership.GetUser().ProviderUserKey;
                careerhistory.CreatedBy = (int)Membership.GetUser().ProviderUserKey;
                careerhistory.CreatedOn = DateTimeOffset.Now;
                db.Careerhistories.Add(careerhistory);
                db.SaveChanges();
                return RedirectToAction("ViewProfile", new { username = careerhistoryVM.UserName });
            }

            ViewBag.InstitutionList = new SelectList(db.Institutions.Where(i => i.Status == "Current").OrderBy(i => i.InstitutionName).AsEnumerable(), "InstitutionID", "InstitutionName", careerhistoryVM.InstitutionID);
            return View(careerhistoryVM);
        }

        //
        //GET: /Account/EditResearchHistory?username = username

            [Authorize]
            [Restrict("Disabled")]
        public ActionResult EditResearchHistory(int id)
        {
            Careerhistory careerhistory = db.Careerhistories.Find(id);
            if (careerhistory == null) return HttpNotFound();
            else
            {
                if (User.IsInRole("Administrator") || User.IsInRole("Superuser") || (careerhistory.UserProfileUserId == (int)Membership.GetUser().ProviderUserKey))
                {
                    CareerhistoryViewModel careerhistoryVM = new CareerhistoryViewModel
                    {
                        UserName = careerhistory.UserProfile.UserName,
                        InstitutionID = careerhistory.InstitutionInstitutionID,
                        StartTime = careerhistory.StartTime,
                        EndTime = careerhistory.EndTime,
                        CareerhistoryID = careerhistory.CareerhistoryID,
                        InstitutionName = careerhistory.Institutions.InstitutionName,
                        Position = careerhistory.Position,
                        UserId = careerhistory.UserProfileUserId
                    };

                    ViewBag.InstitutionList = new SelectList(db.Institutions.Where(i => i.Status == "Current").OrderBy(i => i.InstitutionName).AsEnumerable(), "InstitutionID", "InstitutionName", careerhistoryVM.InstitutionID);

                    return View(careerhistoryVM);
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }

        //
        // POST: /Review/EditResearchHistory/
            [Authorize]
            [Restrict("Disabled")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditResearchHistory(CareerhistoryViewModel careerhistoryVM)
        {
            if (careerhistoryVM.StartTime > careerhistoryVM.EndTime)
            {
                ModelState.AddModelError(string.Empty, "Start date must be before end date");
                ViewBag.InstitutionList = new SelectList(db.Institutions.Where(i => i.Status == "Current").OrderBy(i => i.InstitutionName).AsEnumerable(), "InstitutionID", "InstitutionName", careerhistoryVM.InstitutionID);
                return View(careerhistoryVM);
            }

            Careerhistory careerhistory = db.Careerhistories.Find(careerhistoryVM.CareerhistoryID);

            if (ModelState.IsValid)
            {
                careerhistory.InstitutionInstitutionID = careerhistoryVM.InstitutionID;
                careerhistory.StartTime = careerhistoryVM.StartTime;
                careerhistory.EndTime = careerhistoryVM.EndTime;
                careerhistory.Position = careerhistoryVM.Position;
                careerhistory.LastUpdatedBy = (int)Membership.GetUser().ProviderUserKey;
                db.SaveChanges();
                return RedirectToAction("ViewProfile");
            }

            return View(careerhistoryVM);
        }

        //
        // POST: /Review/EditResearchHistory/       
            [Authorize]
            [Restrict("Disabled")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteResearchHistory(int id = 0)
        {
            Careerhistory careerhistory = db.Careerhistories.Find(id);

            if (ModelState.IsValid)
            {
                if (User.IsInRole("Administrator") || User.IsInRole("Superuser") || (careerhistory.UserProfile.UserName == User.Identity.Name))
                {
                    careerhistory.Status = "Deleted";
                    careerhistory.LastUpdatedBy = (int)Membership.GetUser().ProviderUserKey;
                    db.SaveChanges();
                }
            }

            return RedirectToAction("ViewProfile", new { username = careerhistory.UserProfile.UserName });
        }

        //
        // GET: /Account/Roles/5
            [Authorize(Roles = "Administrator, Superuser")]
            [Restrict("Disabled")]
        public ActionResult ManageRoles(string username)
        {
            UserProfile userprofile = db.UserProfiles.Find((int)Membership.GetUser(username).ProviderUserKey);

            if (userprofile == null | userprofile.Status != "Current")
            {
                return HttpNotFound();
            }

            ViewBag.UserName = userprofile.UserName;
            ViewBag.UserId = userprofile.UserId;
            if (TempData["ResultMessage"] != null) ViewBag.ResultMessage = TempData["ResultMessage"];

            string[] RolesForThisUser = Roles.GetRolesForUser(userprofile.UserName);
            ViewBag.RolesForThisUser = RolesForThisUser;

            SelectList allroles = new SelectList(Roles.GetAllRoles());
            ViewBag.Roles = allroles;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Superuser")]
        [Restrict("Disabled")]
        public ActionResult AddRoles(string RoleName, string UserName, string UserId)
        {
            if (RoleName != "")
            {
                if (Roles.IsUserInRole(UserName, RoleName))
                {
                    TempData["ResultMessage"] = "This user is already in this role.";
                }
                else
                {
                    Roles.AddUserToRole(UserName, RoleName);
                    TempData["ResultMessage"] = "Role added for this user successfully!";
                }
            }
            return RedirectToAction("ManageRoles", new { username = UserName });
        }

        //
        // POST: /Review/Delete/5
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Superuser")]
        [Restrict("Disabled")]
        public ActionResult DeleteRole(string UserId, string UserName, string RoleName)
        {
            if (Roles.IsUserInRole(UserName, RoleName))
            {
                Roles.RemoveUserFromRole(UserName, RoleName);
                TempData["ResultMessage"] = "Role removed from this user successfully!";
            }
            else
            {
                TempData["ResultMessage"] = "This user doesn't belong to selected role.";
            }

            ViewBag.RolesForThisUser = Roles.GetRolesForUser(UserName);
            SelectList list = new SelectList(Roles.GetAllRoles());
            TempData["AllRoles"] = list;
            ViewBag.Roles = list;

            //return View("ManageRoles"); 

            return RedirectToAction("ManageRoles", new { username = UserName });
        }

        //
        // GET: /Account/Login

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid && WebSecurity.Login(model.UserName, model.Password, persistCookie: model.RememberMe))
            {
                return RedirectToLocal(returnUrl);
            }

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(model);
        }

        //
        // POST: /Account/LogOff

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult LogOff()
        {
            WebSecurity.Logout();

            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                try
                {
                    WebSecurity.CreateUserAndAccount(model.UserName, model.Password,
                           new
                           {
                               Email = model.Email
                               ,
                               ForeName = model.ForeName
                               ,
                               SurName = model.SurName
                               ,
                               Details = model.Details
                               ,
                               CreatedOn = DateTimeOffset.Now
                               ,
                               Status = "Current"
                               ,
                               LastUpdatedOn = DateTimeOffset.Now
                           });

                    WebSecurity.Login(model.UserName, model.Password);

                    return RedirectToAction("ViewProfile");
                    }

                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePassword
  
        [Authorize]
        [Restrict("Disabled")]
        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : "";
            ViewBag.HasLocalPassword = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }

        //
        // POST: /Account/Manage
      
        [Authorize]
        [Restrict("Disabled")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Manage(LocalPasswordModel model)
        {
            bool hasLocalAccount = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            ViewBag.HasLocalPassword = hasLocalAccount;
            ViewBag.ReturnUrl = Url.Action("Manage");
            if (hasLocalAccount)
            {
                if (ModelState.IsValid)
                {
                    // ChangePassword will throw an exception rather than return false in certain failure scenarios.
                    bool changePasswordSucceeded;
                    try
                    {
                        changePasswordSucceeded = WebSecurity.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword);
                    }
                    catch (Exception)
                    {
                        changePasswordSucceeded = false;
                    }

                    if (changePasswordSucceeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "The current password is incorrect or the new password is invalid.");
                    }
                }
            }
            else
            {
                // User does not have a local password so remove any validation errors caused by a missing
                // OldPassword field
                ModelState state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        WebSecurity.CreateAccount(User.Identity.Name, model.NewPassword);
                        return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError("", String.Format("Unable to create local account. An account with the name \"{0}\" may already exist.", User.Identity.Name));
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/LostPassword

        [AllowAnonymous]
        public ActionResult LostPassword()
        {
            ViewBag.sent = "no";
            return View();
        }

        //
        // POST: Account/LostPassword

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult LostPassword(LostPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                MembershipUser user;
                using (var context = new MultipartContext())
                {
                    var foundUserName = (from u in context.UserProfiles
                                         where u.Email == model.Email
                                         select u.UserName).FirstOrDefault();

                    if (foundUserName != null)
                    {
                        user = Membership.GetUser(foundUserName.ToString());
                    }
                    else
                    {
                        user = null;
                    }
                }
                if (user != null)
                {
                    // Generae password token that will be used in the email link to authenticate user
                    var token = WebSecurity.GeneratePasswordResetToken(user.UserName);
                    // Generate the html link sent via email
                    string resetLink = "<a href='" + Url.Action("ResetPassword", "Account", new { rt = token }, "http") + "'>Reset Password Link</a><br/>";

                    MailMessage mail = new MailMessage();

                    mail.To.Add(new MailAddress(model.Email));
                    mail.From = new MailAddress("Multipart.Team@ed.ac.uk");
                    mail.Bcc.Add(new MailAddress("multipart.camarades@gmail.com"));
                    mail.Subject = "Reset your password for Multi-PART application website";
                    mail.Body = "We have received a request to reset your password on Multi-PART application website. Please ignore this email if the request was not sent by you. <br/> If you want to reset your password, please click on the link: " + resetLink + "<br><br/> Thank you, <br/>Multi-PART Group";

                    mail.IsBodyHtml = true;

                    SmtpClient smtp = new SmtpClient();

                    // Attempt to send the email
                    try
                    {
                        smtp.Send(mail);
                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError("", "Issue sending email: " + e.Message + "<br/>");
                    }
                }
            }
            ViewBag.sent = "yes";
            return View(model);
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string rt)
        {
            ViewBag.Message = "";
            ResetPasswordModel model = new ResetPasswordModel();
            model.ReturnToken = rt;
            return View(model);
        }

        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                bool resetResponse = WebSecurity.ResetPassword(model.ReturnToken, model.Password);
                if (resetResponse)
                {
                    ViewBag.Message = "Successfully Changed";
                }
                else
                {
                    ViewBag.Message = "Something went wrong!";
                }
            }
            return View(model);
        }

        // POST: /Account/SuspendUser

        #region Helpers
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }

        private static bool IsPrivateIpAddress(string ipAddress)
        {
            // http://en.wikipedia.org/wiki/Private_network
            // Private IP Addresses are: 
            //  24-bit block: 10.0.0.0 through 10.255.255.255
            //  20-bit block: 172.16.0.0 through 172.31.255.255
            //  16-bit block: 192.168.0.0 through 192.168.255.255
            //  Link-local addresses: 169.254.0.0 through 169.254.255.255 (http://en.wikipedia.org/wiki/Link-local_address)

            var ip = IPAddress.Parse(ipAddress);
            var octets = ip.GetAddressBytes();

            var is24BitBlock = octets[0] == 10;
            if (is24BitBlock) return true; // Return to prevent further processing

            var is20BitBlock = octets[0] == 172 && octets[1] >= 16 && octets[1] <= 31;
            if (is20BitBlock) return true; // Return to prevent further processing

            var is16BitBlock = octets[0] == 192 && octets[1] == 168;
            if (is16BitBlock) return true; // Return to prevent further processing

            var isLinkLocalAddress = octets[0] == 169 && octets[1] == 254;
            return isLinkLocalAddress;
        }

        private string getipaddress()
        {
            string szRemoteAddr = Request.UserHostAddress;
            string szXForwardedFor = Request.ServerVariables["X_FORWARDED_FOR"];
            string szIP = "";

            if (szXForwardedFor == null)
            {
                szIP = szRemoteAddr;
            }
            else
            {
                szIP = szXForwardedFor;
                if (szIP.IndexOf(",") > 0)
                {
                    string[] arIPs = szIP.Split(',');

                    foreach (string item in arIPs)
                    {
                        if (!IsPrivateIpAddress(item))
                        {
                            return item;
                        }
                    }
                }
            }
            return szIP;
        }

        private string logUserAccess(string username)
        {
            var ip = getipaddress();
            var filename = AppDomain.CurrentDomain.BaseDirectory + "logs\\" + "logs.txt";
            var sw = new System.IO.StreamWriter(filename, true);

            try
            {
                sw.WriteLine(DateTime.Now.ToString() + "               " + ip + "               " + username);
            }
            catch (Exception e)
            {
                throw new Exception("writing to logs error: " + username + " : Error : " + e);
            }
            finally
            {
                sw.Close();
            }

            return ip;
        }




        #endregion

    }
}
