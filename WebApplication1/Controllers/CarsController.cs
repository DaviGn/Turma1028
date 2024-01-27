using Application.Services;
using Domain.Options;
using Domain.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using Web.Filters;

namespace WebApplication1.Controllers;

//Create
//Read
//Update
//Delete

// Inversion of Control
// Injeção de dependência

[ApiController]
[Route("[controller]")]
[CustomActionFilter]
[ExceptionFilter]
public class CarsController : ControllerBase
{
    private readonly ClassOptions _config;
    private readonly ICarService _service;

    public CarsController(ICarService service, IOptions<ClassOptions> config)
    {
        _config = config.Value;
        _service = service;
    }

    [HttpGet]
    //[CustomActionFilter]
    //[RequireAuth]
    public IActionResult List([FromQuery] CarroFilters filtros)
    {
        var cars = _service.List();
        return Ok(cars);
    }

    // Route param
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var car = _service.GetById(id);
        return car is null ? NotFound() : Ok(car);
    }

    [HttpPost]
    public IActionResult Post([FromBody] BaseCarRequest car)
    {
        var newCar = _service.Create(car);
        return Ok(newCar);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] UpdateCarRequest car)
    {
        car.Id = id;
        var updatedCar = _service.Update(car);
        return Ok(updatedCar);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _service.Delete(id);
        return NoContent();
    }
}

public class CarroFilters
{
    public string? Marca { get; set; }
    public string? Modelo { get; set; }
    public string? OrderBy { get; set; }
}