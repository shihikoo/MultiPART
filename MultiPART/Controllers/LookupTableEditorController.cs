using MultiPART.Authorization;
using MultiPART.Models;
using MultiPART.Models.LookupTable;
using MultiPART.Models.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;


namespace MultiPART.Controllers
{

    /*
     This controller is designed to allow administrator and superuser to edit the content of the lookup tables 
     * (
     * Animal Supplier
     * Specie, Strain, 
     * AnimalHusbandryField, AnimalHusbandryOption)
     * DataEntryField, DataEntryOption, 
     * ProcedureDetailOption, ProcedureDetailOptionField,
     * Option, OptionField, 
     * 

     */
    [Authorize(Roles = "Administrator, Superuser")]
    [Restrict("Disabled")]
    public class LookupTableEditorController : Controller
    {
        private MultipartContext db = new MultipartContext();

        //
        // GET: /LookupTableEditor/

        public ActionResult Index(string lookuptablename = "")
        {

            ViewBag.CategoryName = "";
            ViewBag.PurposeOrTypeName = "";
            ViewBag.MultipleName = "";
            ViewBag.lookuptables = _lookuptables;

            if (lookuptablename == "")
            {
                return View();
            }

            else
            {
                ViewBag.lookuptablename = lookuptablename;

                if (lookuptablename == "Species")
                {
                    var LookupTableVM = from l in db.Species
                                        where l.Status == "Current"
                                        orderby l.SpeciesName
                                        select new LookupTableViewModel
                                        {
                                            ID = l.SpeciesID,
                                            Name = l.SpeciesName,
                                            LookupTableName = lookuptablename,
                                        };
                    ViewBag.ContentName = "Species";

                    return View(LookupTableVM.ToList());
                }
                else if (lookuptablename == "Strains")
                {
                    var LookupTableVM = db.Strains.Where(l => l.Status == "Current").OrderBy(l => l.SpeciesID).AsEnumerable().Select(l => new LookupTableViewModel
                                        {
                                            ID = l.StrainID,
                                            Name = l.StrainName,
                                            CategoryID = l.SpeciesID,
                                            Categories = new SelectList(db.Species.Where(s => s.Status == "Current").AsEnumerable(), "SpeciesID", "SpeciesName"),
                                            LookupTableName = lookuptablename,
                                        }
                        );
                    ViewBag.ContentName = "Strains";
                    ViewBag.CategoryName = "Species";

                    return View(LookupTableVM.ToList());
                }
                else if (lookuptablename == "AnimalHusbandryFields")
                {
                    var LookupTableVM = db.AnimalHusbandryFields.Where(l => l.Status == "Current").OrderBy(l => l.AnimalHusbandryFieldName).AsEnumerable().Select(l => new LookupTableViewModel
                                        {
                                            ID = l.AnimalHusbandryFieldID,
                                            Name = l.AnimalHusbandryFieldName,
                                            CategoryID = l.AnimalHusbandryFieldTypeID,
                                            LookupTableName = lookuptablename,
                                            Categories = new SelectList(db.Options.Where(s => s.Status == "Current" && s.OptionFields.OptionFieldName == "Field Type").AsEnumerable(), "OptionID", "OptionValue")
                                        }
                );
                    ViewBag.ContentName = "Animal Husbandry Form Fields";
                    ViewBag.CategoryName = "Field Type";

                    return View(LookupTableVM.ToList());
                }
                else if (lookuptablename == "AnimalHusbandryOptions")
                {
                    var LookupTableVM = db.AnimalHusbandryOptions.Where(l => l.Status == "Current").OrderBy(l => l.AnimalHusbandryFields.AnimalHusbandryFieldName).AsEnumerable().Select(l => new LookupTableViewModel
                                        {
                                            ID = l.AnimalHusbandryOptionID,
                                            Name = l.AnimalHusbandryOptionName,
                                            CategoryID = l.AnimalHusbandryFieldID,
                                            LookupTableName = lookuptablename,
                                            Categories = new SelectList(db.AnimalHusbandryFields.Where(s => s.Status == "Current" && s.Options.OptionValue == "lookup").AsEnumerable(), "AnimalHusbandryFieldID", "AnimalHusbandryFieldName")
                                        }
                );

                    ViewBag.ContentName = "Animal Husbandry Lookup Options";
                    ViewBag.CategoryName = "Lookup Field";

                    return View(LookupTableVM.ToList());
                }
                else if (lookuptablename == "ProcedureDetailFields")
                {
                    var LookupTableVM = db.ProcedureDetailOptionFields.Where(l => l.Status == "Current").AsEnumerable().Select(l => new LookupTableViewModel
                                        {
                                            ID = l.ProcedureDetailOptionFieldID,
                                            Name = l.ProcedureDetailOptionFieldName,
                                            CategoryID = l.ProcedureDetailFieldTypeID,
                                            LookupTableName = lookuptablename,
                                            Categories = new SelectList(db.Options.Where(s => s.Status == "Current" && s.OptionFields.OptionFieldName == "Field Type").AsEnumerable(), "OptionID", "OptionValue"),
                                            PurposeOrTypeID = l.ProcedurePurposeOrTypeID,
                                            PurposesTypes = new SelectList(db.Options.Where(s => s.Status == "Current" && (s.OptionFields.OptionFieldName == "Procedure Type" || s.OptionFields.OptionFieldName == "Procedure Purpose")).AsEnumerable(), "OptionID", "OptionValue"),
                                            Multiple = l.Multiple,
                                        }
                                        );

                    ViewBag.ContentName = "Procedure Detail Form Fields";
                    ViewBag.CategoryName = "Field Type";
                    ViewBag.PurposeOrTypeName = "Purpose Or Type of the Procedure";
                    ViewBag.MultipleName = "Multiple";

                    return View(LookupTableVM.OrderBy(l => l.PurposeOrTypeID).ToList());
                }
                else if (lookuptablename == "ProcedureDetailOptions")
                {
                    var LookupTableVM = db.ProcedureDetailOptions.Where(l => l.Status == "Current").OrderBy(l => l.ProcedureDetailOptionFields.ProcedureDetailOptionFieldName).AsEnumerable().Select(l => new LookupTableViewModel
                                        {
                                            ID = l.ProcedureDetailOptionID,
                                            Name = l.ProcedureDetailOptionName,
                                            LookupTableName = lookuptablename,
                                            CategoryID = l.ProcedureDetailOptionFieldID,
                                            Categories = new SelectList(db.ProcedureDetailOptionFields.Where(s => s.Status == "Current" && s.ProcedureDetailFieldType.OptionValue == "lookup").AsEnumerable(), "ProcedureDetailOptionFieldID", "ProcedureDetailOptionFieldName")
                                        }
                                        );

                    ViewBag.ContentName = "Procedure Detail Lookup Options";
                    ViewBag.CategoryName = "Lookup Field";

                    return View(LookupTableVM.ToList());
                }
                else if (lookuptablename == "DataEntryFields")
                {
                    var LookupTableVM = db.DataEntryFields.Where(l => l.Status == "Current").OrderBy(l => l.DataEntryFieldName).AsEnumerable().Select(l => new LookupTableViewModel
                                            {
                                                ID = l.DataEntryFieldID,
                                                Name = l.DataEntryFieldName,
                                                CategoryID = l.FieldTypeID,
                                                LookupTableName = lookuptablename,
                                                Categories = new SelectList(db.Options.Where(s => s.Status == "Current" && s.OptionFields.OptionFieldName == "Field Type").AsEnumerable(), "OptionID", "OptionValue")
                                            }
                                        );
                    ViewBag.ContentName = "Data Entry Form Fields";
                    ViewBag.CategoryName = "Field Type";

                    return View(LookupTableVM.ToList());
                }
                else if (lookuptablename == "DataEntryOptions")
                {
                    var LookupTableVM = db.DataEntryOptions.Where(l => l.Status == "Current").OrderBy(l => l.DataEntryField.DataEntryFieldName).AsEnumerable().Select(l => new LookupTableViewModel
                    {
                        ID = l.DataEntryOptionID,
                        Name = l.DataEntryOptionName,
                        CategoryID = l.DataEntryFieldDataEntryFieldID,
                        LookupTableName = lookuptablename,
                        Categories = new SelectList(db.DataEntryFields.Where(s => s.Status == "Current" && s.FieldType.OptionValue == "lookup").AsEnumerable(), "DataEntryFieldID", "DataEntryFieldName")
                    }
                                        );

                    ViewBag.ContentName = "Data Entry Lookup Options";
                    ViewBag.CategoryName = "Lookup Field";

                    return View(LookupTableVM.ToList());
                }
                //else if (lookuptablename == "OptionFields")
                //{
                //    var LookupTableVM = from l in db.OptionFields
                //                        where l.Status == "Current"
                //                        select new LookupTableViewModel
                //                        {
                //                            ID = l.OptionFieldID,
                //                            Name = l.OptionFieldName,
                //                             LookupTableName = lookuptablename,
                //                        };
                //    ViewBag.ContentName = "General Lookup Fields";
                //    return View(LookupTableVM.ToList());
                //}
                //else if (lookuptablename == "Options")
                //{

                //    var LookupTableVM = db.Options.Where(l => l.Status == "Current").OrderBy(l => l.OptionFields.OptionFieldID).AsEnumerable().Select(l => new LookupTableViewModel
                //    {
                //        ID = l.OptionID,
                //        Name = l.OptionValue,
                //        CategoryID = l.OptionFieldOptionFieldID,
                //        LookupTableName = lookuptablename,
                //        Categories = new SelectList(db.OptionFields.Where(s => s.Status == "Current").AsEnumerable(), "OptionFieldID", "OptionFieldName")
                //    }
                //                        );

                //    ViewBag.ContentName = "General Lookup Options";
                //    ViewBag.CategoryName = "Lookup Field";  

                //    return View(LookupTableVM.ToList());
                //}
                else
                {
                    ViewBag.ErrorMessage = "Table not found.";
                    return View("Error");
                }
            }
        }

