CREATE TABLE Inventory (
        [Id] INT IDENTITY(1,1) PRIMARY KEY CLUSTERED ([Id] ASC),
        [Item] NVARCHAR(50) NOT NULL UNIQUE, -- Unique item name constraint
        [Category] NVARCHAR(50) NOT NULL,
        [Price] FLOAT(53) NOT NULL,
        [Quantity] INT NOT NULL DEFAULT 0,
        CreatedDate DATETIME DEFAULT GETDATE(),
        LastUpdated DATETIME DEFAULT GETDATE()
    );