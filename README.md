# FitGym Tool

## Overview

FitGym Tool is a comprehensive gym management system designed to streamline operations for gym administrators and provide a seamless experience for gym members. The application facilitates easy onboarding, workout planning, diet management, admissions, fee submissions, attendance tracking, and various other gym-related activities.

## Architecture

The application follows a modern, layered architecture pattern with clear separation of concerns:

### Backend Architecture (.NET Core)

-   **API Layer** (`FitGymTool.API`) - RESTful Web API controllers and middleware
-   **Business Layer** (`FitGymTool.Business`) - Business logic and service implementations
-   **Data Access Layer** (`FitGymTool.DataAccess`) - Repository pattern and data access logic
-   **Shared Layer** (`FitGymTool.Shared`) - Common DTOs, models, constants, and utilities
-   **Database Layer** (`FitGymTool.Database`) - SQL Server database schema and stored procedures

### Frontend Architecture (Angular)

-   **Angular 20** - Modern TypeScript-based SPA framework
-   **PrimeNG** - Rich UI component library
-   **Bootstrap 5** - Responsive CSS framework
-   **Chart.js** - Data visualization and analytics
-   **Azure MSAL** - Microsoft Authentication Library for secure login

## Technology Stack

### Backend Technologies

-   **.NET Core** - Cross-platform web API framework
-   **C#** - Primary programming language
-   **Entity Framework Core** - ORM for database operations
-   **SQL Server** - Relational database management system
-   **Azure App Configuration** - Cloud-based configuration management
-   **Azure Identity** - Authentication and authorization
-   **Swagger/OpenAPI** - API documentation and testing

### Frontend Technologies

-   **Angular 20** - Latest version of the Angular framework
-   **TypeScript** - Strongly-typed JavaScript superset
-   **PrimeNG** - Enterprise-class UI components
-   **Bootstrap 5** - Mobile-first responsive design
-   **Chart.js** - Interactive charts and graphs
-   **SCSS** - Enhanced CSS with variables and mixins

### Development Tools

-   **Visual Studio** - Primary IDE for backend development
-   **Angular CLI** - Command-line interface for Angular development
-   **npm** - Package manager for frontend dependencies

## Core Features

### 1. Member Management

-   **Member Registration**: Complete onboarding process for new gym members
-   **Member Profiles**: Comprehensive member information including personal details, contact information, and membership status
-   **Member Search**: Find members by email ID or other criteria
-   **Member Updates**: Modify existing member information
-   **Member Deletion**: Remove inactive or cancelled memberships

### 2. Fees Management

-   **Fee Structure**: Configurable pricing plans based on membership duration
-   **Payment Tracking**: Complete payment history and status monitoring
-   **Revenue Analytics**: Current month fees and revenue status reporting
-   **Payment Status**: Track paid, pending, and overdue payments
-   **Duration-based Pricing**: Flexible fee structures for different membership periods

### 3. Dashboard & Analytics

-   **Revenue Insights**: Real-time financial performance metrics
-   **Member Statistics**: Active member counts and membership trends
-   **Payment Analytics**: Fee collection status and outstanding amounts
-   **Visual Reports**: Chart-based data visualization for better insights

### 4. Authentication & Security

-   **Azure AD Integration**: Secure authentication using Microsoft identity platform
-   **Role-based Access**: Different access levels for admins and regular users
-   **JWT Token Management**: Secure API communication
-   **Authorization Middleware**: Protected endpoints with proper access control

## Database Schema

### Core Tables

-   **MemberDetails**: Complete member information with personal and membership data
-   **FeesStructure**: Configurable fee amounts based on membership duration
-   **FeesPaymentHistory**: Comprehensive payment tracking with status and dates
-   **MembershipStatusMapping**: Member status categories (Active, Inactive, Suspended)
-   **FeesDurationMapping**: Membership duration options (Monthly, Quarterly, Annual)
-   **FeesPaymentStatusMapping**: Payment status tracking (Paid, Pending, Overdue)

### Key Features of Database Design

