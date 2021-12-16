# DWD.UI.Calendar
The Calendar package provides common calendar functions shared across UI domains.
---

## Quarter
The quarter is based on a year and quarter number.

A quarter is an immutable object that provides the year and quarter number as properties.
You can compare a quarter to another quarter chronologically and clone a quarter as the previous quarter.
---

## Quarters
Quarters represents an ordered collection of quarters of the year.

- You can add Quarters to the collection and get the collection as an Array or a read-only List. 
- You can compare a Quarters collection to another one to determine if they hold the same quarters in the same order.
---

## UIWeek
A UIWeek is an immutable object representing a week on the UI calendar, in which the first day of the week
is defined as Sunday. *Note that the first week of the UI year may contain days from the previous calendar year.*
It can be constructed from either a date or a combination of year and week-of-year number.

A UIWeek provides the year, week-of-year number, quarter number, start date, and end date of the week.

For unemployment purposes the quarter starts on Sunday of the 1st, 14th, 27th, and 40th UI weeks of the year.

