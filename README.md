# Binge Buddy

## Prerequisites

- Docker
- Docker Compose

## Services and technologies used

- ASP.NET Core API
- .NET Worker Service
- SQL Server
- Redis
- Entity Framework
- AutoBogus
- AutoFixture
- XUnit
- TestContainers

## How to run

1. Copy the `.env.example` file and rename it to `.env`
2. Fill in the necessary values (Note: the Connection String needs to be a `SQL Server` connection string)
3. First run the migrations by running `docker-compose --profile migrations up -d --build`
4. Then run the rest of the applications by running: `docker-compose --profile binge up -d --build`
5. Notice the `Scraper` container taking care of the TV Maze API scraping
6. Navigate to the `API` container's URL by going to `http://localhost:8888/shows` or optionally use paging by going to `http://localhost:8888/shows?limit=10&offset=0`. Replace the parameters to your desired values

Example `.env` file:

```
DATABASE_PASSWORD=P@ssw0rd!
CONNECTION_STRING="Server=host.docker.internal;Database=BingeBuddy;User Id=sa;Password=P@ssw0rd!;TrustServerCertificate=True"
```

## Overview

Binge Buddy is an API-only application to retrieve show data imported by the TV Maze API.
There are two runnable projects in this application.

### Scraper

The Scraper is responsible for retrieving data from the TV Maze API and storing this in a SQL database.
This is done using .NET's background services with a timed interval. This allows continuous scraping of the TV Maze API.
Since we are hitting well-known endpoints, we do not have to worry about rate limiting at the moment.

### API

The API is an ASP.NET Core API project that exposes and endpoint to query all shows (with pagination).
These requests are cached based on the page size using Redis.

## Automatic tests

- Unit tests are available for a small part of the code.
- Integration tests are available for complete functional end-to-end tests using TestContainers to simulate a production environment for the API

## Future concerns

- Implement Polly for resilient requests (in case rate limiting occurs)
- Implement other API endpoints
- Implement scraping from where we left off previously by calculating the last successful item to have been scraped and calculating the next page
- Combine show data with cast data
