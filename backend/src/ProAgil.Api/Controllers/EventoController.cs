using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.Business.Interfaces;
using ProAgil.Business.Models;

namespace ProAgil.Api.Controllers 
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase 
    {
        private readonly IEventoRepository _eventoRepo;

        public EventoController (IEventoRepository eventoRepo) 
        {
            _eventoRepo = eventoRepo;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var results = await _eventoRepo.GetAllEventoAsync(true);

                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou!");
            }
        }

        [HttpGet("{eventoId}")]
        public async Task<IActionResult> GetById(int eventoId)
        {
            try
            {
                var results = await _eventoRepo.GetEventoByIdAsync(eventoId, true);

                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou!");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Evento model)
        {
            try
            {
                _eventoRepo.Add(model);

                if (await _eventoRepo.SaveChangesAsync())
                {
                    return Created($"/api/evento/{model.Id}", model);
                }
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou!");
            }

            return BadRequest();
        }

        [HttpPut("{eventoId}")]
        public async Task<IActionResult> Put(int eventoId, Evento model)
        {
            try
            {
                var evento = await _eventoRepo.GetEventoByIdAsync(eventoId, false);

                if (evento == null) return NotFound();

                _eventoRepo.Update(model);

                if (await _eventoRepo.SaveChangesAsync())
                {
                    return Created($"/api/evento/{model.Id}", model);
                }
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou!");
            }

            return BadRequest();
        }

        [HttpDelete("{eventoId}")]
        public async Task<IActionResult> Delete(int eventoId)
        {
            try
            {
                var evento = await _eventoRepo.GetEventoByIdAsync(eventoId, false);

                if (evento == null) return NotFound();

                _eventoRepo.Delete(evento);

                if (await _eventoRepo.SaveChangesAsync()) return Ok();
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou!");
            }

            return BadRequest();
        }
    }
}