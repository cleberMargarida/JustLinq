# JustLinq
Is a Micro-ORM that allows you to use LINQ expressions to perform queries on your database, using LINQ and fluent API's calls. Unlike other ORMs, this Micro-ORM does not require you to use POCO classes, Mark properties with DataModel attributes, Dataset's abstractions, extensively configurations or complex entity models.

#### Purpose
The main purpose it's to provide a tool that simplifies database access using LINQ for developers. One of the main advantages of this Micro-ORM is its simplicity in reaching databases, saving developers time and effort, By eliminating the need for complex POCO models and entities. Take your existents models, access it and work with databases easily. _Hardware is Cheap, Programmers are Expensive_.

#### How to use it?
1. Install the JustLinq package using NuGet.
2. Create an instance of the IDatabase using some provider and a string that represents the connection string to your data source.
4. Use LINQ expressions to perform queries on your data.

Usage:
```csharp
var database = new Database(opt => opt.UseSqlServer("ConnectionString"));

var query = database.Query<Employee>();

var result = query.Where(x => x.Name == "Bob Smith" &&
                              x.Email == "bob.smith@example.com" &&
                              x.Id > 0)
                  .Select(x => x.Id);
```
Output query:
```sql
SELECT [Employee].[Id]
FROM [Employee] AS [Employee]
WHERE [Employee].[Name] = 'Bob Smith'
    AND [Employee].[Email] = 'bob.smith@example.com'
    AND [Employee].[Id] > 0
```

#### Contributing:

If you would like to contribute to this project, please feel free to submit a pull request. Contributions are always welcome!

#### License:

This Micro-ORM is licensed under the MIT License. You are free to use, modify, and distribute it as you see fit.