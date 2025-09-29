
# 🧾 StockIT — Small Business Web-Based Inventory Management System

📦 **StockIT** is a modern web application built to empower small businesses with efficient, real-time inventory management — without the cost or complexity of enterprise systems.

## 🌐 Live Demo

👉 [Try StockIT Now — Deployed on Microsoft Azure App Service.](https://stockit.azurewebsites.net/)

-----

## 🧭 Table of Contents

  * [✨ Overview](https://github.com/stere8/stockit/edit/master/README.md#-overview)
  * [🚀 Features](https://github.com/stere8/stockit/edit/master/README.md#-features)
  * [🛠️ Tech Stack](https://github.com/stere8/stockit/edit/master/README.md#%EF%B8%8F-tech-stack)
  * [🏗️ Architecture](https://github.com/stere8/stockit/edit/master/README.md#%EF%B8%8F-architecture)
  * [⚡ Getting Started](https://github.com/stere8/stockit/edit/master/README.md#-getting-started)
  * [☁️ Deployment Guide](https://github.com/stere8/stockit/edit/master/README.md#%EF%B8%8F-deployment-guide)
  * [🧪 Testing Strategy](https://github.com/stere8/stockit/edit/master/README.md#-testing-strategy)
  * [📊 Comparative Analysis](https://github.com/stere8/stockit/edit/master/README.md#-comparative-analysis)
  * [📝 Future Roadmap](https://github.com/stere8/stockit/edit/master/README.md#-future-roadmap)
  * [🤝 Contributing](https://github.com/stere8/stockit/edit/master/README.md#-contributing)
  * [👤 Author](https://github.com/stere8/stockit/edit/master/README.md#-author)
  * [🙏 Acknowledgments](https://github.com/stere8/stockit/edit/master/README.md#-acknowledgments)

-----

## ✨ Overview

Small businesses often rely on manual spreadsheets, outdated software, or no tracking at all, leading to:
❌ **Inventory inaccuracies**
🕓 **Wasted time** on repetitive tasks
💸 **Lost sales** due to stockouts or overstocking

StockIT was created to solve these problems.

🧠 **Mission:** Deliver a free, simple, and scalable inventory management tool for startups and small shops.

Developed as part of my **Engineering Thesis** at the University of Economy (Bydgoszcz, 2025), StockIT combines **.NET 8**, **Entity Framework Core**, and **Bootstrap** to deliver a clean, modern, cloud-ready inventory system.

-----

## 🚀 Features

### 🏷️ Core Inventory Management

  * **Product & Category Management:** Add, edit, delete, and view products with detailed information and images.
  * **Real-time Stock Tracking:** Monitor inventory levels with live updates.
  * **Category Organization:** Organize products into customizable categories.

### 🔍 Advanced Functionality

  * **Smart Search & Filter:** Instantly search products by name/description or filter by category.
  * **Data Export:** Download inventory reports in **CSV** and **Excel** formats for offline use.
  * **Bulk Operations:** Efficiently manage multiple products and categories at once.

### 🎯 User Experience

  * **Responsive Design:** Works seamlessly on desktop, tablet, and mobile devices.
  * **Intuitive Interface:** Clean, modern UI built with Bootstrap 5.
  * **Secure CRUD:** Validation and security are built into every operation.

-----

## 🛠️ Tech Stack

| Component | Technology | Description |
| :--- | :--- | :--- |
| **Backend** | `ASP.NET Core 8 (C#)`, Razor Pages | Fast, modern web framework. |
| **Frontend** | `Bootstrap 5`, `JavaScript/jQuery`, `HTML5/CSS3` | Responsive, clean, and intuitive UI. |
| **Database** | `SQL Server` + `Entity Framework Core` | Code-First with Migrations for robust data management. |
| **DevOps/Infra** | `Microsoft Azure Web App`, `Docker`, `Git` | Cloud-ready deployment and source control. |
| **Testing** | `xUnit` | Unit and integration testing framework. |

-----

## 🏗️ Architecture

StockIT follows a clean, multi-layered architecture for maintainability and scalability.

  * **Presentation Layer:** Razor Pages + Bootstrap for a responsive UI.
  * **Business Logic Layer (BLL):** Encapsulates business logic in services like `ProductService`.
  * **Data Access Layer (DAL):** EF Core with SQL Server using the Code-First approach.

### Project Structure

```
StockIT.sln
├── StockIT/              # Web Application (UI & Controllers)
│   ├── Pages/
│   ├── Controllers/
│   └── wwwroot/
├── StockIT.BLL/          # Business Logic Layer
│   ├── Services/
│   └── DbContext/
└── StockIT.Test/         # Unit & Integration Tests
```

### Database Schema

  * **Products:** `ID`, `Name`, `Description`, `Quantity`, `Price`, `CategoryID`, `ImagePath`
  * **Categories:** `ID`, `Name`
  * **Relationships:** A one-to-many relationship exists between `Categories` and `Products`, with cascading deletes enabled for data integrity.

-----

## ⚡ Getting Started

### ✅ Prerequisites

  * [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
  * SQL Server (LocalDB or Express version is sufficient)
  * [Git](https://git-scm.com/downloads)
  * Visual Studio 2022 or VS Code

### 🧰 Clone & Run

**1. Clone the repository:**

```bash
git clone https://github.com/stere8/stockit.git
cd stockit
```

**2. Configure the database connection:**
Update the `DefaultConnection` string in `appsettings.json`.

**3. Restore dependencies, apply migrations, and run the app:**

```bash
dotnet restore
dotnet ef database update  # This applies the database schema
dotnet run --project StockIT
```

The application will start on `https://localhost:7000` or a similar port.

-----

## ☁️ Deployment Guide

The project is ready for deployment to **Azure Web App**.

**1. Publish the application:**

```bash
dotnet publish -c Release -o ./publish/StockIT
cd publish/StockIT
zip -r ../stockit.zip .
```

**2. Deploy using Azure CLI:**

```bash
az webapp deploy \
  --resource-group <your_resource_group> \
  --name <your_app_name> \
  --src-path ../stockit.zip \
  --type zip
```

**3. Configuration:**
Configure **App Settings** in the Azure portal, including `ASPNETCORE_ENVIRONMENT=Production` and your Azure SQL connection string.

-----

## 🧪 Testing Strategy

The project includes a comprehensive test suite to ensure reliability and code quality.

  * **Unit Tests:** Validate individual components and services in isolation.
  * **Integration Tests:** Verify interactions between different layers (e.g., controller to database).
  * **User Acceptance Testing (UAT):** Validated against real-world scenarios with feedback from over 10 small business owners.

To run all tests, execute the following command:

```bash
dotnet test
```

-----

## 📊 Comparative Analysis

| Feature | StockIT | Zoho Inventory | Square for Retail |
| :--- | :--- | :--- | :--- |
| **🎯 Target** | **Startups & Small Shops** | Small/Medium Business | Small Retailers |
| **💰 Pricing** | **Free** | Tiered (Freemium) | Tiered (Freemium) |
| **🧠 Inventory** | **✅ Core + Reporting** | ✅ Advanced | ✅ POS-focused |
| **🧩 Integrations** | 🔸 Planned | ✅ Zoho Suite | ✅ POS Ecosystem |
| **⭐ Ease of Use** | ⭐⭐⭐⭐ | ⭐⭐⭐ | ⭐⭐⭐ |

**Conclusion:** StockIT excels in **simplicity**, **ease of use**, and **cost-efficiency** for its target audience.

-----

## 📝 Future Roadmap

  * [ ] **Advanced Analytics:** Implement a dashboard with charts for sales trends and stock analysis.
  * [ ] **API & Integrations:** Develop a REST API for integrations with accounting software (e.g., QuickBooks).
  * [ ] **Barcode Scanning:** Add support for barcode scanning to speed up product management.
  * [ ] **CI/CD Pipeline:** Automate deployment with GitHub Actions and Azure.
  * [ ] **Multi-Location Support:** Allow users to manage inventory across multiple stores.

-----

## 🤝 Contributing

Contributions are welcome\! If you have suggestions or want to fix a bug, please follow these steps:

1.  Fork the repository.
2.  Create a new feature branch (`git checkout -b feature/AmazingFeature`).
3.  Commit your changes (`git commit -m 'Add some AmazingFeature'`).
4.  Push to the branch (`git push origin feature/AmazingFeature`).
5.  Open a **Pull Request**.

-----

## 👤 Author

  * **Developer:** Oreste Twizeyimana
  * **Location:** Bydgoszcz, Poland
  * **Github Portfolio:** [GitHub Profile](https://www.google.com/search?q=github-profile-link-here)
  * **Personal Portfolio** [Oracle Consults](oracleconsults.app.netlify)
  * **Email:** oreste.twizeyimana99@gmail.com

**⭐ If you find this project useful, please consider giving it a star\! ⭐**

-----

## 🙏 Acknowledgments

  * Thesis Supervisor: Mgr inż. Tomasz Ocetkiewicz
  * University of Economy in Bydgoszcz
  * The .NET and ASP.NET Core communities
  * All the small business owners who provided invaluable feedback during development.
