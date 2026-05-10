CREATE TABLE Login (
        [Id] INT IDENTITY(1,1) PRIMARY KEY CLUSTERED ([Id] ASC),
        [username] NVARCHAR(50) NOT NULL UNIQUE, -- Unique username constraint
        [password] NVARCHAR(50) NOT NULL,
        [role] NVARCHAR(50) NOT NULL CHECK (role IN ('user', 'admin')),
        CreatedDate DATETIME DEFAULT GETDATE()
    );