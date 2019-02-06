# honoplay
Honoplay Stack



# Klasör Yapısı

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

# Kullanılan 3rd party kütüphaneler

## MediatR
Uygulama içi mesajlaşma için kullanılıyor. 3 arayüz önemli:

IRequest
IRequestHandler
INotification


https://github.com/jbogard/MediatR
https://github.com/jbogard/MediatR/wiki

## FluentValidation
Bir nesne için doğrulama kuralları oluşturmayı sağlar.

https://fluentvalidation.net/start