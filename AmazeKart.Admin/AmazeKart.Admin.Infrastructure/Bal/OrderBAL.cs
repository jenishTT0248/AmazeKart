﻿using AmazeKart.Admin.Core.Enums;
using AmazeKart.Admin.Core.IBal;
using AmazeKart.Admin.Core.IServices;
using AutoMapper;
using ObjectModel = AmazeKart.Admin.Core.ObjectModel;
using ViewModel = AmazeKart.Admin.Core.ViewModel;
using ViewModelResponse = AmazeKart.Admin.Core.ViewModel.Response;

namespace AmazeKart.Admin.Infrastructure.Bal
{
    public class OrderBAL : IOrderBAL
    {
        private readonly IMapper _mapper;
        private readonly IOrderService _orderService;

        public OrderBAL(IMapper mapper, IOrderService orderService)
        {
            _mapper = mapper;
            _orderService = orderService;
        }

        public ResultMessage Create(ViewModel.Order entity)
        {
            if (entity == null) return ResultMessage.RecordNotFound;

            ObjectModel.Order order = new ObjectModel.Order();
            _mapper.Map<ViewModel.Order, ObjectModel.Order>(entity, order);
            return _orderService.Create(order);
        }

        public ResultMessage Delete(int orderId)
        {
            return _orderService.Delete(orderId);
        }

        public IQueryable<ViewModelResponse.OrderResponse> GetAll()
        {
            var orders = _orderService.GetAll().ToList();
            List<ViewModelResponse.OrderResponse> orderList = new();
            orderList = _mapper.Map<List<ObjectModel.Order>, List<ViewModelResponse.OrderResponse>>(orders);
            return orderList.AsQueryable();
        }

        public ViewModelResponse.OrderResponse GetById(int orderId)
        {
            ObjectModel.Order order = _orderService.GetById(orderId);
            ViewModelResponse.OrderResponse orderViewModel = _mapper.Map<ObjectModel.Order, ViewModelResponse.OrderResponse>(order);
            return orderViewModel;
        }

        public ResultMessage Update(ViewModel.Order entity)
        {
            if (entity == null) return ResultMessage.RecordNotFound;

            ObjectModel.Order order = new ObjectModel.Order();
            _mapper.Map<ViewModel.Order, ObjectModel.Order>(entity, order);
            return _orderService.Update(order);
        }
    }
}