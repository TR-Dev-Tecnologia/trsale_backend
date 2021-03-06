using Dapper;
using Microsoft.EntityFrameworkCore;
using TRSale.Domain.Entites;
using TRSale.Domain.Interfaces.Infra;
using TRSale.Domain.Interfaces.Repositories;

namespace TRSale.DataBase.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(TRSaleContext context, IUnitOfWork uow) : base(context, uow)
        {
            
        }

        public User? FindByEmail(string email)
        {
            return _connection.Query<User>($@"Select * from User where Email = @Email", new {Email = email}, _uow.CurrentTransaction()).FirstOrDefault();
        }

        public User? FindByToken(string token)
        {
            return _connection.Query<User>($@"Select * from User where PasswordToken = @PasswordToken", new {PasswordToken = token}, _uow.CurrentTransaction()).FirstOrDefault();
        }
    }
}