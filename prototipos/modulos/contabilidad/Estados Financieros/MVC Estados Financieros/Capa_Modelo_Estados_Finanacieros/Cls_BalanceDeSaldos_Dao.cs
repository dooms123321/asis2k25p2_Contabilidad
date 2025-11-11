// =====================================================================================
// Inicio de código de Arón Ricardo Esquit Silva   0901-22-13036   09/11/2025
// Descripción: DAO para obtención de datos del Balance de Saldos (actual e histórico)
// =====================================================================================

using System;
using System.Data;
using System.Data.Odbc;

namespace Capa_Modelo_Estados_Financieros
{
    public class Cls_BalanceDeSaldos_Dao
    {
        // Conexión ODBC general
        private readonly Cls_Conexion gConexion = new Cls_Conexion();

        // ---------------------------------------------------------------------------------
        // Método: Fun_Consultar_Balance_Saldos
        // Obtiene los saldos desde el catálogo de cuentas (modo actual)
        // ---------------------------------------------------------------------------------
        public DataTable Fun_Consultar_Balance_Saldos(int iNivel)
        {
            DataTable dts_Balance = new DataTable();

            try
            {
                string sQuery = @"
                    SELECT 
                        Pk_Codigo_Cuenta AS Codigo,
                        Cmp_CtaNombre AS Nombre,
                        CASE 
                            WHEN Cmp_CtaNaturaleza = 1 THEN Cmp_CtaSaldoActual 
                            ELSE 0 
                        END AS Debe,
                        CASE 
                            WHEN Cmp_CtaNaturaleza = 0 THEN Cmp_CtaSaldoActual 
                            ELSE 0 
                        END AS Haber
                    FROM Tbl_Catalogo_Cuentas
                    WHERE ((LENGTH(Pk_Codigo_Cuenta) - LENGTH(REPLACE(Pk_Codigo_Cuenta, '.', ''))) + 1) <= " + iNivel + @"
                    ORDER BY Pk_Codigo_Cuenta;";

                using (OdbcConnection gConn = gConexion.conexion())
                {
                    OdbcDataAdapter adp_Balance = new OdbcDataAdapter(sQuery, gConn);
                    adp_Balance.Fill(dts_Balance);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al consultar el balance de saldos actual: " + ex.Message);
            }

            return dts_Balance;
        }

        // ---------------------------------------------------------------------------------
        // Método: Fun_Consultar_Balance_Saldos_Historico
        // Obtiene los saldos desde la tabla histórica filtrando por año, mes y nivel
        // ---------------------------------------------------------------------------------
        public DataTable Fun_Consultar_Balance_Saldos_Historico(int iNivel, int iAnio, int iMes)
        {
            DataTable dts_Balance = new DataTable();

            try
            {
                string sQuery = @"
                    SELECT 
                        Pk_Codigo_Cuenta AS Codigo,
                        Cmp_CtaNombre AS Nombre,
                        CASE 
                           WHEN IFNULL(Cmp_CtaNaturaleza, 1) = 1 THEN Cmp_CtaSaldoActual 
                           ELSE 0 
                       END AS Debe,
                       CASE 
                          WHEN IFNULL(Cmp_CtaNaturaleza, 1) = 0 THEN Cmp_CtaSaldoActual 
                          ELSE 0 
                        END AS Haber
                    FROM Tbl_Historico_Catalogo_Cuentas
                    WHERE Cmp_Anio = " + iAnio + @" 
                      AND Cmp_Mes  = " + iMes + @"
                    ORDER BY Pk_Codigo_Cuenta;";

                using (OdbcConnection gConn = gConexion.conexion())
                {
                    OdbcDataAdapter adp_Balance = new OdbcDataAdapter(sQuery, gConn);
                    adp_Balance.Fill(dts_Balance);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al consultar el balance histórico: " + ex.Message);
            }

            return dts_Balance;
        }
    }
}

// Fin de código de Arón Ricardo Esquit Silva
// =====================================================================================
