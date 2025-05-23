
# 💰 Financial Account Management API

A basic Financial Account Management API built with ASP.NET Core Web API and Entity Framework Core. Includes full CRUD operations for Accounts and Transactions, along with LINQ-based financial reports.

## 🛠 Technologies
- ASP.NET Core
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

2. Update `appsettings.json` with your local DB connection:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=localhost;Database=FinancialDb;Trusted_Connection=True;TrustServerCertificate=True"
   }
   ```

3. Open Package Manager Console and run:
   ```powershell
   Drop-Database         # optional, for a fresh start
   Update-Database       # applies migrations
   ```

4. Run the application:
   ```powershell
   dotnet run
   ```

5. Open your browser to:
   ```
   https://localhost:7049/swagger/index.html
   ```
   to access the Swagger UI and test endpoints.

## 🔌 API Usage Guide

### 🧑 Account Endpoints
- `GET /api/accounts` – List all accounts
- `GET /api/accounts/{id}` – Get account by ID
- `POST /api/accounts` – Add a new account
- `PUT /api/accounts/{id}` – Update existing account
- `DELETE /api/accounts/{id}` – Delete account

Example JSON for creating an account:
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
- `POST /api/transactions` – Add a new transaction (Deposit or Withdrawal)
- `PUT /api/transactions/{id}` – Update existing transaction
- `DELETE /api/transactions/{id}` – Delete transaction

Example JSON for creating a transaction:
```json
{
  "accountId": 1,
  "transactionType": "Deposit",
  "amount": 200
}
```

### 📊 Reports Endpoints
- `GET /api/reports/transactions/by-account/{accountId}` – All transactions for an account
- `GET /api/reports/accounts/total-balance` – Total balance of all accounts
- `GET /api/reports/accounts/below-balance/{threshold}` – Accounts below threshold
- `GET /api/reports/accounts/top` – Top 5 accounts by balance

## 📌 Notes
- Database seeding will insert 20 manual records for both accounts and transactions if tables are empty.
- To reseed, drop the DB and run the app again.
- Business logic automatically updates balances based on deposits/withdrawals.
- For testing in Swagger, use seeded account IDs (1–20).
