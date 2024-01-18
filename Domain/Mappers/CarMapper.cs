using Domain.Entities;
using Domain.Requests;
using Domain.Responses;

namespace Domain.Mappers;

public static class CarMapper
{
    public static CarResponse ToResponse(Car car) => new CarResponse
    {
        Id = car.Id,
        Brand = car.Brand,
        Model = car.Model
    };

    public static Car ToEntity(BaseCarRequest car) => new Car
    {
        Brand = car.Brand,
        Model = car.Model
    };

    public static Car ToEntity(UpdateCarRequest car) => new Car
    {
        Id = car.Id,
        Brand = car.Brand,
        Model = car.Model
    };
}
