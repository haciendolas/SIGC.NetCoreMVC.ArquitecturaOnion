namespace SIGC.DomainModel.Dtos
{
    public class LocalOptions
    {
        public string PhysicalPathBase { get; set; } // Ruta física en el sistema de archivos
        public string VirtualPathBase { get; set; }  // Ruta virtual desde donde se servirán los archivos
    }
}