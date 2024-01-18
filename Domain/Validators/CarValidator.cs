using Domain.Requests;
using Domain.Responses;
using System.Collections.Generic;

namespace Domain.Validators;

public class CarValidator : IValidator<BaseCarRequest>
{
    public List<ErrorMessageResponse> Validate(BaseCarRequest car)
    {
        var errors = new List<ErrorMessageResponse>();

        if (string.IsNullOrEmpty(car.Brand))
            errors.Add(new ErrorMessageResponse
            {
                Field = "Brand",
                Message = "Field is required!"
            });

        if (string.IsNullOrEmpty(car.Model))
            errors.Add(new ErrorMessageResponse
            {
                Field = "Model",
                Message = "Field is required!"
            });

        return errors;
    }
}
