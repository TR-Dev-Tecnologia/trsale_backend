using TRSale.Domain.Helpers;

namespace TRSale.Domain.Entites
{
    public class User: BaseEntity
    { 
        private User(): base()
        {
            
        }  
        public User(string name, string email, string password): base()
        {
            Name = name;
            Email = email;
            Password = CriptoHelper.HashPassword(password);
        }

        public string Name { get; private set; } = null!;      
        public string Email { get; private set; } = null!;
        public string Password { get; private set; } = null!;     

        public string? PasswordToken { get; private set; } = null!;

        public DateTime? PasswordTokenValidity { get; private set; }

        public DateTime? LastAccess { get; private set; }

        public bool Authenticate(string password)
        {
            var result = CriptoHelper.VerifyHashedPassword(this.Password, password);            
            if (result){
                this.LastAccess = DateTime.Now;
            }
            return result;
        }

        public void GenereatePasswordToken()
        {
            this.PasswordToken = Guid.NewGuid().ToString();
            this.PasswordTokenValidity = DateTime.Now.AddMinutes(30);
        }

        public void GenereatePasswordToken(DateTime validity)
        {
            this.PasswordToken = Guid.NewGuid().ToString();
            this.PasswordTokenValidity = validity;
        }

        public void UpdatePassword(string token, string newPassword)
        {
            if (this.PasswordToken != token)
                throw new ArgumentException("Token invalid!");
            
            if (this.PasswordTokenValidity < DateTime.Now)
                throw new ArgumentException("Token invalid");

            this.Password = CriptoHelper.HashPassword(newPassword);
            this.PasswordToken = null;
            this.PasswordTokenValidity = null;            
        }

    }
}