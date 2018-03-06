using System.Runtime.ConstrainedExecution;
using MultiPART.Models.LinkTable;
using MultiPART.Models.LookupTable;
using MultiPART.Models.Table;
using System.Data.Entity;
using MultiPART.UnitOfWork;

namespace MultiPART.Models
{
    public class MultipartContext : DbContext, IDbContext
    {
        public MultipartContext()
            : base("name=DefaultConnection")
        {
        }

        public virtual DbSet<UserProfile> UserProfiles { get; set; }
        public virtual DbSet<Cohort> Cohorts { get; set; }
        public virtual DbSet<Institution> Institutions { get; set; }
        public virtual DbSet<MultiPARTProject> MultiPARTProjects { get; set; }
        public virtual DbSet<Procedure> Procedures { get; set; }
        public virtual DbSet<Researchgroup> Researchgroups { get; set; }
        public virtual DbSet<Animal> Animals { get; set; }
        public virtual DbSet<AnimalHusbandry> AnimalHusbandries { get; set; }
        public virtual DbSet<DataEntryDesign> DataEntryDesigns { get; set; }

        public virtual DbSet<DataEntryDetail> DataEntryDetails { get; set; }
        public virtual DbSet<DataEntryDetailFile> DataEntryDetailFiles { get; set; }
        public virtual DbSet<DataEntryDetailOption> DataEntryDetailOptions { get; set; }
        public virtual DbSet<DataEntryDetailValue> DataEntryDetailValues { get; set; }
        public virtual DbSet<File> Files { get; set; }
        public virtual DbSet<LoginTracker> LoginTrackers { get; set; }
        public virtual DbSet<Administration> Administrations { get; set; }
        public virtual DbSet<BehavioralScore> BehavioralScores { get; set; }

