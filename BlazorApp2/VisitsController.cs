using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace YourNamespace.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VisitsController : ControllerBase
    {
        private static List<Visit> _visits = new List<Visit>();

        // GET: api/visits/{animalId}
        [HttpGet("{animalId}/visits")]
        public ActionResult<IEnumerable<Visit>> GetVisitsForAnimal(Guid animalId)
        {
            var visits = _visits.Where(v => v.AnimalId == animalId).ToList();
            if (visits == null || visits.Count == 0)
            {
                return NotFound("No visits found for the specified animal.");
            }
            return Ok(visits);
        }

        // POST: api/visits
        [HttpPost("visits")]
        public ActionResult<Visit> AddVisit([FromBody] VisitDto visitDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Validate if the associated animal exists
            if (!_visits.Any(v => v.Id == visitDto.AnimalId))
            {
                return NotFound("Animal not found.");
            }

            // Map the DTO to a Visit entity and add it to your data storage.
            var visit = new Visit
            {
                Id = Guid.NewGuid(),
                AnimalId = visitDto.AnimalId,
                Date = visitDto.Date,
                Description = visitDto.Description,
                Price = visitDto.Price
            };

            _visits.Add(visit);

            return CreatedAtAction(nameof(GetVisitsForAnimal), new { animalId = visitDto.AnimalId }, visit);
        }
    }

    public class VisitDto
    {
        public Guid AnimalId { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }

    public class Visit
    {
        public Guid Id { get; set; }
        public Guid AnimalId { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
