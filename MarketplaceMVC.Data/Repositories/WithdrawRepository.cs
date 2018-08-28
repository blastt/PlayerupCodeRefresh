using MarketplaceMVC.Data.Infrastructure;
using MarketplaceMVC.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceMVC.Data.Repositories
{
    public class WithdrawRepository : RepositoryBase<Withdraw>, IWithdrawRepository
    {
        public WithdrawRepository(IDbFactory dbFactory)
            : base(dbFactory) { }

    }

    public interface IWithdrawRepository : IRepository<Withdraw>
    {

    }
}
