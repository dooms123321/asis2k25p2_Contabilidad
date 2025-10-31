// Inicio de código de Arón Ricardo Esquit Silva   0901-22-13036   30/10/2025

using System;
using System.Data;
using System.Data.Odbc;

namespace Capa_Modelo_Estados_Financieros
{
    public class Cls_EstadoDeResultados_Dao
    {
        private readonly Cls_Conexion gConexion = new Cls_Conexion();

        // Método para consultar todas las cuentas de ingresos, costos y gastos
        public DataTable fun_consultar_estado_resultados()
        {
            DataTable dts_EstadoResultados = new DataTable();

            try
            {
                string sQuery = "SELECT " +
                                "Fk_Codigo_Cuenta AS Codigo, " +
                                "Cmp_Nombre_Cuenta AS Nombre, " +
                                "Cmp_Tipo_Cuenta AS Tipo, " +
                                "Cmp_Valor AS Valor " +
                                "FROM Tbl_Estado_Resultados " +
                                "ORDER BY Fk_Codigo_Cuenta;";

                using (OdbcConnection conn = gConexion.conexion())
                {
                    OdbcDataAdapter adp = new OdbcDataAdapter(sQuery, conn);
                    adp.Fill(dts_EstadoResultados);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al consultar el estado de resultados: " + ex.Message);
            }

            return dts_EstadoResultados;
        }
    }
}
