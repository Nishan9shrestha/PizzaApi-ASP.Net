using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ContosoPizza.Models;
using ContosoPizza.Services;

namespace ContosoPizza.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PizzaController : ControllerBase
    {
        // Constructor
        public PizzaController()
        {
        }

        // Sample GET action
        [HttpGet]//To get all pizzas
        public ActionResult<List<Pizza>> GetAll() => PizzaService.GetAll();

        [HttpGet("{id}")]//To get a pizza by id and when running in browser do http://localhost:5208/Pizza/id
        public ActionResult<Pizza> Get(int id)
        {
            var pizza = PizzaService.Get(id);
            if (pizza == null)
                return NotFound();

            return Ok(pizza);
        }

        [HttpPost]//To create a pizza
        public IActionResult Create(Pizza pizza)
        {//only takes a pizza object
            PizzaService.Add(pizza);
            return CreatedAtAction(nameof(Create), new { id = pizza.Id }, pizza);

        }

        [HttpPut("{id}")]//To update a pizza
        public IActionResult Update(int id, Pizza pizza)
        {//requires id and pizza object for update
            if (id != pizza.Id)
                return BadRequest();

            var existingPizza = PizzaService.Get(id);
            if (existingPizza is null)
                return NotFound();

            PizzaService.Update(pizza);
            return NoContent();

        }

        [HttpDelete("{id}")]//To delete a pizza
        public IActionResult Delete(int id)
        {//only requires the id of the object
            var pizza = PizzaService.Get(id);
            if (pizza is null)
                return BadRequest();

            PizzaService.Delete(id);
            return NoContent();

        }

    }
}
