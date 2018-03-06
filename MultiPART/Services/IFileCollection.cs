using System;
using System.Collections.Generic;
using System.Web;

namespace MultiPART.Services
{
    public interface IFileCollection
    {
        IEnumerable<IFileWrapper> Files { get; set; }
    }

    public interface IFileWrapper
    {
        HttpPostedFileBase File { get; set; }
        String Description { get; set; }
        string RelativePath { get; }
    }
}