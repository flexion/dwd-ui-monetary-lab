namespace DWD.UI.Calendar.Tests;

using Xunit;

public class QuartersTests
{
    [Fact]
    public void QuartersShouldBeConvertableToArray()
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
    public void QuartersShouldBeConvertableToList()
    {
        var sut = new Quarters();
        sut.Add(new Quarter(2021, 4));
        sut.Add(new Quarter(2022, 3));
        sut.Add(new Quarter(2022, 1));
        sut.Add(new Quarter(2022, 2));

        var list = sut.ToList();
        Assert.NotNull(list);
        Assert.Equal(4, list.Count);
        Assert.Equal(new Quarter(2022, 3), list[3]);
    }

    [Fact]
    public void QuartersShouldBeEquatable()
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

        var q4 = new Quarters();

        Assert.True(q1.Equals(q2));
        Assert.True(q1.Equals((object)q2));
        Assert.False(q1.Equals(q3));
        Assert.False(q1.Equals((object)q3));
        Assert.False(q1.Equals(q4));
        Assert.False(q1.Equals((object)null));
    }

    [Fact]
    public void QuartersShouldHaveReferenceEquality()
    {
        var a = new Quarters();
        var b = a;
        Assert.True(b.Equals(a));
    }

    [Fact]
    public void QuartersShouldHaveCorrectEqualityWithNull()
    {
        var a = new Quarters();
        Assert.False(a.Equals(null));
        Assert.False(a.Equals((object)null));
    }

    [Fact]
    public void QuartersShouldProvideHashCode()
    {
        var a = new Quarters();
        var b = new Quarters();
        var c = b;
        var aHash = a.GetHashCode();
        var bHash = b.GetHashCode();
        var cHash = c.GetHashCode();
        Assert.NotEqual(aHash, bHash);
        Assert.Equal(bHash, cHash);
    }
}
