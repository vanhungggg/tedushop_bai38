using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeduShop.Common.ViewModel;
using TeduShop.Data.Infrastructure;
using TeduShop.Data.Repositories;

namespace TeduShop.Service
{
    public interface IStatisticService
    {
        IEnumerable<RevenueStatisticViewModel> GetRevenueStatistics(string fromDate, string toDate);
    }
    public class StatisticService : IStatisticService
    {
        private IOrderRepository _orderRepository;
        private IUnitOfWork _unitOfWork;

        public StatisticService(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<RevenueStatisticViewModel> GetRevenueStatistics(string fromDate, string toDate)
        {
            return _orderRepository.GetRevenueStatistics(fromDate, toDate);
        }
    }
}
