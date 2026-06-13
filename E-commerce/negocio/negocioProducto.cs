using accesoAdatos;
using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace negocio
{
    public class negocioProducto
    {
        public List<Producto> listar()
        {
            List<Producto> lista = new List<Producto>();
            AccesoDatos datos = new AccesoDatos();
            negocioImagen negocioImagen = new negocioImagen();
            try
            {

                datos.setearConsulta("SELECT * from VW_ListarProductos");
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Producto producto = new Producto();
                    producto.Id = (int)datos.Lector["IDProducto"];
                    producto.Codigo = (string)datos.Lector["Codigo"];
                    producto.Nombre = (string)datos.Lector["Nombre"];
                    producto.IdMarca = new Marca();
                    producto.IdMarca.Id = (int)datos.Lector["MarcaId"];
                    producto.IdMarca.Nombre = (string)datos.Lector["MarcaNombre"];
                    producto.IdCategoria = new Categoria();
                    producto.IdCategoria.Id = (int)datos.Lector["CategoriaId"];
                    producto.IdCategoria.Nombre = (string)datos.Lector["CategoriaNombre"];
                    producto.Descripcion = (string)datos.Lector["Descripcion"];
                    producto.PrecioCompra = (decimal)datos.Lector["PrecioCompra"];
                    //producto.PorcentajeGanancia = (decimal)datos.Lector["PorcentajeGanancia"];
                    producto.PrecioVenta = (decimal)datos.Lector["PrecioVenta"];
                    producto.StockActual = (int)datos.Lector["StockActual"];
                    producto.StockMinimo = (int)datos.Lector["StockMinimo"];
                    producto.Estado = (bool)datos.Lector["Estado"];
                    producto.Imagenes = negocioImagen.ObetenerimagenesId(producto.Id);

                    lista.Add(producto);
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

        public void agregarProducto(Producto nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO Producto (Codigo,Nombre,IDMarca,IDCategoria,Descripcion,PrecioCompra,PorcentajeGanancia,PrecioVenta,StockActual,StockMinimo,Estado) " +
                "VALUES (@codigo,@nombre,@marcaId,@categoriaId,@descripcion,@precioCompra,@porcentajeGanancia,@precioVenta,@stockActual,@stockMinimo,@estado); select scope_identity();");

                datos.agregarParametros("@codigo", nuevo.Codigo);
                datos.agregarParametros("@nombre", nuevo.Nombre);
                datos.agregarParametros("@marcaId", nuevo.IdMarca.Id);
                datos.agregarParametros("@categoriaId", nuevo.IdCategoria.Id);
                datos.agregarParametros("@descripcion", nuevo.Descripcion);
                datos.agregarParametros("@precioCompra", nuevo.PrecioCompra);
                decimal porcentaje = nuevo.PrecioCompra == 0 ? 0 :
                Math.Round(((nuevo.PrecioVenta - nuevo.PrecioCompra) / nuevo.PrecioCompra) * 100, 2);

                datos.agregarParametros("@porcentajeGanancia", porcentaje);


                datos.agregarParametros("@precioVenta", nuevo.PrecioVenta);
                datos.agregarParametros("@stockActual", nuevo.StockActual);
                datos.agregarParametros("@stockMinimo", nuevo.StockMinimo);
                datos.agregarParametros("@estado", nuevo.Estado);
                object resultado = datos.ejecutarEscalar();
                nuevo.Id = Convert.ToInt32(resultado);

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally { datos.cerrarConexion(); }
        }

        public void modificarProducto(Producto aModificar)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("update Producto set Codigo = @codigo,Nombre=@nombre,IDMarca=@marcaId,IDCategoria=@categoriaId,Descripcion=@descripcion,PrecioCompra=@precioCompra,PorcentajeGanancia=@porcentajeGanancia,PrecioVenta=@precioVenta,StockActual=@stockActual,StockMinimo=@stockMinimo,Estado=@estado where IDProducto=@id");
                datos.agregarParametros("@codigo", aModificar.Codigo);
                datos.agregarParametros("@nombre", aModificar.Nombre);
                datos.agregarParametros("@marcaId", aModificar.IdMarca.Id);
                datos.agregarParametros("@categoriaId", aModificar.IdCategoria.Id);
                datos.agregarParametros("@descripcion", aModificar.Descripcion);
                datos.agregarParametros("@precioCompra", aModificar.PrecioCompra);
                decimal porcentaje = aModificar.PrecioCompra == 0 ? 0 :
                Math.Round(((aModificar.PrecioVenta - aModificar.PrecioCompra) / aModificar.PrecioCompra) * 100, 2);
                datos.agregarParametros("@porcentajeGanancia", porcentaje);
                datos.agregarParametros("@precioVenta", aModificar.PrecioVenta);
                datos.agregarParametros("@stockActual", aModificar.StockActual);
                datos.agregarParametros("@stockMinimo", aModificar.StockMinimo);
                datos.agregarParametros("@estado", aModificar.Estado);
                datos.agregarParametros("@id", aModificar.Id);

                datos.ejecutarAccion();

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public Producto obtenerPorId(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            Producto producto = new Producto();
            negocioImagen ng = new negocioImagen();

            try
            {
                datos.setearConsulta("SELECT * FROM Producto WHERE IDProducto = @id");
                datos.agregarParametros("@id", id);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    producto.Id = (int)datos.Lector["IDProducto"];
                    producto.Codigo = datos.Lector["Codigo"].ToString();
                    producto.Nombre = datos.Lector["Nombre"].ToString();
                    producto.Descripcion = datos.Lector["Descripcion"].ToString();
                    producto.PrecioCompra = Convert.ToDecimal(datos.Lector["PrecioCompra"]);

                    //producto.PorcentajeGanancia = Convert.ToDecimal(datos.Lector["PorcentajeGanancia"]);
                    producto.PrecioVenta = Convert.ToDecimal(datos.Lector["PrecioVenta"]);
                    producto.StockActual = Convert.ToInt32(datos.Lector["StockActual"]);
                    producto.StockMinimo = Convert.ToInt32(datos.Lector["StockMinimo"]);
                    producto.Estado = Convert.ToBoolean(datos.Lector["Estado"]);

                    int idMarca = Convert.ToInt32(datos.Lector["IDMarca"]);
                    producto.IdMarca = new Marca(idMarca, "");

                    int idCategoria = Convert.ToInt32(datos.Lector["IDCategoria"]);
                    producto.IdCategoria = new Categoria(idCategoria, "");

                    producto.Imagenes = ng.ObetenerimagenesId(id);

                    return producto;
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

        public void darDeBaja(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE Producto SET Estado = 0 WHERE IDProducto = @id");
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


        public List<Producto> listarInactivos()
        {
            List<Producto> lista = new List<Producto>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT * FROM Producto WHERE Estado = 0");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Producto producto = new Producto();
                    producto.Id = (int)datos.Lector["IDProducto"];
                    producto.Codigo = datos.Lector["Codigo"].ToString();
                    producto.Nombre = datos.Lector["Nombre"].ToString();
                    producto.IdMarca = new Marca();
                    producto.IdMarca.Id = (int)datos.Lector["IDMarca"];
                    producto.Descripcion = datos.Lector["Descripcion"].ToString();
                    producto.PrecioCompra = (decimal)datos.Lector["PrecioCompra"];
                    //producto.PorcentajeGanancia = (decimal)datos.Lector["PorcentajeGanancia"];
                    producto.PrecioVenta = (decimal)datos.Lector["PrecioVenta"];
                    producto.StockActual = (int)datos.Lector["StockActual"];
                    producto.StockMinimo = (int)datos.Lector["StockMinimo"];
                    producto.Estado = (bool)datos.Lector["Estado"];

                    lista.Add(producto);
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

        public void darDeAlta(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE Producto SET Estado = 1 WHERE IDProducto = @id");
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

        public void eliminarProducto(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("DELETE FROM Producto WHERE IDProducto = @id");
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

        public bool ExistenProductosPorMarca(int idMarca)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT COUNT(*) FROM Producto WHERE IDMarca = @idMarca");
                datos.agregarParametros("@idMarca", idMarca);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                    return (int)datos.Lector[0] > 0;

                return false;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public bool ExistenProductosPorCategoria(int idCategoria)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT COUNT(*) FROM Producto WHERE IDCategoria = @idCategoria");
                datos.agregarParametros("@idCategoria", idCategoria);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                    return (int)datos.Lector[0] > 0;

                return false;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

    }
}
