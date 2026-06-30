USE [TPI_BDII];
GO

-----------------------------------------------
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

----------------------------------------------------------

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
----------------------------------------------------------

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

----------------------------------------------------------

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
            SELECT 'OK' AS Resultado, 'Contrase�a actualizada correctamente' AS Mensaje;
        END
        ELSE
        BEGIN
            SELECT 'ERROR' AS Resultado, 'La contrase�a actual no coincide' AS Mensaje;
        END
    END TRY
    BEGIN CATCH
        SELECT 'ERROR' AS Resultado, ERROR_MESSAGE() AS Mensaje;
    END CATCH
END;
GO

----------------------------------------------------------
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