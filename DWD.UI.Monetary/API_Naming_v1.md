# API Endpoint Naming

## Organize the API design around resources

[Read more ...](https://docs.microsoft.com/en-us/azure/architecture/best-practices/api-design)

[Even more ...](https://cloud.google.com/apis/design/standard_methods)

## Implement Versioning Strategy
Use the [URL Path](https://github.com/dotnet/aspnet-api-versioning/wiki/Versioning-via-the-URL-Path) versioning strategy utilizing the [Microsoft.AspNetCore.Mvc.Versioning](https://www.nuget.org/packages/Microsoft.AspNetCore.Mvc.Versioning/) library.

There are four types of version specified in the [Microsoft Versioning Guidelines](https://github.com/dotnet/aspnet-api-versioning/wiki). the Microsoft.AspNetCore.Mvc.Versioning library is capable of implementing facilitating all four if we need to pivot in the future.

- URI
- Version Query Parameter
- Custom Request Header
- Media Versioning Accept Header

## Business Entity Focused Endpoints
When possible, resource URIs should be based on nouns (the resource) and not verbs (the operations on the resource).

## Define API by HTTP methods
The HTTP protocol defines a number of methods that assign semantic meaning to a request. The common HTTP methods used by most RESTful web APIs are:

- GET retrieves a representation of the resource at the specified URI. The body of the response message contains the details of the requested resource.
- POST creates a new resource at the specified URI. The body of the request message provides the details of the new resource. Note that POST can also be used to trigger operations that don't actually create resources.
- PUT either creates or replaces the resource at the specified URI. The body of the request message specifies the resource to be created or updated.
- PATCH performs a partial update of a resource. The request body specifies the set of changes to apply to the resource.
- DELETE removes the resource at the specified URI.

***PATCH is a special case that we will not use unless necessary***
### Examples

| **Resource**           | **POST**                          | **GET**                             | **PUT**                                       | **DELETE**                       |
| :--------------------- | :-------------------------------- | :---------------------------------- | :-------------------------------------------- | :------------------------------- |
| /v1/claimants          | Create a new claimant             | Retrieve all claimants              | Bulk update of claimants                      | Remove all claimants             |
| /v1/claimants/1        | Error                             | Retrieve the details for claimant 1 | Update the details of claimants1 if it exists | Remove claimant 1                |
| /v1/claimants/1/claims | Create a new claim for claimant 1 | Retrieve all claims for claimant 1  | Bulk update of claims for claimant 1          | Remove all claims for claimant 1 |

## Rename Existing Endpoints

| **From**                                                                             | **To**                                                          |
|--------------------------------------------------------------------------------------|-----------------------------------------------------------------|
| GET /BasePeriod/GetStandardBasePeriodFromInitialClaimDate?initialClaimDate=1/1/2021  | GET /v1/standard-base-period-by-date?initialClaimDate=1/1/2021  |
| GET /BasePeriod/GetAlternateBasePeriodFromInitialClaimDate?initialClaimDate=1/1/2021 | GET /v1/alternate-base-period-by-date?initialClaimDate=1/1/2021 |
| GET /BasePeriod/GetStandardBasePeriodFromYearAndWeek?year=2021&week=1                | GET /v1/standard-base-period-by-year-week?year=2021&week=1      |
| GET /BasePeriod/GetAlternateBasePeriodFromYearAndWeek?year=2021&week=1               | GET /v1/alternate-base-period-by-year-week?year=2021&week=1     |
| GET /benefityear/lookup-by-date?requestedDate=1/1/2021                               | GET /v1/benefit-year?requestedDate=1/1/2021                     |
| POST /Eligibility/VerifyEligibility                                                  | POST /v1/eligibility-verification                               |
| GET /WageEntry/GetClaimantWage/{id}                                                  | GET /v1/wage-entries/{wageEntryId}                              |
| GET /WageEntry/GetAllClaimantWages                                                   | GET /v1/wage-entries                                            |
| GET /WageEntry/GetAllClaimantWagesForClaimant/{claimantId}                           | GET /v1/claimants/{claimantId}/wage-entries                     |
| PUT /WageEntry/UpdateClaimantWage/{id}                                               | PUT /v1/wage-entries/{wageEntryId}                              |
| DELETE /WageEntry/DeleteClaimantWage/{id}                                            | DELETE /v1/wage-entries/{wageEntryId}                           |
| POST /WageEntry/CreateClaimantWag                                                    | POST /v1/claimants/{claimantId}/wage-entries                    |

## Path Variables vs Query String For Base Period
We should use unique identifier for path variables and neither initialClaimDate nor year/week unique identifiers for base period. So we should use query string to get base periods.
Also we noticed that the dates with `MM/DD/YYYY` is not supported with path variables as it thinks `MM`, `DD` and `YYYY` are separate path variables and we will get `404 Not found`