        [HttpPost]
        public ActionResult Index(ICollection<LookupTableViewModel> lookupVM, string lookuptablename)
        {


            if (lookuptablename == "Species")
            {
                foreach (var lookupVMitem in lookupVM)
                {
                    if (lookupVMitem.ID == 0)
                    {
                        Species record = new Species()
                        {
                            SpeciesID = lookupVMitem.ID,
                            SpeciesName = lookupVMitem.Name,
                        };
                        db.Species.Add(record);
                    }
                    else if (lookupVMitem.ID > 0)
                    {
                        Species record = db.Species.Find(lookupVMitem.ID);
                        record.SpeciesName = lookupVMitem.Name;

                    }
                    else
                    {
                        return View("Error");
                    }
                    db.SaveChanges();
                }
            }
            else if (lookuptablename == "Strains")
            {
                foreach (var lookupVMitem in lookupVM)
                {
                    if (lookupVMitem.ID == 0)
                    {
                        Strain record = new Strain()
                        {
                            StrainID = lookupVMitem.ID,
                            StrainName = lookupVMitem.Name,
                            SpeciesID = lookupVMitem.CategoryID,
                        };
                        db.Strains.Add(record);
                    }
                    else if (lookupVMitem.ID > 0)
                    {
                        Strain record = db.Strains.Find(lookupVMitem.ID);
                        record.StrainName = lookupVMitem.Name;
                        record.SpeciesID = lookupVMitem.CategoryID;
                    }
                    else
                    {
                        return View("Error");
                    }
                    db.SaveChanges();
                }
            }
            else if (lookuptablename == "AnimalHusbandryFields")
            {
                foreach (var lookupVMitem in lookupVM)
                {
                    if (lookupVMitem.ID == 0)
                    {
                        AnimalHusbandryField record = new AnimalHusbandryField()
                        {
                            AnimalHusbandryFieldID = lookupVMitem.ID,
                            AnimalHusbandryFieldName = lookupVMitem.Name,
                            AnimalHusbandryFieldTypeID = lookupVMitem.CategoryID,
                        };
                        db.AnimalHusbandryFields.Add(record);
                    }
                    else if (lookupVMitem.ID > 0)
                    {
                        AnimalHusbandryField record = db.AnimalHusbandryFields.Find(lookupVMitem.ID);
                        record.AnimalHusbandryFieldName = lookupVMitem.Name;
                        record.AnimalHusbandryFieldTypeID = lookupVMitem.CategoryID;
                    }
                    else
                    {
                        return View("Error");
                    }
                    db.SaveChanges();
                }
            }
            else if (lookuptablename == "AnimalHusbandryOptions")
            {
                foreach (var lookupVMitem in lookupVM)
                {
                    if (lookupVMitem.ID == 0)
                    {
                        AnimalHusbandryOption record = new AnimalHusbandryOption()
                        {
                            AnimalHusbandryOptionID = lookupVMitem.ID,
                            AnimalHusbandryOptionName = lookupVMitem.Name,
                            AnimalHusbandryFieldID = lookupVMitem.CategoryID,
                        };
                        db.AnimalHusbandryOptions.Add(record);
                    }
                    else if (lookupVMitem.ID > 0)
                    {
                        AnimalHusbandryOption record = db.AnimalHusbandryOptions.Find(lookupVMitem.ID);
                        record.AnimalHusbandryOptionName = lookupVMitem.Name;
                        record.AnimalHusbandryFieldID = lookupVMitem.CategoryID;
                    }
                    else
                    {
                        return View("Error");
                    }
                    db.SaveChanges();
                }
            }
            else if (lookuptablename == "ProcedureDetailFields")
            {
                foreach (var lookupVMitem in lookupVM)
                {
                    if (lookupVMitem.ID == 0)
                    {
                        ProcedureDetailOptionField record = new ProcedureDetailOptionField()
                        {
                            ProcedureDetailOptionFieldID = lookupVMitem.ID,
                            ProcedureDetailOptionFieldName = lookupVMitem.Name,
                            ProcedureDetailFieldTypeID = lookupVMitem.CategoryID,
                            ProcedurePurposeOrTypeID = lookupVMitem.PurposeOrTypeID,
                            Multiple = lookupVMitem.Multiple,
                        };
                        db.ProcedureDetailOptionFields.Add(record);
                    }
                    else if (lookupVMitem.ID > 0)
                    {
                        ProcedureDetailOptionField record = db.ProcedureDetailOptionFields.Find(lookupVMitem.ID);
                        record.ProcedureDetailOptionFieldName = lookupVMitem.Name;
                        record.ProcedureDetailFieldTypeID = lookupVMitem.CategoryID;
                        record.ProcedurePurposeOrTypeID = lookupVMitem.PurposeOrTypeID;
                        record.Multiple = lookupVMitem.Multiple;
                    }
                    else
                    {
                        return View("Error");
                    }
                    db.SaveChanges();
                }
            }
            else if (lookuptablename == "ProcedureDetailOptions")
            {
                foreach (var lookupVMitem in lookupVM)
                {
                    if (lookupVMitem.ID == 0)
                    {
                        ProcedureDetailOption record = new ProcedureDetailOption()
                        {
                            ProcedureDetailOptionID = lookupVMitem.ID,
                            ProcedureDetailOptionName = lookupVMitem.Name,
                            ProcedureDetailOptionFieldID = lookupVMitem.CategoryID,
                        };
                        db.ProcedureDetailOptions.Add(record);
                    }
                    else if (lookupVMitem.ID > 0)
                    {
                        ProcedureDetailOption record = db.ProcedureDetailOptions.Find(lookupVMitem.ID);
                        record.ProcedureDetailOptionName = lookupVMitem.Name;
                        record.ProcedureDetailOptionFieldID = lookupVMitem.CategoryID;
                    }
                    else
                    {
                        return View("Error");
                    }
                    db.SaveChanges();
                }
            }
            else if (lookuptablename == "DataEntryFields")
            {
                foreach (var lookupVMitem in lookupVM)
                {
                    if (lookupVMitem.ID == 0)
                    {
                        DataEntryField record = new DataEntryField()
                        {
                            DataEntryFieldID = lookupVMitem.ID,
                            DataEntryFieldName = lookupVMitem.Name,
                            FieldTypeID = lookupVMitem.CategoryID,
                        };
                        db.DataEntryFields.Add(record);
                    }
                    else if (lookupVMitem.ID > 0)
                    {
                        DataEntryField record = db.DataEntryFields.Find(lookupVMitem.ID);
                        record.DataEntryFieldName = lookupVMitem.Name;
                        record.FieldTypeID = lookupVMitem.CategoryID;
                    }
                    else
                    {
                        return View("Error");
                    }
                    db.SaveChanges();
                }
            }
            else if (lookuptablename == "DataEntryOptions")
            {
                foreach (var lookupVMitem in lookupVM)
                {
                    if (lookupVMitem.ID == 0)
                    {
                        DataEntryOption record = new DataEntryOption()
                        {
                            DataEntryOptionID = lookupVMitem.ID,
                            DataEntryOptionName = lookupVMitem.Name,
                            DataEntryFieldDataEntryFieldID = lookupVMitem.CategoryID,
                        };
                        db.DataEntryOptions.Add(record);
                    }
                    else if (lookupVMitem.ID > 0)
                    {
                        DataEntryOption record = db.DataEntryOptions.Find(lookupVMitem.ID);
                        record.DataEntryOptionName = lookupVMitem.Name;
                        record.DataEntryFieldDataEntryFieldID = lookupVMitem.CategoryID;

                    }
                    else
                    {
                        return View("Error");
                    }
                    db.SaveChanges();
                }
            }
            //else if (lookuptablename == "OptionFields")
            //{
            //    foreach (var lookupVMitem in lookupVM)
            //    {
            //        if (lookupVMitem.ID == 0)
            //        {
            //            OptionField record = new OptionField()
            //            {
            //                OptionFieldID = lookupVMitem.ID,
            //                OptionFieldName = lookupVMitem.Name,
            //            };
            //            db.OptionFields.Add(record);
            //        }
            //        else if (lookupVMitem.ID > 0)
            //        {
            //            OptionField record = db.OptionFields.Find(lookupVMitem.ID);
            //            record.OptionFieldName = lookupVMitem.Name;

            //        }
            //        else
            //        {
            //            return View("Error");
            //        }
            //        db.SaveChanges();
            //    }
            //}
            //else if (lookuptablename == "Options")
            //{
            //    foreach (var lookupVMitem in lookupVM)
            //    {
            //        if (lookupVMitem.ID == 0)
            //        {
            //            Option record = new Option()
            //            {
            //                OptionID = lookupVMitem.ID,
            //                OptionValue = lookupVMitem.Name,
            //                OptionFieldOptionFieldID = lookupVMitem.CategoryID,
            //            };
            //            db.Options.Add(record);
            //        }
            //        else if (lookupVMitem.ID > 0)
            //        {
            //            Option record = db.Options.Find(lookupVMitem.ID);
            //            record.OptionValue = lookupVMitem.Name;
            //            record.OptionFieldOptionFieldID = lookupVMitem.CategoryID;
            //        }
            //        else
            //        {
            //            return View("Error");
            //        }
            //        db.SaveChanges();
            //    }
            //}

            return RedirectToAction("Index", new { lookuptablename = lookuptablename });
        }

