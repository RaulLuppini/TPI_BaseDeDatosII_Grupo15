CREATE DATABASE Ecommerce_DB;
GO

USE Ecommerce_DB;
GO



CREATE PROCEDURE agregar_Pedido
    @idUsuario INT,
    @precioTotal DECIMAL(10,2),
    @estado VARCHAR(30),
    @metodoDePago VARCHAR(40)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Pedido (idUsuario, precioTotal, estado, metodoDePago)
    VALUES (@idUsuario, @precioTotal, @estado, @metodoDePago);

    SELECT SCOPE_IDENTITY() AS id_generado;
END
GO


CREATE TABLE Usuario (
    Id INT PRIMARY KEY IDENTITY(1,1),         
    DNI VARCHAR(20) NOT NULL,                 
    Nombre VARCHAR(50) NOT NULL,              
    Apellido VARCHAR(50) NOT NULL,           
    Correo VARCHAR(100) NOT NULL,             
    Contraseña VARCHAR(100) NOT NULL,         
    Rol VARCHAR(20) NOT NULL,                 
    Telefono VARCHAR(20),                     
    Direccion VARCHAR(100),                   
    CodigoPostal VARCHAR(10)                  
);

CREATE TABLE Marca (
    Id INT PRIMARY KEY IDENTITY(1,1),    
    Nombre VARCHAR(50) NOT NULL           
);

Create Table Categoria (
    id int primary key IDENTITY(1,1),
    nombreCategoria varchar(50) not null

);


CREATE TABLE Producto (
    Id INT PRIMARY KEY IDENTITY(1,1),         
    Codigo VARCHAR(20) NOT NULL,                 
    Nombre VARCHAR(50) NOT NULL,
    MarcaId INT NOT NULL,   
    CategoriaId INT NOT NULL,
    Descripcion VARCHAR(250) NOT NULL,           
    PrecioCompra DECIMAL(10,2) NOT NULL,             
    PorcentajeGanancia DECIMAL(10,2) NOT NULL,         
    PrecioVenta DECIMAL(10,2) NOT NULL,                 
    StockActual INT NOT NULL,                     
    StockMinimo INT NOT NULL,
    Estado BIT DEFAULT 0
    FOREIGN KEY (MarcaId) REFERENCES Marca(Id) ,
    fOREIGN KEY (CategoriaId) References Categoria(id)
        
);


Create table imagenes(
id int identity(1,1) not null,
idProducto int not null,
url varchar(800) not null,
primary key(id),
foreign key (idProducto) references Producto(id)
);

Create table Pedido(
id int identity(1,1) not null,
idUsuario int not null,
precioTotal decimal (10,2) not null,
estado varchar(30) not null,
metodoDepago varchar(40)
primary key (id),
foreign key(idUsuario) references Usuario(id)
);

Create table DetalleProducto(
id INT IDENTITY(1,1) NOT NULL,
idProducto INT NOT NULL,
idPedido INT Not null,
cantidadProducto int not null check (cantidadProducto>0),
PrecioUnitario DECIMAL(10,2) NOT NULL,  
PrecioRebajado DECIMAL(10,2) NOT NULL
primary key(id),
foreign key(idPedido) references pedido(id),
foreign key(idProducto) references Producto(Id)
);

