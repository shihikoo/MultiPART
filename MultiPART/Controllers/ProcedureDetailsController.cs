using MultiPART.Authorization;
using MultiPART.Models;
using MultiPART.Models.LookupTable;
using MultiPART.Models.Table;
using MultiPART.Models.ViewModel;
using MultiPART.Services;
using MultiPART.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace MultiPART.Controllers
{
    [Authorize]
    [Restrict("Disabled")]
    public class ProcedureDetailsController : Controller
    {
        private MultipartContext db = new MultipartContext();
        private readonly IUnitOfWork _uow = new UnitOfWork<MultipartContext>();
        private readonly ProjectUserValidationService _projectuservalidationservice;

        public ProcedureDetailsController()
        {
            var membershipUser = Membership.GetUser();
            if (membershipUser != null && membershipUser.ProviderUserKey != null)
            {
                string[] roles = Roles.GetRolesForUser(membershipUser.UserName);
                var userKey = (int)membershipUser.ProviderUserKey;
                _projectuservalidationservice = new ProjectUserValidationService(new ModelStateWrapper(ModelState), _uow, userKey, roles);
            }
            else { throw new UnauthorizedAccessException("MembershipUser or ProviderUserKey is null - Is the user authenticated?"); }
        }

        //
        // GET: /ProcedureDetails/Index?procedureid=5

        public ActionResult Index(int procedureid, int projectid)
        {

            var access = _projectuservalidationservice.GetUserProjectAccessType(projectid);

            if (access == AssessType.None)
            {
                ViewBag.ErrorMessage = "Access Denied. Please contact administrator for further assistance.";
                return View("Error");
            }

            Procedure procedure = GetValidProcedure(procedureid);

            if (procedure == null) return HttpNotFound();

            ViewBag.ProcedureName = procedure.ProcedureLabel;

            var proceduredetailVM = (from pdof in db.ProcedureDetailOptionFields
                                      .Where(p => p.Status == "Current" && (p.ProcedurePurposeOrTypeID == procedure.ProcedureTypeID || p.ProcedurePurposeOrTypeID == procedure.ProcedurePurposeID))
                                      orderby pdof.ProcedurePurposeOrType.OptionValue, pdof.ProcedureDetailOptionFieldName
                                     select pdof).AsEnumerable()
                                 .Select(pdof => new ProcedureDetailViewModel
                                 {
                                     ProcedureID = procedureid,
                                     FieldID = pdof.ProcedureDetailOptionFieldID,
                                     FieldName = pdof.ProcedureDetailOptionFieldName,
                                     FieldType = pdof.ProcedureDetailFieldType.OptionValue,
                                     Multiple = pdof.Multiple,

                                     Options = new MultiSelectList(pdof.ProcedureDetailOptions.Where(pdo => pdo.Status == "Current").AsEnumerable(), "ProcedureDetailOptionID", "ProcedureDetailOptionName"),

                                     DisplayValue = pdof.ProcedureDetailFieldType.OptionValue == EntryFieldType.Lookup.ToString()
                                   ? String.Join(", ", pdof.ProcedureDetails.Where(pd => pd.Status == "Current" && pd.ProcedureProcedureID == procedureid).Select(pd => pd.ProcedureDetailOptions.ProcedureDetailOptionName))
                                   : String.Join(", ", pdof.ProcedureDetails.Where(pd => pd.Status == "Current" && pd.ProcedureProcedureID == procedureid).Select(pd => pd.ProcedureDetailValue)),

                                     OptionID = (pdof.ProcedureDetailFieldType.OptionValue == EntryFieldType.Lookup.ToString() && pdof.Multiple == false) ?
                                     pdof.ProcedureDetails.Where(pd => pd.Status == "Current" && pd.ProcedureProcedureID == procedureid).Select(pd => pd.ProcedureDetailOptionID).FirstOrDefault()
                                     : null,

                                     Value = pdof.ProcedureDetailFieldType.OptionValue == EntryFieldType.Lookup.ToString() ? null
                                     : pdof.ProcedureDetails.Where(pd => pd.Status == "Current" && pd.ProcedureProcedureID == procedureid).Select(pd => pd.ProcedureDetailValue).FirstOrDefault(),

                                     OptionIDs = (pdof.ProcedureDetailFieldType.OptionValue == EntryFieldType.Lookup.ToString() && pdof.Multiple == true) ?
                                     pdof.ProcedureDetails.Where(pd => pd.Status == "Current" && pd.ProcedureProcedureID == procedureid).Select(pd => pd.ProcedureDetailOptionID).ToList().ToArray()
                                     : null,

                                 });

            if (access == AssessType.View)
            {
                return View("IndexDisplay", proceduredetailVM.ToList());
            }

            ViewBag.MultiPARTProjectID = projectid;
            ViewBag.Procedureid = procedureid;

            return View(proceduredetailVM.ToList());
        }

        //
        // POST: /ProcedureDetails/Index

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(int procedureid, int projectid, IEnumerable<ProcedureDetailViewModel> ProcedureDetailVM)
        {

            if (!_projectuservalidationservice.UserCanEditProject(projectid))
            {
                ViewBag.ErrorMessage = "Access Denied. Please contact administrator for further assistance.";
                return View("Error");
            }

            var userKey = (int)Membership.GetUser().ProviderUserKey;

            if (!ModelState.IsValid) return RedirectToAction("Index", new { procedureid = procedureid, projectid = projectid });

            Procedure procedure = GetValidProcedure(procedureid);
            if (procedure == null) return HttpNotFound();

            foreach (var eachProcedureDetail in ProcedureDetailVM)
            {
                if (!ProcedureDetailFieldIsValid(eachProcedureDetail.FieldID)) 
                {
                    ViewBag.ErrorMessage = "Data conflicts occurred. Please contact administrator for further assistance.";
                    return View("Error");
                };

                if (eachProcedureDetail.FieldType == EntryFieldType.Lookup.ToString() && eachProcedureDetail.Multiple == false)
                {
                    // the procedure details with the same fields
                    var ExistingProceduredetails = db.ProcedureDetails.Where(pd => pd.ProcedureDetailOptionFieldID == eachProcedureDetail.FieldID && pd.ProcedureProcedureID == procedureid);

                    foreach (var proceduredetail in ExistingProceduredetails)
                    {
                        if (proceduredetail.Status != "Deleted")
                        {
                            proceduredetail.Status = "Deleted";
                            proceduredetail.LastUpdatedBy = userKey;
                        }
                    }
                    if (eachProcedureDetail.OptionID > 0)
                    {
                        // procedure details with the same field and same option
                        var ExistingProceduredetail = ExistingProceduredetails.Where(pd => pd.ProcedureDetailOptionID == eachProcedureDetail.OptionID);

                        if (ExistingProceduredetail.Count() == 0)
                        {
                            ProcedureDetail Newproceduredetail = new ProcedureDetail()
                               {
                                   ProcedureProcedureID = eachProcedureDetail.ProcedureID,
                                   ProcedureDetailOptionFieldID = eachProcedureDetail.FieldID,
                                   ProcedureDetailOptionID = eachProcedureDetail.OptionID,

                                   LastUpdatedBy = userKey,
                               };
                            db.ProcedureDetails.Add(Newproceduredetail);
                        }
                        else
                        {
                            ExistingProceduredetail.FirstOrDefault().Status = "Current";
                            ExistingProceduredetail.FirstOrDefault().LastUpdatedBy = userKey;

                        }
                    }
                }

                if (eachProcedureDetail.FieldType == EntryFieldType.Lookup.ToString() && eachProcedureDetail.Multiple == true)
                {
                    var ExistingProceduredetails = db.ProcedureDetails.Where(pd => pd.ProcedureDetailOptionFieldID == eachProcedureDetail.FieldID && pd.ProcedureProcedureID == procedureid);

                    foreach (var proceduredetail in ExistingProceduredetails)
                    {
                        proceduredetail.Status = "Deleted";
                        proceduredetail.LastUpdatedBy = userKey;
                    }

                    if (eachProcedureDetail.OptionIDs != null)
                    {
                        foreach (var optionID in eachProcedureDetail.OptionIDs)
                        {
                            var ExistingProceduredetail = ExistingProceduredetails.Where(pd => pd.ProcedureDetailOptionID == optionID);

                            if (ExistingProceduredetail.Count() == 0)
                            {
                                ProcedureDetail procedureDetail = new ProcedureDetail
                                {
                                    ProcedureDetailOptionFieldID = eachProcedureDetail.FieldID,
                                    ProcedureProcedureID = eachProcedureDetail.ProcedureID,
                                    ProcedureDetailOptionID = optionID,

                                    LastUpdatedBy = userKey,
                                };

                                db.ProcedureDetails.Add(procedureDetail);
                            }
                            else
                            {
                                ExistingProceduredetail.FirstOrDefault().Status = "Current";
                                ExistingProceduredetail.FirstOrDefault().LastUpdatedBy = userKey;

                            }
                        }
                    }
                }
                else if (eachProcedureDetail.FieldType != EntryFieldType.Lookup.ToString())
                {
                    var ExistingProceduredetails = db.ProcedureDetails.Where(pd => pd.ProcedureDetailOptionFieldID == eachProcedureDetail.FieldID && pd.ProcedureProcedureID == procedureid);

                    //set everything to "Deleted" as a reset before adding new values
                    foreach (var proceduredetail in ExistingProceduredetails)
                    {
                        proceduredetail.Status = "Deleted";
                        proceduredetail.LastUpdatedBy = userKey;
                    }
                    if (eachProcedureDetail.Value != null)
                    {
                        if (ExistingProceduredetails.Count() == 0)
                        {
                            ProcedureDetail proceduredetail = new ProcedureDetail()
                            {
                                ProcedureProcedureID = eachProcedureDetail.ProcedureID,
                                ProcedureDetailValue = eachProcedureDetail.Value,
                                ProcedureDetailOptionFieldID = eachProcedureDetail.FieldID,

                                LastUpdatedBy = (int)Membership.GetUser().ProviderUserKey,
                            };
                            db.ProcedureDetails.Add(proceduredetail);
                        }
                        else
                        {
                            ExistingProceduredetails.FirstOrDefault().ProcedureDetailValue = eachProcedureDetail.Value;
                            ExistingProceduredetails.FirstOrDefault().Status = "Current";
                            ExistingProceduredetails.FirstOrDefault().LastUpdatedBy = userKey;

                        }
                    }
                }
            }

            db.SaveChanges();

      //      ViewBag.UpdatedMessage = "Information Updated";

            return RedirectToAction("Details", "MultiPARTProject", new { projectid = projectid });
        }

        #region Helpers

        private bool ProcedureDetailFieldIsValid(int procedurefieldid)
        {
            ProcedureDetailOptionField proceduredetailfield = db.ProcedureDetailOptionFields.Find(procedurefieldid);
            if (proceduredetailfield == null) return false;
            if (proceduredetailfield.Status == "Deleted") return false;

            return true;
        }

        private Procedure GetValidProcedure(int procedureid = 0)
        {
            var procedure = db.Procedures.Find(procedureid);

            if (ProcedureIsValid(procedure)) return procedure;

            return null;
        }

        private bool ProcedureIsValid(Procedure procedure)
        {
            if (procedure == null) return false;
            if (procedure.Status == "Deleted") return false;

            return true;

        }
        #endregion

    }
}
