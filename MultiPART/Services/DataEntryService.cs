using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using MultiPART.Models.LinkTable;
using MultiPART.Models.LookupTable;
using MultiPART.Models.Table;
using MultiPART.Models.ViewModel;
using MultiPART.Models.ViewModel.DataEntryViewModels;
using MultiPART.Repositories;
using MultiPART.UnitOfWork;

namespace MultiPART.Services

{    
    public interface IDataEntryService
    {
        IEnumerable<DataEntryDesignViewModel> GetDesignViewModels(int procedureid);
        IEnumerable<DataEntryDesign> GetDataEntryDesigns(IEnumerable<DataEntryDesignViewModel> dataEntryDesignViewModels);
        bool ValidateDesigns(IEnumerable<DataEntryDesign> designs);
        IEnumerable<DataEntryFormPreviewViewModel> GetFormPreviewModel(int procedureid);
        DataEntryViewModel GetDataEntryViewModels(int administrationid, int animalid, int projectId, int researchGroupId, int diseaseModelInductionId);
    }

    public class DataEntryService : IDataEntryService
    {
        public DataEntryService(IValidationDictionary validationDictionary, IUnitOfWork uow, int userKey)
        {
            _dataEntryDesignRepository = uow.GetRepository<DataEntryDesign>();
            _dataEntryFieldRepository = uow.GetRepository<DataEntryField>();
            _animalAdministrationRepository = uow.GetRepository<AnimalAdministration>();
            _procedureRepository = uow.GetRepository<Procedure>();
            _administrationRepository = uow.GetRepository<Administration>();
            _dataEntryRepository = uow.DataEntryRepository;
            _animalRepository = uow.GetRepository<Animal>();
            _userKey = userKey;
            _fileService = new FileService(validationDictionary, uow, userKey);
            _validationDictionary = validationDictionary;
        }

        public DataEntryService(IValidationDictionary validationDictionary, IUnitOfWork uow)
            : this(validationDictionary, uow, 0)
        {
        }

        private readonly IGenericRepository<AnimalAdministration, int> _animalAdministrationRepository;
        private readonly IGenericRepository<DataEntryDesign, int> _dataEntryDesignRepository;
        private readonly IGenericRepository<DataEntryField, int> _dataEntryFieldRepository;
        private readonly IGenericRepository<Procedure, int> _procedureRepository;
        private readonly IGenericRepository<Administration, int> _administrationRepository;
        private readonly DataEntryRepository _dataEntryRepository;
        private readonly IGenericRepository<Animal, int> _animalRepository;
        private readonly int _userKey;
        private readonly FileService _fileService;
        private IValidationDictionary _validationDictionary;

        private Dictionary<Type, Type> _viewModelTypeByModelType = new Dictionary<Type, Type>()
            {
            {typeof (DataEntryDetailFile), typeof (DataEntryDetailFileViewModel)},
            {typeof (DataEntryDetailOption), typeof (DataEntryDetailOptionViewModel)},
            {typeof (DataEntryDetailValue), typeof (DataEntryDetailValueViewModel)}
            };

        private Dictionary<string, DataEntryViewModelType> _viewModelTypeByFieldType = new Dictionary
            <string, DataEntryViewModelType>
            {
                {EntryFieldType.Date.ToString(), DataEntryViewModelType.Value},
                {EntryFieldType.DateTime.ToString(), DataEntryViewModelType.Value},
                {EntryFieldType.File.ToString(), DataEntryViewModelType.File},
                {EntryFieldType.Integer.ToString(), DataEntryViewModelType.Value},
                {EntryFieldType.Lookup.ToString(), DataEntryViewModelType.Option},
            {EntryFieldType.Double.ToString(), DataEntryViewModelType.Value},
                {EntryFieldType.Text.ToString(), DataEntryViewModelType.Value},
                {EntryFieldType.Time.ToString(), DataEntryViewModelType.Value}
            };

