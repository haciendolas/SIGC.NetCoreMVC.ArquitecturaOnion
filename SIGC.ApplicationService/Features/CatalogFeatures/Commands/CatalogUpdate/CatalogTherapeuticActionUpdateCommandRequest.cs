namespace SIGC.ApplicationService.Features.CatalogFeatures.Commands.CatalogUpdate
{
    public sealed record CatalogTherapeuticActionUpdateCommandRequest
    (
        int CatalogID,
        short TherapeuticActionID 
    );    
}