# Clean Architecture
The team is following a clean architecture approach. Clean architecture was introducted in a [blog post](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html) by Robert C Martin in August 2012.  Clean architecture is similar to [orthagonal architecture](https://en.wikipedia.org/wiki/Hexagonal_architecture_(software)), among others.

In 2018, Robert C. Martin published the book [Clean Architecture - A craftsman's guide to software structure and design](https://www.amazon.com/dp/0134494164/ref=cm_sw_em_r_mt_dp_KMPTGHH4T1133T2SXAH4).  This book is recommended reading for team members.

## Introduction
The primary objective of clean architecture is to organize software in such a way that concerns are separated cleanly and core business logic is free from specific implementation details.  But why, what does this do for us?
* Flexibility - we have options.  If our core business logic has a clean separation from our database persistence code, then we have the option to replace one database platform with another.
* Testability - each software component can be more easily isolated and tested indepdently from other software components.

## Approach
For the unemployment insurance project, the team is following a clean architecture approach and we are fine tuning some of the terminology to better fit with C# and .NET common best practices.

## Terminology
In this section we discuss the team's use of clean architecture terminology.  Specifically each of these sections explain how the team is using clean architecture language in way that is compatible with DWD and C# / .NET.

### Entity
Replace the term “Entity” with “Business Entity”, but its use is limited to the folder/namespace that contains business entities.  This is proposed because .NET makes heavy use of the word entity via its Entity Framework ORM.  Entity Framework uses entities as object representations of database tables.  In addition to the use of “Business Entity” to represent business classes, use “Data Object” to represent data classes.  For Data Object’s we will also append “DataObject” as a suffix to the class as this will help us, and help intellisense, discriminate between business entities and data objects when both namespaces have been imported into a class.  Examples: 

* Business Entities 
  * \DWD.UI.Benefits.Domain\BusinessEntity\BasePeriod.cs 
  * \DWD.UI.Benefits.Domain\BusinessEntity\CalendarQuarter.cs 
  * \DWD.UI.Benefits.Domain\BusinessEntity\Claimant.cs 
  * etc. 

* Data Objects 
  * \DWD.UI.Benefits.Data\DataObject\BaseClaimDataObject.cs 
  * \DWD.UI.Benefits.Data\DataObject\ClaimantDataObject.cs 
  * \DWD.UI.Benefits.Data\DataObject\WeeklyClaimDataObject.cs   
  * etc. 

### Use Case
The term “Use Case” will only be applied to the folder/namespace that contains business logic.  For example: 
* \DWD.UI.Benefits.Domain\UseCases\CheckEligibility.cs 
* \DWD.UI.Benefits.Domain\UseCases\EstablishMonetary.cs 
* \DWD.UI.Benefits.Domain\UseCases\RecalculateMonetary.cs 
* etc. 

### Adapter
We can think of adapter as a general term that is not used directly in our code.  Rather, it refers generally to any number of classes: Gateway, Controller, Presenter, Repository, Data Mapper, or Mapper.  Proposals for the use of each are provided here: 
* Gateway -  
  * 1 - Limit the use of Gateway to the PoEAA definition of external system or interface - “an object that encapsulates access to an external system or interface”. 
    * For example, our benefits engine may have a ClaimantPortalGateway that sends messages to the claimant portal. 
  * 2 – Use the Repository name and pattern for the data access gateway as it has been widely adopted in enterprise .NET applications for many years. 
    * Use “Data Mappers” as a type of Adapter when translating from data entities to business entities and back again. 
  * Controller – continue with the use of “Controller” for outside-in processing. 
  * Presenter – only use the term “Presenter” when delivering something to a user, in other cases use Mapper (but use Data Mapper for the special case described previously).   
    * Presenter examples: 
      * AddressViewPresenter.cs - provides an input form for the claimant to complete. 
      * WeeklyClaimsReportPresenter.cs  - provides a weekly claims report for the user. 
    * Mapper and Data Mapper examples: 
      * BasePeriodClaimantPortalMapper.cs - maps our internal BasePeriod data structure into a different BasePeriod data structure suitable for use by the claimant portal. 
      * ClaimantDataMapper.cs - translates between Claimant Data Entity and Claimant Business Entity. 

### Frameworks & Drivers
As with adapter, we can think of frameworks & drivers as a general term that is not used directly in our code.  These are any external libraries that we need to interact with.  To keep our software flexible, we wrap the external library and invert the dependency (the normal clean architecture approach).   

* Example – Let's say we’re using a SMS library called AmazingSMS, but we are looking for a new provider because this one is expensive.  We will create our own interface wrapper called ISmsService and will implement that interface with SmsService that calls into AmazingSMS.  In the future we can replace the internals of our SmsService to call a different provider’s library (or we can implement other versions of SmsService if we want to support multiple providers). 