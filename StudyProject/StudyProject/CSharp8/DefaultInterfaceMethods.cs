using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace StudyProject.CSharp8
{
    /// 默认接口方法，interface 现在可以在里面定义默认方法，并且可以定义静态变量
    public interface ICustomer
    {
        IEnumerable<IOrder> PreviousOrders { get; }
        DateTime DateJoined { get; }
        DateTime? LastOrder { get; }
        string Name { get; }
        IDictionary<DateTime, string> Reminders { get; }
        public static void SetLoyaltyThresholds(TimeSpan ago, int minimumOrders = 10, decimal percentageDiscount = 0.10m)
        {
            length = ago;
            orderCount = minimumOrders;
            discountPercent = percentageDiscount;
        }
        private static TimeSpan length = new TimeSpan(365 * 2, 0, 0, 0); // two years
        private static int orderCount = 10;
        private static decimal discountPercent = 0.10m;
        public decimal ComputeLoyaltyDiscount() => DefaultLoyaltyDiscount(this);//这里这样定义方便继承重写
        protected static decimal DefaultLoyaltyDiscount(ICustomer c)
        {
            DateTime start = DateTime.Now - length;

            if ((c.DateJoined < start) && (c.PreviousOrders.Count() > orderCount))
            {
                return discountPercent;
            }
            return 0;
        }
    }
    public interface IOrder
    {
        DateTime Purchased { get; }
        decimal Cost { get; }
    }
}
