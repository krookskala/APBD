﻿CREATE TABLE Animal (
                        Id INT IDENTITY(1,1) PRIMARY KEY,
                        Name NVARCHAR(200) NOT NULL,
                        Description NVARCHAR(200),
                        Category NVARCHAR(200) NOT NULL,
                        Area NVARCHAR(200) NOT NULL
);
