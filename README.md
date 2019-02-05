# honoplay
Honoplay Stack



# Klas�r Yap�s�

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
SPA � Angular or React
Web API
Razor Pages
MVC
Web Forms

# Kullan�lan 3rd party k�t�phaneler

## MediatR
Uygulama i�i mesajla�ma i�in kullan�l�yor. 3 aray�z �nemli:

IRequest
IRequestHandler
INotification


https://github.com/jbogard/MediatR
https://github.com/jbogard/MediatR/wiki

## FluentValidation
Bir nesne i�in do�rulama kurallar� olu�turmay� sa�lar.

https://fluentvalidation.net/start