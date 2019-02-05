# honoplay
Honoplay Stack



# Klasör Yapýsý

## Common

## Core
This will contain all cross-cutting concerns.

### Domain
Entities
Value Objects
Enumerations
Logic
Exceptions

### Application
Interfaces
Models
Logic
Commands / Queries
Validators
Exceptions

## Infrastracture

### Persistence
DbContext
Migrations
Configurations
Seeding
Abstractions

### Infrastructure
Implementations, e.g.
API Clients
File System
Email / SMS
System Clock
Anything external

## Presentation
SPA – Angular or React
Web API
Razor Pages
MVC
Web Forms

# Kullanýlan 3rd party kütüphaneler

## MediatR
Uygulama içi mesajlaþma için kullanýlýyor. 3 arayüz önemli:

IRequest
IRequestHandler
INotification


https://github.com/jbogard/MediatR
https://github.com/jbogard/MediatR/wiki

## FluentValidation
Bir nesne için doðrulama kurallarý oluþturmayý saðlar.

https://fluentvalidation.net/start