        [ValidateAntiForgeryToken]
        [HttpDelete]
        public ActionResult Delete(int lookuptableid, string lookuptablename)
        {
            if (lookuptablename == "AnimalSuppliers")
            {
                var LookupTable = db.AnimalSuppliers.Find(lookuptableid);
                if (LookupTable != null) LookupTable.Status = "Deleted";

            }
            else if (lookuptablename == "Species")
            {
                var LookupTable = db.Species.Find(lookuptableid);
                if (LookupTable != null) LookupTable.Status = "Deleted";

            }
            else if (lookuptablename == "Strains")
            {
                var LookupTable = db.Strains.Find(lookuptableid);
                if (LookupTable != null) LookupTable.Status = "Deleted";

            }
            else if (lookuptablename == "AnimalHusbandryFields")
            {
                var LookupTable = db.AnimalHusbandryFields.Find(lookuptableid);
                if (LookupTable != null) LookupTable.Status = "Deleted";

            }
            else if (lookuptablename == "AnimalHusbandryOptions")
            {
                var LookupTable = db.AnimalHusbandryOptions.Find(lookuptableid);
                if (LookupTable != null) LookupTable.Status = "Deleted";

            }
            else if (lookuptablename == "ProcedureDetailFields")
            {
                var LookupTable = db.ProcedureDetailOptionFields.Find(lookuptableid);
                if (LookupTable != null) LookupTable.Status = "Deleted";

            }
            else if (lookuptablename == "ProcedureDetailOptions")
            {
                var LookupTable = db.ProcedureDetailOptions.Find(lookuptableid);
                if (LookupTable != null) LookupTable.Status = "Deleted";

            }
            else if (lookuptablename == "DataEntryFields")
            {
                var LookupTable = db.DataEntryFields.Find(lookuptableid);
                if (LookupTable != null) LookupTable.Status = "Deleted";

            }
            else if (lookuptablename == "DataEntryOptions")
            {
                var LookupTable = db.DataEntryOptions.Find(lookuptableid);
                if (LookupTable != null) LookupTable.Status = "Deleted";

            }
            //else if (lookuptablename == "OptionFields")
            //{
            //    var LookupTable = db.OptionFields.Find(lookuptableid);
            //    if (LookupTable != null) LookupTable.Status = "Deleted";

            //}
            //else if (lookuptablename == "Options")
            //{
            //    var LookupTable = db.Options.Find(lookuptableid);
            //    if (LookupTable != null) LookupTable.Status = "Deleted";
            //}
            else
            {
                ViewBag.ErrorMessage = "Table not found.";
                return View("Error");
            }

            db.SaveChanges();

            return new HttpStatusCodeResult(202);
            //RedirectToAction("Index", lookuptablename);

        }

