# API Endpoint Naming (Pre-Draft)

## Organize the API design around resources

[Read more ...](https://docs.microsoft.com/en-us/azure/architecture/best-practices/api-design)

[Even more ...](https://cloud.google.com/apis/design/standard_methods)

Focus on the business entities that the web API exposes. For example, in an e-commerce system, the primary entities might be customers and orders. Creating an order can be achieved by sending an HTTP POST request that contains the order information. The HTTP response indicates whether the order was placed successfully or not. When possible, resource URIs should be based on nouns (the resource) and not verbs (the operations on the resource).

The effect of a specific request should depend on whether the resource is a collection or an individual item. The following table summarizes the common conventions adopted by most RESTful implementations using the claimants example. Not all of these requests might be implemented—it depends on the specific scenario.

| **Resource**           | **POST**                          | **GET**                             | **PUT**                                       | **DELETE**                       |
| :--------------------- |:----------------------------------|:------------------------------------|:----------------------------------------------|:---------------------------------|
| /v1/claimants          | Create a new claimant             | Retrieve all claimants              | Bulk update of claimants                      | Remove all claimants             |
| /v1/claimants/1        | Error                             | Retrieve the details for claimant 1 | Update the details of claimants1 if it exists | Remove claimant 1                |
| /v1/claimants/1/claims | Create a new claim for claimant 1 | Retrieve all claims for claimant 1  | Bulk update of claim for claimant 1           | Remove all claims for claimant 1 |


##  Versioning a RESTful web API
[Microsoft verioning guidlines...](https://github.com/dotnet/aspnet-api-versioning/wiki)

These are the methods for API versioning
 - URL Versioning
 - Version Query Parameter
 - Custom Request Header
 - Media Versioning Accept header

Fortunately, the Microsoft.AspNetCore.Mvc.Versioning library handles all methods. We will begin with URL versioning, but have the option to migrate to another method later.

## Proposed Namings for Endpoints

| **From**                                                                                   | **To**                                                            |
|--------------------------------------------------------------------------------------------|-------------------------------------------------------------------|
| GET /BasePeriod/<br />GetStandardBasePeriodFromInitialClaimDate?initialClaimDate=1/1/2021  | GET /v1/standard-base-period-by-date?initialClaimDate=1/1/2021    |
| GET /BasePeriod/<br />GetAlternateBasePeriodFromInitialClaimDate?initialClaimDate=1/1/2021 | GET /v1/alternative-base-period-by-date?initialClaimDate=1/1/2021 |
| GET /BasePeriod/<br />GetStandardBasePeriodFromYearAndWeek?year=2021&week=23               | GET /v1/standard-base-period-by-year-week?year=2021&week=23       |
| GET /BasePeriod/<br />GetAlternateBasePeriodFromYearAndWeek?year=2021&week=23              | GET /v1/alternative-base-period-by-year-week?year=2021&week=23    |
| GET /benefityear/<br />lookup-by-date?requestedDate=1/1/2021                               | GET /v1/benefit-year?requestedDate=1/1/2021                       |
| POST /Eligibility/VerifyEligibility                                                        | POST /v1/eligibility-verification                                 |
| GET /WageEntry/GetClaimantWage/{id}                                                        | GET /v1/wage-entries/{wageEntryId}                                |
| GET /WageEntry/GetAllClaimantWages                                                         | GET /v1/wage-entries                                              |
| GET /WageEntry/<br />GetAllClaimantWagesForClaimant/{claimantId}                           | GET /v1/claimants/{claimantId}/wage-entries                       |
| PUT /WageEntry/UpdateClaimantWage/{id}                                                     | PUT /v1/wage-entries/{wageEntryId}                                |
| DELETE /WageEntry/DeleteClaimantWage/{id}                                                  | DELETE /v1/wage-entries/{wageEntryId}                             |
| POST /WageEntry/CreateClaimantWag                                                          | POST /v1/claimants/{claimantId}/wage-entries                      |
