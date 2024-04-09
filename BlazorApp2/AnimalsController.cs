using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace YourNamespace.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnimalsController : ControllerBase
    {
        private static List<Animal> _animals = new List<Animal>();

        // GET: api/animals
        [HttpGet]
        public ActionResult<IEnumerable<Animal>> GetAnimals()
        {
            return _animals;
        }

        // GET: api/animals/{id}
        [HttpGet("{id}")]
        public ActionResult<Animal> GetAnimal(int id)
        {
            var animal = _animals.FirstOrDefault(a => a.Id == id);
            if (animal == null)
            {
                return NotFound();
            }
            return animal;
        }

        // POST: api/animals
        [HttpPost]
        public ActionResult<Animal> AddAnimal(Animal animal)
        {
            _animals.Add(animal);
            return CreatedAtAction(nameof(GetAnimal), new { id = animal.Id }, animal);
        }

        // PUT: api/animals/{id}
        [HttpPut("{id}")]
        public IActionResult EditAnimal(int id, Animal animal)
        {
            var existingAnimal = _animals.FirstOrDefault(a => a.Id == id);
            if (existingAnimal == null)
            {
                return NotFound();
            }
            existingAnimal.Name = animal.Name;
            existingAnimal.Category = animal.Category;
            existingAnimal.Weight = animal.Weight;
            existingAnimal.FurColor = animal.FurColor;
            return NoContent();
        }

        // DELETE: api/animals/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteAnimal(int id)
        {
            var animal = _animals.FirstOrDefault(a => a.Id == id);
            if (animal == null)
            {
                return NotFound();
            }
            _animals.Remove(animal);
            return NoContent();
        }
    }
}