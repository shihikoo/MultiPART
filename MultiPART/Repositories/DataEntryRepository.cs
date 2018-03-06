using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using MultiPART.Models;
using MultiPART.Models.LinkTable;
using MultiPART.UnitOfWork;

namespace MultiPART.Repositories
{
    public class DataEntryRepository : GenericRepository<IDbContext, DataEntry, int>, IInsertOrUpdateRepository<IDbContext, DataEntry, int>
    {
        public DataEntryRepository(IDbContext context)
            : base(context)
        {
            Context = context;
        }

        public IDbContext Context { get; set; }

        /// <summary>
        /// Inserts entities with Key 0 as new records otherwise looks up record based on key performs update
        /// </summary>
        /// <param name="dataEntry"></param>
        public void InsertOrUpdate(IEnumerable<DataEntry> dataEntries)
        {
            foreach (DataEntry dataEntry in dataEntries)
            {
                if (dataEntry.DataEntryID == 0)
                {
                    Context.Entry(dataEntry).State =
                        EntityState.Added;
                    foreach (var detail in dataEntry.DataEntryDetails)
                    {
                        Context.Entry(detail).State =
                            EntityState.Added;
                    }
                }
                else
                {
                    Context.Entry(Get(dataEntry.DataEntryID)).CurrentValues.SetValues(dataEntry);
                    foreach (var detail in dataEntry.DataEntryDetails)
                    {
                        if (detail.DataEntryDetailID == 0)
                        {
                            Context.Entry(detail).State =
                                EntityState.Added;
                        }
                        else
                        {
                            if (dataEntry.DataEntryID == 0)
                            {
                                Context.Entry(detail).State =
                                                               EntityState.Modified;
                            }
                        }
                    
                    }    
                }
            }
        }
    }
}