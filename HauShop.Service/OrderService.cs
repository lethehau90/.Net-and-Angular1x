using HauShop.Data.Infrastructure;
using HauShop.Data.Repositories;
using HauShop.Model.Models;
using System;
using System.Collections.Generic;

namespace HauShop.Service
{
    public interface IOrderService
    {
        Order Create(Order order, List<OrderDetail> orderDetails);

        void UpdateStatus(int orderId);
        void Save();
    }

    public class OrderService : IOrderService
    {
        private IOrderRepository _orderRepository;
        private IOrderDetailRepository _orderDetailRepository;
        private IUnitOfWork _unitOfWork;

        public OrderService(IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository, IUnitOfWork unitOfWork)
        {
            this._orderDetailRepository = orderDetailRepository;
            this._orderRepository = orderRepository;
            this._unitOfWork = unitOfWork;
        }

        public Order Create(Order order, List<OrderDetail> orderDetails)
        {
            try
            {
              _orderRepository.Add(order);
                _unitOfWork.Commit();
                foreach (var orderDetail in orderDetails)
                {
                    orderDetail.OrderID = order.ID;
                    _orderDetailRepository.Add(orderDetail);
                }
                return order;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void UpdateStatus(int orderId)
        {
            var order = _orderRepository.GetSingleById(orderId);
            order.Status = true;
            _orderRepository.Update(order);
        }


        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}