namespace DWD.UI.Monetary.Tests
{
    using System;
    using Domain.Utilities;
    using DWD.UI.Monetary.Domain.BusinessEntities;
    using Xunit;

    public class DateTimeExtensionsTests
    {
        [Fact]
        public void JanuaryFirst2021()
        {
            DateTime januaryFirst2021 = new(2021, 1, 1);

            var quarter = new UIQuarter(januaryFirst2021, new CalendarQuarter());

            Assert.Equal(2020, quarter.Year);
            Assert.Equal(4, quarter.QuarterNumber);
        }
    }
}
