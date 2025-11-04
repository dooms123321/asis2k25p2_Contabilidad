// Inicio de código de Arón Ricardo Esquit Silva   0901-22-13036   29/10/2025

using System;
using System.Data;
using System.Data.Odbc;

namespace Capa_Modelo_Estados_Financieros
{
    public class Cls_BalanceDeSaldos_Dao
    {
        private readonly Cls_Conexion gConexion = new Cls_Conexion();

        // Método para obtener todos los registros del balance de saldos
        public DataTable fun_consultar_balance_saldos()
        {
            DataTable dts_Balance = new DataTable();

            try
            {
                string sQuery = "SELECT Fk_Codigo_Cuenta AS Codigo, " +
                                "Cmp_Nombre_Cuenta AS Nombre, " +
                                "Cmp_Debe AS Debe, " +
                                "Cmp_Haber AS Haber, " +
                                "Cmp_Saldo AS Saldo " +
                                "FROM Tbl_Balance_Saldos " +
                                "ORDER BY Fk_Codigo_Cuenta;";

                using (OdbcConnection conn = gConexion.conexion())
                {
                    OdbcDataAdapter adp = new OdbcDataAdapter(sQuery, conn);
                    adp.Fill(dts_Balance);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al consultar el balance de saldos: " + ex.Message);
            }

            return dts_Balance;
        }
    }
}
