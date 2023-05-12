﻿using AmazeKart.User.Core.Enums;
using AmazeKart.User.Core.IBal;
using AmazeKart.User.Core.IServices;
using AutoMapper;
using ObjectModel = AmazeKart.User.Core.ObjectModel;
using ViewModel = AmazeKart.User.Core.ViewModel;
using ViewModelResponse = AmazeKart.User.Core.ViewModel.Response;

namespace AmazeKart.User.Infrastructure.Bal
{
    public class CartBAL : ICartBAL
    {
        private readonly ICartService _cartService;
        private readonly IMapper _mapper;
        public CartBAL(IMapper mapper, ICartService cartService)
        {
            _mapper = mapper;
            _cartService = cartService;
        }

        public ResultMessage Create(ViewModel.Cart entity)
        {
            if (entity == null) return ResultMessage.RecordNotFound;

            ObjectModel.Cart cart = new ObjectModel.Cart();
            _mapper.Map<ViewModel.Cart, ObjectModel.Cart>(entity, cart);
            return _cartService.Create(cart);
        }

        public ResultMessage Update(ViewModel.Cart entity)
        {
            if (entity == null) return ResultMessage.RecordNotFound;

            ObjectModel.Cart cart = new ObjectModel.Cart();
            _mapper.Map<ViewModel.Cart, ObjectModel.Cart>(entity, cart);
            return _cartService.Update(cart);
        }

        public ResultMessage Delete(int cartId)
        {
            return _cartService.Delete(cartId);
        }

        public IQueryable<ViewModelResponse.CartResponse> GetAll()
        {
            var cartData = _cartService.GetAll().ToList();
            List<ViewModelResponse.CartResponse> cartViewModel = new();
            cartViewModel = _mapper.Map<List<ObjectModel.Cart>, List<ViewModelResponse.CartResponse>>(cartData);
            return cartViewModel.AsQueryable();
        }

        public ViewModelResponse.CartResponse GetById(int cartId)
        {
            ObjectModel.Cart cart = _cartService.GetById(cartId);
            ViewModelResponse.CartResponse cartViewModel = _mapper.Map<ObjectModel.Cart, ViewModelResponse.CartResponse>(cart);
            return cartViewModel;
        }
    }
}