use Ecommerce_DB;

create procedure Desahacer_Compra
@idPedido bigint,
@exito bit
as 
begin 

set nocount on 

begin try

if @exito <> 1
begin 

Update Pedido 

set estado = 'Cancelado' where Pedido.id=@idPedido;

end 

end try

begin catch

end catch 
end