        public IEnumerable<DataEntryDesignViewModel> GetDesignViewModels(int procedureid)
        {
            //Get current entry fields
            var entryFields = _dataEntryFieldRepository.GetCurrent();

            //Get current field designs for procedure
            var procedureDesigns =
                _dataEntryDesignRepository.GetCurrent().Where(design => design.ProcedureProcedureID == procedureid);

            //Left join query on entryFields and procedureDesignFields returned as DataEntryDesignViewModel
            var query =
                from entryField in entryFields
                join procedureDesign in procedureDesigns on entryField equals procedureDesign.DataEntryField into
                    designedFields
                from designedField in designedFields.DefaultIfEmpty()
                select new DataEntryDesignViewModel()
                {
                    ProcedureID = procedureid,
                    DataEntryFieldID = entryField.DataEntryFieldID,
                    DataEntryFieldName = entryField.DataEntryFieldName,
                    DataEntryDesignID = (designedField == null) ? 0 : designedField.DataEntryDesignID,
                    Mandatory = (designedField != null) && designedField.Mandatory,
                    Multiple = (designedField != null) && designedField.Multiple,
                    Selected = (designedField != null)
                };
            return query;
        }

        public IEnumerable<DataEntryDesign> GetDataEntryDesigns(
            IEnumerable<DataEntryDesignViewModel> dataEntryDesignViewModels)
        {
            //Get all field designs
            var dataEntryDesign = _dataEntryDesignRepository.GetAll();

            var query =
                from fieldDesignViewModel in dataEntryDesignViewModels
                join dbFieldDesign in dataEntryDesign
                on new { PID = fieldDesignViewModel.ProcedureID, FID = fieldDesignViewModel.DataEntryFieldID }
                    equals
                    new { PID = dbFieldDesign.ProcedureProcedureID, FID = dbFieldDesign.DataEntryFieldDataEntryFieldID }
                    into gj
                from fieldDesignDb in gj.DefaultIfEmpty()
                select
                    new DataEntryDesign
                    {
                        DataEntryDesignID = (fieldDesignDb == null) ? 0 : fieldDesignViewModel.DataEntryDesignID,
                        DataEntryFieldDataEntryFieldID = fieldDesignViewModel.DataEntryFieldID,
                        Mandatory = fieldDesignViewModel.Mandatory,
                        Multiple = fieldDesignViewModel.Multiple,
                        Status = fieldDesignViewModel.Selected ? "Current" : "Deleted",
                        ProcedureProcedureID = fieldDesignViewModel.ProcedureID,
                        LastUpdatedBy = _userKey,
                    };
            return query;
        }

        public bool ValidateDesigns(IEnumerable<DataEntryDesign> designs)
        {
            foreach (var design in designs)
            {
                if (design.DataEntryDesignID == 0 && design.Status == "Current")
                {
                    _dataEntryDesignRepository.Add(design);
                }
                else if (design.DataEntryDesignID > 0)
                {
                    DataEntryDesign existing = _dataEntryDesignRepository.Get(design.DataEntryDesignID);

                    if (existing != null)
                    {
                        if (existing.DataEntryFieldDataEntryFieldID == design.DataEntryFieldDataEntryFieldID &&
                            existing.ProcedureProcedureID == design.ProcedureProcedureID)
                        {
                            existing.Mandatory = design.Mandatory;
                            existing.Status = design.Status;
                            existing.Multiple = design.Multiple;
                        }
                        else
                        {
                            throw new Exception("DataEntryDesignID conflict with Procedure/DataEntryField ID mismatch");
                        }
                    }
                }
            }
            return false;
        }

        public IEnumerable<DataEntryFormPreviewViewModel> GetFormPreviewModel(int procedureid)
        {
            var currentFieldDesigns =
               _dataEntryDesignRepository.GetCurrent().Where(d => d.ProcedureProcedureID == procedureid);
            var projectId = _procedureRepository.Get(procedureid).MultiPARTProjectMultiPARTProjectID;
            var dataEntryDesignVm = from fieldDesign in currentFieldDesigns
                                    select new DataEntryFormPreviewViewModel
                                    {
                                        ProcedureID = procedureid,
                                        ProjectID = projectId,
                                        DataEntryFieldName = fieldDesign.DataEntryField.DataEntryFieldName,
                                        Mandatory = fieldDesign.Mandatory == true ? " * " : "",
                                        Multiple = fieldDesign.Multiple == true ? "____ ____ ____" : "____"
                                    };
            return dataEntryDesignVm;
        }

