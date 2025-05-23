
# 💰 Financial Account Management API

A basic financial account management system built with ASP.NET Core Web API and Entity Framework Core. Includes full CRUD operations for Accounts and Transactions, plus LINQ-based financial reports.

## 🛠 Technologies
- ASP.NET Core Web API
- Entity Framework Core (Code-First)
- SQL Server
- Swagger UI (for API testing)

## 🚀 Getting Started

### 📦 Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/Raeevan/FinancialAccountManagement.Api.git
   cd FinancialAccountManagement.Api
   ```

2. Update your `appsettings.json`:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=localhost;Database=FinancialDb;Trusted_Connection=True;TrustServerCertificate=True"
   }
   ```

3. Open Package Manager Console and run:
   ```powershell
   Drop-Database         # optional: clean start
   Update-Database       # apply migrations
   ```

4. Run the application:
   ```bash
   dotnet run
   ```

5. Open Swagger in your browser:
   ```
   https://localhost:7049/swagger/index.html
   ```

## 🔌 API Usage Guide

### 🧑 Account Endpoints
- `GET /api/accounts` – List all accounts
- `GET /api/accounts/{id}` – Get account by ID
- `POST /api/accounts` – Add a new account
- `PUT /api/accounts/{id}` – Update existing account
- `DELETE /api/accounts/{id}` – Delete account

Example JSON:
```json
{
  "accountNumber": "ACC-999",
  "accountHolder": "John Doe",
  "balance": 1000
}
```

### 💸 Transaction Endpoints
- `GET /api/transactions` – List all transactions
- `GET /api/transactions/{id}` – Get transaction by ID
- `POST /api/transactions` – Create a deposit or withdrawal
- `PUT /api/transactions/{id}` – Update a transaction
- `DELETE /api/transactions/{id}` – Delete a transaction

Example JSON for POST:
```json
{
  "accountId": 1,
  "transactionType": "Deposit",
  "amount": 200
}
```

### 📊 Reports Endpoints
- `GET /api/reports/transactions/by-account/{accountId}`
- `GET /api/reports/accounts/total-balance`
- `GET /api/reports/accounts/below-balance/{threshold}`
- `GET /api/reports/accounts/top`

## 📌 Notes
- Database seeding inserts 20 predefined accounts and transactions.
- To reseed, drop the database and restart the app.
- All business logic (like balance updates on deposit/withdrawal) is handled automatically.
- Swagger is available only in the Development environment.
