using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Controllers;

//Create
//Read
//Update
//Delete

[ApiController]
[Route("[controller]")]
public class CarrosController : ControllerBase
{
    private static List<Carro> _carros = new List<Carro>
    {
        new Carro(1, "Porsche", "Cayanne"),
        new Carro(2, "Porsche", "911"),
        new Carro(3, "Audi", "A3"),
        new Carro(4, "Audi", "A4"),
        new Carro(5, "BMW", "320"),
        new Carro(6, "BMW", "X7"),
    };

    [HttpGet]
    public IActionResult List()
    {
        return Ok(_carros);
    }

    // Route param
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var carro = _carros.FirstOrDefault(x => x.Id == id);
        return carro is null ? NotFound() : Ok(carro);
    }

    [HttpPost]
    public IActionResult Post([FromBody] Carro carro)
    {
        var errors = new List<ErrorMessage>();

        if (string.IsNullOrEmpty(carro.Marca))
            errors.Add(new ErrorMessage
            {
                Field = "Marca",
                Message = "Marca é obrigatório"
            });

        if (string.IsNullOrEmpty(carro.Modelo))
            errors.Add(new ErrorMessage
            {
                Field = "Modelo",
                Message = "Modelo é obrigatório"
            });

        if (errors.Any()) // Count > 0
            return BadRequest(errors);

        carro.Id = _carros.Count + 1;
        _carros.Add(carro); // insert
        return Ok(carro);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] Carro carro)
    {
        var carroOriginal = _carros.FirstOrDefault(x => x.Id == id);

        if (carroOriginal is null)
            return NotFound();

        var errors = new List<ErrorMessage>();

        if (string.IsNullOrEmpty(carro.Marca))
            errors.Add(new ErrorMessage
            {
                Field = "Marca",
                Message = "Marca é obrigatório"
            });

        if (string.IsNullOrEmpty(carro.Modelo))
            errors.Add(new ErrorMessage
            {
                Field = "Modelo",
                Message = "Modelo é obrigatório"
            });

        if (errors.Any()) // Count > 0
            return BadRequest(errors);

        // Update
        carroOriginal.Marca = carro.Marca;
        carroOriginal.Modelo = carro.Modelo;

        return Ok(carroOriginal);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var carroOriginal = _carros.FirstOrDefault(x => x.Id == id);

        if (carroOriginal is null)
            return NotFound();

        _carros.Remove(carroOriginal);
        return NoContent();
    }
}

public class Carro
{
    public Carro()
    {

    }

    public Carro(int id, string marca, string modelo)
    {
        Id = id;
        Marca = marca;
        Modelo = modelo;
    }

    public int Id { get; set; }

    //[Required(ErrorMessage = "Campo obrigatório")]
    public string? Marca { get; set; }

    //[Required(ErrorMessage = "Campo obrigatório")]
    public string? Modelo { get; set; }
}

//public record Carro(int id, string marca, string modelo);