        public DataEntryViewModel GetDataEntryViewModels(int administrationid, int animalid, int projectId, int researchGroupId, int diseaseModelInductionId)
        {
            //Get animal
            var animal = _animalRepository.Get(animalid);

            //Get administration
            var administration = _administrationRepository.Get(administrationid);

            int procedureid = administration.ProcedureID;

            //Get active animaladministration
            var animalAdministration = 
               _animalAdministrationRepository.GetCurrent()
                   .FirstOrDefault(ap => ap.AnimalID == animalid && ap.AdministrationID == administrationid);

            if (animalAdministration == null)
            {
                animalAdministration = new AnimalAdministration()
                {
                    AnimalID = animalid,
                    Animal = animal,
                    AnimalAdministrationID = 0,
                    Administration = administration,
                    AdministrationID = administrationid,
                    StartTime = DateTime.Now,
                    EndTime = DateTime.Now
                };
            }
            //get any existing current dataentries
            var animalAdministrationEntries = animalAdministration.DataEntries != null ? animalAdministration.DataEntries.Where(de => de.IsActive) : new Collection<DataEntry>();

            //get any currrent designs from db for this procedure
            var designs = _dataEntryDesignRepository.GetCurrent().Where(de => de.ProcedureProcedureID == procedureid);

            //left join current designs with any current data entries in the database
            var query = from design in designs.AsEnumerable()
                        join entry in animalAdministrationEntries.AsEnumerable()
                            on design equals entry.DataEntryDesign into designEntriesGroup
                        //set default entry value for design without any current entries
                        from entries in designEntriesGroup.DefaultIfEmpty(new DataEntry()).AsEnumerable()
                        select new DataEntryFieldViewModel
                        {
                            ProjectID = projectId,
                            ResearchGroupID = researchGroupId,
                            AnimalID = animalid,
                            ProcedureID = procedureid,
                            DataEntryID = entries.DataEntryID,
                            DataEntryFieldName = design.DataEntryField.DataEntryFieldName,
                            DesignID = design.DataEntryDesignID,
                            ViewModelType = _viewModelTypeByFieldType[design.DataEntryField.FieldType.OptionValue],
                            Mandatory = design.Mandatory,
                            Multiple = design.Multiple,
                            Options = GetOptionsFromDesignId(design.DataEntryDesignID),
                            DataEntryDetails =
                                GetDataEntryDetailViewModels(entries.DataEntryDetails,
                                    design.DataEntryField.FieldType.OptionValue)
                        };

            //Create DataEntryViewModel from existing AnimalProcedure or create default new view model if none exists yet
            var dataEntryViewModel = new DataEntryViewModel()
            {
                StartTime = animalAdministration.StartTime,
                EndTime = animalAdministration.EndTime,
                ProcedureName = administration.Procedure.OptionsProcedureType.OptionValue,
                Species = animal.Cohort.Strain.StrainName,
                Strain = animal.Cohort.Strain.Species.SpeciesName,
                AnimalLabel = animal.AnimalLabel,
                AnimalID = animalid,
                ProjectID = projectId,
                ResearchGroupID = researchGroupId,
                DiseaseModelInductionID = diseaseModelInductionId,
                // ReSharper disable once PossibleNullReferenceException
                AnimalAdministrationID = animalAdministration.AnimalAdministrationID,
                AdministrationID = administrationid,
                ProcedureID = procedureid,
                Properties = new Collection<DataEntryFieldViewModel>(query.ToList())
            };
            return dataEntryViewModel;
        }

        private Collection<DataEntryDetailViewModel> GetDataEntryDetailViewModels(
            IEnumerable<DataEntryDetail> dbDataEntryDetails, string fieldType)
        {
            //Create empty collection<BaseViewModel class)
            var viewModelCollection = new Collection<DataEntryDetailViewModel>();

            //return empty collection if dbDataEntryDetails is null
            if (dbDataEntryDetails == null)
            {
                return viewModelCollection;
            }

            foreach (DataEntryDetail detail in dbDataEntryDetails)
            {
                DataEntryDetailViewModel entryViewModel;
                switch (fieldType)
                {
                    case "File":
                        entryViewModel = GetFileDetailViewModel(detail);
                        break;
                    case "Lookup":
                        entryViewModel = GetOptionDetailViewModel(detail);
                        break;
                    case "DateTime":
                    case "Double":
                    case "Integer":
                    case "Date":
                    case "Time":
                    case "Text":
                        entryViewModel = GetValueDetailViewModel(detail);
                        break;
                    default:
                        entryViewModel = null;
                        break;
                }
                viewModelCollection.Add(entryViewModel);
            }
            return viewModelCollection;
        }

