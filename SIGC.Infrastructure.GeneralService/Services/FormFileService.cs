using Microsoft.AspNetCore.Http;
using SIGC.DomainService.IServices;

namespace SIGC.Infrastructure.GeneralService.Services
{
    public class FormFileService : IFileDataService
    {
        private readonly IFormFile FormFile;

        public FormFileService(IFormFile FormFile)
        {
            this.FormFile = FormFile;
        }

        public string FileName => FormFile.FileName;
        public string ContentType => FormFile.ContentType;
        public Stream OpenReadStream() => FormFile.OpenReadStream();
    }
}