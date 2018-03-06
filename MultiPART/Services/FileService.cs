using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using MultiPART.Migrations;
using MultiPART.Repositories;
using MultiPART.UnitOfWork;
using MultiPART.Utilities;
using File = MultiPART.Models.Table.File;

namespace MultiPART.Services
{
    public class FileService
    {
        private readonly IGenericRepository<File> _fileRepository;

        public FileService(IValidationDictionary validationDictionary, IUnitOfWork uow,int userKey)
        {
            _fileRepository = uow.GetRepository<File>();
            _userKey = userKey;
        }

        public static string[] ValidFileTypes =
            {
                "application/pdf",
                "application/msword",
                "application/postscript",
                "application/zip",
                "application/x-compressed",
                "application/x-zip-compressed",
                "application/octet-stream",
                "multipart/x-zip",
                "multipart/x-gzip",
                "application/x-gzip",
                "application/vnd.ms-excel",
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                "image/bmp",
                "image/x-windows-bmp",
                "image/gif",
                "image/jpeg",
                "image/pjpeg",
                "image/png",
                "image/tiff",
                "image/x-tiff"
            };

        private readonly int _userKey;

        bool VerifyFileType(IFileWrapper postedFileWrapper)
        {
            //verify that file is on the allowed files list
            
                if (!ValidFileTypes.Contains(postedFileWrapper.File.ContentType))
                {
                    return false;
                }
            
            return true;
        }
        bool VerifyMultipleFileType(IFileCollection fileUploadViewModel)
        {
            //iterate through files in upload collection and verify that they are on the allowed files list
            foreach (IFileWrapper fileWrapper in fileUploadViewModel.Files)
            {
                if (!VerifyFileType(fileWrapper))
                {
                    return false;
                }
            }
            return true;
        }
        bool CollectionContainsFiles(IFileCollection fileUploadViewModel)
        {
            //iterate thorugh upload collection and check if each object contains a file
            foreach (IFileWrapper file in fileUploadViewModel.Files)
            {
                if (!ContainsFile(file))
                {
                    return false;
                }
            }
            return true;
        }
        bool ContainsFile(IFileWrapper fileWrapper)
        {
            return fileWrapper.File.HasFile();
        }

        public File UploadNewFile(IFileWrapper fileWrapper)
        {
            string relativeUploadPath = fileWrapper.RelativePath;
            var uploadDir = WebConfigurationManager.AppSettings["fileUploadDirectory"];

            relativeUploadPath = uploadDir +"/" + relativeUploadPath;
            string directoryPath = HttpContext.Current.Server.MapPath(relativeUploadPath);
            
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            var fileName = fileWrapper.File.FileName;
            fileName= fileName.Replace(" ", "-");
            var filePath = Path.Combine(directoryPath, fileName);
            var virtualPath = Path.Combine(relativeUploadPath, fileName);
                
                //save actual file
                fileWrapper.File.SaveAs(filePath);
                
                //create new file record in database
                var fileRecord = new File()
                {
                    FileName = fileName,
                    Description = fileWrapper.Description,
                    FileType = fileWrapper.File.ContentType,
                    FileUrl = virtualPath,
                    CreatedBy = _userKey
                };
                 _fileRepository.Add(fileRecord);
            return fileRecord;
        }

        

    
     void UploadNewFiles(IFileCollection fileUploadViewModel)
        {
           
            //iterate through each fileViewModel in collection
            foreach (IFileWrapper file in fileUploadViewModel.Files)
            {
                UploadNewFile(file);
            }
            
        }

    }
}