        private DataEntryDetailViewModel GetValueDetailViewModel(DataEntryDetail detail)
        {
            var valueDetail = (DataEntryDetailValue)detail;
            DataEntryDetailViewModel entryViewModel = new DataEntryDetailValueViewModel()
            {
                DataEntryDataEntryID = valueDetail.DataEntryDataEntryID,
                DataEntryDetailID = valueDetail.DataEntryDetailID,
                StartTime = valueDetail.StartTime,
                EndTime = valueDetail.EndTime,
                Value = valueDetail.Value,
                DesignID = detail.DataEntry.DataEntryDesignDataEntryDesignID,
                Active = valueDetail.IsActive

            };
            return entryViewModel;
        }

        private DataEntryDetailViewModel GetOptionDetailViewModel(DataEntryDetail detail)
        {
            var optionDetail = (DataEntryDetailOption)detail;
            DataEntryDetailViewModel entryViewModel = new DataEntryDetailOptionViewModel()
            {
                DataEntryDataEntryID = optionDetail.DataEntryDataEntryID,
                DataEntryDetailID = optionDetail.DataEntryDetailID,
                StartTime = optionDetail.StartTime,
                EndTime = optionDetail.EndTime,
                OptionID = optionDetail.DataEntryOptionID,
                Options = GetOptionsFromDesignId(optionDetail.DataEntry.DataEntryDesignDataEntryDesignID),
                DesignID = detail.DataEntry.DataEntryDesignDataEntryDesignID,
                Active = optionDetail.IsActive
            };
            return entryViewModel;
        }

        private DataEntryDetailViewModel GetFileDetailViewModel(DataEntryDetail detail)
        {
            var fileDetail = (DataEntryDetailFile)detail;
            var dataEntry = detail.DataEntry;
            DataEntryDetailViewModel entryViewModel =
                new DataEntryDetailFileViewModel
            {
                DataEntryDataEntryID = fileDetail.DataEntryDataEntryID,
                DataEntryDetailID = fileDetail.DataEntryDetailID,
                Description = fileDetail.File.Description,
                EndTime = fileDetail.EndTime,
                FileID = fileDetail.FileID,
                FileType = fileDetail.File.FileType,
                FileUrl = fileDetail.File.FileUrl,
                StartTime = fileDetail.StartTime,
                DesignID = detail.DataEntry.DataEntryDesignDataEntryDesignID,
                Active = fileDetail.IsActive,
                ProjectID = dataEntry.DataEntryDesign.Procedure.MultiPARTProjectMultiPARTProjectID,
                ProcedureID = dataEntry.AnimalAdministration.Administration.ProcedureID, // AnimalProcedureID,  //chris: is it a bug? why animalprocedureid assigned as procedureid?
                AnimalID = dataEntry.AnimalAdministration.AnimalID,
                };
            return entryViewModel;
        }

        public bool CreateExperimentData(DataEntryViewModel dataEntryViewModel)
        {
            //Get list of DataEntryFieldViewModels(Properties) from view model
            var propertyViewModels = dataEntryViewModel.Properties.ToList();

            //Remove properties that contain no DataEntryDetails, they have nothing to add to database
            propertyViewModels.RemoveAll(p => p.DataEntryDetails == null);

            // Validate submitted properties
            if (!ValidateProperties(propertyViewModels) | !_validationDictionary.IsValid) return false;

            var entries = new Collection<DataEntry>(
                (
                    from property in propertyViewModels
                    select new DataEntry()
                    {
                        DataEntryDesignDataEntryDesignID = property.DesignID,
                        DataEntryDetails = GetDetailsFromProperty(property).ToList()
                    }).ToList());

            //Get animalProcedure
            var animalAdministration = _animalAdministrationRepository.Get(dataEntryViewModel.AnimalAdministrationID);

            if (animalAdministration == null)
            {
                // create new record
                animalAdministration = new AnimalAdministration()
                {
                    AnimalID = dataEntryViewModel.AnimalID,
                    StartTime = dataEntryViewModel.StartTime,
                    EndTime = dataEntryViewModel.EndTime,
                    CreatedBy = _userKey,
                    LastUpdatedBy = _userKey,
                    AdministrationID = dataEntryViewModel.AdministrationID,
                    DataEntries = entries
                };
                _animalAdministrationRepository.Add(animalAdministration);
            }
            else
            {
                //update current record
                foreach (var entry in animalAdministration.DataEntries)
                {
                    entry.SoftDelete(true);
                }
                animalAdministration.DataEntries = entries;
            }
           
            return true;
        }

