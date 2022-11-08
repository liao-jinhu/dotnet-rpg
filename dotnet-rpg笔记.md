# 笔记



创建项目1. dotnet new webapi
运行2.dotnet watch run

dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection --version 12.0.0

dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet tool install --global dotnet-ef
dotnet add package Microsoft.EntityFrameworkCore.Design

Encrypt=true;TrustServerCertificate=true
dotnet ef -h
dotnet ef migrations add InitialCreate

1.dotnet ef migrations add InitialCreate
2.dotnet ef database update

dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
dotnet add package Swashbuckle.AspNetCore.Filters 