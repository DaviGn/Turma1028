using Domain.Entities;
using Domain.Exceptions;
using Domain.Mappers;
using Domain.Requests;
using Domain.Responses;
using Domain.Validators;
using Infrastructure.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Application.Services;

public interface ICarService
{
    List<CarResponse> List();
    CarResponse? GetById(int id);
    CarResponse Create(BaseCarRequest newCar);
    CarResponse Update(UpdateCarRequest updatedCar);
    void Delete(int id);
}

public class CarService : ICarService
{
    private readonly IValidator<BaseCarRequest> _validator;
    private readonly ICarRepository _repository;

    public CarService(ICarRepository repository, IValidator<BaseCarRequest> validator)
    {
        _validator = validator;
        _repository = repository;
    }

    public List<CarResponse> List()
    {
        var cars = _repository.List();
        var response = cars.Select(car => CarMapper.ToResponse(car)).ToList();
        return response;
    }

    public CarResponse? GetById(int id)
    {
        var car = _repository.GetById(id);
        return car is null ? null : CarMapper.ToResponse(car);
    }

    public CarResponse Create(BaseCarRequest request)
    {
        var errors = _validator.Validate(request);

        if (errors.Any())
            throw new BadRequestException(errors);

        var newCar = CarMapper.ToEntity(request);
        var car = _repository.Create(newCar);
        return CarMapper.ToResponse(car);
    }

    public CarResponse Update(UpdateCarRequest request)
    {
        var errors = _validator.Validate(request);

        if (errors.Any())
            throw new BadRequestException(errors);

        var updateCar = CarMapper.ToEntity(request);
        var car = _repository.Update(updateCar);
        return CarMapper.ToResponse(car);
    }

    public void Delete(int id)
    {
        _repository.Delete(id);
    }
}
