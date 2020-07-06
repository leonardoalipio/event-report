using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.Api.Dtos;
using ProAgil.Business.Interfaces;
using ProAgil.Business.Models;

namespace ProAgil.Api.Controllers 
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase 
    {
        private readonly IEventoRepository _eventoRepo;
        private readonly IMapper _mapper;

        public EventoController (IEventoRepository eventoRepo, IMapper iMapper) 
        {
            _eventoRepo = eventoRepo;
            _mapper = iMapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {                
                var eventos = await _eventoRepo.GetAllEventoAsync(true);

                var results = _mapper.Map<IEnumerable<EventoDto>>(eventos);

                return Ok(results);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Banco de Dados Falhou! {ex.Message}");
            }
        }

        [HttpGet("{eventoId}")]
        public async Task<IActionResult> GetById(int eventoId)
        {
            try
            {
                var evento = await _eventoRepo.GetEventoByIdAsync(eventoId, true);

                var results = _mapper.Map<EventoDto>(evento);

                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou!");
            }
        }

        [HttpGet("getByTema/{tema}")]
        public async Task<IActionResult> Get(string tema)
        {
            try
            {
                var eventos = await _eventoRepo.GetAllEventoByTemaAsync(tema, true);

                var results = _mapper.Map<EventoDto[]>(eventos);

                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou!");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(EventoDto model)
        {
            try
            {
                var novoEvento = _mapper.Map<Evento>(model);

                _eventoRepo.Add(novoEvento);

                if (await _eventoRepo.SaveChangesAsync())
                {
                    return Created($"/api/evento/{model.Id}", _mapper.Map<EventoDto>(novoEvento));
                }
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Banco de Dados Falhou! {ex.Message}");
            }

            return BadRequest();
        }

        [HttpPut("{eventoId}")]
        public async Task<IActionResult> Put(int eventoId, EventoDto model)
        {
            try
            {
                var evento = await _eventoRepo.GetEventoByIdAsync(eventoId, false);

                if (evento == null) return NotFound();

                _mapper.Map(model, evento);

                _eventoRepo.Update(evento);

                if (await _eventoRepo.SaveChangesAsync())
                {
                    return Created($"/api/evento/{model.Id}", _mapper.Map<EventoDto>(evento));
                }
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Banco de Dados Falhou! {ex.Message}");
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