namespace SIGC.DomainModel.Dtos.AttributeValueList
{
    public sealed record AttributeValueListResponseDto
    (
        byte AttributeID,
        string AttributeName,
        bool AttributeIsVariant,
        short AttributeValueID,
        string AttributeValueName
    );   
}