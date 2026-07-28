namespace SIGC.DomainModel.Dtos.Presentation
{
    public sealed record PresentationListResponseDto
    (
        int PresentationID,
        string PresentationName,
        decimal PresentationEquivalence
    );    
}