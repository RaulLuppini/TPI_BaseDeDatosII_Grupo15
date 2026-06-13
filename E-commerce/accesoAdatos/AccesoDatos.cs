using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace accesoAdatos
{
    public class AccesoDatos
    {
        private SqlConnection conexion;
        private SqlCommand comando;
        private SqlDataReader lector;
        public SqlDataReader Lector => lector;
        public AccesoDatos()
        {
            conexion = new SqlConnection("Server = localhost\\SQLEXPRESS; Database = TPI_BDII; Integrated Security = True; TrustServerCertificate = True; Encrypt = False;");
            comando = new SqlCommand();
        }
        public void setearConsulta(string consulta)
        {
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = consulta;
        }
        public void ejecutarLectura()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                lector=comando.ExecuteReader();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void ejecutarAccion()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally 
            { conexion.Close(); }
        }
        public object ejecutarEscalar()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                return comando.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.Close();
            }
        }

        public void setearProcedimiento(string nombre)
        {
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.CommandText = nombre;
        }

        public void cerrarConexion()
        {
            if(lector != null)
                lector.Close();
            conexion.Close();
        }
        public void agregarParametros(string nombre,object valor)
        {
            comando.Parameters.AddWithValue(nombre, valor);
        }
        public void limpiarParametros()
        {
            comando.Parameters.Clear();
        }

        public SqlParameter setParametro(string param, SqlDbType sql)
        {
           return comando.Parameters.Add(param,sql);
        }
    }
}
