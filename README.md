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

- `GET /api/Books`: Fetches a list of all books.
- `GET /api/Books/{id}`: Fetch a book by id.
- `GET /api/Books/search`: Fetch a book by its attributes.
- `POST /api/Books`: Post a new book object.
- `POST /api/Books/{id}`: Update a book by id.
- `POST /api/Books/{id}`: Delete a book by id.

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
` Response status code: <Response status code> `

## Use case diagram
**User(customer) Actions**  
- Create Account: The customer can create an account to access the application.
- Login: After creating an account, the customer can log in to the application.
- Browse and Filter Books: Once logged in, the customer can search for books and apply filters to refine their search.
- Purchase Books: The customer can buy books from the available selection.
- Rate Books: After purchasing a book, the customer can provide a rating and feedback.
- Access Purchase History: The customer can view their purchase history to see the books they've bought.
- Add Books to Wishlist: Customers can add books they are interested in to their wishlist.  

**Administrator Actions**
- Update Book Details: The administrator has the ability to modify book information.
- Regulate Book Prices: Administrators can manage the pricing of books.
- Manage User Accounts: Administrators can make changes to user accounts, including creating, updating, or deactivating them.
![Use case diagram](https://gitlab.fi.muni.cz/xzovak/pv179-bookhub/-/raw/2-milestone-1/UseCase.png?ref_type=heads)

## Entity relationship diagram
**Entities in ERD:**
- Book: Represents individual books available in the system. Contains attributes `book ID`, `Title`, `AuthorId`, `GenreId`, `PublisherId` and `Price`. Each book may be associated with one authors, belong to one genre, be published by a specific publisher, and have multiple reviews.

- Customer: Represents users of the system. Contains attributes `CustomerID`, `Username`, and `Password`.

- Author: Represents authors who write books. Contains attributes `AuthorID` and `Name`. An author can be associated with one or more books they've authored.

- Genre: Represents book categories or genres. Contains attributes like `GenreID` and `Name`. Multiple books can belong to the same genre.

- Publisher: Represents book publishers. Contains attributes like `PublisherID` and `Name`. A publisher can publish multiple books.

- WishlistItem: Represents items that a customer adds to their wishlist. Contains attributes like `WishlistItemID`, `BookID` and `CustomerID`. Each item is associated with a customer and a specific book.

- PurchaseHistory: Represents the purchase history of customers. Contains attributes like `PurchaseHistoryID`, `BookID`, `CustomerID` and `PurchaseDate`. Each entry is associated with a customer and a book they've purchased.

- Review: Represents reviews left by customers for books. Contains attributes like `ReviewID`, `CustomerID`, `BookID`, `Rating`, and `Comments`. Each review is associated with a customer and the book they reviewed.
![Entity relation diagram](https://gitlab.fi.muni.cz/xzovak/pv179-bookhub/-/raw/2-milestone-1/DataModel.png?ref_type=heads)

## Notes

- The SQLite file (`bookhub.db`) is stored in the local application data folder.
