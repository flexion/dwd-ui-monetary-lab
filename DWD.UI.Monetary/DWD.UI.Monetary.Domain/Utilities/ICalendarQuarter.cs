namespace DWD.UI.Monetary.Domain.Utilities;

using System;

public interface ICalendarQuarter
{
    public int CalendarQuarterNumber(DateTime dateTime);
    public DateTime FirstDayOfCalendarQuarter(int year, int quarterNumber);
}