        private bool ValidateProperties(List<DataEntryFieldViewModel> propertyViewModels)
        {
            bool valid = true;
            foreach (var property in propertyViewModels)
            {
                //check for multiple details
                if (!property.Multiple && property.DataEntryDetails.Count > 1)
                {
                    _validationDictionary.AddError("Properties", "This property does not allow multiple details");
                    valid = false;
                }
                //check all details have value/option/file submited

                foreach (var detail in property.DataEntryDetails)
                {
                    if (!detail.Validate(_validationDictionary)) valid = false;
                }
            }
            return valid;
        }

        private IEnumerable<DataEntryDetail> GetDetailsFromProperty(DataEntryFieldViewModel property)
        {
            switch (property.ViewModelType)
            {
                case DataEntryViewModelType.File:

                    var query = from detail in property.DataEntryDetails
                                select detail as DataEntryDetailFileViewModel;
                    return from fileDetail in query
                           select new DataEntryDetailFile()
                           {
                               CreatedBy = _userKey,

                               StartTime = fileDetail.StartTime,
                               EndTime = fileDetail.EndTime,
                               File = _fileService.UploadNewFile(fileDetail),
                               LastUpdatedBy = _userKey,
                               DataEntryDataEntryID = property.DataEntryID,
                               DataEntryDetailID = fileDetail.DataEntryDetailID
                           };
                case DataEntryViewModelType.Option:
                    var optionQuery = from detail in property.DataEntryDetails
                                      select detail as DataEntryDetailOptionViewModel;
                    return from optionDetail in optionQuery
                           select new DataEntryDetailOption()
                           {
                               CreatedBy = _userKey,
                               StartTime = optionDetail.StartTime,
                               EndTime = optionDetail.EndTime,
                               DataEntryOptionID = optionDetail.OptionID,
                               DataEntryDataEntryID = property.DataEntryID,
                               DataEntryDetailID = optionDetail.DataEntryDetailID
                           };
                case DataEntryViewModelType.Value:
                    var valueQuery = from detail in property.DataEntryDetails
                                     select detail as DataEntryDetailValueViewModel;
                    return from valueDetail in valueQuery
                           select new DataEntryDetailValue()
                           {
                               CreatedBy = _userKey,
                               StartTime = valueDetail.StartTime,
                               EndTime = valueDetail.EndTime,
                               Value = valueDetail.Value,
                               DataEntryDataEntryID = property.DataEntryID,
                               DataEntryDetailID = valueDetail.DataEntryDetailID
                           };

            }
            throw new Exception("ViewModelType not recognised");
        }

        public void RepopulateViewModel(DataEntryViewModel dataEntryViewModel)
        {
            foreach (
                var property in
                    dataEntryViewModel.Properties.Where(p => p.ViewModelType == DataEntryViewModelType.Option && p.DataEntryDetails != null))
            {
                property.Options = GetOptionsFromDesignId(property.DesignID);
                foreach (var detail in property.DataEntryDetails)
                {
                    var optionDetail = (DataEntryDetailOptionViewModel)detail;
                    optionDetail.Options = GetOptionsFromDesignId(property.DesignID);
                }
            }
        }

        private IEnumerable<SelectListItem> GetOptionsFromDesignId(int designId)
        {
            var design = _dataEntryDesignRepository.Get(designId);
            //if (design.DataEntryField.FieldType.OptionValue != EntryFieldType.Lookup.ToString()) throw new Exception("Cannot find SelectListItems for non Lookup detail type");
            return new List<SelectListItem>(from option in design.DataEntryField.DataEntryOptions
                                            select new SelectListItem()
                                            {
                                                Text = option.DataEntryOptionName,
                                                Value = option.DataEntryOptionID.ToString(CultureInfo.InvariantCulture)
                                            });
        }

        public AnimalAdministration GetAnimalAdministration(int animalId, int administrationId)
        {
            try
            {
                return
    _animalAdministrationRepository.GetCurrent().FirstOrDefault(ap => ap.AnimalID == animalId && ap.AdministrationID == administrationId);

            }
            catch (Exception)
            {

                return null;
            }
        }
    }
}