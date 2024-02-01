using Domain.Requests;
using Domain.Responses;
using System.Collections.Generic;

namespace Domain.Validators;

public class UserValidator : IValidator<BaseUserRequest>
{
    public List<ErrorMessageResponse> Validate(BaseUserRequest car)
    {
        var errors = new List<ErrorMessageResponse>();

        if (string.IsNullOrEmpty(car.Name))
            errors.Add(new ErrorMessageResponse
            {
                Field = "Name",
                Message = "Field is required!"
            });

        if (string.IsNullOrEmpty(car.Email))
            errors.Add(new ErrorMessageResponse
            {
                Field = "Email",
                Message = "Field is required!"
            });

        if (string.IsNullOrEmpty(car.Password))
            errors.Add(new ErrorMessageResponse
            {
                Field = "Password",
                Message = "Field is required!"
            });

        if (string.IsNullOrEmpty(car.Role))
            errors.Add(new ErrorMessageResponse
            {
                Field = "Role",
                Message = "Field is required!"
            });


        return errors;
    }
}
