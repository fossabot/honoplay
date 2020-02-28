# honoplay
Honoplay Stack



# Klasör Yapısı
[![FOSSA Status](https://app.fossa.io/api/projects/git%2Bgithub.com%2Fomegabigdata%2Fhonoplay.svg?type=shield)](https://app.fossa.io/projects/git%2Bgithub.com%2Fomegabigdata%2Fhonoplay?ref=badge_shield)


## Common

## Core
This will contain all cross-cutting concerns.

### Domain
Entities
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

## Moq
Redis cache kullanılmış metotları test ederken yapılan dependency injection işlemlerinde hata vermesi sebebiyle eklenmiştir.
Redis cache için kullanılan IDistributedCache interface'i mocklanmıştır.

https://github.com/moq/moq4
https://www.nuget.org/packages/moq/

## Redis Cache
Redis cache genellikle çok fazla kullanılan verilere daha hızlı ulaşabilmek için kullanılır. Bu yüzden de kullanılan verileri KEY-VALUE şeklinde RAM üzerine kaydeder.
İstenirse sabit disklere de kayıt yapılabilir.
Projede giriş yapan AdminUser rolündeki kişinin Departments, TrainerUsers, TraineeUser ile işlem yaparken daha hızlı yapabilmesi için kullanılmıştır.

https://www.nuget.org/packages/Microsoft.Extensions.Caching.Redis/2.2.0


## Newtonsoft.Json
Genellikle JSON serialize ve deserialize yapmak amacıyla kullanılır.
Projede ise Redis cache ve Testlerde post request verilerinin serialize ve deserialize etmek amacıyla kullanılmıştır.

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

# Database

Honoplay.Persistence klasöründe olduğunu doğruladıktan sonra
`dotnet ef migrations add InitialCreate`  
`dotnet ef database update`

## License
[![FOSSA Status](https://app.fossa.io/api/projects/git%2Bgithub.com%2Fomegabigdata%2Fhonoplay.svg?type=large)](https://app.fossa.io/projects/git%2Bgithub.com%2Fomegabigdata%2Fhonoplay?ref=badge_large)