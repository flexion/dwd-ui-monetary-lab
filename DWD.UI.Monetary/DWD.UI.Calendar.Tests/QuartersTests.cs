namespace DWD.UI.Calendar.Tests
{
    using Xunit;

    public class QuartersShould
    {
        [Fact]
        public void BeConvertableToArray()
        {
            var sut = new Quarters();
            sut.Add(new Quarter(2021, 4));
            sut.Add(new Quarter(2022, 3));
            sut.Add(new Quarter(2022, 1));
            sut.Add(new Quarter(2022, 2));

            var ary = sut.ToArray();
            Assert.NotNull(ary);
            Assert.Equal(4, ary.Length);
            Assert.Equal(new Quarter(2022, 3), ary[3]);
        }

        [Fact]
        public void BeEquatable()
        {
            var q1 = new Quarters();
            q1.Add(new Quarter(2021, 4));
            q1.Add(new Quarter(2022, 3));
            q1.Add(new Quarter(2022, 1));
            q1.Add(new Quarter(2022, 2));

            var q2 = new Quarters();
            q2.Add(new Quarter(2022, 1));
            q2.Add(new Quarter(2021, 4));
            q2.Add(new Quarter(2022, 3));
            q2.Add(new Quarter(2022, 2));

            var q3 = new Quarters();
            q3.Add(new Quarter(2022, 1));
            q3.Add(new Quarter(2021, 4));
            q3.Add(new Quarter(2025, 3));
            q3.Add(new Quarter(2022, 2));

            Assert.Equal(q1, q2);
            Assert.NotEqual(q1, q3);
        }
    }
}
