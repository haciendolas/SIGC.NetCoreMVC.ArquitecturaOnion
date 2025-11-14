namespace SIGC.DomainModel.Dtos
{
    public class FileEntryDto(string FileName, string FileLocation) : IFileEntry
    {
        public string FileName { get; set; } = FileName;
        public string FileLocation { get; set; } = FileLocation;
    }
}