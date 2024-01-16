using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AnimaisController : ControllerBase
    {
        private static List<Animal> _animais = new List<Animal>();

        [HttpGet]
        public IActionResult List()
        {
            return Ok(_animais);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var animal = _animais.FirstOrDefault(x => x.Id == id);
            return animal is null ? NotFound() : Ok(animal);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Animal animal)
        {
            var errors = new List<ErrorMessage>();

            if (string.IsNullOrEmpty(animal.Nome))
                errors.Add(new ErrorMessage
                {
                    Field = "Nome",
                    Message = "Nome é obrigatório"
                });

            if (string.IsNullOrEmpty(animal.Especie))
                errors.Add(new ErrorMessage
                {
                    Field = "Especie",
                    Message = "Espécie é obrigatório"
                });


            if (string.IsNullOrEmpty(animal.Raca))
                errors.Add(new ErrorMessage
                {
                    Field = "Raca",
                    Message = "Raça é obrigatório"
                });

            if (errors.Any()) // Count > 0
                return BadRequest(errors);

            animal.Id = _animais.Count + 1;
            _animais.Add(animal);
            return Ok(animal);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Animal animal)
        {
            var animalOriginal = _animais.FirstOrDefault(x => x.Id == id);

            if (animalOriginal is null)
                return NotFound();

            var errors = new List<ErrorMessage>();

            if (string.IsNullOrEmpty(animal.Nome))
                errors.Add(new ErrorMessage
                {
                    Field = "Nome",
                    Message = "Nome é obrigatório"
                });

            if (string.IsNullOrEmpty(animal.Especie))
                errors.Add(new ErrorMessage
                {
                    Field = "Especie",
                    Message = "Espécie é obrigatório"
                });


            if (string.IsNullOrEmpty(animal.Raca))
                errors.Add(new ErrorMessage
                {
                    Field = "Raca",
                    Message = "Raça é obrigatório"
                });

            if (errors.Any()) // Count > 0
                return BadRequest(errors);

            animalOriginal.Nome = animal.Nome;
            animalOriginal.Especie = animal.Especie;
            animalOriginal.Raca = animal.Raca;

            return Ok(animalOriginal);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var animal = _animais.FirstOrDefault(x => x.Id == id);

            if (animal is null)
                return NotFound();

            _animais.Remove(animal);
            return NoContent();
        }
    }

    public class Animal
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Especie { get; set; }
        public string? Raca { get; set; }
    }
}
