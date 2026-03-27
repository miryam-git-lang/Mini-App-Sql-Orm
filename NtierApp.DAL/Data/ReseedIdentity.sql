-- Reseed IDENTITY to consecutive numbers (1, 2, 3...)
-- Execute this after AddNumberColumn.sql to reset numbering

-- For MenuItems
DBCC CHECKIDENT ('MenuItems', RESEED, 0);

-- For Orders
DBCC CHECKIDENT ('Orders', RESEED, 0);

-- For OrderItems
DBCC CHECKIDENT ('OrderItems', RESEED, 0);
