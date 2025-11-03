namespace SIGC.Presentation.AspNetCoreMVC.Helpers
{
    public class DataTableHelper
    {
        /// <summary>
        /// Request sequence number sent by DataTable, same value must be returned in response
        /// </summary>   
        public string sEcho { get; set; }
        /// <summary>
        /// Text used for filtering
        /// </summary>
        public string sSearch { get; set; }
        /// <summary>
        /// Number of records that should be shown in table
        /// </summary>
        public int iDisplayLength { get; set; }
        /// <summary>
        /// First record that should be shown(used for paging)
        /// </summary>
        public int iDisplayStart { get; set; }
        /// <summary>
        /// Number of columns in table
        /// </summary>
        public int iColumns { get; set; }
        /// <summary>
        /// Number of columns that are used in sorting
        /// </summary>
        public int iSortingCols { get; set; }
        /// <summary>
        /// Comma separated list of column names
        /// </summary>
        public string sColumns { get; set; }
        /// <summary>
        /// Selected col for ordering
        /// </summary>
        public Int32 iSortCol_0 { get; set; }
        /// <summary>
        /// Selected col order asc or desc
        /// </summary>
        public string sSortDir_0 { get; set; }

        //Parametros Adicionales
        public int sCompanyID { get; set; }
        public short sStateID { get; set; }
        public short? sTaxpayerTypeID { get; set; }
        public short? sRubroID { get; set; }
        public string? sCompanyDocumentNumber { get; set; }
        public string? sCompanySocialReason { get; set; }
    }
}
