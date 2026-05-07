# Mechanic Workshop Budget API

## 📌 Description

This project is a REST API developed with .NET for managing workshop budgets. It includes input validation, business rules, automatic total calculation, and in-memory data persistence.

Developed as part of a technical assessment for a .NET Backend Developer position, this project also served as an opportunity to strengthen backend architecture concepts, API development practices, and portfolio projects.

---

## 🚀 Technologies

* .NET / ASP.NET Core
* Entity Framework Core (InMemory)
* C#
* Swagger (OpenAPI)

---

## ⚙️ Features

* Create a new budget
* Retrieve all budgets
* Retrieve budget by ID
* Update budgets
* Delete budgets
* Input validation
* Automatic total calculation
* Business rules validation
* In-memory persistence using EF Core

---

## 🏗️ Project Structure

The project was organized with separation of responsibilities using:

* DTOs
* Services
* Models
* Endpoints
* Data layer

---

## 🧪 Tests Performed

* Valid scenarios ✔️
* Missing required fields ✔️
* Empty items list ✔️
* Invalid values (zero or negative) ✔️
* Nonexistent budget ✔️

---

## 📌 Business Rules

* `clienteId` and `veiculoId` are required
* At least one item must be provided
* Each item must have:
  * description
  * quantity > 0
  * unit value > 0
* Total value is calculated by the API (not provided by client)
