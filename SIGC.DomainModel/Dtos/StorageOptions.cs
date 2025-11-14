namespace SIGC.DomainModel.Dtos
{
    public class StorageOptions
    {
        public string Provider { get; set; }

        public LocalOptions Local { get; set; }       

        public bool UsedLocal()
        {
            return Provider == "Local";
        }

        public bool UsedAmazon()
        {
            return Provider == "AmazonS3";
        }
 
    }
}
