using Domain.Responses;
using System.Collections.Generic;

namespace Domain.Validators;

public interface IValidator<T>
{
    public List<ErrorMessageResponse> Validate(T obj);
}
