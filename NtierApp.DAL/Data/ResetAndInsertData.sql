-- Drop existing data and reseed the tables with sequential numbers
-- This will reset the Number column to be sequential (1, 2, 3...)

-- Delete existing data
DELETE FROM OrderItems;
DELETE FROM Orders;
DELETE FROM MenuItems;

-- Reseed identity to start from 1
DBCC CHECKIDENT ('OrderItems', RESEED, 0);
DBCC CHECKIDENT ('Orders', RESEED, 0);
DBCC CHECKIDENT ('MenuItems', RESEED, 0);

-- Now re-insert the data with sequential numbers
INSERT INTO MenuItems (Id, Name, Price, Category, IsDeleted, CreatedAt)
VALUES 
    (NEWID(), 'Bruschetta', 8.99, 0, 0, GETDATE()),
    (NEWID(), 'Caprese Salad', 12.50, 2, 0, GETDATE()),
    (NEWID(), 'Tomato Soup', 9.99, 1, 0, GETDATE()),
    (NEWID(), 'Grilled Salmon', 28.00, 3, 0, GETDATE()),
    (NEWID(), 'Ribeye Steak', 35.50, 4, 0, GETDATE()),
    (NEWID(), 'Pasta Carbonara', 18.75, 3, 0, GETDATE()),
    (NEWID(), 'Chocolate Lava Cake', 9.99, 5, 0, GETDATE()),
    (NEWID(), 'Caesar Salad', 11.99, 2, 0, GETDATE()),
    (NEWID(), 'Chicken Wings', 14.50, 0, 0, GETDATE()),
    (NEWID(), 'Grilled Chicken Breast', 22.00, 4, 0, GETDATE()),
    (NEWID(), 'Fresh Orange Juice', 5.50, 6, 0, GETDATE()),
    (NEWID(), 'Tiramisu', 8.50, 5, 0, GETDATE()),
    (NEWID(), 'Beef Burger', 16.99, 3, 0, GETDATE())

-- Declare variables for Order IDs
DECLARE @Order1Id UNIQUEIDENTIFIER = NEWID()
DECLARE @Order2Id UNIQUEIDENTIFIER = NEWID()
DECLARE @Order3Id UNIQUEIDENTIFIER = NEWID()
DECLARE @Order4Id UNIQUEIDENTIFIER = NEWID()
DECLARE @Order5Id UNIQUEIDENTIFIER = NEWID()

-- Insert sample orders
INSERT INTO Orders (Id, TotalAmount, Date, IsDeleted, CreatedAt)
VALUES 
    (@Order1Id, 65.48, DATEADD(DAY, -5, GETDATE()), 0, DATEADD(DAY, -5, GETDATE())),
    (@Order2Id, 102.99, DATEADD(DAY, -3, GETDATE()), 0, DATEADD(DAY, -3, GETDATE())),
    (@Order3Id, 84.50, DATEADD(DAY, -2, GETDATE()), 0, DATEADD(DAY, -2, GETDATE())),
    (@Order4Id, 127.75, DATEADD(DAY, -1, GETDATE()), 0, DATEADD(DAY, -1, GETDATE())),
    (@Order5Id, 93.98, GETDATE(), 0, GETDATE())

-- Insert sample order items
INSERT INTO OrderItems (Id, MenuItemId, OrderId, Count, IsDeleted, CreatedAt)
VALUES 
    (NEWID(), (SELECT Id FROM MenuItems WHERE Name = 'Bruschetta'), @Order1Id, 2, 0, GETDATE()),
    (NEWID(), (SELECT Id FROM MenuItems WHERE Name = 'Caprese Salad'), @Order1Id, 1, 0, GETDATE()),
    (NEWID(), (SELECT Id FROM MenuItems WHERE Name = 'Fresh Orange Juice'), @Order1Id, 3, 0, GETDATE()),
    (NEWID(), (SELECT Id FROM MenuItems WHERE Name = 'Grilled Salmon'), @Order2Id, 2, 0, GETDATE()),
    (NEWID(), (SELECT Id FROM MenuItems WHERE Name = 'Caesar Salad'), @Order2Id, 2, 0, GETDATE()),
    (NEWID(), (SELECT Id FROM MenuItems WHERE Name = 'Chocolate Lava Cake'), @Order2Id, 2, 0, GETDATE()),
    (NEWID(), (SELECT Id FROM MenuItems WHERE Name = 'Ribeye Steak'), @Order3Id, 2, 0, GETDATE()),
    (NEWID(), (SELECT Id FROM MenuItems WHERE Name = 'Tomato Soup'), @Order3Id, 1, 0, GETDATE()),
    (NEWID(), (SELECT Id FROM MenuItems WHERE Name = 'Tiramisu'), @Order3Id, 1, 0, GETDATE()),
    (NEWID(), (SELECT Id FROM MenuItems WHERE Name = 'Pasta Carbonara'), @Order4Id, 3, 0, GETDATE()),
    (NEWID(), (SELECT Id FROM MenuItems WHERE Name = 'Chicken Wings'), @Order4Id, 2, 0, GETDATE()),
    (NEWID(), (SELECT Id FROM MenuItems WHERE Name = 'Fresh Orange Juice'), @Order4Id, 2, 0, GETDATE()),
    (NEWID(), (SELECT Id FROM MenuItems WHERE Name = 'Grilled Chicken Breast'), @Order5Id, 2, 0, GETDATE()),
    (NEWID(), (SELECT Id FROM MenuItems WHERE Name = 'Beef Burger'), @Order5Id, 1, 0, GETDATE()),
    (NEWID(), (SELECT Id FROM MenuItems WHERE Name = 'Chocolate Lava Cake'), @Order5Id, 2, 0, GETDATE())
