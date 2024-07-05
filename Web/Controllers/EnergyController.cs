using Infrastructure.DAL;
using Infrastructure.Data.EfModels;
using Microsoft.AspNetCore.Mvc;
using Web.Models;
using Web.Static;

namespace Web.Controllers;

[ApiController]
public class EnergyController : ControllerBase
{
    private readonly IRepository _repository;
    private const int availableVerificationPeriod = 5;
    public EnergyController(IRepository repository) { _repository = repository; }

    [HttpGet("GetOverdueCurrentTransformer/{consumptionObjectId}")]
    public async Task<IActionResult> GetOverdueCurrentTransformerAsync(int consumptionObjectId)
    {
        DateOnly check = DateOnly.FromDateTime(DateTime.Now.AddYears(-availableVerificationPeriod));
        try
        {
            var measuringPoints = await _repository.GetMeasuringPointsAsync(consumptionObjectId);
            var overdueCurrentTransformers = measuringPoints
                .Where(x => x.CurrentTransformer.Verificated <= check)
                .Select(x => x.CurrentTransformer.InventoryNumber).ToList();

            return Ok(overdueCurrentTransformers);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.InnerException?.Message);
        }
    }
    [HttpGet("GetOverdueVoltageTransformer/{consumptionObjectId}")]
    public async Task<IActionResult> GetOverdueVoltageTransformerAsync(int consumptionObjectId)
    {
        DateOnly check = DateOnly.FromDateTime(DateTime.Now.AddYears(-availableVerificationPeriod));
        try
        {
            var measuringPoints = await _repository.GetMeasuringPointsAsync(consumptionObjectId);
            var overdueVoltageTransformers = measuringPoints
                .Where(x => x.VoltageTransformer.Verificated <= check)
                .Select(x => x.VoltageTransformer.InventoryNumber).ToList();

            return Ok(overdueVoltageTransformers);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.InnerException?.Message);
        }
    }
    [HttpGet("GetOverdueElectricMeter/{consumptionObjectId}")]
    public async Task<IActionResult> GetOverdueElectricMeterAsync(int consumptionObjectId)
    {
        DateOnly check = DateOnly.FromDateTime(DateTime.Now.AddYears(-availableVerificationPeriod));
        try
        {
            var measuringPoints = await _repository.GetMeasuringPointsAsync(consumptionObjectId);
            var overdueElectricMeters = measuringPoints
                .Where(x => x.ElectricMeter.Verificated <= check)
                .Select(x => x.ElectricMeter.InventoryNumber).ToList();

            return Ok(overdueElectricMeters);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.InnerException?.Message);
        }
    }
    [HttpGet("GetCalculationMeter/{year}")]
    public async Task<IActionResult> GetCalculationMeterAsync(int year)
    {
        try
        {
            var plugIns = await _repository.GetCalculationMeterPlugInsAsync();
            var result = plugIns
                .Where(x => x.PlugedIn.Year <= year && (x.PlugedOut is null || x.PlugedOut.Value.Year >= year))
                .Select(x => x.CalculationMeterId).ToList();

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.InnerException?.Message);
        }
    }
    [HttpPost("AddMeasuringPoint")]
    public async Task<IActionResult> AddMeasuringPointAsync([FromBody] MeasuringPointModel body)
    {
        try
        {
            return Ok(await _repository.AddObjectAsync<MeasuringPoint>(body.GetMeasuringPoint()));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.InnerException?.Message);
        }
    }
}
