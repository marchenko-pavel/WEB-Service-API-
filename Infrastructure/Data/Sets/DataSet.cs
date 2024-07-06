using Infrastructure.Data.EfModels;

namespace Infrastructure.Data.Sets;
public static class DataSet
{
    private static readonly int firstSeed = 111;
    private static readonly int secondSeed = 222;
    private static readonly int thirdSeed = 333;
    private static readonly int fourthSeed = 444;

    public static List<Organization> Organizations = new List<Organization>()
    {
        new (){ Name = "Main org", Address = "Москва"},
        new (){ Name = "Branch org №1", Address = "Саратов", ParentOrgId = 1},
        new (){ Name = "Branch org №2", Address = "Волгоград", ParentOrgId = 1}
    };
    public static List<ConsumptionObject> ConsumptionObjects = new List<ConsumptionObject>()
    {
        new (){Name ="ПС 110/10 Весна", Address = "Саратов-1", OrganizationId = 2},
        new (){Name ="ПС 110/10 Лето", Address = "Саратов-2", OrganizationId = 2},
        new (){Name ="ПС 110/10 Зима", Address = "Волгоград-1", OrganizationId = 3},
        new (){Name ="ПС 110/10 Осень", Address = "Волгоград-2", OrganizationId = 3}
    };
    public static List<DeliveryPoint> DeliveryPoints = new List<DeliveryPoint>()
    {
        new (){ Name = "Точка учета 1", MaxPower = 10, ConsumptionObjectId = 3, CalculationMeter = new CalculationMeter()},
        new (){ Name = "Точка учета 2", MaxPower = 10, ConsumptionObjectId = 3, CalculationMeter = new CalculationMeter()},
        new (){ Name = "Точка учета 3", MaxPower = 10, ConsumptionObjectId = 4, CalculationMeter = new CalculationMeter()},
        new (){ Name = "Точка учета 4", MaxPower = 10, ConsumptionObjectId = 4, CalculationMeter = new CalculationMeter()}
    };
    public static List<ElectricMeterType> ElectricMeterTypes = new List<ElectricMeterType>()
    {
        new (){ Name = "Индукционный (механический)" },
        new (){ Name = "Электронный" }
    };
    public static List<CurrentTransformerType> CurrentTransformerTypes = new List<CurrentTransformerType>()
    {
        new (){ Name = "Обмоточный" },
        new (){ Name = "Тороидальный" },
        new (){ Name = "Стержневой" }
    };
    public static List<VoltageTransformerType> VoltageTransformerTypes = new List<VoltageTransformerType>()
    {
        new (){ Name = "Заземляемый" },
        new (){ Name = "Не заземляемый" }
    };
    public static List<MeasuringPoint> MeasuringPoints = new List<MeasuringPoint>()
    {
        new (){
            Name = "Точка измерения 1",
            ConsumptionObjectId = 3,
            ElectricMeter = new(){InventoryNumber = $"EM-{firstSeed}-{firstSeed}-{firstSeed}",
            TypeId = 1, Verificated = DateOnly.FromDateTime(new DateTime(2019,06,1,15,0,0))},
            CurrentTransformer = new(){InventoryNumber=$"CT-{firstSeed}-{firstSeed}-{firstSeed}",
            TypeId=1, Verificated = DateOnly.FromDateTime(new DateTime(2020,12,30,15,0,0)), Kt=7.5},
            VoltageTransformer = new(){InventoryNumber=$"VT-{firstSeed}-{firstSeed}-{firstSeed}",
            TypeId=1, Verificated = DateOnly.FromDateTime(new DateTime(2020,12,30,15,0,0)), Kt=7.5}
        },
        new (){
            Name = "Точка измерения 2",
            ConsumptionObjectId = 3,
            ElectricMeter = new(){InventoryNumber = $"EM-{secondSeed}-{secondSeed}-{secondSeed}",
            TypeId = 1, Verificated = DateOnly.FromDateTime(new DateTime(2020,12,30,15,0,0))},
            CurrentTransformer = new(){InventoryNumber=$"CT-{secondSeed}-{secondSeed}-{secondSeed}",
            TypeId=2, Verificated = DateOnly.FromDateTime(new DateTime(2019,06,1,15,0,0)), Kt=9.5 },
            VoltageTransformer = new(){ InventoryNumber=$"VT-{secondSeed}-{secondSeed}-{secondSeed}",
            TypeId=2, Verificated = DateOnly.FromDateTime(new DateTime(2019,06,1,15,0,0)), Kt=9.5}
        },
        new (){
            Name = "Точка измерения 3",
            ConsumptionObjectId = 4,
            ElectricMeter = new(){InventoryNumber = $"EM-{thirdSeed}-{thirdSeed}-{thirdSeed}",
            TypeId = 2, Verificated = DateOnly.FromDateTime(DateTime.Now.AddDays(-7))},
            CurrentTransformer = new(){InventoryNumber=$"CT-{thirdSeed}-{thirdSeed}-{thirdSeed}",
            TypeId=3, Verificated = DateOnly.FromDateTime(DateTime.Now.AddDays(-7)), Kt=8.5},
            VoltageTransformer = new(){InventoryNumber=$"VT-{thirdSeed}-{thirdSeed}-{thirdSeed}",
            TypeId=1, Verificated = DateOnly.FromDateTime(DateTime.Now.AddDays(-7)), Kt=8.5}
        },
        new (){
            Name = "Точка измерения 4",
            ConsumptionObjectId = 4,
            ElectricMeter = new(){InventoryNumber = $"EM-{fourthSeed}-{fourthSeed}-{fourthSeed}",
            TypeId = 1, Verificated = DateOnly.FromDateTime(DateTime.Now.AddDays(-7))},
            CurrentTransformer = new(){InventoryNumber=$"CT-{fourthSeed}-{fourthSeed}-{fourthSeed}",
            TypeId=1, Verificated = DateOnly.FromDateTime(DateTime.Now.AddDays(-7)), Kt=5.5},
            VoltageTransformer = new(){InventoryNumber=$"VT-{fourthSeed}-{fourthSeed}-{fourthSeed}",
            TypeId=2, Verificated = DateOnly.FromDateTime(DateTime.Now.AddDays(-7)), Kt=5.5}
        }
    };
    public static List<CalculationMeterPlugIn> CalculationMeterPlugIns = new List<CalculationMeterPlugIn>()
    {
        new (){MeasuringPointId = 1, CalculationMeterId = 1, PlugedIn = new DateTime(2018,6,1,15,0,0)},
        new (){MeasuringPointId = 1, CalculationMeterId = 2, PlugedIn = new DateTime(2019,6,1,15,0,0)},
        new (){MeasuringPointId = 1, CalculationMeterId = 3, PlugedIn = new DateTime(2017,6,1,15,0,0), PlugedOut = new DateTime(2017,12,30,15,0,0)},
        new (){MeasuringPointId = 1, CalculationMeterId = 2, PlugedIn = new DateTime(2017,6,1,15,0,0), PlugedOut = new DateTime(2018,12,30,15,0,0)},
        new (){MeasuringPointId = 1, CalculationMeterId = 4, PlugedIn = new DateTime(2017,6,1,15,0,0), PlugedOut= new DateTime(2019,6,1,15,0,0)}
    };
}
