using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeduShop.Data.Infrastructure;
using TeduShop.Data.Repositories;
using TeduShop.Model.Models;

namespace TeduShop.Service
{
    public interface IOrderService
    {
        bool Create(Order order);
    }
    public class OrderService : IOrderService
    {
        private IOrderRepository _orderRepository;
        private IOrderDetailRepository _orderdetailRepository;
        private IUnitOfWork _unitOfWork;

        public OrderService(IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository, IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _orderdetailRepository = orderDetailRepository;
            _unitOfWork = unitOfWork;
        }
        public bool Create(Order order)
        {
            try
            {
                _orderRepository.Add(order);
                _unitOfWork.Commit();

                foreach (var orderDetail in order.OrderDetails)
                {
                    orderDetail.OrderID = order.ID;
                    _orderdetailRepository.Add(orderDetail);
                }
                _unitOfWork.Commit();

                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