-   **GUID-based Security**: Unique identifiers for secure member references
-   **Audit Trail**: Created/Modified timestamps and user tracking
-   **Soft Deletes**: IsActive flags for data retention
-   **Referential Integrity**: Foreign key constraints maintaining data consistency
-   **Optimized Indexing**: Performance-tuned indexes for common queries

## API Endpoints

### Members Controller

-   `POST /api/members/add/{isFromAdmin}` - Add new member
-   `GET /api/members/all` - Retrieve all members
-   `POST /api/members/by-email` - Get member by email ID
-   `PUT /api/members/update` - Update member details
-   `DELETE /api/members/{memberId}` - Delete member

### Member Fees Controller

-   `GET /api/member-fees/current-month-status` - Get current month fees and revenue status

### Common Controller

-   `GET /api/common/mapping-data` - Retrieve master data for dropdowns and mappings

## Security Features

### Authentication

-   **Azure Active Directory** integration for enterprise-grade security
-   **Multi-factor Authentication** support through Azure AD
-   **Single Sign-On (SSO)** capabilities for seamless user experience

### Authorization

-   **Claims-based Authorization** with user roles and permissions
-   **API Endpoint Protection** with JWT token validation
-   **User Context Tracking** for audit and logging purposes

### Data Protection

-   **SQL Injection Prevention** through parameterized queries
-   **Input Validation** at multiple layers (client, API, business logic)
-   **Secure Configuration** management through Azure App Configuration

## User Interface Features

### Responsive Design

-   **Mobile-first Approach** ensuring compatibility across all devices
-   **Bootstrap Grid System** for consistent layout structure
-   **PrimeNG Components** providing rich, accessible UI elements

### User Experience

-   **Intuitive Navigation** with clear menu structure
-   **Real-time Feedback** for user actions and form submissions
-   **Error Handling** with user-friendly error messages
-   **Loading States** and progress indicators for better UX

### Data Visualization

-   **Interactive Charts** using Chart.js for analytics
-   **Dashboard Widgets** showing key performance indicators
-   **Responsive Tables** with sorting, filtering, and pagination

## Development Practices

### Code Quality

-   **Clean Architecture** with clear separation of concerns
-   **SOLID Principles** implementation throughout the codebase
-   **Dependency Injection** for loose coupling and testability
-   **Comprehensive Logging** for debugging and monitoring

### Error Handling

-   **Global Exception Handler** for consistent error responses
-   **Custom Exception Types** for specific business scenarios
-   **Structured Logging** with correlation IDs for request tracking

### Performance Optimization

-   **Async/Await Patterns** for non-blocking operations
-   **Database Indexing** for optimized query performance
-   **Caching Strategies** for frequently accessed data
-   **Lazy Loading** for improved application startup time

## Deployment & Infrastructure

### Cloud-Ready Architecture

-   **Azure App Service** compatible deployment model
-   **Azure SQL Database** for scalable data storage
-   **Azure App Configuration** for centralized configuration management
-   **Azure Application Insights** for monitoring and diagnostics

### Environment Management

-   **Development/Production** configuration separation
-   **Environment-specific Settings** through configuration files
-   **Secure Connection Strings** and API keys management

## Future Enhancements

The application architecture supports easy extension for additional features such as:

-   **Workout Plan Management** - Custom exercise routines and tracking
-   **Diet Chart Management** - Nutritional planning and monitoring
-   **Attendance Tracking** - Check-in/check-out functionality
-   **Equipment Management** - Gym equipment inventory and maintenance
-   **Trainer Management** - Staff scheduling and client assignments
-   **Notification System** - Automated reminders and alerts
-   **Mobile Application** - Native iOS/Android apps
-   **Integration APIs** - Third-party fitness app integrations

## Conclusion

FitGym Tool represents a modern, scalable solution for gym management that combines robust backend architecture with an intuitive user interface. The application leverages industry-standard technologies and best practices to deliver a reliable, secure, and user-friendly experience for both gym administrators and members.

The modular architecture ensures easy maintenance and future enhancements, while the comprehensive feature set addresses the core needs of gym management operations. With its cloud-ready design and enterprise-grade security, FitGym Tool is positioned to scale with growing business requirements.
