using SIGC.DomainModel.Enums;

namespace SIGC.DomainModel.ValueObjects
{
    public sealed record CatalogVariantValue
    (   int CompanyID,
        int CatalogVariantID,
        short AttributeValueID, 
        RecordOriginEnum RecordOriginID,
        RecordStateEnum RecordStateID,
        int CreatedById,
        string CreatedByName,
        string CreatedByFullName,
        DateTime CreatedDate
    );    
}