# Project Structure
The project structure is defined below: 

## Web
Contains a web api controller (`/MarketAuction`) with two routes
- `/` get all the information available in the mock api response
- `marketAuctionValue?id={id}&year={year}` get the market/auction value for an specific id and year


## Core
All business logic is implemented in this project, this means, all entities, interfaces and services are there.

## Tests
Unit tests were added to ensure `MarketAuctionService` methods are working as expected (meeting business logic criteria).


# Project Setup
1. run `dotnet restore` for all projects
2. For manually testing the api, locate inside the `Web` project folder and run `dotnet run`
3. For running test suite, locate inside the `Tests` project folder and run `dotnet test`