📌 TaskManager Project Documentation
________________________________________
1. Overview
TaskManager is a modular and extensible Task List Manager Web Service built using C# and .NET 6, following the Clean Architecture pattern. It exposes a RESTful API to perform full CRUD operations on task items and compute task-related metrics automatically.
The system interacts with a remote data source via MockAPI.io, simulating persistence with real HTTP communication.
🔑 Core Features
•	Task Properties:
o	TaskId (string): Unique identifier (auto-generated or supplied)
o	TaskName (string): Concise title of the task
o	TaskDescription (string): Detailed description
o	StartDate (DateTime): When the task starts
o	AllottedTime (int in days): Planned duration
o	ElapsedTime (int in days): Actual time spent
o	TaskStatus (bool): false = Pending, true = Closed
o	Computed Fields (calculated dynamically):
	EndDate = StartDate + ElapsedTime
	DueDate = StartDate + AllottedTime
	DaysOverdue = max(ElapsedTime - AllottedTime, 0) (if Pending)
	DaysLate = max(AllottedTime - ElapsedTime, 0) (if Closed)
•	REST API Endpoints:
o	POST /api/tasks – Create a new task
o	GET /api/tasks – Retrieve all tasks (with computed metrics)
o	GET /api/tasks/{id} – Retrieve a task by ID (with computed metrics)
o	DELETE /api/tasks/{id} – Delete a task by ID
•	External Data Source:
o	Uses MockAPI.io for remote persistence via HttpClient.



________________________________________
2. Clean Architecture Layers
Layer	Responsibilities
Domain	Contains core business logic: TaskItem entity, computed properties, and validation using FluentValidation.
Application	Defines business contracts (ITaskService) and implements orchestration logic (TaskService).
Infrastructure	Implements data persistence logic using HttpClient to communicate with MockAPI (TaskRepository).
Presentation	Handles incoming HTTP requests and responses using ASP.NET Core Web API. Includes input validation, Swagger UI, and error handling middleware.

________________________________________
-	Architectural Diagram
 





3. Project Structure
<root-folder>/
├── TaskManager.Api/             # Presentation Layer
│   ├── Controllers/
│   ├── Middleware/
│   └── Program.cs
│
├── TaskManager.Application/     # Application Layer
│   ├── Interfaces/ITaskService.cs
│   └── Services/TaskService.cs
│
├── TaskManager.Domain/          # Domain Layer
│   ├── Entities/TaskItem.cs
│   └── Validators/TaskItemValidator.cs
│
├── TaskManager.Infrastructure/  # Infrastructure Layer
│   └── Repository/TaskRepository.cs

________________________________________
4. Data Flow – CRUD Operations
🔸 POST /api/tasks
1.	Controller receives request payload
2.	Validates using TaskItemValidator
3.	Calls TaskService.AddTaskAsync()
4.	Delegates to repository → POST to MockAPI
5.	Task created with computed fields → returned as 201 Created
🔸 GET /api/tasks
1.	Controller calls TaskService.GetAllTasksAsync()
2.	Repository fetches tasks from MockAPI
3.	Domain layer computes metrics (e.g., DueDate, DaysOverdue)
4.	Controller returns enriched task list
🔸 GET /api/tasks/{id}
1.	Controller calls TaskService.GetTaskByIdAsync(id)
2.	Task retrieved and returned with computed values
🔸 DELETE /api/tasks/{id}
1.	Controller calls TaskService.DeleteTaskAsync(id)
2.	Repository issues DELETE request to MockAPI
3.	Returns 204 No Content on success or 404 Not Found if not found
________________________________________
5. Computed Properties in Domain
public DateTime EndDate => StartDate.AddDays(ElapsedTime);
public DateTime DueDate => StartDate.AddDays(AllottedTime);
public int DaysOverdue => !TaskStatus ? Math.Max(ElapsedTime - AllottedTime, 0) : 0;
public int DaysLate => TaskStatus ? Math.Max(AllottedTime - ElapsedTime, 0) : 0;
________________________________________
6. Input Validation
Implemented using FluentValidation in TaskItemValidator.cs.
Rules include:
•	Required fields (TaskName, StartDate)
•	Positive integers for AllottedTime and ElapsedTime
•	Start date must not be in the past (optional rule)
•	Elapsed time should not exceed allotted time if the task is still pending (optional rule)
________________________________________
7. Getting Started
🔧 Prerequisites
•	.NET 6 SDK
•	Visual Studio (recommended)
📥 Setup Instructions
•	git clone https://github.com/CodeQueenTosin/TaskManager.git
•	dotnet restore
•	dotnet build

▶️ Run the API
•	dotnet run
•	Access the Swagger UI at:
📍 https://localhost:7216/swagger/index.html

________________________________________
Conclusion
This documentation outlines the design and implementation of the TaskManager Web Service, a modular and testable task management API built with C# and .NET 6 using Clean Architecture.
✅ Testable & Decoupled: Core logic resides in the Domain layer.
✅ Maintainable: Easy to swap out the data source (e.g., from MockAPI to SQL Server).
✅ Scalable Architecture: Clean Architecture facilitates long-term growth and team collaboration.
✅ Clear Responsibility Separation: Each layer has a single focus and minimal coupling.

