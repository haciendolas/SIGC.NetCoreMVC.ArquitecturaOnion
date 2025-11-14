using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using SIGC.DomainModel.Dtos;
using SIGC.DomainService.IServices;

namespace SIGC.Infrastructure.GeneralService.Services
{
    internal class LocalFileStorageServuce : IFileStorageService
    {
        private readonly LocalOptions LocalOptions;
        private readonly IHttpContextAccessor HttpContextAccessor;
        private readonly IHostEnvironment HostEnvironment;
    
        public LocalFileStorageServuce(IOptions<LocalOptions> LocalOptions, IHttpContextAccessor HttpContextAccessor,IHostEnvironment HostEnvironment)
        {
            this.LocalOptions = LocalOptions.Value;
            this.HttpContextAccessor = HttpContextAccessor;
            this.HostEnvironment = HostEnvironment;
        }

        public async Task CreateAsync(IFileEntry FileEntry, Stream Stream, CancellationToken CancellationToken = default)
        {   
            var BasePath = Path.Combine(HostEnvironment.ContentRootPath, LocalOptions.PhysicalPathBase);

            var FilePath = Path.Combine(BasePath, FileEntry.FileLocation); 

            var Folder = Path.GetDirectoryName(FilePath);

            if (!Directory.Exists(Folder)) Directory.CreateDirectory(Folder); 

            // using (var FileStream = File.Create(FilePath))         
            using (var FileStream = new FileStream(FilePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                await Stream.CopyToAsync(FileStream, CancellationToken);
            }
        }

        public async Task DeleteAsync(IFileEntry FileEntry, CancellationToken CancellationToken = default)
        {
            await Task.Run(() => {
                var FilePath = Path.Combine(HostEnvironment.ContentRootPath, LocalOptions.PhysicalPathBase, FileEntry.FileLocation);     
                if (File.Exists(FilePath)) File.Delete(FilePath);
            }, CancellationToken);
        }

        public Task<byte[]> ReadAsync(IFileEntry FileEntry, CancellationToken CancellationToken = default)
        {
            var FilePath = Path.Combine(HostEnvironment.ContentRootPath, LocalOptions.PhysicalPathBase, FileEntry.FileLocation);

            return File.ReadAllBytesAsync(FilePath, CancellationToken);
        }

        public string GetFileUrl(IFileEntry fileEntry)
        {
            var baseUrl = GetVirtualBasePath();
            return $"{baseUrl}/{fileEntry.FileLocation}";
        }

        private string GetVirtualBasePath()
        {
            var httpContext = HttpContextAccessor.HttpContext!;
            var baseUrl = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}";
            var basePath = httpContext.Request.PathBase.HasValue ? httpContext.Request.PathBase.Value : "";
            return $"{baseUrl}{basePath}{LocalOptions.VirtualPathBase}";
        }
    }
}