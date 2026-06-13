using accesoAdatos;
using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class negocioUsuario
    {
        public List<Usuario> listarUsuarios()
        {
            List<Usuario> lista = new List<Usuario>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT * from VW_ListarUsuario");
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Usuario users = new Usuario();
                    users.Id = (int)datos.Lector["IDUsuario"];
                    users.Dni = (string)datos.Lector["Dni"];
                    users.Nombre = (string)datos.Lector["Nombre"];
                    users.Apellido = (string)datos.Lector["Apellido"];
                    users.Email = (string)datos.Lector["Correo"];
                    users.Direccion = datos.Lector["Direccion"] != DBNull.Value ? (string)datos.Lector["Direccion"] : null;
                    users.CodigoPostal = datos.Lector["CodigoPostal"] != DBNull.Value ? (string)datos.Lector["CodigoPostal"] : null;
                    users.Estado = (bool)datos.Lector["Estado"];
                    users.RolUsuario = (string)datos.Lector["Rol"];
                    users.Telefono = (string)datos.Lector["Telefono"];
                    lista.Add(users);
                }
                return lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public Usuario Login(string email, string clave)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT * FROM Usuario WHERE Correo = @email AND Contrasenia = @clave");
                datos.agregarParametros("@email", email);
                datos.agregarParametros("@clave", clave);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    Usuario usuario = new Usuario();
                    usuario.Id = (int)datos.Lector["IDUsuario"];
                    usuario.Nombre = (string)datos.Lector["Nombre"];
                    usuario.Apellido = (string)datos.Lector["Apellido"];
                    usuario.Email = (string)datos.Lector["Correo"];
                    usuario.RolUsuario = (string)datos.Lector["Rol"];
                    usuario.Telefono = (string)datos.Lector["Telefono"];

                    return usuario;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void agregarUsuario(Usuario nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO Usuario (Dni,Nombre,Apellido,Correo,Contrasenia,Rol,Telefono,Direccion,CodigoPostal,Estado) VALUES (@dni,@nombre,@apellido,@correo,@contrasenia,@rol,@telefono,@direccion,@codigoPostal,@estado);");
                datos.agregarParametros("@dni", nuevo.Dni);
                datos.agregarParametros("@nombre", nuevo.Nombre);
                datos.agregarParametros("@apellido", nuevo.Apellido);
                datos.agregarParametros("@correo", nuevo.Email);
                datos.agregarParametros("@contrasenia", nuevo.Contraseña);
                datos.agregarParametros("@rol", nuevo.RolUsuario);
                datos.agregarParametros("@telefono", nuevo.Telefono);
                datos.agregarParametros("@direccion", nuevo.Direccion);
                datos.agregarParametros("@codigoPostal", nuevo.CodigoPostal);
                datos.agregarParametros("@estado", nuevo.Estado);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }

        }

        public void modificarUsuario(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE Usuario SET Dni = @dni, Nombre = @nombre, Apellido = @apellido, Correo = @correo, Contrasenia = @clave, Rol = @rol, Telefono = @telefono, Direccion = @direccion, CodigoPostal = @codigoPostal, Estado = @estado WHERE IDUsuario = @id");
                datos.agregarParametros("@dni", usuario.Dni);
                datos.agregarParametros("@nombre", usuario.Nombre);
                datos.agregarParametros("@apellido", usuario.Apellido);
                datos.agregarParametros("@correo", usuario.Email);
                datos.agregarParametros("@clave", usuario.Contraseña);
                datos.agregarParametros("@rol", usuario.RolUsuario);
                datos.agregarParametros("@telefono", usuario.Telefono);
                datos.agregarParametros("@direccion", usuario.Direccion);
                datos.agregarParametros("@codigoPostal", usuario.CodigoPostal);
                datos.agregarParametros("@estado", usuario.Estado);
                datos.agregarParametros("@id", usuario.Id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void darDeAlta(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE Usuario SET Estado = 1 WHERE IDUsuario = @id");
                datos.agregarParametros("@id", id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void darDeBaja(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE Usuario SET Estado = 0 WHERE IDUsuario = @id");
                datos.agregarParametros("@id", id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void eliminarUsuario(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("DELETE FROM Usuario WHERE IDUsuario = @id");
                datos.agregarParametros("@id", id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public Usuario buscarPorId(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            Usuario usuario = new Usuario();

            try
            {
                datos.setearConsulta("SELECT IDUsuario, Dni, Nombre, Apellido, Correo, Contrasenia, Rol, Telefono, Direccion, CodigoPostal, Estado FROM Usuario WHERE Id = @id");
                datos.agregarParametros("@id", id);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    usuario.Id = (int)datos.Lector["IDUsuario"];
                    usuario.Dni = datos.Lector["Dni"].ToString();
                    usuario.Nombre = datos.Lector["Nombre"].ToString();
                    usuario.Apellido = datos.Lector["Apellido"].ToString();
                    usuario.Email = datos.Lector["Correo"].ToString();
                    usuario.Contraseña = datos.Lector["Contrasenia"].ToString();
                    usuario.RolUsuario = datos.Lector["Rol"].ToString();
                    usuario.Telefono = datos.Lector["Telefono"] != DBNull.Value ? datos.Lector["Telefono"].ToString() : null;
                    usuario.Direccion = datos.Lector["Direccion"] != DBNull.Value ? datos.Lector["Direccion"].ToString() : null;
                    usuario.CodigoPostal = datos.Lector["CodigoPostal"] != DBNull.Value ? datos.Lector["CodigoPostal"].ToString() : null;
                    usuario.Estado = (bool)datos.Lector["Estado"];
                }

                return usuario;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public string UpdatePassword(int id, string current, string newPassword)
        {
            AccesoDatos datos= new AccesoDatos();

            try
            {

                datos.setearProcedimiento("cambiar_password");
                datos.limpiarParametros();
                datos.agregarParametros("@idUsuario",id);
                datos.agregarParametros("@currentPassword",current);
                datos.agregarParametros("@newPassword", newPassword);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    string resultado = datos.Lector["Resultado"].ToString();
                    string mensaje = datos.Lector["Mensaje"].ToString();

                    if (resultado == "OK")
                    {
                        return "OK";

                    }

                    return mensaje; // mensaje del SP (contraseña incorrecta)
                }

                return "Error inesperado";

            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

    }
}