        //lookup tables
        public virtual DbSet<AnimalSupplier> AnimalSuppliers { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Option> Options { get; set; }
        public virtual DbSet<OptionField> OptionFields { get; set; }
        public virtual DbSet<Species> Species { get; set; }
        public virtual DbSet<Strain> Strains { get; set; }
        public virtual DbSet<ProcedureDetailOption> ProcedureDetailOptions { get; set; }
        public virtual DbSet<ProcedureDetailOptionField> ProcedureDetailOptionFields { get; set; }
        public virtual DbSet<ProcedureDetail> ProcedureDetails { get; set; }
        public virtual DbSet<DataEntryField> DataEntryFields { get; set; }
        public virtual DbSet<DataEntryOption> DataEntryOptions { get; set; }
        public virtual DbSet<AnimalHusbandryField> AnimalHusbandryFields { get; set; }
        public virtual DbSet<AnimalHusbandryOption> AnimalHusbandryOptions { get; set; }
        public virtual DbSet<SIUnit> SIUnits { get; set; }
        public virtual DbSet<Unit> Units { get; set; }
        public virtual DbSet<Question> Questions { get; set; }

        //link tables
        public virtual DbSet<DataEntry> DataEntries { get; set; }
        public virtual DbSet<Careerhistory> Careerhistories { get; set; }
        public virtual DbSet<ResearchgroupInMultiPARTProject> ResearchgroupInMultiPARTProjects { get; set; }
        public virtual DbSet<AnimalAdministration> AnimalAdministrations { get; set; }
        public virtual DbSet<UserDataentryAssignment> UserDataentryAssignments { get; set; }
        public virtual DbSet<UserInResearchgroup> UserInResearchgroups { get; set; }
        public virtual DbSet<UserProjectAssignment> UserProjectAssignments { get; set; }
        public virtual DbSet<CohortProcedureAssignment> CohortProcedureAssignments { get; set; }
        public virtual DbSet<ResearchgroupCohortAssignment> ResearchgroupCohortAssignments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SIUnit>()
              .HasMany(e => e.Units)
              .WithRequired(e => e.SIUnite)
              .WillCascadeOnDelete(true);

            modelBuilder.Entity<Species>()
                .HasMany(e => e.Strains)
                .WithRequired(e => e.Species)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<UserProfile>()
                .HasMany(e => e.Careerhistories)
                .WithRequired(e => e.UserProfile)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserProfile>()
                .HasMany(e => e.UserInResearchgroups)
                .WithRequired(e => e.UserProfiles)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Institution>()
                .HasMany(e => e.Careerhistories)
                .WithRequired(e => e.Institutions)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Institution>()
              .HasMany(e => e.Researchgroups)
              .WithRequired(e => e.Institutions)
              .WillCascadeOnDelete(true);

            modelBuilder.Entity<Country>()
              .HasMany(e => e.Institutions)
              .WithRequired(e => e.Countries)
              .WillCascadeOnDelete(true);

            modelBuilder.Entity<MultiPARTProject>()
                .HasMany(e => e.ResearchgroupInMultiPARTProjects)
                .WithRequired(e => e.MultiPartProject)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Researchgroup>()
             .HasMany(e => e.UserInResearchgroups)
             .WithRequired(e => e.Researchgroups)
             .WillCascadeOnDelete(true);

            modelBuilder.Entity<Researchgroup>()
             .HasMany(e => e.ResearchgroupInMultiPARTProjects)
             .WithRequired(e => e.Researchgroups)
             .WillCascadeOnDelete(true);

            modelBuilder.Entity<ResearchgroupInMultiPARTProject>()
               .HasMany(e => e.UserProjectAssignments)
               .WithRequired(e => e.ResearchgroupInMultiPARTProjects)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserInResearchgroup>()
               .HasMany(e => e.UserProjectAssignments)
               .WithRequired(e => e.UserInResearchgroups)
               .WillCascadeOnDelete(true);

            modelBuilder.Entity<OptionField>()
               .HasMany(e => e.Options)
               .WithRequired(e => e.OptionFields)
               .WillCascadeOnDelete(true);

            modelBuilder.Entity<Option>()
               .HasMany(e => e.UserInResearchgroups)
               .WithRequired(e => e.Options)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<Option>()
                .HasMany(e => e.ResearchgroupInMultiPARTProjects)
                .WithRequired(e => e.Options)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Option>()
                .HasMany(e => e.UserProjectAssignments)
                .WithRequired(e => e.Options)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Option>()
                .HasMany(e => e.Procedures)
                .WithOptional(e => e.OptionsProcedureType)
                .HasForeignKey(e => e.ProcedureTypeID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Option>()
                .HasMany(e => e.Procedures)
                .WithRequired(e => e.OptionsProcedurePurpose)
                .HasForeignKey(e => e.ProcedurePurposeID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Option>()
                .HasMany(e => e.DataEntryFields)
                .WithRequired(e => e.FieldType)
                .HasForeignKey(e => e.FieldTypeID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MultiPARTProject>()
               .HasMany(e => e.Procedures)
               .WithRequired(e => e.MultiPARTProjects)
               .WillCascadeOnDelete(true);

            modelBuilder.Entity<MultiPARTProject>()
               .HasMany(e => e.Cohorts)
               .WithRequired(e => e.MultiPARTProjects)
               .WillCascadeOnDelete(true);

            modelBuilder.Entity<Strain>()
               .HasMany(e => e.Cohorts)
               .WithOptional(e => e.Strain)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<Option>()
               .HasMany(e => e.Cohorts)
               .WithOptional(e => e.OptionsSex)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<Option>()
               .HasMany(e => e.Animals)
               .WithOptional(e => e.OptionsSex)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<Option>()
               .HasMany(e => e.Cohorts)
               .WithOptional(e => e.OptionsCategoricalAge)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<AnimalSupplier>()
               .HasMany(e => e.Cohorts)
               .WithOptional(e => e.AnimalSuppliers)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<Procedure>()
              .HasMany(e => e.CohortProcedureAssignments)
              .WithRequired(e => e.Procedures)
              .WillCascadeOnDelete(false);

            modelBuilder.Entity<Animal>()
                .HasMany(e => e.AnimalAdministrations)
                .WithRequired(e => e.Animal)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Administration>()
                .HasMany(e => e.AnimalAdministrations)
                .WithRequired(e => e.Administration)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Cohort>()
             .HasMany(e => e.CohortProcedureAssignments)
             .WithRequired(e => e.Cohorts)
             .WillCascadeOnDelete(true);

            modelBuilder.Entity<Cohort>()
             .HasMany(e => e.ResearchgroupCohortAssignments)
             .WithRequired(e => e.Cohorts)
             .WillCascadeOnDelete(true);

            modelBuilder.Entity<Researchgroup>()
             .HasMany(e => e.ResearchgroupCohortAssignments)
             .WithRequired(e => e.Researchgroups)
             .WillCascadeOnDelete(true);

            modelBuilder.Entity<Cohort>()
             .HasMany(e => e.Animals)
             .WithRequired(e => e.Cohort)
             .WillCascadeOnDelete(false);

            modelBuilder.Entity<Researchgroup>()
             .HasMany(e => e.Animals)
             .WithRequired(e => e.Researchgroup)
             .WillCascadeOnDelete(false);

            modelBuilder.Entity<Researchgroup>()
             .HasMany(e => e.AnimalHusbandries)
             .WithRequired(e => e.Researchgroups)
             .WillCascadeOnDelete(true);

            modelBuilder.Entity<Strain>()
             .HasMany(e => e.AnimalHusbandries)
             .WithRequired(e => e.Strains)
             .WillCascadeOnDelete(true);

            modelBuilder.Entity<MultiPARTProject>()
             .HasMany(e => e.AnimalHusbandries)
             .WithRequired(e => e.MultiPARTProjects)
             .WillCascadeOnDelete(true);

            modelBuilder.Entity<ProcedureDetailOptionField>()
            .HasMany(e => e.ProcedureDetailOptions)
            .WithRequired(e => e.ProcedureDetailOptionFields)
            .WillCascadeOnDelete(false);

            modelBuilder.Entity<Procedure>()
            .HasMany(e => e.ProcedureDetails)
            .WithRequired(e => e.Procedures)
            .WillCascadeOnDelete(true);

            modelBuilder.Entity<ProcedureDetailOption>()
            .HasMany(e => e.ProcedureDetails)
            .WithOptional(e => e.ProcedureDetailOptions)
            .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProcedureDetailOptionField>()
            .HasMany(e => e.ProcedureDetails)
            .WithRequired(e => e.ProcedureDetailOptionFields)
            .WillCascadeOnDelete(false);

            modelBuilder.Entity<Procedure>()
            .HasMany(e => e.DataEntryDesigns)
            .WithRequired(e => e.Procedure)
            .WillCascadeOnDelete(true);

            modelBuilder.Entity<AnimalAdministration>()
                .HasMany(e => e.DataEntries)
                .WithRequired(e => e.AnimalAdministration)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<DataEntryDesign>()
           .HasMany(e => e.DataEntries)
           .WithRequired(e => e.DataEntryDesign)
           .WillCascadeOnDelete(true);

            modelBuilder.Entity<DataEntry>()
                .HasMany(e => e.DataEntryDetails)
                .WithRequired(e => e.DataEntry)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<DataEntryField>()
            .HasMany(e => e.DataEntryDesigns)
            .WithRequired(e => e.DataEntryField)
            .WillCascadeOnDelete(true);

            modelBuilder.Entity<DataEntryField>()
            .HasMany(e => e.DataEntryOptions)
            .WithRequired(e => e.DataEntryField)
            .WillCascadeOnDelete(true);

            modelBuilder.Entity<DataEntryDetailOption>()
            .HasRequired(e => e.DataEntryOption)
            .WithMany(e => e.DataEntryDetailOptions)
            .WillCascadeOnDelete(false);

            modelBuilder.Entity<DataEntryDetailFile>()
                .HasRequired(e => e.File)
                .WithMany(e => e.DataEntryDetailFiles)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<File>()
               .HasMany(e => e.MultiPARTProjects)
               .WithOptional(e => e.Files)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<AnimalHusbandryField>()
             .HasMany(e => e.AnimalHusbandries)
             .WithRequired(e => e.AnimalHusbandryFields)
             .WillCascadeOnDelete(true);

            modelBuilder.Entity<AnimalHusbandryOption>()
                .HasMany(e => e.AnimalHusbandries)
                .WithOptional(e => e.AnimalHusbandryOptions)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AnimalHusbandryField>()
                .HasMany(e => e.AnimalHusbandryOptions)
                .WithRequired(e => e.AnimalHusbandryFields)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Option>()
                .HasMany(e => e.AnimalHusbandryFields)
                .WithRequired(e => e.Options)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Option>()
                .HasMany(e => e.ProcedureDetailOptionFields)
                .WithRequired(e => e.ProcedureDetailFieldType)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Option>()
                .HasMany(e => e.ProcedureDetailOptionFields)
                .WithRequired(e => e.ProcedurePurposeOrType)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Procedure>()
                .HasMany(e => e.Administrations)
                .WithRequired(e => e.Procedure)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<ProcedureDetail>()
                .HasMany(e => e.Questions)
                .WithRequired(e => e.Checklist)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<UserDataentryAssignment>()
                .HasRequired(e => e.Users)
                .WithMany(e => e.UserDataentryAssignments)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<UserDataentryAssignment>()
                .HasRequired(e => e.DataEntry)
                .WithMany(e => e.UserDataentryAssignments)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BehavioralScore>()
                .HasRequired(e => e.Question)
                .WithMany(e => e.BehavioralScores)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<BehavioralScore>()
                .HasRequired(e => e.UserDataentryAssignment)
                .WithMany(e => e.BehavioralScores)
                .WillCascadeOnDelete(true);
        }

    }
}