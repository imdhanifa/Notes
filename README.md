# 📝 Notes API – CQRS & Clean Architecture (.NET 9)

The **Notes API** is a RESTful web service built with **.NET 9**, designed to manage personal notes. It supports full **CRUD** operations while following the **Clean Architecture** and **CQRS (Command Query Responsibility Segregation)** patterns for better separation of concerns and scalability.

---

## 🚀 Features

- 📌 Create, Read, Update, and Delete notes
- ✍️ Each note contains:
  - Title
  - Content
  - Created and Updated timestamps
- ⚙️ Implements CQRS with MediatR:
  - `Commands`: Handle Create, Update, Delete
  - `Queries`: Handle Read operations
- 🧱 Clean Architecture:
  - Domain Layer (core business models)
  - Application Layer (use cases, CQRS handlers)
  - Infrastructure Layer (data access with EF Core)
  - API Layer (Minimal API endpoints)
- 🧪 Ready for unit and integration testing

---

## 🧱 Clean Architecture Overview

