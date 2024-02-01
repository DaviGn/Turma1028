using Domain.Exceptions;
using Domain.Mappers;
using Domain.Requests;
using Domain.Responses;
using Domain.Validators;
using Infrastructure.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services;

public interface ICarService
{
    Task<List<CarResponse>> List(int userId);
    Task<CarResponse?> GetById(int id, int userId);
    Task<CarResponse> Create(BaseCarRequest newCar);
    Task<CarResponse> Update(UpdateCarRequest updatedCar);
    Task Delete(int id, int userId);
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

    public async Task<List<CarResponse>> List(int userId)
    {
        var cars = await _repository.List(userId);
        var response = cars.Select(car => CarMapper.ToResponse(car)).ToList();
        return response;
    }

    public async Task<CarResponse?> GetById(int id, int userId)
    {
        var car = await _repository.GetById(id);

        if (car is null || car.UserId != userId)
            return null;

        return CarMapper.ToResponse(car);
    }

    public async Task<CarResponse> Create(BaseCarRequest request)
    {
        var errors = _validator.Validate(request);

        if (errors.Any())
            throw new BadRequestException(errors);

        var newCar = CarMapper.ToEntity(request);
        var car = await _repository.Create(newCar);
        return CarMapper.ToResponse(car);
    }

    public async Task<CarResponse> Update(UpdateCarRequest request)
    {
        var errors = _validator.Validate(request);

        if (errors.Any())
            throw new BadRequestException(errors);

        var existingCar = await _repository.GetById(request.Id);

        if (existingCar is null)
            throw new NotFoundException("Car not found!");

        var updateCar = CarMapper.ToEntity(request);
        var car = await _repository.Update(updateCar);
        return CarMapper.ToResponse(car);
    }

    public async Task Delete(int id, int userId)
    {
        await _repository.Delete(id, userId);
    }
}
