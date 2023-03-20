using System;
using System.Linq;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Entities.DTOs;

// https://github.com/CodeMazeBlog/advanced-rest-concepts-aspnetcore/tree/sorting-end

namespace Flats.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApartmentsController : ControllerBase
    {
       
        private readonly ILogger<ApartmentsController> _logger;
        private IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public ApartmentsController(ILogger<ApartmentsController> logger, IMapper mapper, IRepositoryWrapper repository)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        //Todo:
        //1. Make method asynchronous
        //2. Analyze the possibility of competitive access
        [HttpGet]
        public IActionResult GetApartments([FromQuery] ApartmentParameters apartParameters)
        {
            if (apartParameters.NotValidPrice)
            {
                return BadRequest("Max price cannot be less than min price");
            }

            var pagedApats = _repository.Apartments.GetApartments(apartParameters);
            
            var metadata = new
            {
                pagedApats.TotalCount,
                pagedApats.PageSize,
                pagedApats.CurrentPage,
                pagedApats.TotalPages,
                pagedApats.HasNext,
                pagedApats.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            var pagedApartDTO = pagedApats.Select(a => new { dto = _mapper.Map<ApartmentsDTO>(a) });

            _logger.LogInformation($"Returned {pagedApats.TotalCount} apartments from database.");

            return Ok(pagedApartDTO);
        }

        //Todo:
        //1. Make method asynchronous
        //2. Analyze the possibility of competitive access
        [HttpPut("{id}")]
        public IActionResult Update(int id, ApartmentsDTO apartDTO)
        {
            try
            {
                if (apartDTO == null)
                {
                    _logger.LogError("Apartment object sent from client is null.");
                    return BadRequest("Apartment object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid apartment object sent from client.");
                    return BadRequest("Invalid apartment object");
                }

                if (id != apartDTO.ApartmentId)
                    return BadRequest();

                Apartments apart = _mapper.Map<Apartments>(apartDTO);

                if(_repository.Apartments.GetApartmentById(id) == null)
                    return NotFound();

                _repository.Apartments.UpdateApartment(apart);
                _repository.Save();
                return Ok(apartDTO);
            }
            catch(Exception ex)
            {
                _logger.LogError("Error while updating: " + ex.Message);
                return BadRequest("Error while updating: " + ex.Message);
            }
        }

        //Todo:
        //1. Make method asynchronous
        //2. Analyze the possibility of competitive access
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {

            var apart = _repository.Apartments.GetApartmentById(id);
            if (apart == null)
            { 
                _logger.LogError($"Apartment with id: {id}, hasn't been found in db.");
                return NotFound();
            }

            try
            {
                _repository.Apartments.DeleteApartment(apart);
                _repository.Save();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while deleting: " + ex.Message);
                return BadRequest("Error while deleting: " + ex.Message);
            }
            return NoContent();
        }

    }
}
