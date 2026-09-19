using SIGC.DomainModel.Enums;

namespace SIGC.DomainModel.ValueObjects
{
    public sealed record CatalogTherapeuticAction
    (   int CompanyID,
        int CatalogID,
        short TherapeuticActionID, 
        RecordOriginEnum RecordOriginID,
        RecordStateEnum RecordStateID,
        int CreatedById,
        string CreatedByName,
        string CreatedByFullName,
        DateTime CreatedDate
    );    
}