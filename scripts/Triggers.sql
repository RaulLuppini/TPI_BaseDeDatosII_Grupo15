USE [TPI_BDII];
GO

--Trigger auditoria imagenes borradas del producto
CREATE TRIGGER trg_RespaldoImagenEliminada
ON Imagenes
AFTER DELETE
AS
BEGIN
    INSERT INTO AuditoriaImagenesBorradas (IDProducto, URLBorrada, FechaBorrado)
    SELECT d.IDProducto, d.URL, GETDATE()
    FROM deleted d;
END;
GO

--------------------------------------------------------------
Create trigger TR_DescontarDeStock
on DetalleProducto 
after insert
as begin 
 Update Prod
 set Prod.StockActual = Prod.StockActual - I.Cantidad
 from Producto as Prod
 inner join inserted as I ON Prod.IDProducto = I.IDProducto;
 end;

GO
--------------------------------------------------------------

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

--Trigger para restaurar stock post venta cancelada o seleccion cancelada

Create trigger trg_RestaurarStock ON Pedido
AFTER UPDATE
AS
BEGIN
    IF EXISTS (SELECT 1 FROM inserted i JOIN deleted d ON i.IDPedido = d.IDPedido 
               JOIN EstadoPedido es ON i.IDEstado = es.IDEstado
               WHERE es.Nombre = 'Cancelado' AND d.IDEstado <> i.IDEstado)
    BEGIN
        UPDATE p
        SET p.StockActual = p.StockActual + d.Cantidad
        FROM Producto p
       
        INNER JOIN DetalleProducto d ON p.IDProducto = d.IDProducto
        INNER JOIN inserted i ON d.IDPedido = i.IDPedido
        WHERE i.IDEstado = (SELECT IDEstado FROM EstadoPedido WHERE Nombre = 'Cancelado');
    END
END;
GO