
USE RestorauntDb
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
    (NEWID(), 'Beef Burger', 16.99, 3, 0, GETDATE());
