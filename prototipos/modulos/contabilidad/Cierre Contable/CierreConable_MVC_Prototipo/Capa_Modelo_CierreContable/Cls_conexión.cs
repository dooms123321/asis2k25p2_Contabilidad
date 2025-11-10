using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;

namespace Capa_Modelo_CierreContable
{
    class Cls_conexión
    {

        // Devuelve la cadena de conexión ODBC
        public string ObtenerCadenaConexion()
        {
            return "Bd_Hoteleria";
        }

        // Abre y retorna una nueva conexión ODBC
        public OdbcConnection conexion()
        {
            OdbcConnection conn = new OdbcConnection(ObtenerCadenaConexion());
            try
            {
                conn.Open();
            }
            catch (OdbcException)
            {
                Console.WriteLine("No Conectó");
            }
            return conn;
        }

        // Alternativo: método estándar para abrir conexión (sin try/catch interno)
        public OdbcConnection AbrirConexion()
        {
            OdbcConnection conn = new OdbcConnection(ObtenerCadenaConexion());
            conn.Open();
            return conn;
        }

        // Cierra la conexión recibida
        public void desconexion(OdbcConnection conn)
        {
            try
            {
                conn.Close();
            }
            catch (OdbcException)
            {
                Console.WriteLine("No se pudo cerrar la conexión");
            }
        }
    }
}
