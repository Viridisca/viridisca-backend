# Viridiska Backend 🌱 (Нужны правки в этом первичном варианте 😊)

**Репозиторий организации:** [DotNotFact/viridisca-backend](https://github.com/Viridisca/viridisca-backend)  
*"Где модули растут, а архитектура дышит"*

---

## 🏗️ Технологический стек
| **Категория**       | **Технологии**                                                                 |
|----------------------|--------------------------------------------------------------------------------|
| **Язык**            | C# 12 (.NET 8)                                                                |
| **Архитектура**     | Modular Monolith + Clean Architecture                                         |
| **ORM**             | Entity Framework Core 8 (Code-First + Fluent API)                             |
| **Документация**    | Swagger/OpenAPI 3.0 (с аутентификацией)                                       |
| **Шаблоны**         | CQRS, MediatR, Repository Pattern, Specification Pattern                      |
| **Валидация**       | FluentValidation + ProblemDetails (RFC 7807)                                  |
| **Модульность**     | Autofac (DI) + Plugin-based Module System                                     |

---

## 🌐 Web API Фичи
```json
{
  "Security": {
    "Ролевая модель": ["Student", "Teacher", "Dean", "Admin"],
    "Аутентификация": "JWT + Refresh Tokens",
    "Авторизация": "Policy-based (RBAC)"
  },
  "Модули": {
    "EducationCore": "Управление курсами/группами",
    "AnalyticsEngine": "Прогнозная аналитика (ML.NET)",
    "AttendanceTracker": "Геолокация + Face Recognition API",
    "Reporting": "Генерация PDF/Excel через Quartz.NET"
  },
  "Интеграции": [
    "Syncfusion Blazor",
    "Microsoft Graph API (Teams)",
    "Payment Gateway (Stripe)"
  ]
}
```

---

## 🧩 Структура проекта
```
src/
├── Core/                 # Domain Models, Interfaces
│   └── SharedKernel      # EventBus, Exceptions, Base Entities
├── Infrastructure/       # EF Core Migrations, Repositories
│   └── Plugins/          # Модули как отдельные сборки (DLL)
├── Application/          # CQRS Handlers, DTOs, Validators
└── WebAPI/               # Minimal APIs + Webhooks
```

---

## 🚀 Начало работы
1. **Клонирование**:
```bash
git clone https://github.com/Viridisca/viridisca-backend.git
```

2. **Настройка**:
```bash
cd viridisca-backend
dotnet restore
dotnet ef migrations apply --project src/Infrastructure
```

3. **Запуск**:
```bash
ASPNETCORE_ENVIRONMENT=Development dotnet run --project src/WebAPI
```
*Доступ к Swagger:* `https://localhost:7271/swagger`

---

## 📜 Лицензия  
`MIT License` — Свободное использование для образовательных проектов.  
*Коммерческое применение требует согласования.*  

---

**Философия кода:**  
*"Каждый модуль — это дерево в лесу архитектуры. Корни — в Core, ветви — в Infrastructure, а плоды — в API."* 🌳💻

---
