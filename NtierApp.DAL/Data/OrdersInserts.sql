USE RestorauntDb

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
