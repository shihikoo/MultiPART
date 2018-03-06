using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using MultiPART.Models;
using MultiPART.Models.Table;
using MultiPART.Models.ViewModel;
using System.Web.Security;
using MultiPART.Authorization;

namespace MultiPART.Controllers
{
    [Authorize(Roles = "Administrator, Superuser")]
    [Restrict("Disabled")]
    public class InstitutionController : Controller
    {
        private MultipartContext db = new MultipartContext();

        //
        // GET: /Institution/

        public ActionResult Index(string sortOrder = "InstitutionName")
        {
            var insitutions = from i in db.Institutions
                              where i.Status == "Current"
                              orderby i.InstitutionName
                              select new InstitutionListViewModel
                              {
                                  InstitutionID = i.InstitutionID,
                                  InstitutionName = i.InstitutionName,
                                  Country = i.Countries.CountryName,
                                  CreatedOn = i.CreatedOn,
                                  //LastUpdatedOn = i.LastUpdatedOn
                              };
            var result = insitutions;

            switch (sortOrder)
            {
                case "InstitutionName":
                    result = result.OrderBy(s => s.InstitutionName);
                    break;
                case "InstitutionName desc":
                    result = result.OrderByDescending(s => s.InstitutionName);
                    break;
                case "CountryName":
                    result = result.OrderBy(s => s.Country);
                    break;
                case "CountryName desc":
                    result = result.OrderByDescending(s => s.Country);
                    break;
                case "CreatedOn":
                    result = result.OrderBy(s => s.CreatedOn);
                    break;
                case "CreatedOn desc":
                    result = result.OrderByDescending(s => s.CreatedOn);
                    break;
                default:
                    result = result.OrderBy(s => s.Country);
                    break;
            }

            ViewBag.sortOrder = sortOrder;

            return View(result.ToList());

            //return View(insitutions.ToList());
        }

        //
        // GET: /Institution/Create

        public ActionResult Create()
        {
            ViewBag.CountryList = new SelectList(db.Countries.Where(m => m.Status == "Current").OrderBy(c => c.CountryName), "CountryID", "CountryName");
            return View();
        }

        //
        // POST: /Institution/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(InstitutionViewModel institutionVM)
        {
            Institution institution = new Institution();

            if (ModelState.IsValid)
            {
                institution.InstitutionName = institutionVM.InstitutionName;
                institution.CountryCountryID = institutionVM.CountryCountryID;
                institution.LastUpdatedBy = (int)Membership.GetUser().ProviderUserKey;
                //institution.LastUpdatedOn = DateTimeOffset.Now;
                institution.CreatedBy = (int)Membership.GetUser().ProviderUserKey;
                institution.CreatedOn = DateTimeOffset.Now;
                db.Institutions.Add(institution);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CountryList = new SelectList(db.Countries.Where(c => c.Status == "Current").OrderBy(c => c.CountryName).AsEnumerable(), "CountryID", "CountryName", institution.CountryCountryID);
            return View(institution);
        }

        //
        // GET: /Institution/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Institution institution = db.Institutions.Find(id);

            if (institution == null)
            {
                return HttpNotFound();
            }

            InstitutionViewModel institutionVM = new InstitutionViewModel() {
                InstitutionID = institution.InstitutionID,
                InstitutionName = institution.InstitutionName,
                CountryCountryID = institution.CountryCountryID        
            
            };

            ViewBag.CountryList = new SelectList(db.Countries.Where(c => c.Status == "Current").OrderBy(c => c.CountryName).AsEnumerable(), "CountryID", "CountryName", institutionVM.CountryCountryID);

            return View(institutionVM);
        }

        //
        // POST: /Institution/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(InstitutionViewModel institutionViewModel)
        {
            if (ModelState.IsValid)
            {
                    Institution institution = db.Institutions.Find(institutionViewModel.InstitutionID);

                    institution.InstitutionName = institutionViewModel.InstitutionName;
                    institution.CountryCountryID = institutionViewModel.CountryCountryID;
                    institution.LastUpdatedBy = (int)Membership.GetUser().ProviderUserKey;
                    //institution.LastUpdatedOn = DateTimeOffset.Now;

                db.Entry(institution).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CountryList = new SelectList(db.Countries.Where(c => c.Status == "Current").OrderBy(c => c.CountryName).AsEnumerable(), "CountryID", "CountryName", institutionViewModel.CountryCountryID);
            return View(institutionViewModel);
        }

        //
        // POST: /Institution/Delete/5
         [Authorize(Roles = "Administrator, Superuser")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Institution institution = db.Institutions.Find(id);
            institution.Status = "Deleted";
            institution.LastUpdatedBy = (int)Membership.GetUser().ProviderUserKey;
            //institution.LastUpdatedOn = DateTimeOffset.Now;

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}