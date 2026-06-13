use Ecommerce_DB
go

create procedure cambiar_password
@idUsuario bigint, 
@currentPassword varchar(30),
@newPassword varchar(30)
As
Begin
begin Try

   IF EXISTS (select 1 from usuario where Usuario.Id=@idUsuario and Usuario.Contraseña=@currentPassword)

   begin

   update Usuario
   set Contraseña = @newPassword
   where Usuario.Id=@idUsuario

    SELECT 'OK' AS Resultado, 'Contraseña actualizada correctamente' AS Mensaje;

    end
    else 
    begin
      SELECT 'ERROR' AS Resultado, 'La contraseña actual no coincide' AS Mensaje;
        END
    END TRY

     BEGIN CATCH
        SELECT 
            'ERROR' AS Resultado,
            ERROR_MESSAGE() AS Mensaje;
    END CATCH
END
