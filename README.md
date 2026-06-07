# MyShop — Full Stack + Unit Tests

Full stack CRUD app: .NET 10 Web API (Clean Architecture) + React frontend + xUnit tests.

## Live frontend
https://YOUR_USERNAME.github.io/MyShop/
(The local API is not publicly hosted, so the live site shows an error when fetching data.)

## Tech
- Backend: .NET 10 Web API, EF Core, SQL Server, Repository pattern, DTOs, DI via interfaces
- Frontend: React + Vite (CRUD, routing, error handling)
- Tests: xUnit + NSubstitute (10 unit tests)
- CI: GitHub Actions builds and runs all tests on every push

## Architecture (Clean Architecture)
- **Domain** – entities (Category, Product, 1-to-many)
- **Application** – interfaces, services, DTOs
- **Infrastructure** – EF Core DbContext + repositories
- **API** – controllers + DI configuration

## Run the backend
\`\`\`bash
cd backend
dotnet ef database update --project MyShop.Infrastructure --startup-project MyShop.API
dotnet run --project MyShop.API
\`\`\`
API UI (Scalar): https://localhost:7001/scalar

## Run the frontend
\`\`\`bash
cd frontend
npm install
npm run dev
\`\`\`

## Run tests
\`\`\`bash
cd backend
dotnet test
\`\`\`
