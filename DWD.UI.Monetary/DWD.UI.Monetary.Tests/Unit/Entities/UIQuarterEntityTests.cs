namespace DWD.UI.Monetary.Tests.Unit.Entities
{
    using System;
    using DWD.UI.Monetary.Domain.BusinessEntities;
    using Xunit;

    /// <summary>
    /// Unit tests for the UIQuarter entity.
    /// </summary>
    public class UIQuarterEntityTests
    {
        [Fact]
        public void JanuaryFirst2021()
        {
            DateTime januaryFirst2021 = new(2021, 1, 1);

            var quarter = new UIQuarter(januaryFirst2021);

            Assert.Equal(2020, quarter.Year);
            Assert.Equal(4, quarter.QuarterNumber);
        }
    }
}
