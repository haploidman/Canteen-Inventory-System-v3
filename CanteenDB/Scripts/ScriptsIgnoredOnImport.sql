
-- ============================================
-- Canteen Inventory System - Database Setup Script
-- ============================================

IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'CanteenInventoryDB')
BEGIN
    CREATE DATABASE CanteenInventoryDB;
END
GO

USE CanteenInventoryDB;
GO

-- ============================================
-- TABLE 1: LOGIN (User Authentication)
-- ============================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Login')
BEGIN
    --The following statement was imported into the database project as a schema object and named dbo.Login.
--CREATE TABLE [dbo].[Login] (
--        [Id] INT IDENTITY(1,1) PRIMARY KEY CLUSTERED ([Id] ASC),
--        [username] NVARCHAR(50) NOT NULL UNIQUE, -- Unique username constraint
--        [password] NVARCHAR(50) NOT NULL,
--        [role] NVARCHAR(50) NOT NULL CHECK (role IN ('user', 'admin')),
--        CreatedDate DATETIME DEFAULT GETDATE()
--    );


END
GO

-- ============================================
-- TABLE 2: INVENTORY (Main Inventory Items)
-- ============================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Inventory')
BEGIN
    --The following statement was imported into the database project as a schema object and named dbo.Inventory.
--CREATE TABLE [dbo].[Inventory] (
--        [Id] INT IDENTITY(1,1) PRIMARY KEY CLUSTERED ([Id] ASC),
--        [Item] NVARCHAR(50) NOT NULL UNIQUE, -- Unique item name constraint
--        [Category] NVARCHAR(50) NOT NULL,
--        [Price] FLOAT(53) NOT NULL,
--        [Quantity] INT NOT NULL DEFAULT 0,
--        CreatedDate DATETIME DEFAULT GETDATE(),
--        LastUpdated DATETIME DEFAULT GETDATE()
--    );


END
GO

-- ============================================
-- TABLE 3: ARCHIVE (Recycle Bin / History)
-- ============================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Archive')
BEGIN
    -- Archive mirrors Inventory structure but replaces LastUpdated with DateArchived
    --The following statement was imported into the database project as a schema object and named dbo.Archive.
--CREATE TABLE [dbo].[Archive] (
--        [Id] INT IDENTITY(1,1) PRIMARY KEY CLUSTERED ([Id] ASC),
--        [Item] NVARCHAR(50) NOT NULL UNIQUE, -- Item name being archived
--        [Category] NVARCHAR(50) NOT NULL,
--        [Price] FLOAT(53) NOT NULL,
--        [Quantity] INT NOT NULL DEFAULT 0,
--        CreatedDate DATETIME DEFAULT GETDATE(),
--        DateArchived DATETIME DEFAULT GETDATE() -- Replaces LastUpdated from Inventory
--    );


END
GO

-- ============================================
-- SAMPLE DATA
-- ============================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Login')
BEGIN
    -- Insert cashier user from your image
    INSERT INTO [dbo].[Login] ([username], [password], [role]) 
    VALUES ('cashier', 'kaheraako', 'user');
    
    -- Insert manager user from your image
    INSERT INTO [dbo].[Login] ([username], [password], [role]) 
    VALUES ('manager', 'managerako', 'admin');
END
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Inventory')
BEGIN
    -- Insert sample inventory items
    INSERT INTO [dbo].[Inventory] ([Item], [Category], [Price], [Quantity]) 
    VALUES ('Rice', 'Food', 15, 50);
    
    INSERT INTO [dbo].[Inventory] ([Item], [Category], [Price], [Quantity]) 
    VALUES ('Vegetables', 'Food', 40, 30);
    
    INSERT INTO [dbo].[Inventory] ([Item], [Category], [Price], [Quantity]) 
    VALUES ('Water Bottles', 'Beverage', 20, 40);
END
GO
