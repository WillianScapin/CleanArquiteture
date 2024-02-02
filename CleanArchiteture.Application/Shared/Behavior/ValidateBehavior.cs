﻿using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchiteture.Application.Shared.Behavior
{
    public class ValidationBehavior<TRequest, TResponse> :
                        IPipelineBehavior<TRequest, TResponse>
                        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, 
            RequestHandlerDelegate<TResponse> next, 
            CancellationToken cancellationToken)
        {
            if (!_validators.Any()) return await next();

            var context = new ValidationContext<TRequest>(request);

            if (_validators.Any())
            {
                context = new ValidationContext<TRequest>(request);
                var validationResults = await Task.WhenAll(_validators.Select(w => 
                                                            w.ValidateAsync(context,cancellationToken)));

                var faliures = validationResults.SelectMany(w => w.Errors).Where(w => w != null).ToList();

                if (faliures.Count != 0)
                    throw new FluentValidation.ValidationException(faliures);
            }

            return await next();
        }
    }
}
