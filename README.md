# Mechanic Workshop Budget API

## 📌 Description

This project is a REST API developed with .NET for managing workshop budgets. It includes input validation, business rules, automatic total calculation, and in-memory data persistence.

Developed as part of a technical assessment for a .NET Backend Developer position, this project also served as an opportunity to strengthen my API development skills and expand my portfolio.

---

## 🚀 Technologies

* .NET / ASP.NET Core
* Entity Framework Core (InMemory)
* C#
* Swagger (OpenAPI)

---

## ⚙️ Features

* Create a new budget
* Input validation
* Automatic total calculation (items + overall budget)
* In-memory persistence using EF Core
* Retrieve created budgets

---

## 🧪 Tests Performed

* Valid budget creation ✔️
* Missing required fields ✔️
* Empty items list ✔️
* Invalid values (zero or negative) ✔️

---

## 📌 Business Rules

* `clienteId` and `veiculoId` are required
* At least one item must be provided
* Each item must have:
  * description
  * quantity > 0
  * unit value > 0
* Total value is calculated by the API (not provided by client)