        public ActionResult Create(string lookuptablename)
        {
            ViewBag.CategoryName = "";
            ViewBag.PurposeOrTypeName = "";

            LookupTableViewModel LookupTableVM = new LookupTableViewModel()
            {
                LookupTableName = lookuptablename,
            };

            switch (lookuptablename)
            {
                case "AnimalSuppliers":
                    ViewBag.ContentName = "Animal Suppliers";

                    break;
                case "Species":
                    ViewBag.ContentName = "Species";
                    break;
                case "Strains":
                    LookupTableVM.Categories = new SelectList(db.Species.Where(s => s.Status == "Current").AsEnumerable(), "SpeciesID", "SpeciesName");
                    ViewBag.ContentName = "Strains";
                    ViewBag.CategoryName = "Species";
                    break;
                case "AnimalHusbandryFields":
                    LookupTableVM.Categories = new SelectList(db.Options.Where(s => s.Status == "Current" && s.OptionFields.OptionFieldName == "Field Type").AsEnumerable(), "OptionID", "OptionValue");
                    ViewBag.ContentName = "Animal Husbandry Form Fields";
                    ViewBag.CategoryName = "Field Type";
                    break;
                case "AnimalHusbandryOptions":
                    LookupTableVM.Categories = new SelectList(db.AnimalHusbandryFields.Where(s => s.Status == "Current" && s.Options.OptionValue == "lookup").AsEnumerable(),
                         "AnimalHusbandryFieldID", "AnimalHusbandryFieldName");
                    ViewBag.ContentName = "Animal Husbandry Lookup Options";
                    ViewBag.CategoryName = "Lookup Field";
                    break;
                case "ProcedureDetailFields":
                    LookupTableVM.Categories = new SelectList(db.Options.Where(s => s.Status == "Current" && s.OptionFields.OptionFieldName == "Field Type").AsEnumerable(), "OptionID", "OptionValue");
                    LookupTableVM.PurposesTypes = new SelectList(db.Options.Where(s => s.Status == "Current" && (s.OptionFields.OptionFieldName == "Procedure Type" || s.OptionFields.OptionFieldName == "Procedure Purpose")).AsEnumerable(), "OptionID", "OptionValue");

                    ViewBag.ContentName = "Procedure Detail Form Fields";
                    ViewBag.CategoryName = "Field Type";
                    ViewBag.PurposeOrTypeName = "Purpose Or Type of the Procedure";
                    break;

                case "ProcedureDetailOptions":
                    LookupTableVM.Categories = new SelectList(db.ProcedureDetailOptionFields.Where(s => s.Status == "Current" && s.ProcedureDetailFieldType.OptionValue == "lookup").AsEnumerable(),
                            "ProcedureDetailOptionFieldID", "ProcedureDetailOptionFieldName");

                    ViewBag.ContentName = "Procedure Detail Lookup Options";
                    ViewBag.CategoryName = "Lookup Field";
                    break;
                case "DataEntryFields":
                    LookupTableVM.Categories = new SelectList(db.Options.Where(s => s.Status == "Current" && s.OptionFields.OptionFieldName == "Field Type").AsEnumerable(), "OptionID", "OptionValue");

                    ViewBag.ContentName = "Data Entry Form Fields";
                    ViewBag.CategoryName = "Field Type";
                    break;
                case "DataEntryOptions":
                    LookupTableVM.Categories = new SelectList(db.DataEntryFields.Where(s => s.Status == "Current" && s.FieldType.OptionValue == "lookup").AsEnumerable(), "DataEntryFieldID", "DataEntryFieldName");
                    ViewBag.ContentName = "Data Entry Lookup Options";
                    ViewBag.CategoryName = "Lookup Field";
                    break;
                //case "OptionFields":
                //    ViewBag.ContentName = "General Lookup Fields";
                //    break;
                //case "Options":
                //    LookupTableVM.Categories = new SelectList(db.OptionFields.Where(s => s.Status == "Current").AsEnumerable(), "OptionFieldID", "OptionFieldName");

                //    ViewBag.ContentName = "General Lookup Options";
                //    ViewBag.CategoryName = "Lookup Field";
                //    break;
            }
            return View("Create", LookupTableVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LookupTableViewModel lookupVM)
        {
            switch (lookupVM.LookupTableName)
            {
                case "AnimalSuppliers":
                    db.AnimalSuppliers.Add(
                        new AnimalSupplier()
                    {
                        AnimalSupplierName = lookupVM.Name,
                    }
                    );
                    break;
                case "Species":
                    db.Species.Add(
                        new Species()
                        {
                            SpeciesName = lookupVM.Name,
                        }
                    );
                    break;
                case "Strains":
                    db.Strains.Add(
                        new Strain()
                        {
                            StrainName = lookupVM.Name,
                            SpeciesID = lookupVM.CategoryID,
                        }
                    );
                    break;
                case "AnimalHusbandryFields":
                    db.AnimalHusbandryFields.Add(
                        new AnimalHusbandryField()
                        {
                            AnimalHusbandryFieldName = lookupVM.Name,
                            AnimalHusbandryFieldTypeID = lookupVM.CategoryID,
                        }
                    );
                    break;
                case "AnimalHusbandryOptions":
                    db.AnimalHusbandryOptions.Add(
                        new AnimalHusbandryOption()
                        {
                            AnimalHusbandryOptionName = lookupVM.Name,
                            AnimalHusbandryFieldID = lookupVM.CategoryID,
                        }
                    );
                    break;
                case "ProcedureDetailFields":
                    db.ProcedureDetailOptionFields.Add(
                        new ProcedureDetailOptionField()
                        {
                            ProcedureDetailOptionFieldName = lookupVM.Name,
                            ProcedureDetailFieldTypeID = lookupVM.CategoryID,
                            ProcedurePurposeOrTypeID = lookupVM.PurposeOrTypeID,
                        }
                    );
                    break;

                case "ProcedureDetailOptions":
                    db.ProcedureDetailOptions.Add(
                        new ProcedureDetailOption()
                        {
                            ProcedureDetailOptionName = lookupVM.Name,
                            ProcedureDetailOptionFieldID = lookupVM.CategoryID,
                        }
                    );
                    break;
                case "DataEntryFields":
                    db.DataEntryFields.Add(
                        new DataEntryField()
                        {
                            DataEntryFieldName = lookupVM.Name,
                            FieldTypeID = lookupVM.CategoryID,
                        }
                    );
                    break;
                case "DataEntryOptions":
                    db.DataEntryOptions.Add(
                        new DataEntryOption()
                        {
                            DataEntryOptionName = lookupVM.Name,
                            DataEntryFieldDataEntryFieldID = lookupVM.CategoryID,
                        }
                    );
                    break;
                //case "OptionFields":
                //    db.OptionFields.Add(
                //        new OptionField()
                //        {
                //            OptionFieldName = lookupVM.Name,
                //        }
                //    );
                //    break;
                //case "Options":
                //    db.Options.Add(
                //        new Option()
                //        {
                //            OptionValue = lookupVM.Name,
                //            OptionFieldOptionFieldID = lookupVM.CategoryID
                //        }
                //    );
                //    break;
            }

            db.SaveChanges();

            return RedirectToAction("Index", new { lookuptablename = lookupVM.LookupTableName });
        }



        static List<SelectListItem> _lookuptables = new List<SelectListItem>
        {
                        new SelectListItem {
                Value = "Species",
                Text = "Species",
            },
                         new SelectListItem {
                Value = "Strains",
                Text = "Strains",
            },
                                
            new SelectListItem {
                Value = "AnimalHusbandryFields",
                Text = "Animal Husbandry Fields",
            },                        
            new SelectListItem {
                Value = "AnimalHusbandryOptions",
                Text = "Animal Husbandry Options",
            },                        
            new SelectListItem {
                Value = "ProcedureDetailFields",
                Text = "Procedure Detail Fields",
            },                       
            new SelectListItem {
                Value = "ProcedureDetailOptions",
                Text = "Procedure Detail Options",
            },                       
            new SelectListItem {
                Value = "DataEntryFields",
                Text = "Data Entry Fields",
            },
               new SelectListItem {
                Value = "DataEntryOptions",
                Text = "Data Entry Options",
            },
            //   new SelectListItem {
            //    Value = "OptionFields",
            //    Text = "General Lookup Fields",
            //},
            //   new SelectListItem {
            //    Value = "Options",
            //    Text = "General Lookup Options",
            //},
        };

    }
}
