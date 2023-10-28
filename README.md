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

5. Navigate to `https://localhost:5001/swagger` to access the Swagger.

## API Endpoints

- `GET /api/book/list`: Fetches a list of all books.
- `GET /api/book/{id}`: Fetch a book by id.
- `GET /api/book/search`: Fetch a book by its attributes.
- `POST /api/book/create`: Post a new book object.
- `POST /api/book/{id}/update`: Update a book by id.
- `POST /api/book/{id}/delete`: Delete a book by id.

Similar endpoints are available for authors and customer.

## Error handling

The application returns standard HTTP status codes for errors.

Common error codes include:
- 400 Bad Request
- 401 Unauthorized
- 403 Forbidden
- 404 Not Found
- 500 Internal Server Error

## Authentication

The application employs authentication middleware to intercept and authenticate incoming requests. The middleware checks whether the request contains a valid authentication token. This token is a hardcoded string "YourHardcodedToken". The middleware verifies that the token in the request matches this predefined value to authenticate the user.

If a request fails to validate, an error code `403 Forbidden` is returned.  
In case no valid authentication information is provided, an error code `401 Unauthorized` is returned.

## Logging

A logging middleware is used to intercept incoming requests. The request is then formatted and output to the console.

The resulting log entry is structured as follows:  
` Received request: <Request Method> <Request Path> `

## Use case diagram
![Use case diagram](https://gitlab.fi.muni.cz/xzovak/pv179-bookhub/-/raw/2-milestone-1/UseCase.png?ref_type=heads)

## Entity relationship diagram

![Entity relation diagram](https://gitlab.fi.muni.cz/xzovak/pv179-bookhub/-/raw/2-milestone-1/DataModel.png?ref_type=heads)

## Notes

- The SQLite file (`bookhub.db`) is stored in the local application data folder.
