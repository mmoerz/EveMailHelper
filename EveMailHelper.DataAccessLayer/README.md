

# What commands do what?

## Powershell
``` dotnet ef migrations add <yourVersion>```
adds the migration called yourVersion to the pool of migrations with the current changes

``` dotnet ef migrations remove ```
removes the latest migration

```  dotnet ef database update ```
applies the changes to the database

...
some delete commands are also needed !!!

### Install dotnet-ef
Open any Powershell in the solution (for a project) and use:
```
dotnet tool install --global dotnet-ef
```

## Package Manager Console

Update-Database 

Add-Migration [migrationname]

Remove-Migration

For removing migrations that have already been applied to the database, use the Update-Database command with a prior migration name to revert to it.

# needed connection string
use user-secrets!!!
Place the secrets for the connection(s) into both the class library and the .net core web project.
(Otherwise suffer exceptions on migration/update/...)

```
"ConnectionStrings": {
    "Default": "Server=Test100;Integrated Security=True;Initial Catalog=Schulung20"
    "Default2": "Server=Test1000;User ID=sa;Password=changeme;;Initial Catalog=Schulung20"
}
```

# Scaffolding Original Command
My $.5 Tip: refrain from re-scaffolding.

Packagemanagementconsole:
```
Scaffold-DbContext -Provider Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Connection "Data Source=localhost;Initial Catalog=EveSde;User ID=sa;Password=replaceme;" -Context "EveSde" -ContextDir "Context"
```
Powershell:
```
dotnet ef dbcontext scaffold --output-dir Models --context-dir Context --context EveSde "Data Source=localhost;Initial Catalog=EveSde;User ID=sa;Password=tNCqxOh6IMKesh23LXvQ._;" Microsoft.EntityFrameworkCore.SqlServer
```

Add -Force to recreate and overwrite.
Be aware though, that several tables already have their own configuration class. (You will have to fix this manually)

# Install Packages
Packagemanagementconsole:
```
Install-Package Microsoft.EntityFrameworkCore.SqlServer
Install-Package Microsoft.EntityFrameworkCore.Tools
Install-Package Microsoft.Extensions.Configuration.UserSecrets
```

UserSecrets is necessary for safely storing the connection string parameters (and loading them while using the ef tools).

Necessary, if the powershell tools are not installed globally:
```
Install-Package Microsoft.EntityFrameworkCore.Tools 
```

# Knowledgebase
## driving Unittests - using in memory database

var options = new DbContextOptionsBuilder<WingtipToysDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString("N")).Options;
var dbContext = new WingtipToysDbContext(options);

## Troubleshooting
Errormessage like:
```
It was not possible to find any compatible framework version
The framework 'Microsoft.NETCore.App', version '2.0.0' (x64) was not found.
```
install Microsoft.EntityFrameworkCore.Design AND RECOMPILE the project!!!

Errormessage like:
```
 ---> System.ArgumentNullException: Value cannot be null. (Parameter 'connectionString')
   at Microsoft.EntityFrameworkCore.Utilities.Check.NotEmpty(String value, String parameterName)
   at Microsoft.EntityFrameworkCore.SqlServerDbContextOptionsExtensions.UseSqlServer(DbContextOptionsBuilder optionsBuilder, String connectionString, Action`1 sqlServerOptionsAction)
   at Microsoft.EntityFrameworkCore.SqlServerDbContextOptionsExtensions.UseSqlServer[TContext](DbContextOptionsBuilder`1 optionsBuilder, String connectionString, Action`1 sqlServerOptionsAction)
   --- End of inner exception stack trace ---
```
you are missing the configuration for ef tools - a class derived from IDesignTimeDbContextFactory 

## Links
[How to write unittest for ef core](https://bitiniyan.com/2019/02/02/how-to-write-unit-tests-with-entity-framework-core/)

For an example of dbcontextFactory for ASP.NET Core see
[ApplicationDbContext](https://docs.microsoft.com/en-us/ef/core/dbcontext-configuration/) in the Microsoft documentation.

[Package Manager Console EF Core Command Reference](https://docs.microsoft.com/en-us/ef/core/cli/powershell?msclkid=be8cf4dbcf7f11ec9a8f52d9400bf9b3)

* temporary tables (tables with an automated history) https://learn.microsoft.com/en-us/ef/core/providers/sql-server/temporal-tables https://devblogs.microsoft.com/dotnet/prime-your-flux-capacitor-sql-server-temporal-tables-in-ef-core-6-0/ https://www.c-sharpcorner.com/article/working-with-temporal-tables-in-ef-core-6-0/