namespace TRSale.Domain.Entites
{
    public class Member: BaseEntity
    {
        public Member(Guid companyId, Guid userId): base()
        {
            CompanyId = companyId;              
            UserId = userId;   
        }

        private Member(): base()
        {
            
        }

        public Guid CompanyId { get; private set; }

        public virtual Company Company { get; private set; } = null!;

        public Guid UserId { get; private set; }

        public virtual User User { get; private set; } = null!;
        
    }
}