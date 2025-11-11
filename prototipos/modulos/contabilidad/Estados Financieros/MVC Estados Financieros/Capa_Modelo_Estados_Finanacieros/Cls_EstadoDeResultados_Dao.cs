// =====================================================================================
// Inicio de código de Arón Ricardo Esquit Silva   0901-22-13036   10/11/2025
// Descripción: DAO para consulta del Estado de Resultados (actual e histórico)
// =====================================================================================

using System;
using System.Data;
using System.Data.Odbc;

namespace Capa_Modelo_Estados_Financieros
{
    public class Cls_EstadoResultados_Dao
    {
        private readonly Cls_Conexion gConexion = new Cls_Conexion();

        // ---------------------------------------------------------------------------------
        // Método: fun_consultar_estado_resultados
        // Obtiene las cuentas de resultado (4,5,6) desde el catálogo de cuentas (modo actual)
        // ---------------------------------------------------------------------------------
        public DataTable fun_consultar_estado_resultados(int iNivel)
        {
            DataTable dts_Resultados = new DataTable();

            try
            {
                string sQuery = @"
                    SELECT 
                        Pk_Codigo_Cuenta AS Codigo,
                        Cmp_CtaNombre AS Nombre,
                        CASE 
                            WHEN Pk_Codigo_Cuenta LIKE '4%' THEN 'Ingreso'
                            WHEN Pk_Codigo_Cuenta LIKE '5%' THEN 'Costo' 
                            WHEN Pk_Codigo_Cuenta LIKE '6%' THEN 'Gasto'
                        END AS Tipo,
                        Cmp_CtaSaldoActual AS Saldo
                    FROM Tbl_Catalogo_Cuentas
                    WHERE (Pk_Codigo_Cuenta LIKE '4%' 
                           OR Pk_Codigo_Cuenta LIKE '5%' 
                           OR Pk_Codigo_Cuenta LIKE '6%')
                    AND ((LENGTH(Pk_Codigo_Cuenta) - LENGTH(REPLACE(Pk_Codigo_Cuenta, '.', ''))) + 1) <= " + iNivel + @"
                    ORDER BY Pk_Codigo_Cuenta;";

                using (OdbcConnection gConn = gConexion.conexion())
                {
                    OdbcDataAdapter adp_Resultados = new OdbcDataAdapter(sQuery, gConn);
                    adp_Resultados.Fill(dts_Resultados);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al consultar estado de resultados actual: " + ex.Message);
            }

            return dts_Resultados;
        }

        // ---------------------------------------------------------------------------------
        // Método: fun_consultar_estado_resultados_historico
        // Obtiene las cuentas de resultado (4,5,6) desde el catálogo histórico filtrado por año y mes
        // ---------------------------------------------------------------------------------
        public DataTable fun_consultar_estado_resultados_historico(int iNivel, int iAnio, int iMes)
        {
            DataTable dts_Resultados = new DataTable();

            try
            {
                string sQuery = @"
                    SELECT 
                        Pk_Codigo_Cuenta AS Codigo,
                        Cmp_CtaNombre AS Nombre,
                        CASE 
                            WHEN Pk_Codigo_Cuenta LIKE '4%' THEN 'Ingreso'
                            WHEN Pk_Codigo_Cuenta LIKE '5%' THEN 'Costo' 
                            WHEN Pk_Codigo_Cuenta LIKE '6%' THEN 'Gasto'
                        END AS Tipo,
                        Cmp_CtaSaldoActual AS Saldo
                    FROM Tbl_Historico_Catalogo_Cuentas
                    WHERE (Pk_Codigo_Cuenta LIKE '4%' 
                           OR Pk_Codigo_Cuenta LIKE '5%' 
                           OR Pk_Codigo_Cuenta LIKE '6%')
                      AND Cmp_Anio = " + iAnio + @"
                      AND Cmp_Mes = " + iMes + @"
                      AND ((LENGTH(Pk_Codigo_Cuenta) - LENGTH(REPLACE(Pk_Codigo_Cuenta, '.', ''))) + 1) <= " + iNivel + @"
                    ORDER BY Pk_Codigo_Cuenta;";

                using (OdbcConnection gConn = gConexion.conexion())
                {
                    OdbcDataAdapter adp_Resultados = new OdbcDataAdapter(sQuery, gConn);
                    adp_Resultados.Fill(dts_Resultados);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al consultar estado de resultados histórico: " + ex.Message);
            }

            return dts_Resultados;
        }

        // ---------------------------------------------------------------------------------
        // Método: fun_calcular_utilidad_neta
        // Calcula la utilidad neta del ejercicio (modo actual)
        // ---------------------------------------------------------------------------------
        public decimal fun_calcular_utilidad_neta()
        {
            decimal deUtilidadNeta = 0;

            try
            {
                string sQuery = @"
                    SELECT 
                        SUM(CASE WHEN Pk_Codigo_Cuenta LIKE '4%' THEN Cmp_CtaSaldoActual ELSE 0 END) -
                        SUM(CASE WHEN Pk_Codigo_Cuenta LIKE '5%' THEN Cmp_CtaSaldoActual ELSE 0 END) -
                        SUM(CASE WHEN Pk_Codigo_Cuenta LIKE '6%' THEN Cmp_CtaSaldoActual ELSE 0 END)
                    AS Utilidad_Neta
                    FROM Tbl_Catalogo_Cuentas
                    WHERE Pk_Codigo_Cuenta LIKE '4%' 
                       OR Pk_Codigo_Cuenta LIKE '5%' 
                       OR Pk_Codigo_Cuenta LIKE '6%';";

                using (OdbcConnection gConn = gConexion.conexion())
                {
                    OdbcCommand cmd = new OdbcCommand(sQuery, gConn);
                    object objResultado = cmd.ExecuteScalar();

                    if (objResultado != null && objResultado != DBNull.Value)
                        deUtilidadNeta = Convert.ToDecimal(objResultado);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al calcular utilidad neta actual: " + ex.Message);
            }

            return deUtilidadNeta;
        }

        // ---------------------------------------------------------------------------------
        // Método: fun_calcular_utilidad_neta_historico
        // Calcula la utilidad neta del ejercicio para el año y mes seleccionados
        // ---------------------------------------------------------------------------------
        public decimal fun_calcular_utilidad_neta_historico(int iAnio, int iMes)
        {
            decimal deUtilidadNeta = 0;

            try
            {
                string sQuery = @"
                    SELECT 
                        SUM(CASE WHEN Pk_Codigo_Cuenta LIKE '4%' THEN Cmp_CtaSaldoActual ELSE 0 END) -
                        SUM(CASE WHEN Pk_Codigo_Cuenta LIKE '5%' THEN Cmp_CtaSaldoActual ELSE 0 END) -
                        SUM(CASE WHEN Pk_Codigo_Cuenta LIKE '6%' THEN Cmp_CtaSaldoActual ELSE 0 END)
                    AS Utilidad_Neta
                    FROM Tbl_Historico_Catalogo_Cuentas
                    WHERE (Pk_Codigo_Cuenta LIKE '4%' 
                           OR Pk_Codigo_Cuenta LIKE '5%' 
                           OR Pk_Codigo_Cuenta LIKE '6%')
                      AND Cmp_Anio = " + iAnio + @"
                      AND Cmp_Mes = " + iMes + ";";

                using (OdbcConnection gConn = gConexion.conexion())
                {
                    OdbcCommand cmd = new OdbcCommand(sQuery, gConn);
                    object objResultado = cmd.ExecuteScalar();

                    if (objResultado != null && objResultado != DBNull.Value)
                        deUtilidadNeta = Convert.ToDecimal(objResultado);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al calcular utilidad neta histórica: " + ex.Message);
            }

            return deUtilidadNeta;
        }
    }
}

// Fin de código de Arón Ricardo Esquit Silva
// =====================================================================================
