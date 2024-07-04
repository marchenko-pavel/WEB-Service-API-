using Infrastructure.DAL;
using Infrastructure.Data.EfModels;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Web.Models;

namespace Web.Controllers;

[ApiController]
public class EnergyController : ControllerBase
{
    private readonly IRepository _repository;
    private const int availableVerificationPeriod = 5;
    public EnergyController(IRepository repository) { _repository = repository; }

    [HttpGet("GetOverdueVoltageTransformer/{consumptionObjectId}")]
    public async Task<IActionResult> GetOverdueVoltageTransformerAsync(int consumptionObjectId)
    {
        DateOnly check = DateOnly.FromDateTime(DateTime.Now.AddYears(-availableVerificationPeriod));
        try
        {
            var measuringPoints = await _repository.GetMeasuringPointsAsync(consumptionObjectId);
            var overdueVoltageTransformers = measuringPoints
                .Where(x => x.VoltageTransformer.Verificated <= check)
                .Select(x => x.ElectricMeter.InventoryNumber).ToList();

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
        MeasuringPoint? measurePoin = null;
        StringBuilder log = new();
        string succedMsg = "устешно дабавлен";
        string failMsg = "не добавлен";

        try
        {
            var isSucced = await AddMeasurPointAsync(body);
            if (isSucced) measurePoin = await _repository.GetMeasuringPointAsync(body.Name);

            if (measurePoin is not null)
            {
                log.Append($"Данные операции: точка измерения '{body.Name}' - {succedMsg}; \n");

                isSucced = await AddElectricMeterAsync(measurePoin.Id, body);
                log.Append($"счетчик №'{body.ElectricMeter.InventoryNumber}' - " + (isSucced ? succedMsg : failMsg) + "; \n");

                isSucced = await AddCurrentTransformerAsync(measurePoin.Id, body);
                log.Append($"трансформатор тока №'{body.CurrentTransformer.InventoryNumber}' - " + (isSucced ? succedMsg : failMsg) + "; \n");

                isSucced = await AddVoltageTransformerAsync(measurePoin.Id, body);
                log.Append($"трансформатор напряжения №'{body.VoltageTransformer.InventoryNumber}' - " + (isSucced ? succedMsg : failMsg));

                return Ok(log.ToString());
            }
            else { return BadRequest($"Не удалось добавить в БД точку измерения '{body.Name}'"); }
        }
        catch (Exception ex)
        {
            return BadRequest(String.Concat(ex.InnerException?.Message, "\n", log.ToString()));
        }
    }
    private async Task<bool> AddMeasurPointAsync(MeasuringPointModel body)
    {
        return await _repository.AddObjectAsync<MeasuringPoint>(
            new MeasuringPoint()
            {
                Name = body.Name,
                ConsumptionObjectId = body.ConsumptionObjectId
            });
    }
    private async Task<bool> AddElectricMeterAsync(int measurePoinId, MeasuringPointModel body)
    {
        return await _repository.AddObjectAsync<ElectricMeter>(
            new ElectricMeter()
            {
                Id = measurePoinId,
                InventoryNumber = body.ElectricMeter.InventoryNumber,
                TypeId = body.ElectricMeter.TypeId,
                Verificated = DateOnly.FromDateTime(body.ElectricMeter.Verificated)
            });
    }
    private async Task<bool> AddCurrentTransformerAsync(int measurePoinId, MeasuringPointModel body)
    {
        return await _repository.AddObjectAsync<CurrentTransformer>(
            new CurrentTransformer()
            {
                Id = measurePoinId,
                InventoryNumber = body.CurrentTransformer.InventoryNumber,
                TypeId = body.CurrentTransformer.TypeId,
                Verificated = DateOnly.FromDateTime(body.CurrentTransformer.Verificated),
                Kt = body.CurrentTransformer.Kt
            });
    }
    private async Task<bool> AddVoltageTransformerAsync(int measurePoinId, MeasuringPointModel body)
    {
        return await _repository.AddObjectAsync<VoltageTransformer>(
            new VoltageTransformer()
            {
                Id = measurePoinId,
                InventoryNumber = body.VoltageTransformer.InventoryNumber,
                TypeId = body.VoltageTransformer.TypeId,
                Verificated = DateOnly.FromDateTime(body.VoltageTransformer.Verificated),
                Kt = body.VoltageTransformer.Kt
            });
    }
}
