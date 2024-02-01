using Application.Services;
using Domain.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApplication1.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
//[CustomActionFilter]
//[ExceptionFilter]
public class CarsController : ControllerBase
{
    private readonly ICarService _service;

    public CarsController(ICarService service)
    {
        _service = service;
    }

    [HttpGet]
    //[CustomActionFilter]
    //[RequireAuth]
    public async Task<IActionResult> List([FromQuery] CarroFilters filtros)
    {
        var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var cars = await _service.List(userId);
        return Ok(cars);
    }

    // Route param
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var car = await _service.GetById(id, userId);
        return car is null ? NotFound() : Ok(car);
    }

    [HttpPost]
    //[Authorize(Roles = "Teacher")]
    public async Task<IActionResult> Post([FromBody] BaseCarRequest car)
    {
        car.UserId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var newCar = await _service.Create(car);
        return Ok(newCar);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateCarRequest car)
    {
        car.Id = id;
        var updatedCar = await _service.Update(car);
        return Ok(updatedCar);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        await _service.Delete(id, userId);
        return NoContent();
    }
}

public class CarroFilters
{
    public string? Marca { get; set; }
    public string? Modelo { get; set; }
    public string? OrderBy { get; set; }
}