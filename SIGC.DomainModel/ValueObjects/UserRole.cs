namespace SIGC.DomainModel.ValueObjects
{
    public record struct UserRole
    (
        int CompanyID,
        int UserID,
        int RoleID
    );
}