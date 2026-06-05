using SIGC.DomainModel.Enums;

namespace SIGC.DomainModel.Models
{ 
    public class User
    {
        public int UserId { get; set; }
        public string UserFirstName { get; private set; } = null!;
        public string UserLastName { get; private set; } = null!;
        public string UserName { get; private set; } = null!;
        public string UserPassword { get; private set; } = null!;
        public string? UserMail { get; private set; } = null!;
        public string? UserPhoto { get; private set; }
        public RecordStateEnum StateId { get; private set; }
        public DateTime CreatedDateTime { get; private set; }
        public int CreatedBy { get; private set; }

        protected User() { }

        public static User Create(
                                string UserFirstName,
                                string UserLastName,
                                string UserName, 
                                string UserPassword,
                                string? UserEmail,
                                string? UserPhoto,
                                RecordStateEnum StateId,
                                DateTime CreatedDateTime, 
                                int CreatedBy)
        {
                 Validate(UserName, CreatedDateTime, CreatedBy);
                 return new User(){
                                UserFirstName = UserFirstName,
                                UserLastName = UserLastName,
                                UserName = UserName,
                                UserPassword = UserPassword,
                                UserMail   = UserEmail,
                                UserPhoto =UserPhoto,
                                StateId = StateId,
                                CreatedDateTime = CreatedDateTime,
                                CreatedBy = CreatedBy
                            };
        }

        public static User Update(
                                int UserId, 
                                string UserFirstName,
                                string UserLastName,
                                string UserName,
                                string UserPassword,
                                string? UserEmail,
                                string? UserPhoto,
                                RecordStateEnum StateId,
                                DateTime CreatedDateTime,
                                int CreatedBy)
        {
             Validate(UserName, CreatedDateTime, CreatedBy);
            return new User(){
                                UserId = UserId,
                                UserFirstName = UserFirstName,
                                UserLastName = UserLastName,
                                UserName = UserName,
                                UserPassword = UserPassword,
                                UserMail = UserEmail,
                                UserPhoto = UserPhoto,
                                StateId = StateId,
                                CreatedDateTime = CreatedDateTime,
                                CreatedBy = CreatedBy
                           };
        }

        private static void Validate(string UserName, DateTime CreatedDateTime, int CreatedBy)
        {
            if (string.IsNullOrWhiteSpace(UserName)) throw new ArgumentNullException("El nombre del usuario no debe estar vacia" + nameof(UserName));
            if (CreatedDateTime.Date < DateTime.Now.Date) throw new ArgumentNullException($"La fecha de creación de ser mayor a {DateTime.Now.ToString("dd/MM/yyyy HH:mm")}");
            if (CreatedBy == 0) throw new ArgumentNullException("El codigo del usuario debe ser mayor a cero");
        }
    }
}