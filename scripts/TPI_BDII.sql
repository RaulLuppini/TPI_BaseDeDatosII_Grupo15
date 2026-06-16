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

CREATE PROCEDURE sp_InsertarProducto
    @Codigo VARCHAR(50),
    @Nombre VARCHAR(100),
    @IDMarca INT,
    @IDCategoria INT,
    @Descripcion NVARCHAR(255),
    @PrecioCompra DECIMAL(10,2),
    @PorcentajeGanancia DECIMAL(5,2),
    @PrecioVenta DECIMAL(10,2),
    @StockActual INT,
    @StockMinimo INT,
    @Estado BIT
AS
BEGIN
    INSERT INTO Producto (Codigo, Nombre, IDMarca, IDCategoria, Descripcion, PrecioCompra, PorcentajeGanancia, PrecioVenta, StockActual, StockMinimo, Estado)
    VALUES (@Codigo, @Nombre, @IDMarca, @IDCategoria, @Descripcion, @PrecioCompra, @PorcentajeGanancia, @PrecioVenta, @StockActual, @StockMinimo, @Estado);
    SELECT SCOPE_IDENTITY() AS IDGenerado;
END;
GO



CREATE PROCEDURE sp_agregar_Pedido
    @IDUsuario INT,
    @Precio DECIMAL(10,2),
    @Estado VARCHAR(30),
    @MetodoDePago VARCHAR(40)
AS
BEGIN
    DECLARE @IDEstado INT, @IDMetodoPago INT;

    SELECT @IDEstado = IDEstado FROM EstadoPedido WHERE Nombre = @Estado;
    SELECT @IDMetodoPago = IDMetodoPago FROM MetodoPago WHERE Nombre = @MetodoDePago;

    INSERT INTO Pedido (IDUsuario, Precio, IDEstado, IDMetodoPago, FechaPedido)
    VALUES (@IDUsuario, @Precio, @IDEstado, @IDMetodoPago, GETDATE());

    SELECT SCOPE_IDENTITY() AS IDGenerado;
END;
GO


CREATE PROCEDURE sp_AgregarDetalleProducto
    @IDProducto INT,
    @IDPedido INT,
    @Cantidad INT,
    @PrecioUnitario DECIMAL(10,2),
    @PrecioRebajado DECIMAL(10,2) = 0,
    @Exito BIT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY
        BEGIN TRAN;

        INSERT INTO DetalleProducto (IDProducto, IDPedido, Cantidad, PrecioUnitario, PrecioRebajado)
        VALUES (@IDProducto, @IDPedido, @Cantidad, @PrecioUnitario, @PrecioRebajado);

        IF @@ROWCOUNT = 1
        BEGIN
            COMMIT TRAN;
            SET @Exito = 1;
        END
        ELSE
        BEGIN
            ROLLBACK TRAN;
            SET @Exito = 0;
        END
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK TRAN;
        DELETE FROM DetalleProducto WHERE IDPedido = @IDPedido;
        SET @Exito = 0;
    END CATCH
END;
GO


CREATE PROCEDURE sp_CambiarPassword
    @IDUsuario INT, 
    @CurrentPassword VARCHAR(150),
    @NewPassword VARCHAR(150)
AS
BEGIN
    BEGIN TRY
        IF EXISTS (SELECT 1 FROM Usuario WHERE IDUsuario = @IDUsuario AND Contrasenia = @CurrentPassword)
        BEGIN
            UPDATE Usuario SET Contrasenia = @NewPassword WHERE IDUsuario = @IDUsuario;
            SELECT 'OK' AS Resultado, 'Contraseña actualizada correctamente' AS Mensaje;
        END
        ELSE
        BEGIN
            SELECT 'ERROR' AS Resultado, 'La contraseña actual no coincide' AS Mensaje;
        END
    END TRY
    BEGIN CATCH
        SELECT 'ERROR' AS Resultado, ERROR_MESSAGE() AS Mensaje;
    END CATCH
END;
GO


CREATE PROCEDURE sp_DeshacerCompra
    @IDPedido INT,
    @Exito BIT
