CREATE DATABASE [OfficeSharedShoppingDb]

GO
USE [OfficeSharedShoppingDb]
GO

CREATE TABLE [Employees]
(
[EmployeeId] INT PRIMARY KEY IDENTITY,
[Name] VARCHAR(100) NOT NULL,
[Email] VARCHAR(50) NOT NULL UNIQUE,
[Password] VARCHAR(256) NOT NULL,
[Department] VARCHAR(100) NOT NULL,
[Phone] VARCHAR(20) NOT NULL
)

CREATE TABLE [Stores]
(
[StoreId] INT PRIMARY KEY IDENTITY,
[Name] VARCHAR(100) NOT NULL
)

CREATE TABLE [ProductCategories]
(
[ProductCategoryId] INT PRIMARY KEY IDENTITY,
[Name] VARCHAR(100) NOT NULL
)

CREATE TABLE [Products]
(
[ProductId] INT PRIMARY KEY IDENTITY,
[Name] VARCHAR(100) NOT NULL,
[CategoryId] INT FOREIGN KEY REFERENCES [ProductCategories]([ProductCategoryId]) NOT NULL
)

CREATE TABLE [ShoppingSessions]
(
[ShoppingSessionId] INT PRIMARY KEY IDENTITY,
[StoreId] INT FOREIGN KEY REFERENCES [Stores]([StoreId]) NOT NULL,
[CreatedByEmployeeId] INT FOREIGN KEY REFERENCES [Employees]([EmployeeId]) NOT NULL,
[Deadline] DATETIME NOT NULL,
[IsActive] BIT NOT NULL,
[CreatedAt] DATETIME NOT NULL
)

CREATE TABLE [SessionRequests]
(
[SessionRequestId] INT PRIMARY KEY IDENTITY,
[SessionId] INT FOREIGN KEY REFERENCES [ShoppingSessions]([ShoppingSessionId]) NOT NULL,
[EmployeeId] INT FOREIGN KEY REFERENCES [Employees]([EmployeeId]) NOT NULL,
[ProductId] INT FOREIGN KEY REFERENCES [Products]([ProductId]) NOT NULL,
[Quantity] INT NOT NULL,
[MaxPrice] DECIMAL(18,2) NOT NULL,
[CreatedAt] DATETIME NOT NULL
)

INSERT INTO [Employees] ([Name], [Email], [Password], [Department], [Phone]) VALUES
('Ivan Petrov', 'ivan.petrov@example.bg', 'A109E36947AD56DE1DCA1CC49F0EF8AC9AD9A7B1AA0DF41FB3C4CB73C1FF01EA', 'Sales', '0888123456'),
('Maria Georgieva', 'maria.georgieva@example.bg', 'A109E36947AD56DE1DCA1CC49F0EF8AC9AD9A7B1AA0DF41FB3C4CB73C1FF01EA', 'Marketing', '0888234567'),
('Georgi Ivanov', 'georgi.ivanov@example.bg', 'A109E36947AD56DE1DCA1CC49F0EF8AC9AD9A7B1AA0DF41FB3C4CB73C1FF01EA', 'IT', '0888345678'),
('Elena Dimitrova', 'elena.dimitrova@example.bg', 'A109E36947AD56DE1DCA1CC49F0EF8AC9AD9A7B1AA0DF41FB3C4CB73C1FF01EA', 'HR', '0888456789'),
('Kiril Stoyanov', 'kiril.stoyanov@example.bg', 'A109E36947AD56DE1DCA1CC49F0EF8AC9AD9A7B1AA0DF41FB3C4CB73C1FF01EA', 'Administration', '0888567890'),
('Alice Johnson', 'alice.johnson@example.com', 'A109E36947AD56DE1DCA1CC49F0EF8AC9AD9A7B1AA0DF41FB3C4CB73C1FF01EA', 'Sales', '5551234567'),
('Bob Smith', 'bob.smith@example.com', 'A109E36947AD56DE1DCA1CC49F0EF8AC9AD9A7B1AA0DF41FB3C4CB73C1FF01EA', 'Marketing', '5552345678'),
('Charlie Davis', 'charlie.davis@example.com', 'A109E36947AD56DE1DCA1CC49F0EF8AC9AD9A7B1AA0DF41FB3C4CB73C1FF01EA', 'IT', '5553456789'),
('Diana Moore', 'diana.moore@example.com', 'A109E36947AD56DE1DCA1CC49F0EF8AC9AD9A7B1AA0DF41FB3C4CB73C1FF01EA', 'HR', '5554567890')


INSERT INTO [Stores] ([Name]) VALUES
('Sofia Tsentar'),
('Plovdiv Mall'),
('Varna Granicata'),
('Burgas Seaside'),
('Ruse Centralen Magazin'),
('Billa'),
('Kaufland'),
('345'),
('Fantastico')

INSERT INTO [ProductCategories] ([Name]) VALUES
('Electronics'),
('Furniture'),
('Office Supplies'),
('Beverages'),
('Food')

INSERT INTO [Products] ([Name], [CategoryId]) VALUES
('Laptop', 1),
('Smartphone', 1),
('Office Chair', 2),
('Desk', 2),
('Printer Paper', 3),
('Stapler', 3),
('Cola', 4),
('Fanta', 4),
('Milk', 4),
('Water', 4),
('Banitsa', 5),
('Waffle', 5),
('Mashed potatoes', 5)



