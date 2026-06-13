use Ecommerce_DB
go

Create procedure agregar_detalleProducto
@idProducto bigint,
@idPedido bigint,
@cantidadProducto int,
@precioUnitario decimal(10,2),
@PrecioRebajado decimal(10,2) = 0,
 @exito bit OUTPUT
AS
begin
 
Set nocount on;
SET XACT_ABORT ON;
begin try

begin tran;

Insert into detalleProducto(idproducto,idpedido,cantidadProducto,PrecioUnitario,PrecioRebajado)
Values(@idProducto,@idPedido,@cantidadProducto,@precioUnitario,@PrecioRebajado);

if @@ROWCOUNT = 1
begin
commit tran
set @exito=1;
end

else
begin
rollback tran
set @exito=0
end
end try

 BEGIN CATCH

  
 
    IF XACT_STATE() <> 0
      ROLLBACK TRAN;
       DELETE FROM detalleProducto WHERE idPedido = @idPedido;
    SET @exito = 0;
    end catch
    end