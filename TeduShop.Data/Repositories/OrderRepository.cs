using System.Collections.Generic;
using System.Data.SqlClient;
using TeduShop.Common.ViewModel;
using TeduShop.Data.Infrastructure;
using TeduShop.Model.Models;

namespace TeduShop.Data.Repositories
{
    public interface IOrderRepository  : IRepository<Order>
    {
        IEnumerable<RevenueStatisticViewModel> GetRevenueStatistics(string fromDate, string toDate);
    }

    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<RevenueStatisticViewModel> GetRevenueStatistics(string fromDate, string toDate)
        {
            var parammeter = new SqlParameter[]
            {
                new SqlParameter("@fromDate",fromDate),
                new SqlParameter("@toDate",toDate)
            };
            return DbContext.Database.SqlQuery<RevenueStatisticViewModel>("GetRevenuesStatictis @fromDate, @toDate", parammeter);
        }
    }
}