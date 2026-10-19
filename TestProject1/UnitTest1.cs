using System;
using System.IO;
using ПИС2;
using Xunit;

namespace ПИС2.Tests
{
    public class FuelRecordTests
    {
        private static readonly string TestLogPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory),
            "logTest.txt");

        public FuelRecordTests()
        {
            Logger.SetLogPath(TestLogPath);
        }

        [Fact]
        public void FromLine_Price_Works()
        {
            FuelRecord r = FuelRecord.FromLine("PRICE;АИ-95;2024.05.17;55.40")!;

            Assert.Equal("Цена", r.Kind);
            Assert.Equal("АИ-95", r.FuelType);
            Assert.Equal(55.40m, r.Price);
        }

        [Fact]
        public void FromLine_Station_Works()
        {
            FuelRecord r = FuelRecord.FromLine("STATION;АИ-92;2024.05.18;52.10;Лукойл;3")!;

            Assert.Equal("АЗС", r.Kind);
            Assert.Equal("АИ-92", r.FuelType);
            Assert.Equal(52.10m, r.Price);
            Assert.Equal("Лукойл", r.StationName);
            Assert.Equal(3, r.Column);
        }

        [Fact]
        public void FromLine_Delivery_Works()
        {
            FuelRecord r = FuelRecord.FromLine("DELIVERY;ДТ;2024.05.19;60.00;10000.0;1")!;

            Assert.Equal("Поставка", r.Kind);
            Assert.Equal("ДТ", r.FuelType);
            Assert.Equal(60.00m, r.Price);
            Assert.Equal(10000.0, r.Volume);
            Assert.True(r.IsPaid);
        }
    }
}