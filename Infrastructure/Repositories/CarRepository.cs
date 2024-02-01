using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories;

public interface ICarRepository
{
    Task<List<Car>> List(int userId);
    Task<Car?> GetById(int id);
    Task<Car> Create(Car newCar);
    Task<Car> Update(Car updatedCar);
    Task Delete(int id, int userId);
}

public class CarRepository : ICarRepository
{
    private readonly Context _context;

    public CarRepository(Context context)
    {
        _context = context;
    }

    public async Task<List<Car>> List(int userId)
    {
        return await _context.Cars.Where(x => x.UserId == userId).ToListAsync();
    }

    public async Task<Car?> GetById(int id)
    {
        return await _context.Cars.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Car> Create(Car newCar)
    {
        await _context.Cars.AddAsync(newCar);
        await _context.SaveChangesAsync();
        return newCar;
    }

    public async Task<Car> Update(Car updatedCar)
    {
        var car = await GetById(updatedCar.Id);

        if (car is null)
            throw new Exception("Car not found!");

        car.Brand = updatedCar.Brand;
        car.Model = updatedCar.Model;
        _context.Cars.Update(car);
        await _context.SaveChangesAsync();
        return car;
    }

    public async Task Delete(int id, int userId)
    {
        var car = await GetById(id);

        if (car is null || car.UserId != userId)
            throw new Exception("Car not found!");

        _context.Cars.Remove(car);
        await _context.SaveChangesAsync();
    }
}
