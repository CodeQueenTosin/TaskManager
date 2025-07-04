# 📌 TaskManager Project Documentation

## 1. Overview

**TaskManager** is a clean, modular task management API built with **C# and .NET 6**, following **Clean Architecture**. It supports full **CRUD operations** and calculates key task metrics using a remote data source ([MockAPI.io](https://mockapi.io)).

### 🔑 Features

- **Task Fields**:
  - `TaskId`, `TaskName`, `TaskDescription`
  - `StartDate`, `AllottedTime`, `ElapsedTime`, `TaskStatus`
  - **Computed**: `EndDate`, `DueDate`, `DaysOverdue`, `DaysLate`

- **API Endpoints**:
  - `POST /api/tasks` – Create Task
  - `GET /api/tasks` – List Tasks
  - `GET /api/tasks/{id}` – Get Task by ID
  - `DELETE /api/tasks/{id}` – Delete Task

- **Data Source**: Uses `HttpClient` to interact with MockAPI.io.

---

## 2. Architecture

| Layer          | Responsibility                                                                  |
|----------------|-------------------------------------------------------------------------------- |
| **Domain**     | Core logic (`TaskItem`, computed properties, FluentValidation)                  |
| **Application**| Business contracts and services (`ITaskService`, `TaskService`)                 |
| **Infrastructure** | HTTP-based persistence (`TaskRepository`)                                   |
| **Presentation**| Web API endpoints, validation, Swagger, global error handling                  |

---

## 3. Project Structure
<root>/
├── TaskManager.Api/ # Presentation
├── TaskManager.Application/ # Application
├── TaskManager.Domain/ # Domain
├── TaskManager.Infrastructure/ # Infrastructure


## 4. CRUD Flow

- **POST**: Validate input → Call Service → Save via Repository → Return created Task with computed fields.
- **GET**: Fetch from MockAPI → Compute metrics → Return enriched list or single item.
- **DELETE**: Remove from MockAPI → Return success or not found.

---

## 5. Computed Properties

```csharp
public DateTime EndDate => StartDate.AddDays(ElapsedTime);
public DateTime DueDate => StartDate.AddDays(AllottedTime);
public int DaysOverdue => !TaskStatus ? Math.Max(ElapsedTime - AllottedTime, 0) : 0;
public int DaysLate => TaskStatus ? Math.Max(AllottedTime - ElapsedTime, 0) : 0;


6. Validation Rules

Via FluentValidation:

Required: TaskName, StartDate

AllottedTime, ElapsedTime must be ≥ 0

Optional: Prevent overdue on pending tasks, disallow past start dates

7. Getting Started
🔧 Prerequisites
.NET 6 SDK

Visual Studio 

⚙️ Setup & Run
git clone https://github.com/CodeQueenTosin/TaskManager.git
cd TaskManager
dotnet restore
dotnet build
dotnet run
🔗 Access Swagger UI:
https://localhost:7216/swagger/index.html

✅ Conclusion
TaskManager is a clean, testable, and maintainable API built with a focus on:

✔️ Testability – Logic isolated in Domain Layer

✔️ Maintainability – Easy to swap MockAPI with real DB

✔️ Scalability – Clean Architecture enables long-term growth

✔️ Clarity – Each layer has a well-defined responsibility

