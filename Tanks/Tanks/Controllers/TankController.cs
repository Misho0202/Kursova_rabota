using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tanks.Application.Interfaces;
using Tanks.Domain.Models;
using Tanks.Domain.DTOs;
using Serilog;

namespace Tanks.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TankController : ControllerBase
    {
        private readonly ITankService _tankService;

        public TankController(ITankService tankService)
        {
            _tankService = tankService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Tank>>> GetAllTanks()
        {
            var tanks = await _tankService.GetAllTanksAsync();
            return Ok(tanks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Tank>> GetTankById(Guid id)
        {
            var tank = await _tankService.GetTankByIdAsync(id);
            if (tank == null)
            {
                Log.Warning($"Танк с ID {id} не беше намерен.");
                return NotFound();
            }

            return Ok(tank);
        }

        [HttpPost]
        public async Task<ActionResult> AddTank([FromBody] TankDto tankDto)
        {
            if (!ModelState.IsValid)
            {
                Log.Warning("Грешка при валидация на входните данни.");
                return BadRequest(ModelState);
            }

            var createdTank = await _tankService.AddTankAsync(tankDto);
            Log.Information($"Нов танк {createdTank.Name} беше добавен с ID: {createdTank.Id}.");

            return CreatedAtAction(nameof(GetTankById), new { id = createdTank.Id }, createdTank);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTank(string id, [FromBody] TankDto tankDto)
        {
            Guid guid = Guid.Parse(id);
            var existingTank = await _tankService.GetTankByIdAsync(guid);
            if (existingTank == null)
            {
                Log.Warning($"Опит за обновяване на несъществуващ танк с ID {id}.");
                return NotFound(new { message = "Танкът не беше намерен." });
            }

            await _tankService.UpdateTankAsync(guid, tankDto);
            Log.Information($"Танкът {id} беше обновен.");
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTank(string id)
        {
            Guid guid = Guid.Parse(id);
            var existingTank = await _tankService.GetTankByIdAsync(guid);
            if (existingTank == null)
            {
                Log.Warning($"Опит за изтриване на несъществуващ танк с ID {id}.");
                return NotFound(new { message = "Танкът не беше намерен." });
            }

            await _tankService.DeleteTankAsync(guid);
            Log.Information($"Танкът {id} беше изтрит.");
            return NoContent();
        }
    }
}
