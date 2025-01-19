# ToDoList_CleanArch_Dapper 
This repository is a demonstration of a modern backend architecture utilizing Clean Architecture principles combined with the MediatR library for handling CQRS (Command Query Responsibility Segregation), Dapper for lightweight and high-performance data access, a Unit of Work pattern for managing database transactions, and a Generic Repository pattern for abstracting data operations.

Key Features:
Clean Architecture:

The project structure separates concerns into distinct layers: Core, Application, Infrastructure, and Presentation.
Each layer has a clear responsibility, promoting maintainability, scalability, and testability.
MediatR Integration:

Implements the CQRS pattern using MediatR, enabling clean separation of commands (write operations) and queries (read operations).
Handlers are used to process requests, ensuring each operation is encapsulated and follows the Single Responsibility Principle.
Dapper for Data Access:

Utilizes Dapper for efficient and straightforward data access, enabling direct execution of SQL queries with minimal overhead.
Combines Dapper's lightweight nature with Clean Architecture to ensure data operations are fast and efficient.
Unit of Work Pattern:

Implements a Unit of Work to manage transactions across multiple repositories, ensuring data consistency and integrity.
Provides a centralized transaction management mechanism, allowing for rollbacks in case of errors.
Generic Repository Pattern:

A Generic Repository is implemented to handle common data operations (CRUD) for any entity.
This reduces code duplication and promotes reusability, making it easy to manage data access logic.
Project Structure:
Core Layer:

Contains the domain models, entities, and interfaces (e.g., IUnitOfWork, IGenericRepository).
Application Layer:

Houses the business logic, including MediatR request and response models, handlers (e.g., GetNoteByIdHandler), and DTOs.
Implements application services and validation logic.
Infrastructure Layer:

Implements the Generic Repository and Unit of Work using Dapper for data access.
Contains database context configurations, connection handling, and SQL queries.
Presentation Layer:

Exposes APIs using controllers (in case of an ASP.NET Core project) that interact with the application layer via MediatR commands and queries.
Example Use Cases:
Add/Update/Delete/Query Operations:

Handlers use the generic repository for CRUD operations, encapsulated within the unit of work for transaction management.
CQRS with MediatR:

Commands and queries are separated, improving performance and clarity. Queries do not modify data, and commands do not return data, adhering to CQRS principles.
Transaction Management:

The Unit of Work ensures all database operations in a use case are either committed or rolled back, maintaining consistency.
Benefits:
Separation of Concerns: Each layer focuses on its responsibility, making the system easy to maintain and extend.
Scalability: The architecture supports adding new features with minimal changes to existing code.
Performance: Dapper ensures fast and efficient data operations, suitable for high-performance applications.
Testability: The clear separation of concerns and use of interfaces make unit testing straightforward.

This repository serves as a robust starting point for building scalable, maintainable, and high-performance .NET applications.