AS
BEGIN
    IF @Exito <> 1
    BEGIN
        UPDATE Pedido
        SET IDEstado = (SELECT IDEstado FROM EstadoPedido WHERE Nombre = 'Cancelado')
        WHERE IDPedido = @IDPedido;
    END
END;
GO


CREATE VIEW VW_ListarProductos AS
SELECT 
    p.IDProducto,
    p.Codigo,
    p.Nombre,
    m.IDMarca AS MarcaId,
    m.Nombre AS MarcaNombre,
    c.IDCategoria AS CategoriaId,
    c.Nombre AS CategoriaNombre,
    p.Descripcion,
    p.PrecioCompra,
    p.PorcentajeGanancia,
    p.PrecioVenta,
    p.StockActual,
    p.StockMinimo,
    p.Estado
FROM Producto p
INNER JOIN Marca m ON p.IDMarca = m.IDMarca
INNER JOIN Categoria c ON p.IDCategoria = c.IDCategoria;
GO


CREATE VIEW VW_PedidosConUsuarios AS
SELECT 
    p.IDPedido,
    p.IDUsuario,
    p.Precio AS PrecioTotal,
    p.FechaPedido,
    u.Nombre AS NombreUsuario,
    u.Apellido AS ApellidoUsuario,
    e.Nombre AS Estado,
    m.Nombre AS MetodoPago
FROM Pedido p
INNER JOIN Usuario u ON p.IDUsuario = u.IDUsuario
INNER JOIN EstadoPedido e ON p.IDEstado = e.IDEstado
INNER JOIN MetodoPago m ON p.IDMetodoPago = m.IDMetodoPago;
GO


CREATE VIEW VW_ListarPedidos AS
SELECT 
    p.IDPedido,
    p.IDUsuario,
    e.Nombre AS Estado,
    m.Nombre AS MetodoPago,
    p.FechaPedido,
    SUM(dp.Cantidad * dp.PrecioUnitario) AS PrecioTotal
FROM Pedido p
INNER JOIN EstadoPedido e ON p.IDEstado = e.IDEstado
INNER JOIN MetodoPago m ON p.IDMetodoPago = m.IDMetodoPago
INNER JOIN DetalleProducto dp ON p.IDPedido = dp.IDPedido
GROUP BY p.IDPedido, p.IDUsuario, e.Nombre, m.Nombre, p.FechaPedido;
GO


CREATE VIEW VW_ListarUsuario AS
SELECT 
    IDUsuario,
    DNI,
    Nombre,
    Apellido,
    Correo,
    Contrasenia,
    Rol,
    Telefono,
    Direccion,
    CodigoPostal,
    Estado
FROM Usuario;
GO

--Vista para chequear productos que requieren reposición
CREATE VIEW VW_ProductosAReponer AS
SELECT 
    p.IDProducto,
    p.Codigo,
    p.Nombre AS Producto,
    m.Nombre AS Marca,
    c.Nombre AS Categoria,
    p.StockActual,
    p.StockMinimo,
    (p.StockMinimo - p.StockActual) AS CantidadAReponer
FROM Producto p
INNER JOIN Marca m ON p.IDMarca = m.IDMarca
INNER JOIN Categoria c ON p.IDCategoria = c.IDCategoria
WHERE p.StockActual <= p.StockMinimo 
  AND p.Estado = 1;
GO

CREATE TRIGGER trg_AuditoriaPrecio
ON Producto
AFTER UPDATE
AS
BEGIN
    INSERT INTO AuditoriaPrecio (IDProducto, PrecioAnterior, PrecioNuevo, FechaCambio)
    SELECT d.IDProducto, d.PrecioVenta, i.PrecioVenta, GETDATE()
    FROM deleted d
    JOIN inserted i ON d.IDProducto = i.IDProducto
    WHERE d.PrecioVenta <> i.PrecioVenta;
END;
GO

--Trigger de eliminación para restaurar stock post venta cancelada o seleccion cancelada
CREATE TRIGGER trg_RestaurarStock
ON DetalleProducto
AFTER DELETE
AS
BEGIN
    UPDATE p
    SET p.StockActual = p.StockActual + d.Cantidad
    FROM Producto p
    INNER JOIN deleted d ON p.IDProducto = d.IDProducto;
END;
GO





