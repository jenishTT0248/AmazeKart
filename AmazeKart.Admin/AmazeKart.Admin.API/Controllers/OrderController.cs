﻿using AmazeKart.Admin.Core.Enums;
using AmazeKart.Admin.Core.IBal;
using AmazeKart.Admin.Core.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AmazeKart.Admin.API.Controllers
{
    [Route("api/Order")]
    public class OrderController : BaseAPIController
    {
        private readonly IOrderBAL _orderBAL;
        public OrderController(IOrderBAL orderBAL)
        {            
            _orderBAL = orderBAL;
        }
        
        [HttpPost, Route("SaveData")]
        public IActionResult SaveData(Order order)
        {
            ResultMessage rMsg = ResultMessage.RecordNotFound;
            MessageConstants resultMessage;

            if (order.Id == 0)
            {
                resultMessage = MessageConstants.RecordInsertSuccessfully;
                rMsg = _orderBAL.Create(order);
            }
            else
            {
                resultMessage = MessageConstants.RecordupdateSuccessfully;
                rMsg = _orderBAL.Update(order);
            }

            if (rMsg != ResultMessage.Success)
                return Ok(new ResponseResult(HttpStatusCode.BadRequest, rMsg.GetStringValue(), null, MessageType.Warning.GetStringValue()));
            return Ok(new ResponseResult(HttpStatusCode.OK, resultMessage.GetStringValue(), null, MessageType.Success.GetStringValue()));
        }

        [HttpPost, Route("DeleteData")]
        public IActionResult Delete(int orderId)
        {
            ResultMessage rMsg = ResultMessage.RecordNotFound;
            MessageConstants resultMessage = MessageConstants.RecordDeleteSuccessfully;
            rMsg = _orderBAL.Delete(orderId);

            if (rMsg != ResultMessage.Success)
                return Ok(new ResponseResult(HttpStatusCode.BadRequest, rMsg.GetStringValue(), null, MessageType.Warning.GetStringValue()));

            return Ok(new ResponseResult(HttpStatusCode.OK, resultMessage.GetStringValue(), null, MessageType.Success.GetStringValue()));
        }

        [HttpGet, Route("GetAll")]
        public IActionResult GetAll()
        {
            List<Order> orders = _orderBAL.GetAll().ToList();
            return Ok(new ResponseResult(HttpStatusCode.OK, string.Empty, orders, MessageType.Success.GetStringValue()));
        }

        [HttpGet, Route("GetById")]
        public IActionResult GetById(int orderId)
        {
            Order order = _orderBAL.GetById(orderId);
            return Ok(new ResponseResult(HttpStatusCode.OK, string.Empty, order, MessageType.Success.GetStringValue()));
        }
    }
}