using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using accesoAdatos;
using dominio;

namespace negocio
{
    public class negocioImagen
    {
        public List<Imagen> listarImagenes(int idProducto)
        {
            List<Imagen> imagenes = new List<Imagen>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT IDImagen, IDProducto, Url FROM Imagenes WHERE IDProducto = @id");
                datos.agregarParametros("@id",idProducto);
                datos.ejecutarLectura();
                while(datos.Lector.Read())
                {
                    Imagen img = new Imagen();
                    img.Id=(int)datos.Lector["IDImagen"];
                    img.IdProducto = idProducto;
                    img.Url=(string)datos.Lector["Url"];
                    imagenes.Add(img);
                }

                return imagenes;
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
        
        public void agregarImagen(Imagen nueva)
        {
            AccesoDatos datos=new AccesoDatos();
            try
            {
                datos.setearConsulta("insert into Imagenes (IDProducto,Url) values (@idProducto,@url)");
                datos.agregarParametros("@idProducto", nueva.IdProducto);
                datos.agregarParametros("@url", nueva.Url);
                datos.ejecutarAccion();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally { datos.cerrarConexion(); }
        }
        public void agregarImagenes(List<Imagen> imagenes)
        {
            foreach (var  img in imagenes)
            {
                agregarImagen(img);
            }
        }
        
        public List<Imagen> ObetenerimagenesId(int id)
        {
            List<Imagen> imagenes = new List<Imagen>();
            AccesoDatos datos = new AccesoDatos();

            try{
                datos.setearConsulta("SELECT URL FROM IMAGENES WHERE IDProducto=@ID");
                datos.limpiarParametros();
                datos.agregarParametros("@id", id);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Imagen img = new Imagen();
                    img.Url = (string)datos.Lector["Url"];
                    img.IdProducto = id;
                    imagenes.Add(img);
                }
                return imagenes;
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


        public void eliminarPorProducto(int idProducto)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("delete from imagenes where IDProducto = @id");
                datos.agregarParametros("@id",idProducto);
                datos.ejecutarAccion();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally { datos.cerrarConexion(); }

        }
    }
}

