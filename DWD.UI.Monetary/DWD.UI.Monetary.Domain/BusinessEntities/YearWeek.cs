namespace DWD.UI.Monetary.Domain.BusinessEntities
{
    using System;

    /// <summary>
    /// Hold the week and year.
    /// </summary>
    public class YearWeek : IEquatable<YearWeek>
    {
        public YearWeek(int theYear, int theWeek)
        {
            this.Year = theYear;
            this.Week = theWeek;
        }

        public int Week { get; }
        public int Year { get; }

        /// <summary>
        /// Generics Equals
        /// </summary>
        /// <param name="other">the other</param>
        /// <returns>true/false</returns>
        public bool Equals(YearWeek other) =>
            this.Equals((object)other!);

        /// <summary>
        /// Override base equals.
        /// </summary>
        /// <param name="obj">other obj</param>
        /// <returns>true/false</returns>
        public override bool Equals(object obj)
        {
            if (obj is YearWeek target)
            {
                return this.Year.Equals(target.Year) && this.Week.Equals(target.Week);
            }

            return false;
        }

        /// <summary>
        /// Hash code override.
        /// </summary>
        /// <returns>hash</returns>
        public override int GetHashCode() => HashCode.Combine(this.Year, this.Week);

    }
}
