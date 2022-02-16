using DOMAIN.Entities;
using FluentValidation;
using INFRASTRUCTURE.Interface;
using INFRASTRUCTURE.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INFRASTRUCTURE.Validator
{

    public abstract class BaseValidator<T> : AbstractValidator<T>
          where T : class
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected BaseValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
    public abstract class BaseValidator<T, TEntity, TKey> : AbstractValidator<T>
         where TEntity : BaseEntity<TKey>
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected BaseValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
