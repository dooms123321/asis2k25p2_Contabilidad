// Inicio de código de Arón Ricardo Esquit Silva   0901-22-13036   30/10/2025

using System;
using System.Data;
using System.Data.Odbc;

namespace Capa_Modelo_Estados_Financieros
{
    public class Cls_BalanceGeneral_Dao
    {
        // Instancia de conexión ODBC
        private readonly Cls_Conexion gConexion = new Cls_Conexion();

        // Método para consultar las cuentas necesarias del balance general
        public DataTable fun_consultar_balance_general()
        {
            DataTable dts_BalanceGeneral = new DataTable();

            try
            {
                using (OdbcConnection conn = gConexion.conexion())
                {
                    string sQuery = @"
                        SELECT 
                            Fk_Codigo_Cuenta AS Codigo,
                            Cmp_Nombre_Cuenta AS Nombre,
                            CASE
                                WHEN Fk_Codigo_Cuenta LIKE '1%' THEN 'Activo'
                                WHEN Fk_Codigo_Cuenta LIKE '2%' THEN 'Pasivo'
                                WHEN Fk_Codigo_Cuenta LIKE '3%' THEN 'Capital'
                            END AS Tipo,
                            Cmp_Saldo AS Valor
                        FROM Tbl_Balance_Saldos
                        WHERE Fk_Codigo_Cuenta LIKE '1%' 
                           OR Fk_Codigo_Cuenta LIKE '2%' 
                           OR Fk_Codigo_Cuenta LIKE '3%'
                        ORDER BY Fk_Codigo_Cuenta;";

                    OdbcDataAdapter da = new OdbcDataAdapter(sQuery, conn);
                    da.Fill(dts_BalanceGeneral);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al consultar el balance general: " + ex.Message);
            }

            return dts_BalanceGeneral;
        }
    }
}
