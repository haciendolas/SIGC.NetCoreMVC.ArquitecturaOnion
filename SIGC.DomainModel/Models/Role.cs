using SIGC.DomainModel.Enums;

namespace SIGC.DomainModel.Models
{
    public class Role
    {
        public int RoleID { get; set; }
        public int CompanyID { get; private set; }
        public string RoleCode { get; private set; } = null!;
        public string RoleName { get; private set; } = null!;
        public string? RoleDescription { get; private set; }
        public StateEnum StateID { get; private set; }
        public DateTime CreatedDateTime { get; private set; }
        public int CreatedBy { get; private set; }

        protected Role() { }

        public static Role Create(int CompanyID,string RoleCode, string RoleName,string RoleDescription, StateEnum StateID, DateTime CreatedDateTime, int CreatedBy)
        {
            //Validate(CategoryName, CreatedDate, CreatedBy);
            return new Role()
            {
                CompanyID = CompanyID,
                RoleCode = RoleCode,
                RoleName = RoleName,
                RoleDescription = RoleDescription,
                StateID = StateID,
                CreatedDateTime = CreatedDateTime,
                CreatedBy = CreatedBy
            };
        }

        public static Role Update(int RoleID, int CompanyID, string RoleCode, string RoleName, string RoleDescription, StateEnum StateID, DateTime CreatedDateTime, int CreatedBy)
        {
            //Validate(CategoryName, CreatedDate, CreatedBy);
            return new Role()
            {
                RoleID = RoleID,
                CompanyID = CompanyID,
                RoleCode = RoleCode,
                RoleName = RoleName,
                RoleDescription = RoleDescription,
                StateID = StateID,
                CreatedDateTime = CreatedDateTime,
                CreatedBy = CreatedBy
            };
        }
        public static Role ChangeState(int CompanyID,int RoleID, StateEnum StateID, DateTime CreatedDateTime, int CreatedBy)
        {           
            return new Role()
            {
                CompanyID = CompanyID,
                RoleID = RoleID,
                StateID = StateID,
                CreatedDateTime = CreatedDateTime,
                CreatedBy = CreatedBy
            };
        }
    }
}