# 📚 Library Management System - CodeZone Task

This is a **Library Management System** developed as part of the CodeZone task using **ASP.NET Core MVC** with **Entity Framework Core** and an **in-memory database**. The system allows administrators to manage authors, books, and borrowing activities with validation and business logic implemented through service and repository layers.

---

## 🚀 Features

- Add, edit, and delete Authors and Books
- Borrow books with real-time availability checks
- Display book availability dynamically in the UI
- Business logic and validation using service layer
- Clean, modular architecture using Dependency Injection

---

## 🏗️ Project Architecture

This project follows a **3-Tier (N-Tier) Architecture**, which separates the application into distinct layers to promote maintainability, scalability, and testability.

```
LibraryManagementSystem/
│
├── 🎨 Presentation Layer (UI)
│   ├── Controllers/        # Handle HTTP requests and return views
│   ├── Views/              # Razor views for rendering HTML pages
│   ├── ViewModels/         # DTOs used to transfer data between UI and Business Logic
│   └── wwwroot/            # Static assets like CSS, JS, and images
│
├── 🧠 Business Logic Layer (Service Layer)
│   ├── Services/           # Contains core logic and rules of the application
│   └── Interfaces/         # Abstractions for services to support loose coupling and testing
│
├── 🗄️ Data Access Layer
│   └── Models/             # Entity Framework Core models and DbContext
│
├── Program.cs              # Application entry point and dependency injection setup
```

---

## 🛠️ Tech Stack

- C#
- ASP.NET Core MVC (.NET 6+)
- Entity Framework Core (In-Memory DB Provider)
- Razor Views (MVC)
- Bootstrap (UI styling)
- JavaScript & JQuery
- AutoMapper (A convention-based object-object mapper)

---

## ⚙️ Setup Instructions

1. **Clone the Repository**

```bash
git clone https://github.com/yehiamohamed030/LibraryManagementSystem-CodeZoneTask.git
```
```bash
cd LibraryManagementSystem-CodeZoneTask
```
2. **Restore & Build**

```bash
dotnet restore
```
```bash
dotnet build
```

3. **Run the Project (No database setup needed as it uses EF in memory database)**

```bash
dotnet run
```
