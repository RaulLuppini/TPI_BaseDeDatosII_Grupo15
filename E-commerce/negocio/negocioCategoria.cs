using accesoAdatos;
using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class negocioCategoria
    {
       public List<Categoria> listarCategoria()
        {
            List<Categoria> lista = new List<Categoria>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("select IDCategoria,Nombre from Categoria");
                datos.ejecutarLectura();
                while(datos.Lector.Read())
                {
                    Categoria categoria = new Categoria();
                    categoria.Id =(int)datos.Lector["IDCategoria"];
                    categoria.Nombre =(string)datos.Lector["Nombre"];
                    lista.Add(categoria);
                }
                return lista;
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

        public void agregarCategoria(Categoria nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("insert into Categoria (Nombre) values (@nombrecategoria)");
                datos.agregarParametros("@nombrecategoria", nuevo.Nombre);
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


        public void eliminarCategoria(int idCategoria)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("DELETE FROM Categoria WHERE IDCategoria = @idCategoria");
                datos.agregarParametros("@idCategoria", idCategoria);
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
        public void modificarCategoria(int id,string nuevoNombre)
        {
            AccesoDatos datos =new AccesoDatos();
            datos.setearConsulta("update categoria set Nombre = @nombre where IDCategoria = @id");
            datos.agregarParametros("@nombre",nuevoNombre);
            datos.agregarParametros("@id",id);
            datos.ejecutarAccion();
        }
    }
}
