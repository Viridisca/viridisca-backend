# Viridisca Identity Module Documentation

## Overview

The Identity module handles all authentication and authorization aspects of the Viridisca LMS (Learning Management System). It provides a comprehensive role-based authentication system supporting the various educational roles in the platform.

## Architecture

The Identity module follows the Clean Architecture principles, with separation into multiple layers:

- **Domain**: Core business logic, entities, and business rules
- **Application**: Application services, commands, and queries
- **Infrastructure**: Technical concerns, database access, and external services
- **Presentation**: API endpoints, controllers, and DTOs
- **API**: Module registration and configuration

## Authentication Flow

1. User submits credentials (username/email and password)
2. System validates credentials and generates JWT token
3. JWT token includes user claims (ID, roles, permissions)
4. Refresh tokens are issued for token renewal
5. Authentication middleware validates tokens on protected requests

## Role System

The Identity module supports the following roles:

| Role | Description | Primary Responsibilities |
|------|-------------|--------------------------|
| SystemAdmin | System administrator with full access | System configuration, user management, security |
| SchoolDirector | Director of educational institution | Strategic decisions, approvals, reporting |
| AcademicAffairsHead | Head of academic department | Curriculum oversight, teacher coordination |
| Teacher | Faculty member giving lessons | Conduct lessons, grading, feedback |
| GroupCurator | Curator responsible for student groups | Student groups management, mentoring |
| Student | Learner enrolled in courses | Access learning materials, submit assignments |
| Parent | Parent or guardian of a student | Monitor student progress, communicate with teachers |
| EducationMethodist | Curriculum methodology specialist | Develop and improve teaching methods |
| FinancialManager | Financial operations manager | Payment processing, financial reporting |
| QualityAssuranceManager | Education quality specialist | Monitor teaching quality, collect feedback |
| ContentManager | Learning content specialist | Develop and update course materials |
| DataAnalyst | Analytics specialist | Data analysis, reporting, insights |
| Librarian | Library resources manager | Digital and physical resource management |
| Psychologist | Student psychological support | Counseling, psychological assessments |
| HealthcareSpecialist | Health services specialist | Medical records, health monitoring |
| TechnicalSupport | IT support specialist | Technical assistance, system maintenance |
| Guest | Unauthenticated or limited access user | Preview content, registration |

## Authorization Policies

The system implements the following authorization policies:

- **RequireAdminRole**: System administration tasks
- **SchoolManagement**: School administrative functions
- **TeachingStaff**: Teaching and educational functions
- **StudentManagement**: Student-related operations
- **ContentManagement**: Course content operations
- **FinanceManagement**: Financial operations
- **DataAnalysis**: Reporting and analytics
- **SupportStaff**: Support services

## API Endpoints

The Identity module exposes the following endpoints:

- `/api/identity/register` - Register a new user
- `/api/identity/login` - Authenticate and receive tokens
- `/api/identity/refresh-token` - Refresh the JWT token
- `/api/identity/logout` - Logout and invalidate tokens
- `/api/identity/confirm-email` - Confirm user email
- `/api/identity/reset-password` - Reset user password
- `/api/identity/change-password` - Change user password
- `/api/identity/profile` - Get or update user profile
- `/api/identity/roles` - Manage user roles

## Configuration

The Identity module uses the following configuration:

```json
"JwtSettings": {
  "Secret": "your-secret-key",
  "Issuer": "Viridisca.API",
  "Audience": "Viridisca.Clients",
  "ExpiryMinutes": 60,
  "RefreshTokenExpiryDays": 7
}
```

## Integration with Other Modules

The Identity module integrates with:

- **Academic Module**: Controls course access permissions
- **Grading Module**: Determines who can input or view grades
- **Communication Module**: Defines communication permissions
- **Finance Module**: Authentication for payment operations
- **Analytics Module**: Access to reporting features

## Security Considerations

- Passwords are hashed using BCrypt
- JWT tokens are signed with HMAC SHA-256
- Refresh tokens use secure rotation mechanism
- Failed login attempts are tracked
- Authentication events are logged
- Email confirmation required for sensitive operations 