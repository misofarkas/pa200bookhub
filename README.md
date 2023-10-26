
# BookHub

BookHub is a web application intended for management of book colections and informations related to them.

## Structure

The solution consists of two projects:

1. **DataAccessLayer**: Contains the data access logic, including the Entity Framework Core context, entities, and data initialization.

2. **WebApplication1**: ASP.NET Core Web API project that exposes the BookHub app through RESTful endpoints.

## Prerequisites

- .NET 7
- SQLite

## How to Run

1. **Clone the Repository**: 
   ```
   git clone https://gitlab.fi.muni.cz/xzovak/pv179-bookhub.git
   ```

2. **Navigate to the Solution Directory**:
   ```
   cd pv179-bookhub
   ```

3. **Restore NuGet Packages**:
   ```
   dotnet restore
   ```

4. **Run the Web API Project**:
   ```
   dotnet run --project WebApplication1
   ```

5. Nvigate to `https://localhost:5001/swagger` to access the Swagger.

## API Endpoints

- `GET /api/book/list`: Fetches a list of all books.

Similar endpoints are available for authors and customer.

## Notes

- The SQLite file (`bookhub.db`) is stored in the local application data folder.
