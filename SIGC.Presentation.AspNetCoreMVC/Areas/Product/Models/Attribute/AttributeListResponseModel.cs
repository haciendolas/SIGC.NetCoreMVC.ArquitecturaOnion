namespace SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.Attribute
{
    public sealed record AttributeListResponseModel
    (
        byte AttributeID,
        string AttributeName,
        bool AttributeIsVariant,
        List<AttributeValueListResponseModel> AttributeValues
    );

    public sealed record AttributeValueListResponseModel
    (
        short AttributeValueID,
        string AttributeValueName
    );
}