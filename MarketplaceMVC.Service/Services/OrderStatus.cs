﻿using MarketplaceMVC.Data.Infrastructure;
using MarketplaceMVC.Data.Repositories;
using MarketplaceMVC.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceMVC.Service
{
    public interface IOrderStatusService
    {
        IEnumerable<OrderStatus> GetAllOrderStatuses();
        Task<List<OrderStatus>> GetAllOrderStatusesAsync();
        OrderStatus GetOrderStatus(int id);
        Task<OrderStatus> GetOrderStatusAsync(int id);
        OrderStatus GetOrderStatusByValue(OrderStatuses value);
        void CreateOrderStatus(OrderStatus message);
        void SaveOrderStatus();
        Task SaveOrderStatusAsync();
    }

    public class OrderStatusService : IOrderStatusService
    {
        private readonly IOrderStatusRepository orderStatusesRepository;
        private readonly IOrderRepository ordersRepository;
        private readonly IUnitOfWork unitOfWork;

        public OrderStatusService(IOrderStatusRepository orderStatusesRepository, IOrderRepository ordersRepository, IUnitOfWork unitOfWork)
        {
            this.orderStatusesRepository = orderStatusesRepository;
            this.unitOfWork = unitOfWork;
            this.ordersRepository = ordersRepository;
        }

        #region IOrderStatusService Members

        public IEnumerable<OrderStatus> GetAllOrderStatuses()
        {
            var orderStatuss = orderStatusesRepository.GetAll();
            return orderStatuss;
        }

        public async Task<List<OrderStatus>> GetAllOrderStatusesAsync()
        {
            return await orderStatusesRepository.GetAllAsync();
        }


        public OrderStatus GetOrderStatus(int id)
        {
            var orderStatus = orderStatusesRepository.GetById(id);
            return orderStatus;
        }

        public async Task<OrderStatus> GetOrderStatusAsync(int id)
        {
            return await orderStatusesRepository.GetByIdAsync(id);
        }

        public OrderStatus GetOrderStatusByValue(OrderStatuses value)
        {

            var orderStatus = orderStatusesRepository.GetOrderStatusByValue(value);
            return orderStatus;
        }


        public void CreateOrderStatus(OrderStatus orderStatus)
        {
            orderStatusesRepository.Add(orderStatus);
        }

        public void SaveOrderStatus()
        {
            unitOfWork.SaveChanges();
        }

        public async Task SaveOrderStatusAsync()
        {
            await unitOfWork.SaveChangesAsync();
        }

        #endregion

    }
}
