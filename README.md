# 📦 Stock Management System

A full-stack web application for managing inventory across multiple transportation methods: road, air, sea, and rail. Designed for efficient tracking, updating, and visualization of stock data.

---

## ⚙️ Technologies Used

- **ASP.NET Core Web API** – Backend services
- **Entity Framework Core** – Database ORM
- **SQL Server (SSMS)** – Relational database
- **JavaScript (Vanilla)** – Frontend logic
- **HTML + CSS** – Web interface and styling
- **JWT Authentication** – Secure access and token management
- **Layered Architecture** – Clean separation of concerns

---

## ✨ Features

- ✅ Authentication system using JWT (JSON Web Tokens)
- ✅ CRUD operations on stocks, vehicles, and shipments
- ✅ Track inventory by category and transport type (air, land, sea, rail)
- ✅ Token stored in localStorage for session management
- ✅ Fetch API used for client-server communication
- ✅ Form validation using C# validation attributes
- ✅ Organized, scalable codebase using layered architecture

---

## 🚀 Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/Kokimoko4a/Stock-Managment-System.git
cd Stock-Managment-System
2. Set Up the Database
Install and run SQL Server

Update the appsettings.json with your connection string

3. Apply Migrations
bash
Copy
Edit
dotnet ef database update
4. Run the Backend API
bash
Copy
Edit
dotnet run
5. Open the Frontend
Open index.html or your custom frontend file in the browser

Ensure the backend URL matches in the fetch requests

🔐 Authentication Flow
On login/register, a JWT token is returned

The token is stored in localStorage

All protected routes require the token in the Authorization header

🗂️ Project Structure
bash
Copy
Edit
/StockManagmentSystem
│
├── /Controllers
├── /DTOs
├── /Models
├── /Services
├── /Repositories
├── appsettings.json
└── Program.cs / Startup.cs

🧠 Architecture
Presentation Layer – Handles API calls and static frontend

Business Logic Layer – Contains core services and rules

Data Access Layer – Uses Entity Framework for database operations

🤝 Contributing
Pull requests and suggestions are welcome! If you find a bug or want to add a feature, open an issue or PR.

📜 License
This project is licensed under the MIT License.

👨‍💻 Author
Developed by Kaloyan Rusev
