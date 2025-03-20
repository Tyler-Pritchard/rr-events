# rr-events

`rr-events` is a standalone microservice built with ASP.NET Core Web API as part of the RRAPP microservice architecture. This service manages upcoming and past event listings for the Rob Rich platform, with a future roadmap including admin-based content management and ticket sales integration via Stripe.

---

## 🚀 Features

- Full CRUD operations for event management
- Automatic nightly archiving of past events
- RESTful API design following enterprise best practices
- PostgreSQL relational database integration via Entity Framework Core
- Clean integration with existing rr-gateway reverse proxy
- Containerized via Docker and deployable via Docker Compose or Kubernetes
- Observability integration (Prometheus/Grafana ready)
- Future support for role-based access (admin, super-fan, etc.)

---

## 🏗 Tech Stack

- **.NET 8 / ASP.NET Core Web API**
- **Entity Framework Core**
- **PostgreSQL**
- **Docker & Docker Compose**
- **Kubernetes (optional deployment layer)**
- **Prometheus & Grafana (monitoring)**
- **rr-gateway (Go-based API Gateway)**

---

## 📂 Project Structure

```
rr-events/
│
├── Controllers/         # API controllers
├── Models/              # Event data models
├── Services/            # Business logic services
├── Data/                # DbContext and EF migrations
├── appsettings.json     # App config
├── Program.cs           # Entry point
├── rr-events.csproj     # .NET project file
```

---

## ⚙ Setup Instructions

### Prerequisites

- [.NET SDK 8+](https://dotnet.microsoft.com/)
- [Docker](https://www.docker.com/)
- PostgreSQL instance (or use included Docker service)
- [RRAPP repo](https://github.com/your-org/rrapp) cloned locally

### Local Development

```bash
# Navigate into rr-events service directory
cd rr-events

# Run the service
dotnet run

# Access sample health check
http://localhost:5243/api/events/health
```

### Run with Docker

```bash
docker build -t rr-events .
docker run -p 5000:5000 rr-events
```

### Run with Docker Compose

Add service to your `docker-compose.yml` and run:
```bash
docker-compose up --build rr-events
```

---

## 📈 Observability

- Health endpoint: `/api/events/health`
- Prometheus annotations: *(to be added when metrics are implemented)*
- Compatible with centralized Prometheus/Grafana stack

---

## 🛣 Roadmap

- [x] Initialize .NET service scaffold
- [x] Replace boilerplate with `/api/events/health`
- [ ] Add Event model and DB schema
- [ ] Implement CRUD endpoints
- [ ] Nightly archive job
- [ ] Role-based admin access (post rr-auth upgrade)
- [ ] Ticket integration via rr-payments service
- [ ] Admin UI (React)

---

## 🧪 Testing

- Unit testing with xUnit or MSTest (TBD)
- Integration testing for API routes
- Test coverage targets: 80% minimum (TBD)

---

## 🤝 Contribution

This project is part of a full-stack microservice portfolio application and not currently accepting external contributors. Feel free to fork and adapt it for your own architecture.

---

## 📬 Contact

Author: [Tyler Pritchard](https://www.github.com/tyler-pritchard)  
Project: [Rob Rich](https://www.robrich.band)
