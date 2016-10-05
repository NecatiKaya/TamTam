using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TamTam.Domain.Extensions
{
    public static class ValidationResultExtensions
    {
        public static void CopyToDictionary(this ValidationResult vr, Caterpillar.Core.Collections.StringToStringDictionary dic)
        {
            if (vr != null)
            {
                if (dic != null)
                {
                    foreach (ValidationFailure eachValidation in vr.Errors)
                    {
                        dic.Add(eachValidation.ErrorCode, eachValidation.ErrorMessage);
                    }
                }
            }
        }
    }
}
