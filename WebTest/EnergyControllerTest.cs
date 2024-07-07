using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net.Http.Json;
using Web;
using Web.Models;

namespace WebTest;
public class EnergyControllerTest : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;
    private readonly JsonSerializerSettings _jsonSettings;
    public EnergyControllerTest(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = _factory.CreateClient(new WebApplicationFactoryClientOptions { AllowAutoRedirect = false });
        _jsonSettings = new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All, MaxDepth = 128 };
    }
    [Fact]
    public async void GetOverdueCurrentTransformersTest()
    {
        //// Arrange
        string[]? result = null;
        //// Act
        var response = await _client.GetAsync("GetOverdueCurrentTransformer/3");
        if (response?.Content is not null)
        {
            var body = response.Content.ReadAsStringAsync().Result;
            if (body is not null) result = JsonConvert.DeserializeObject<string[]>(body, _jsonSettings);
        }
        //// Assert
        Assert.NotNull(response);
        response.EnsureSuccessStatusCode();
        Assert.NotNull(result);
        if (result?.Length > 0)
        {
            Assert.Single(result);
            Assert.Equal("CT-222-222-222", result[0]);
        }
    }
    [Fact]
    public async void GetOverdueVoltageTransformersTest()
    {
        //// Arrange
        string[]? result = null;
        //// Act
        var response = await _client.GetAsync("GetOverdueVoltageTransformer/3");
        if (response?.Content is not null)
        {
            var body = response.Content.ReadAsStringAsync().Result;
            if (body is not null) result = JsonConvert.DeserializeObject<string[]>(body, _jsonSettings);
        }
        //// Assert
        Assert.NotNull(response);
        response.EnsureSuccessStatusCode();
        Assert.NotNull(result);
        if (result?.Length > 0)
        {
            Assert.Single(result);
            Assert.Equal("VT-222-222-222", result[0]);
        }
    }
    [Fact]
    public async void GetOverdueElectricMetersTest()
    {
        //// Arrange
        string[]? result = null;
        //// Act
        var response = await _client.GetAsync("GetOverdueElectricMeter/3");
        if (response?.Content is not null)
        {
            var body = response.Content.ReadAsStringAsync().Result;
            if (body is not null) result = JsonConvert.DeserializeObject<string[]>(body, _jsonSettings);
        }
        //// Assert
        Assert.NotNull(response);
        response.EnsureSuccessStatusCode();
        Assert.NotNull(result);
        if (result?.Length > 0)
        {
            Assert.Single(result);
            Assert.Equal("EM-111-111-111", result[0]);
        }
    }
    [Fact]
    public async void GetCalculationMetersTest()
    {
        //// Arrange
        string[]? result = null;
        //// Act
        var response = await _client.GetAsync("GetCalculationMeter/2018");
        if (response?.Content is not null)
        {
            var body = response.Content.ReadAsStringAsync().Result;
            if (body is not null) result = JsonConvert.DeserializeObject<string[]>(body, _jsonSettings);
        }
        //// Assert
        Assert.NotNull(response);
        response.EnsureSuccessStatusCode();
        Assert.NotNull(result);
        if (result?.Length == 3)
        {
            Assert.Contains("1", result);
            Assert.Contains("2", result);
            Assert.Contains("4", result);
        }
    }
    [Fact]
    public async void AddMeasuringPointTest()
    {
        //// Arrange
        string? result = null;
        MeasuringPointModel body = new MeasuringPointModel()
        {
            Name = "Точка измерения 5",
            ConsumptionObjectId = 1,
            ElectricMeter = new() { InventoryNumber = "EM-test", TypeId = 1, Verificated = new DateTime(2024, 7, 4) },
            CurrentTransformer = new() { InventoryNumber = "CT-test", TypeId = 1, Verificated = new DateTime(2024, 7, 4), Kt = 1.1 },
            VoltageTransformer = new() { InventoryNumber = "VT-test", TypeId = 1, Verificated = new DateTime(2024, 7, 4), Kt = 2.2 }
        };
        JsonContent content = JsonContent.Create(body);
        //// Act
        var response = await _client.PostAsync("AddMeasuringPoint", content);
        if (response?.Content is not null) { result = response.Content.ReadAsStringAsync().Result; }
        //// Assert
        Assert.NotNull(response);
        response.EnsureSuccessStatusCode();
        Assert.Equal("true", result);
    }
}