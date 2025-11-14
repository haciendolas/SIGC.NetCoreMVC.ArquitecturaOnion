using SIGC.DomainModel.Dtos;

namespace SIGC.DomainService.IServices
{
    public interface IFileStorageService
    {
        Task CreateAsync(IFileEntry FileEntry, Stream Stream, CancellationToken CancellationToken = default);
        Task<byte[]> ReadAsync(IFileEntry FileEntry, CancellationToken CancellationToken = default);
        Task DeleteAsync(IFileEntry FileEntry, CancellationToken CancellationToken = default);
        string GetFileUrl(IFileEntry FileEntry);
    }
}