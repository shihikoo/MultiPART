using MultiPART.Authorization;
using MultiPART.Models;
using MultiPART.Models.LinkTable;
using MultiPART.Models.ViewModel;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace MultiPART.Controllers
{
    [Authorize(Roles = "Administrator, Superuser")]
    [Restrict("Disabled")]
    public class ResearchgroupController : Controller
    {
        private MultipartContext db = new MultipartContext();

        //
        // GET: /Researchgroup/

        public ActionResult Index(string sortOrder = "ResearchgroupName")
        {

            var researchgroups = from r in db.Researchgroups
                                 where r.Status == "Current"
                                 orderby r.ResearchgroupName
                                 select new ResearchgroupListViewModel
                                 {
                                     ResearchgroupID = r.ResearchgroupID,
                                     ResearchgroupName = r.ResearchgroupName,
                                     InstitutionName = r.Institutions.InstitutionName,
                                     CreatedOn = r.CreatedOn,
                                     Country = r.Institutions.Countries.CountryName
                                 };

            var result = researchgroups;

            switch (sortOrder)
            {
                case "ResearchgroupName":
                    result = result.OrderBy(s => s.ResearchgroupName);
                    break;
                case "ResearchgroupName desc":
                    result = result.OrderByDescending(s => s.ResearchgroupName);
                    break;
                case "InstitutionName":
                    result = result.OrderBy(s => s.InstitutionName);
                    break;
                case "InstitutionName desc":
                    result = result.OrderByDescending(s => s.InstitutionName);
                    break;
                case "Country":
                    result = result.OrderBy(s => s.Country);
                    break;
                case "Country desc":
                    result = result.OrderByDescending(s => s.Country);
                    break;
                case "CreatedOn":
                    result = result.OrderBy(s => s.CreatedOn);
                    break;
                case "CreatedOn desc":
                    result = result.OrderByDescending(s => s.CreatedOn);
                    break;
                default:
                    result = result.OrderBy(s => s.ResearchgroupName);
                    break;
            }

            ViewBag.sortOrder = sortOrder;

            return View(result.ToList());

        }

        //
        // GET: /Researchgroup/Create

        public ActionResult Create()
        {
            ViewBag.InstitutionList = new SelectList(db.Institutions.Where(c => c.Status == "Current").OrderBy(c => c.InstitutionName).AsEnumerable(), "InstitutionID", "InstitutionName");

            return View();
        }

        //
        // POST: /Researchgroup/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ResearchgroupViewModel ResearchgroupVM)
        {

            if (!ModelState.IsValid)
            {

                ViewBag.InstitutionList = new SelectList(db.Institutions.Where(c => c.Status == "Current").OrderBy(c => c.InstitutionName).AsEnumerable(), "InstitutionID", "InstitutionName", ResearchgroupVM.InstitutionID);
                return View(ResearchgroupVM);
            }
            
                Researchgroup researchgroup = new Researchgroup()
                {
                    ResearchgroupName = ResearchgroupVM.ResearchgroupName,
                    InstitutionInstitutionID = ResearchgroupVM.InstitutionID,
                    LastUpdatedBy = (int)Membership.GetUser().ProviderUserKey,
                    CreatedOn = DateTimeOffset.Now,
                    CreatedBy = (int)Membership.GetUser().ProviderUserKey,
                };

                db.Researchgroups.Add(researchgroup);
                db.SaveChanges();

                return RedirectToAction("Index");
            

        }

        //
        // GET: /Researchgroup/Edit/5

        public ActionResult Edit(int id)
        {
            Researchgroup researchgroup = db.Researchgroups.Find(id);

            if (researchgroup == null)
            {
                return HttpNotFound();
            }

            ResearchgroupViewModel ResearchgroupVM = new ResearchgroupViewModel()
            {
                ResearchgroupID = researchgroup.ResearchgroupID,
                ResearchgroupName = researchgroup.ResearchgroupName,
                InstitutionID = researchgroup.InstitutionInstitutionID,
                CreatedOn = researchgroup.CreatedOn
            };

            ViewBag.InstitutionList = new SelectList(db.Institutions.Where(c => c.Status == "Current").OrderBy(c => c.InstitutionName).AsEnumerable(), "InstitutionID", "InstitutionName", ResearchgroupVM.InstitutionID);

            return View(ResearchgroupVM);
        }

        //
        // POST: /Researchgroup/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ResearchgroupViewModel ResearchgroupVM)
        {
            if (!ModelState.IsValid)
            {

                ViewBag.InstitutionList = new SelectList(db.Institutions.Where(c => c.Status == "Current").OrderBy(c => c.InstitutionName).AsEnumerable(), "InstitutionID", "InstitutionName", ResearchgroupVM.InstitutionID);

                return View();
            }
            
                Researchgroup researchgroup = db.Researchgroups.Find(ResearchgroupVM.ResearchgroupID);

                researchgroup.ResearchgroupName = ResearchgroupVM.ResearchgroupName;
                researchgroup.InstitutionInstitutionID = ResearchgroupVM.InstitutionID;

                researchgroup.LastUpdatedBy = (int)Membership.GetUser().ProviderUserKey;

                db.Entry(researchgroup).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            

        }

        //
        // POST: /Institution/Delete/5
        [Authorize(Roles = "Administrator, Superuser")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Researchgroup researchgroup = db.Researchgroups.Find(id);

            researchgroup.Status = "Deleted";
            researchgroup.LastUpdatedBy = (int)Membership.GetUser().ProviderUserKey;

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //
        // GET: /Researchgroup/Members/5

        public ActionResult ResearchgroupMembers(int researchgroupid)
        {
            var members = from ur in db.UserInResearchgroups.Include("UserProfile").Include("Researchgroup").Include("Option")
                          where ur.Researchgroups.ResearchgroupID == researchgroupid && ur.Status == "Current" && ur.UserProfiles.Status == "Current" && ur.Researchgroups.Status == "Current"
                          orderby ur.UserProfiles.UserName
                          select new ResearchgroupMemberListViewModel
                            {
                                UserInResearchgroupID = ur.UserInResearchgroupID,
                                Name = ur.UserProfiles.ForeName + " " + ur.UserProfiles.SurName,
                                UserRoleInResearchgroup = ur.Options.OptionValue,
                                StartTime = ur.StartTime,
                                EndTime = ur.EndTime
                            };

            ViewBag.ResearchgroupName = db.Researchgroups.Find(researchgroupid).ResearchgroupName;
            ViewBag.ResearchgroupID = researchgroupid;

            if (members == null)
            {
                return HttpNotFound();
            }

            return View(members);
        }

        //
        // GET: /Researchgroup/AddMember

        public ActionResult AddResearchgroupMember(int researchgroupid = 0)
        {
            Researchgroup researchgroup = db.Researchgroups.Find(researchgroupid);

            if (researchgroup == null) return HttpNotFound();

            ResearchgroupMemberViewModel RGMemberVM = new ResearchgroupMemberViewModel()
            {
                ResearchgroupID = researchgroup.ResearchgroupID,
                InstitutionID = researchgroup.InstitutionInstitutionID
            };

            var users = db.UserProfiles
                .Where(c => c.Status == "Current" && db.Careerhistories.Where(ch => ch.InstitutionInstitutionID == RGMemberVM.InstitutionID && ch.Status == "Current" && (ch.EndTime == null || ch.EndTime > DateTime.Now)).Select(ch => ch.UserProfileUserId).Contains(c.UserId))
                .ToList()
                .OrderBy(c => c.UserName)
                .Select(s => new
                {
                    UserId = s.UserId,
                    Name = string.Format("{0} {1} ({2})", s.ForeName, s.SurName, s.Email)
                });

            ViewBag.UserList = new SelectList(users.AsEnumerable(), "UserId", "Name");
            ViewBag.ResearchgroupRoleList = new SelectList(db.Options.Where(o => o.OptionFields.OptionFieldName == "User Role In Research group" && o.Status == "Current").AsEnumerable(), "OptionID", "OptionValue");

            return View(RGMemberVM);
        }

        //
        // POST: /Researchgroup/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddResearchgroupMember(ResearchgroupMemberViewModel RGMemberVM)
        {
            if (ModelState.IsValid)
            {
                if (RGMemberVM.StartTime > RGMemberVM.EndTime)
                {
                    ModelState.AddModelError(string.Empty, "Start date must be before end date");

                    var users = db.UserProfiles
                                   .Where(c => c.Status == "Current" && db.Careerhistories.Where(ch => ch.InstitutionInstitutionID == RGMemberVM.InstitutionID && ch.Status == "Current" && (ch.EndTime == null || ch.EndTime > DateTime.Now)).Select(ch => ch.UserProfileUserId).Contains(c.UserId))
                                   .ToList()
                                   .OrderBy(c => c.UserName)
                                   .Select(s => new
                                   {
                                       UserId = s.UserId,
                                       Name = string.Format("{0} {1} ({2})", s.ForeName, s.SurName, s.Email)
                                   });

                    ViewBag.UserList = new SelectList(users.AsEnumerable(), "UserId", "Name");
                    ViewBag.ResearchgroupRoleList = new SelectList(db.Options.Where(o => o.OptionFields.OptionFieldName == "User Role In Research group" && o.Status == "Current").AsEnumerable(), "OptionID", "OptionValue");

                    return View(RGMemberVM);
                }

                UserInResearchgroup userinresearchgroup = new UserInResearchgroup();

                userinresearchgroup.UserProfileUserId = RGMemberVM.UserID;
                userinresearchgroup.UserRoleinResearchgroupID = RGMemberVM.UserRoleInResearchgroupID;
                userinresearchgroup.ResearchgroupResearchgroupID = RGMemberVM.ResearchgroupID;

                userinresearchgroup.StartTime = RGMemberVM.StartTime;
                userinresearchgroup.EndTime = RGMemberVM.EndTime;

                userinresearchgroup.LastUpdatedBy = (int)Membership.GetUser().ProviderUserKey;
                userinresearchgroup.CreatedBy = (int)Membership.GetUser().ProviderUserKey;
                userinresearchgroup.CreatedOn = DateTimeOffset.Now;

                db.UserInResearchgroups.Add(userinresearchgroup);
                db.SaveChanges();

                return RedirectToAction("ResearchgroupMembers", new { researchgroupid = RGMemberVM.ResearchgroupID });
            }

            var userlist = db.UserProfiles
                 .Where(c => c.Status == "Current" && db.Careerhistories.Where(ch => ch.InstitutionInstitutionID == RGMemberVM.InstitutionID && ch.Status == "Current" && (ch.EndTime == null || ch.EndTime > DateTime.Now)).Select(ch => ch.UserProfileUserId).Contains(c.UserId))
                 .ToList()
                 .OrderBy(c => c.UserName)
                 .Select(s => new
                 {
                     UserId = s.UserId,
                     Name = string.Format("{0} {1} ({2})", s.ForeName, s.SurName, s.Email)
                 });

            ViewBag.UserList = new SelectList(userlist.AsEnumerable(), "UserId", "Name");

            return View(RGMemberVM);
        }

        //
        // GET: /Researchgroup/EditResearchgroupMember/5

        public ActionResult EditResearchgroupMember(int researchgroupmemberid = 0)
        {
            UserInResearchgroup userinresearchgroup = db.UserInResearchgroups.Find(researchgroupmemberid);

            if (userinresearchgroup == null)
            {
                return HttpNotFound();
            }

            ResearchgroupMemberViewModel uirVM = new ResearchgroupMemberViewModel()
            {
                UserInResearchgroupID = userinresearchgroup.UserInResearchgroupID,
                ResearchgroupID = userinresearchgroup.ResearchgroupResearchgroupID,
                UserID = userinresearchgroup.UserProfileUserId,
                UserName = userinresearchgroup.UserProfiles.UserName + " (" + userinresearchgroup.UserProfiles.SurName + ", " + userinresearchgroup.UserProfiles.ForeName + ")",
                UserRoleInResearchgroupID = userinresearchgroup.UserRoleinResearchgroupID,
                InstitutionID = userinresearchgroup.Researchgroups.InstitutionInstitutionID,
                StartTime = userinresearchgroup.StartTime,
                EndTime = userinresearchgroup.EndTime,

            };

            var users = db.UserProfiles
                            .Where(c => c.Status == "Current" && db.Careerhistories.Where(ch => ch.InstitutionInstitutionID == uirVM.InstitutionID && ch.Status == "Current" && (ch.EndTime == null || ch.EndTime > DateTime.Now)).Select(ch => ch.UserProfileUserId).Contains(c.UserId))
                            .ToList()
                            .OrderBy(c => c.UserName)
                            .Select(s => new
                            {
                                UserId = s.UserId,
                                Name = string.Format("{0} {1} ({2})", s.ForeName, s.SurName, s.Email)
                            });

            ViewBag.UserList = new SelectList(users.AsEnumerable(), "UserId", "Name", uirVM.UserID);
            ViewBag.ResearchgroupRoleList = new SelectList(db.Options.Where(o => o.OptionFields.OptionFieldName == "User Role In Research group" && o.Status == "Current").AsEnumerable(), "OptionID", "OptionValue");

            return View(uirVM);
        }

        //
        // POST: /Researchgroup/EditResearchgroupMember

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditResearchgroupMember(ResearchgroupMemberViewModel RGMemberVM)
        {
            if (ModelState.IsValid)
            {
                if (RGMemberVM.StartTime > RGMemberVM.EndTime)
                {
                    ModelState.AddModelError(string.Empty, "Start date must be before end date");

                    var users = db.UserProfiles
                                   .Where(c => c.Status == "Current" && db.Careerhistories.Where(ch => ch.InstitutionInstitutionID == RGMemberVM.InstitutionID && ch.Status == "Current" && (ch.EndTime == null || ch.EndTime > DateTime.Now)).Select(ch => ch.UserProfileUserId).Contains(c.UserId))
                                   .ToList()
                                   .OrderBy(c => c.UserName)
                                   .Select(s => new
                                   {
                                       UserId = s.UserId,
                                       Name = string.Format("{0} {1} ({2})", s.ForeName, s.SurName, s.Email)
                                   });

                    ViewBag.ResearchgroupID = RGMemberVM.ResearchgroupID;
                    ViewBag.UserList = new SelectList(users.AsEnumerable(), "UserId", "Name");
                    ViewBag.ResearchgroupRoleList = new SelectList(db.Options.Where(o => o.OptionFields.OptionFieldName == "User Role In Research group" && o.Status == "Current").AsEnumerable(), "OptionID", "OptionValue");

                    return View(RGMemberVM);
                }

                UserInResearchgroup userinresearchgroup = db.UserInResearchgroups.Find(RGMemberVM.UserInResearchgroupID);

                if (userinresearchgroup == null) return HttpNotFound();

                //    userinresearchgroup.UserInResearchgroupID = RGMemberVM.UserInResearchgroupID;
                //   userinresearchgroup.UserProfileUserId = RGMemberVM.UserID;
                userinresearchgroup.UserRoleinResearchgroupID = RGMemberVM.UserRoleInResearchgroupID;
                userinresearchgroup.StartTime = RGMemberVM.StartTime;
                userinresearchgroup.EndTime = RGMemberVM.EndTime;

                userinresearchgroup.LastUpdatedBy = (int)Membership.GetUser().ProviderUserKey;

                db.Entry(userinresearchgroup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ResearchgroupMembers", new { researchgroupid = RGMemberVM.ResearchgroupID });
            }
            return View(RGMemberVM);
        }

        //
        // POST: /Institution/Delete/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteResearchgroupMember(int researchgroupmemberid = 0)
        {
            UserInResearchgroup userinresearchgroup = db.UserInResearchgroups.Find(researchgroupmemberid);

            if (userinresearchgroup == null) return HttpNotFound();

            userinresearchgroup.Status = "Deleted";
            userinresearchgroup.LastUpdatedBy = (int)Membership.GetUser().ProviderUserKey;

            db.SaveChanges();
            return RedirectToAction("ResearchgroupMembers", new { researchgroupid = userinresearchgroup.Researchgroups.ResearchgroupID });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}
