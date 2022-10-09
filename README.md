# dotnetVeriTabaniGiris-mysql-sqlserver-
.csproj da iplgili değişiklikler yapılır.
ilgili sql database ine ait packeage lar
 -SQL Server için 'dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 6.0.9'
 -MySQL için ''
 -SQLite için 'dotnet add package Microsoft.EntityFrameworkCore.Sqlite --version 6.0.9'
 
  'dotnet tool install  --global dotnet-ef'
  'dotnet add package Microsoft.EntityFrameworkCore.Design'

Conole da SQL sorgularını dönüştürülmüş halini görmek için
  'dotnet add package Microsoft.Extensions.Logging.Console --version 6.0.0'
  
önce istenilen tablolara dair class lar oluşturulur.
Uygulamaya ait context class ı oluşturulur.

Migration klasörünün oluşturulması gerekiyor.
'dotnet ef migrations add InitialCreate'
'dotnet ef database update'
 
Tüm program.cs de kullanılacak metotlar eklenir.




