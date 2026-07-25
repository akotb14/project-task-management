# Project Task Management API

A clean-architecture Web API for managing projects and tasks, built with **ASP.NET Core**. The solution follows a layered design (Domain / Application / Infrastructure / API) with a CQRS-style **Features** organization, pipeline **Behaviors**, and a centralized **Result** pattern for consistent API responses.

---

## ✨ Features

- **Authentication** — user registration and login secured with **JWT**
- **Projects** — create, update, delete, and query projects by user
- **Tasks** — create tasks under a project, list tasks by project, update tasks, and change task status
- **CQRS** with **MediatR** — each use case is a discrete command/query handler under `Features`
- **FluentValidation** — request validation wired in as a MediatR pipeline **Behavior**
- **Global exception handling middleware** — catches unhandled exceptions and maps them to a consistent error response
- **Custom response wrapper** — every endpoint returns a standardized result shape via `ResultHandler`
- **Repository pattern with a Generic Repository** — shared CRUD implementation reused across entities, plus entity-specific repositories where needed
- **EF Core Fluent API configuration** — entity mappings configured via `IEntityTypeConfiguration<T>` rather than data annotations, with **Migrations** for schema management

---

## 🏗️ Architecture

The solution is organized using **Clean Architecture** principles, separating concerns into independent, testable layers:

```
project-task-management/                        # API layer
├── Connected Services
├── Controllers                                  # HTTP endpoints
├── appsettings.json                              # App configuration
├── Program.cs                                    # App entry point / DI setup
└── project-task-management.http                  # HTTP request scratch file

project-task-management.Application/              # Application layer
├── Behaviors                                     # MediatR pipeline behaviors (FluentValidation, etc.)
├── Features                                      # CQRS commands/queries + handlers, one folder per feature
├── Interface                                     # Application contracts (e.g. IGenericRepository, IJwtService)
├── Middlewares                                   # Global exception handling middleware
├── ResultHandler                                 # Custom standardized response/result wrapper
└── ModuleApplicationDependencies.cs              # DI registration (MediatR, FluentValidation, behaviors)

project-task-management.Domain/                  # Domain layer
├── Entities                                      # Core domain models
├── Enums                                         # Domain enumerations
├── Exceptions                                    # Domain-specific exceptions
└── Helper                                        # Domain helper utilities

project-task-management.Infrastructure/           # Infrastructure layer
├── Configuration                                 # EF Core Fluent API entity configurations (IEntityTypeConfiguration<T>)
├── Context                                       # EF Core DbContext
├── Migrations                                    # EF Core migrations
├── Repository                                    # Generic Repository + entity-specific repository implementations
├── Service                                       # External/infrastructure services (e.g. JWT token generation)
└── ModuleInfrastructureDependencies.cs            # DI registration for this layer
```

**Layer responsibilities:**

| Layer | Responsibility |
|---|---|
| **Domain** | Core business entities, enums, exceptions — no external dependencies |
| **Application** | CQRS commands/queries (Features), MediatR pipeline behaviors, FluentValidation rules, interfaces the outer layers implement |
| **Infrastructure** | EF Core `DbContext` with Fluent API configurations, migrations, generic + specific repository implementations, JWT/service implementations |
| **API** | Controllers, global exception middleware, request/response models, app startup and DI wiring |

---

## 🛠️ Tech Stack

- **ASP.NET Core** Web API
- **Clean Architecture** (Domain / Application / Infrastructure / API)
- **MediatR** — CQRS command/query dispatching
- **FluentValidation** — validation rules run as a MediatR pipeline behavior
- **JWT** — authentication/authorization
- **Global exception handling middleware** — centralized error handling
- **Custom response wrapper** — consistent API result shape across all endpoints
- **Repository pattern with Generic Repository** — shared CRUD, extended per entity as needed
- **Entity Framework Core** — Fluent API configurations + Migrations
- **Postman** for API documentation

---

## 📡 API Endpoints

### Auth
| Method | Endpoint | Description |
|---|---|---|
| `POST` | Register | Register a new user |
| `POST` | login | Authenticate a user |

### Projects
| Method | Endpoint | Description |
|---|---|---|
| `POST` | Create | Create a new project |
| `PUT` | Update | Update an existing project |
| `DEL` | Delete | Delete a project |
| `GET` | GetProductsByUser | Get all projects for a user |
| `GET` | GetProjectByIdByUser | Get a specific project by ID for a user |

### Tasks
| Method | Endpoint | Description |
|---|---|---|
| `POST` | Create | Create a new task under a project |
| `GET` | GetTaskbyProject | Get all tasks for a project |
| `PUT` | UpdateTask | Update a task |
| `PUT` | ChangeTaskStatus | Change a task's status |
| `DEL` | Delete | Delete a task |

📚 **Full request/response documentation (Postman):**
- [Authentication](https://documenter.getpostman.com/view/29628603/2sBY4QtfDZ)
- [Projects](https://documenter.getpostman.com/view/29628603/2sBY4QtfSk)
- [Tasks](https://documenter.getpostman.com/view/29628603/2sBY4QtfDY)

---

## 🚀 Getting Started

### Prerequisites
- [.NET SDK](https://dotnet.microsoft.com/download) (matching the project's target framework)
- A relational database supported by EF Core (e.g. SQL Server)
- An IDE such as Visual Studio, Rider, or VS Code

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/akotb14/project-task-management.git
   cd project-task-management
   ```

2. **Restore dependencies**
   ```bash
   dotnet restore
   ```

3. **Configure the database connection**

   Update the connection string (and any other settings, e.g. JWT secrets) in `project-task-management/appsettings.json`.

4. **Apply migrations**
   ```bash
   dotnet ef database update --project project-task-management.Infrastructure --startup-project project-task-management
   ```

5. **Run the API**
   ```bash
   dotnet run --project project-task-management
   ```

6. **Explore the API**

   Use the included `project-task-management.http` file, or import the Postman collections linked above, to try out the endpoints.

---

## 📁 Project Structure Summary

- **`project-task-management`** — API entry point (Controllers, `Program.cs`, configuration)
- **`project-task-management.Application`** — Use cases, behaviors, interfaces, result handling
- **`project-task-management.Domain`** — Entities, enums, exceptions, domain helpers
- **`project-task-management.Infrastructure`** — Persistence (DbContext, migrations, repositories, services)

---

## 📄 License

Specify your license here (e.g. MIT).

## 👤 Author

**akotb14** — [GitHub](https://github.com/akotb14)
