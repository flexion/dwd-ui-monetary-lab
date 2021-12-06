namespace DWD.UI.Monetary.Tests.Business
{
    using System;
    using System.Linq;
    using Domain.BusinessEntities;
    using Domain.UseCases;
    using Xunit;

    /// <summary>
    /// These are standard base period business test cases provided by the SME as part of the user story.
    /// </summary>
    public class StandardBasePeriodTestCases
    {
        /// <summary>
        /// Calculate the standard base period for 10/31/2021.
        /// </summary>
        [Fact]
        public void OctoberThirtyFirst2021()
        {
            // Get base period from date
            var myClaimDate = new DateTime(2021, 10, 31);
            var basePeriodUseCase = new CalculateBasePeriod();
            var result = basePeriodUseCase.CalculateBasePeriodFromInitialClaimDate(myClaimDate, 0, 0);

            // Check result
            Assert.NotNull(result);
            var quarters = result.BasePeriodQuarters;
            Assert.NotNull(quarters);

            var actualQuarters = quarters.OrderBy(q => q.Year)
                .ThenBy(q => q.QuarterNumber)
                .ToArray();
            var testQuarters = new IUIQuarter[4];
            testQuarters[0] = new UIQuarter(2020, 3);
            testQuarters[1] = new UIQuarter(2020, 4);
            testQuarters[2] = new UIQuarter(2021, 1);
            testQuarters[3] = new UIQuarter(2021, 2);

            Assert.Equal(testQuarters, actualQuarters);
        }

        /// <summary>
        /// Calculate the standard base period for 01/03/2021.
        /// </summary>
        [Fact]
        public void JanuaryThird2021()
        {
            // Get base period from date
            var myClaimDate = new DateTime(2021, 1, 3);
            var basePeriodUseCase = new CalculateBasePeriod();
            var result = basePeriodUseCase.CalculateBasePeriodFromInitialClaimDate(myClaimDate, 0, 0);

            // Check result
            Assert.NotNull(result);
            var quarters = result.BasePeriodQuarters;
            Assert.NotNull(quarters);

            var actualQuarters = quarters.OrderBy(q => q.Year)
                .ThenBy(q => q.QuarterNumber)
                .ToArray();
            var testQuarters = new IUIQuarter[4];
            testQuarters[0] = new UIQuarter(2019, 4);
            testQuarters[1] = new UIQuarter(2020, 1);
            testQuarters[2] = new UIQuarter(2020, 2);
            testQuarters[3] = new UIQuarter(2020, 3);

            Assert.Equal(testQuarters, actualQuarters);
        }

        /// <summary>
        /// Calculate the standard base period for 12/06/2020.
        /// </summary>
        /// <remarks>2020 was a leap year.</remarks>
        [Fact]
        public void DecemberSixth2020()
        {
            // Get base period from date
            var myClaimDate = new DateTime(2020, 12, 6);
            var basePeriodUseCase = new CalculateBasePeriod();
            var result = basePeriodUseCase.CalculateBasePeriodFromInitialClaimDate(myClaimDate, 0, 0);

            // Check result
            Assert.NotNull(result);
            var quarters = result.BasePeriodQuarters;
            Assert.NotNull(quarters);

            var actualQuarters = quarters.OrderBy(q => q.Year)
                .ThenBy(q => q.QuarterNumber)
                .ToArray();
            var testQuarters = new IUIQuarter[4];
            testQuarters[0] = new UIQuarter(2019, 3);
            testQuarters[1] = new UIQuarter(2019, 4);
            testQuarters[2] = new UIQuarter(2020, 1);
            testQuarters[3] = new UIQuarter(2020, 2);

            Assert.Equal(testQuarters, actualQuarters);
        }

        /// <summary>
        /// Calculate the standard base period for 04/01/2021.
        /// </summary>
        /// <remarks>04/01/2021 is technically part of calendar Q2 but in UI it is still considered part of Q1.</remarks>
        [Fact]
        public void AprilFirst2021()
        {
            // Get base period from date
            var myClaimDate = new DateTime(2021, 4, 1);
            var basePeriodUseCase = new CalculateBasePeriod();
            var result = basePeriodUseCase.CalculateBasePeriodFromInitialClaimDate(myClaimDate, 0, 0);

            // Check result
            Assert.NotNull(result);
            var quarters = result.BasePeriodQuarters;
            Assert.NotNull(quarters);

            var actualQuarters = quarters.OrderBy(q => q.Year)
                .ThenBy(q => q.QuarterNumber)
                .ToArray();
            var testQuarters = new IUIQuarter[4];
            testQuarters[0] = new UIQuarter(2019, 4);
            testQuarters[1] = new UIQuarter(2020, 1);
            testQuarters[2] = new UIQuarter(2020, 2);
            testQuarters[3] = new UIQuarter(2020, 3);

            Assert.Equal(testQuarters, actualQuarters);
        }

        /// <summary>
        /// Calculate the standard base period for 04/06/2021.
        /// </summary>
        [Fact]
        public void AprilSixth2021()
        {
            // Get base period from date
            var myClaimDate = new DateTime(2021, 4, 6);
            var basePeriodUseCase = new CalculateBasePeriod();
            var result = basePeriodUseCase.CalculateBasePeriodFromInitialClaimDate(myClaimDate, 0, 0);

            // Check result
            Assert.NotNull(result);
            var quarters = result.BasePeriodQuarters;
            Assert.NotNull(quarters);

            var actualQuarters = quarters.OrderBy(q => q.Year)
                .ThenBy(q => q.QuarterNumber)
                .ToArray();
            var testQuarters = new IUIQuarter[4];
            testQuarters[0] = new UIQuarter(2020, 1);
            testQuarters[1] = new UIQuarter(2020, 2);
            testQuarters[2] = new UIQuarter(2020, 3);
            testQuarters[3] = new UIQuarter(2020, 4);

            Assert.Equal(testQuarters, actualQuarters);
        }

        /// <summary>
        /// Calculate the standard base period for 01/01/2022.
        /// </summary>
        [Fact]
        public void JanuaryFirst2022()
        {
            // Get base period from date
            var myClaimDate = new DateTime(2022, 1, 1);
            var basePeriodUseCase = new CalculateBasePeriod();
            var result = basePeriodUseCase.CalculateBasePeriodFromInitialClaimDate(myClaimDate, 0, 0);

            // Check result
            Assert.NotNull(result);
            var quarters = result.BasePeriodQuarters;
            Assert.NotNull(quarters);

            var actualQuarters = quarters.OrderBy(q => q.Year)
                .ThenBy(q => q.QuarterNumber)
                .ToArray();
            var testQuarters = new IUIQuarter[4];
            testQuarters[0] = new UIQuarter(2020, 3);
            testQuarters[1] = new UIQuarter(2020, 4);
            testQuarters[2] = new UIQuarter(2021, 1);
            testQuarters[3] = new UIQuarter(2021, 2);

            Assert.Equal(testQuarters, actualQuarters);
        }

        /// <summary>
        /// Calculate the standard base period for 11/25/2021.
        /// </summary>
        /// <remarks>11/25 Holiday in which we will still take claims.</remarks>
        [Fact]
        public void NovemberTwentyFifth2021()
        {
            // Get base period from date
            var myClaimDate = new DateTime(2021, 11, 25);
            var basePeriodUseCase = new CalculateBasePeriod();
            var result = basePeriodUseCase.CalculateBasePeriodFromInitialClaimDate(myClaimDate, 0, 0);

            // Check result
            Assert.NotNull(result);
            var quarters = result.BasePeriodQuarters;
            Assert.NotNull(quarters);

            var actualQuarters = quarters.OrderBy(q => q.Year)
                .ThenBy(q => q.QuarterNumber)
                .ToArray();
            var testQuarters = new IUIQuarter[4];
            testQuarters[0] = new UIQuarter(2020, 3);
            testQuarters[1] = new UIQuarter(2020, 4);
            testQuarters[2] = new UIQuarter(2021, 1);
            testQuarters[3] = new UIQuarter(2021, 2);

            Assert.Equal(testQuarters, actualQuarters);
        }

        /// <summary>
        /// Calculate the standard base period for 07/04/2021.
        /// </summary>
        /// <remarks>07/04 Holiday falls on Sunday.</remarks>
        [Fact]
        public void JulyFourth2021()
        {
            // Get base period from date
            var myClaimDate = new DateTime(2021, 7, 4);
            var basePeriodUseCase = new CalculateBasePeriod();
            var result = basePeriodUseCase.CalculateBasePeriodFromInitialClaimDate(myClaimDate, 0, 0);

            // Check result
            Assert.NotNull(result);
            var quarters = result.BasePeriodQuarters;
            Assert.NotNull(quarters);

            var actualQuarters = quarters.OrderBy(q => q.Year)
                .ThenBy(q => q.QuarterNumber)
                .ToArray();
            var testQuarters = new IUIQuarter[4];
            testQuarters[0] = new UIQuarter(2020, 2);
            testQuarters[1] = new UIQuarter(2020, 3);
            testQuarters[2] = new UIQuarter(2020, 4);
            testQuarters[3] = new UIQuarter(2021, 1);

            Assert.Equal(testQuarters, actualQuarters);
        }

        /// <summary>
        /// Calculate the standard base period for 07/04/1887.
        /// </summary>
        /// <remarks>Bad request.</remarks>
        [Fact]
        public void JulyFourth1887()
        {
            // Get base period from date
            var myClaimDate = new DateTime(1887, 7, 4);
            var basePeriodUseCase = new CalculateBasePeriod();
            // Expect ArgumentException
            Assert.Throws<ArgumentException>(() =>
                basePeriodUseCase.CalculateBasePeriodFromInitialClaimDate(myClaimDate, 0, 0));
        }

        [Fact]
        public void ShouldThrowArgumentExceptionWhenYearIsLessThanOne()
        {
            var myClaimDate = new DateTime(1, 1, 1);
            var basePeriodUseCase = new CalculateBasePeriod();
            // Expect ArgumentException
            Assert.Throws<ArgumentException>(() =>
                basePeriodUseCase.CalculateBasePeriodFromInitialClaimDate(myClaimDate, 0, 0));
        }

        [Fact]
        public void ShouldThrowArgumentExceptionWhenWeekIsLessThanOne()
        {
            var myClaimDate = new DateTime(1, 1, 1);
            var basePeriodUseCase = new CalculateBasePeriod();
            // Expect ArgumentException
            Assert.Throws<ArgumentException>(() =>
                basePeriodUseCase.CalculateBasePeriodFromInitialClaimDate(myClaimDate, 1989, 0));
        }

        [Fact]
        public void ShouldThrowArgumentExceptionWhenWeekIsGreaterThan53()
        {
            var myClaimDate = new DateTime(1, 1, 1);
            var basePeriodUseCase = new CalculateBasePeriod();
            // Expect ArgumentException
            Assert.Throws<ArgumentException>(() =>
                basePeriodUseCase.CalculateBasePeriodFromInitialClaimDate(myClaimDate, 2022, 54));
        }

        [Fact]
        public void ShouldReturnStandardBasePeriodsWhenYearAndWeekValid()
        {
            // Get base period from date
            var myClaimDate = new DateTime(1, 1, 1);
            var basePeriodUseCase = new CalculateBasePeriod();
            var result = basePeriodUseCase.CalculateBasePeriodFromInitialClaimDate(myClaimDate, 2021, 28);

            // Check result
            Assert.NotNull(result);
            var quarters = result.BasePeriodQuarters;
            Assert.NotNull(quarters);

            var actualQuarters = quarters.OrderBy(q => q.Year)
                .ThenBy(q => q.QuarterNumber)
                .ToArray();
            var testQuarters = new IUIQuarter[4];
            testQuarters[0] = new UIQuarter(2020, 2);
            testQuarters[1] = new UIQuarter(2020, 3);
            testQuarters[2] = new UIQuarter(2020, 4);
            testQuarters[3] = new UIQuarter(2021, 1);

            Assert.Equal(testQuarters, actualQuarters);
        }
    }
}
