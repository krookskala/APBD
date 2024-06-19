CREATE TABLE Customer (
    Id INT IDENTITY (1,1) NOT NULL PRIMARY KEY,
    FirstName VARCHAR(100) NOT NULL,
    LastName VARCHAR(100) NOT NULL,
    City VARCHAR(100) NOT NULL,
    Country VARCHAR(100) NOT NULL,
    Phone VARCHAR(15) NOT NULL    
);

CREATE TABLE Supplier (
    Id INT IDENTITY (1,1) NOT NULL PRIMARY KEY,
    CompanyName VARCHAR(100) NOT NULL,
    ContactName VARCHAR(100) NOT NULL,
    City VARCHAR(100) NOT NULL,
    Country VARCHAR(100) NOT NULL,
    Phone VARCHAR(15) NOT NULL,
    Fax VARCHAR(20) NOT NULL
);

CREATE TABLE Product (
    Id INT IDENTITY (1,1) NOT NULL PRIMARY KEY,
    ProductName VARCHAR(100) NOT NULL,
    SupplierId INT,
    UnitPrice DECIMAL(10,2) NOT NULL,
    Package VARCHAR(100) NOT NULL,
    IsDiscontinued BIT,
    CONSTRAINT FK_Product_Supplier FOREIGN KEY (SupplierId)
                     REFERENCES Supplier(Id)
);

CREATE TABLE Order (
    Id INT IDENTITY (1,1) NOT NULL PRIMARY KEY,
    OrderDate DATETIME NOT NULL,
    CustomerId INT,
    TotalAmount DECIMAL (10,2) NOT NULL,
    CONSTRAINT FK_Order_Customer FOREIGN KEY (CustomerId)
                   REFERENCES Customer(Id)
);

CREATE TABLE OrderItem (
    Id INT IDENTITY (1,1) NOT NULL PRIMARY KEY,
    OrderId INT,
    ProductId INT,
    UnitPrice DECIMAL (10,2) NOT NULL,
    Quantity INT NOT NULL,
    CONSTRAINT FK_OrderItem_Order FOREIGN KEY (OrderId)
                       REFERENCES Order(Id),
    CONSTRAINT FK_OrderItem_Product FOREIGN KEY (ProductId)
                       REFERENCES Product(Id)
        
)