Use Ecommerce_DB
go
create trigger restar_stock on DetalleProducto
after insert
as 
begin


IF EXISTS (
        SELECT 1
        FROM Producto p
        INNER JOIN inserted i ON p.Id = i.IdProducto
        WHERE p.StockActual < i.CantidadProducto
    )
    BEGIN
        RAISERROR ('No se puede realizar el proceso: stock insuficiente.', 16, 1);
        ROLLBACK TRANSACTION; -- ❗ CANCELA EL INSERT
        RETURN;
    END;

update P 
Set P.StockActual = p.StockActual - i.cantidadProducto
from Producto as P
inner join inserted i on P.id=i.idProducto
where P.id = i.idProducto
end
