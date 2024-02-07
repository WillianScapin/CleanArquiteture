using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchiteture.Domain.Middlewares
{
    public class CustonValidationResultModel
    {
        public string Message { get; }

        public List<CustonValidationError> Errors { get; }

        public CustonValidationResultModel(ModelStateDictionary modelState)
        {
            Message = "Validation Failed";
            Errors = modelState.Keys
                    .SelectMany(key => modelState[key].Errors.Select(x => new CustonValidationError(key, x.ErrorMessage)))
                    .ToList();
        }
    }
}
