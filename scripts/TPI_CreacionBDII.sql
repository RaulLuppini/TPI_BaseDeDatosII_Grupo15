USE [master];
GO
DROP DATABASE IF EXISTS [TPI_BDII];
GO
CREATE DATABASE [TPI_BDII];
GO
USE [TPI_BDII];
GO

CREATE TABLE Usuario (
    IDUsuario INT IDENTITY(1,1) NOT NULL,
    DNI VARCHAR(20) NOT NULL,
    Nombre VARCHAR(100) NOT NULL,
    Apellido VARCHAR(100) NOT NULL,
    Correo VARCHAR(150) NOT NULL,
    Contrasenia VARCHAR(150) NOT NULL,
    Rol VARCHAR(50) NOT NULL,
    Telefono VARCHAR(20) NULL,
    Direccion VARCHAR(255) NULL,
    CodigoPostal VARCHAR(10) NULL,
    Estado BIT NOT NULL,
    CONSTRAINT PK_Usuario PRIMARY KEY (IDUsuario)
);


CREATE TABLE Marca (
    IDMarca INT IDENTITY(1,1) NOT NULL,
    Nombre VARCHAR(100) NOT NULL,
    CONSTRAINT PK_Marca PRIMARY KEY (IDMarca),
    CONSTRAINT UQ_Marca UNIQUE (Nombre)
);


CREATE TABLE Categoria (
    IDCategoria INT IDENTITY(1,1) NOT NULL,
    Nombre VARCHAR(100) NOT NULL,
    CONSTRAINT PK_Categoria PRIMARY KEY (IDCategoria)
);


CREATE TABLE EstadoPedido (
    IDEstado INT IDENTITY(1,1) NOT NULL,
    Nombre VARCHAR(50) NOT NULL,
    CONSTRAINT PK_EstadoPedido PRIMARY KEY (IDEstado)
);


CREATE TABLE MetodoPago (
    IDMetodoPago TINYINT IDENTITY(1,1) NOT NULL,
    Nombre VARCHAR(50) NOT NULL,
    CONSTRAINT PK_MetodoPago PRIMARY KEY (IDMetodoPago)
);


CREATE TABLE Producto (
    IDProducto INT IDENTITY(1,1) NOT NULL,
    Codigo NVARCHAR(50) NOT NULL,
    Nombre NVARCHAR(100) NOT NULL,
    IDMarca INT NOT NULL,
    IDCategoria INT NOT NULL,
    Descripcion NVARCHAR(255) NULL,
    PrecioCompra DECIMAL(10,2) NOT NULL,
    PorcentajeGanancia DECIMAL(5,2) NOT NULL,
    PrecioVenta DECIMAL(10,2) NOT NULL,
    StockActual INT NOT NULL,
    StockMinimo INT NOT NULL,
    Estado BIT NOT NULL,
    CONSTRAINT PK_Producto PRIMARY KEY (IDProducto),
    CONSTRAINT FK_Producto_Marca FOREIGN KEY (IDMarca) REFERENCES Marca(IDMarca),
    CONSTRAINT FK_Producto_Categoria FOREIGN KEY (IDCategoria) REFERENCES Categoria(IDCategoria)
);


CREATE TABLE Pedido (
    IDPedido INT IDENTITY(1,1) NOT NULL,
    IDUsuario INT NOT NULL,
    Precio DECIMAL(10,2) NOT NULL,
    IDEstado INT NOT NULL,
    IDMetodoPago TINYINT NOT NULL,
    FechaPedido DATETIME NOT NULL,
    CONSTRAINT PK_Pedido PRIMARY KEY (IDPedido),
    CONSTRAINT FK_Pedido_Usuario FOREIGN KEY (IDUsuario) REFERENCES Usuario(IDUsuario),
    CONSTRAINT FK_Pedido_Estado FOREIGN KEY (IDEstado) REFERENCES EstadoPedido(IDEstado),
    CONSTRAINT FK_Pedido_Metodo FOREIGN KEY (IDMetodoPago) REFERENCES MetodoPago(IDMetodoPago)
);


CREATE TABLE DetalleProducto (
    IDDetalle INT IDENTITY(1,1) NOT NULL,
    IDProducto INT NOT NULL,
    IDPedido INT NOT NULL,
    Cantidad INT NOT NULL,
    PrecioUnitario DECIMAL(10,2) NOT NULL,
    PrecioRebajado DECIMAL(10,2) NULL,
    CONSTRAINT PK_DetalleProducto PRIMARY KEY (IDDetalle),
    CONSTRAINT FK_DetalleProducto_Producto FOREIGN KEY (IDProducto) REFERENCES Producto(IDProducto),
    CONSTRAINT FK_DetalleProducto_Pedido FOREIGN KEY (IDPedido) REFERENCES Pedido(IDPedido)
);


CREATE TABLE Imagenes (
    IDImagen INT IDENTITY(1,1) NOT NULL,
    IDProducto INT NOT NULL,
    URL VARCHAR(255) NOT NULL,
    CONSTRAINT PK_Imagenes PRIMARY KEY (IDImagen),
    CONSTRAINT FK_Imagenes_Producto FOREIGN KEY (IDProducto) REFERENCES Producto(IDProducto)
);


CREATE TABLE AuditoriaPrecio (
    IDAuditoria INT IDENTITY(1,1) NOT NULL,
    IDProducto INT NOT NULL,
    PrecioAnterior DECIMAL(10,2) NOT NULL,
    PrecioNuevo DECIMAL(10,2) NOT NULL,
    FechaCambio DATETIME NOT NULL,
    CONSTRAINT PK_Auditoria PRIMARY KEY (IDAuditoria),
    CONSTRAINT FK_Auditoria_Producto FOREIGN KEY (IDProducto) REFERENCES Producto(IDProducto)
);
GO

CREATE TABLE AuditoriaImagenesBorradas (
    IDAuditoria INT IDENTITY(1,1) PRIMARY KEY,
    IDProducto INT NOT NULL,
    URLBorrada VARCHAR(255) NOT NULL,
    FechaBorrado DATETIME NOT NULL
);
GO