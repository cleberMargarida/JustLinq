CREATE TABLE Employee
(
    Id INT PRIMARY KEY NOT NULL,
    Name NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NULL,
    Phone INT NULL,
    StartDate DATETIME NOT NULL
)

INSERT INTO Employee (Id, Name, Email, Phone, StartDate)
VALUES (1, 'John Doe', 'john.doe@example.com', 1234567890, '2022-01-01')

INSERT INTO Employee (Id, Name, Email, Phone, StartDate)
VALUES (2, 'Jane Doe', 'jane.doe@example.com', NULL, '2022-01-02')

INSERT INTO Employee (Id, Name, Email, Phone, StartDate)
VALUES (3, 'Bob Smith', 'bob.smith@example.com', NULL, '2022-01-03')