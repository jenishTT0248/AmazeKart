﻿using AmazeKart.Admin.Core.Enums;
using AmazeKart.Admin.Core.IRepositories;
using AmazeKart.Admin.Core.IServices;
using AmazeKart.Admin.Core.ObjectModel;
using System.Linq.Expressions;

namespace AmazeKart.Admin.Infrastructure.Services
{
    public class PaymentTypeService : IPaymentTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentTypeRepository _paymentTypeRepository;

        public PaymentTypeService(IUnitOfWork unitOfWork, IPaymentTypeRepository paymentTypeRepository)
        {
            _unitOfWork = unitOfWork;
            _paymentTypeRepository = paymentTypeRepository;
        }

        public ResultMessage Create(PaymentType paymentType)
        {
            if (paymentType == null) return ResultMessage.RecordNotFound;

            bool isTypeExists = _paymentTypeRepository.Any(y => y.Type == paymentType.Type && y.Active);

            if (isTypeExists) return ResultMessage.RecordExists;

            _paymentTypeRepository.Create(paymentType);
            _unitOfWork.Commit();
            return ResultMessage.Success;
        }

        public ResultMessage Update(PaymentType paymentType)
        {
            if (paymentType == null) return ResultMessage.RecordNotFound;

            if (_paymentTypeRepository.Any(x => x.Id != paymentType.Id && x.Type == paymentType.Type && x.Active)) {
                return ResultMessage.RecordExists;
            }

            PaymentType type = _paymentTypeRepository.FindOne(x => x.Id == paymentType.Id && x.Active);
           
            if (type == null) return ResultMessage.RecordNotFound;

            _paymentTypeRepository.SetValues(type, paymentType);
            _paymentTypeRepository.Update(type);
            _unitOfWork.Commit();
            return ResultMessage.Success;
        }

        public ResultMessage Delete(int paymentId)
        {
            PaymentType dbPaymentType = _paymentTypeRepository.FindOne(x => x.Id == paymentId && x.Active);
                
            if (dbPaymentType == null) return ResultMessage.RecordNotFound;

            _paymentTypeRepository.Delete(dbPaymentType);
            _unitOfWork.Commit();
            return ResultMessage.Success;
        }

        public PaymentType GetById(int paymentId)
        {
            return _paymentTypeRepository.FindOne(x => x.Id == paymentId && x.Active);
        }

        public IQueryable<PaymentType> GetAll()
        {
            return _paymentTypeRepository.Find(x => x.Active);
        }
    }
}