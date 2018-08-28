using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceMVC.Model.Models
{
    public enum OrderStatuses
    {
        OrderCreating,
        BuyerPaying,
        MiddlemanFinding,
        SellerProviding,
        MiddlemanChecking,
        BuyerConfirming,
        PayingToSeller,
        Feedbacking,
        ClosedSuccessfully,
        BuyerClosed,
        SellerClosed,
        MiddlemanClosed,
        ClosedAutomatically,
        AbortedByBuyer,
        MiddlemanBackingAccount
    }

    public class OrderStatus : BaseEntity
    {
        public string DuringName { get; set; }
        public string FinishedName { get; set; }
        public OrderStatuses Value { get; set; }


        public IList<Order> Orders { get; set; }

        public IList<StatusLog> NewStatusLogs { get; set; }
        public IList<StatusLog> OldStatusLogs { get; set; }
    }
}
