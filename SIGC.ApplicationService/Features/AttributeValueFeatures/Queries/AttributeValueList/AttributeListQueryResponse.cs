namespace SIGC.ApplicationService.Features.AttributeValueFeatures.Queries.AttributeValueList
{
    public sealed record AttributeListQueryResponse(
        byte AttributeID,
        string AttributeName,
        bool AttributeIsVariant,
        List<AttributeValueListQueryResponse> AttributeValues
    );

    public sealed record AttributeValueListQueryResponse(
        short AttributeValueID,
        string AttributeValueName
    );
}