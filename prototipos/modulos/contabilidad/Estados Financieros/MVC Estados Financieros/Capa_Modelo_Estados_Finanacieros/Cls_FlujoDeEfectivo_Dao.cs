// =====================================================================================
// Inicio de código de Arón Ricardo Esquit Silva   0901-22-13036   09/11/2025
// Descripción: DAO final para el Estado de Flujo de Efectivo (sin niveles, vista completa)
// Proyecto: QUANTUM S.A. - Módulo CTA (Contabilidad)
// =====================================================================================

using System;
using System.Data;
using System.Data.Odbc;

namespace Capa_Modelo_Estados_Financieros
{
    public class Cls_FlujoEfectivo_Dao
    {
        private readonly Cls_Conexion gConexion = new Cls_Conexion();

        // ---------------------------------------------------------------------------------
        // Método: fun_consultar_flujo_efectivo
        // Descripción: Devuelve únicamente las cuentas que participan en el flujo de efectivo,
        // sin incluir cuentas con saldo cero ni irrelevantes.
        // ---------------------------------------------------------------------------------
        public DataTable fun_consultar_flujo_efectivo()
        {
            DataTable dts_Flujo = new DataTable();

            string sQuery = @"
        SELECT 
            c.Pk_Codigo_Cuenta AS Cuenta,
            c.Cmp_CtaNombre AS Nombre,

            -- ENTRADAS (todo lo que aumenta el efectivo)
            CASE 
                WHEN c.Pk_Codigo_Cuenta LIKE '1.1%'   -- Caja general y chica
                  OR c.Pk_Codigo_Cuenta LIKE '1.2%'   -- Bancos
                  OR c.Pk_Codigo_Cuenta LIKE '4%'     -- Ingresos
                  OR c.Pk_Codigo_Cuenta LIKE '2.2%'   -- Préstamos
                  OR c.Pk_Codigo_Cuenta LIKE '3.1%'   -- Capital social
                  OR c.Pk_Codigo_Cuenta LIKE '3.2%'   -- Utilidades retenidas
                THEN c.Cmp_CtaSaldoActual
                ELSE 0
            END AS Entrada,

            -- SALIDAS (todo lo que consume efectivo)
            CASE 
                WHEN c.Pk_Codigo_Cuenta LIKE '5%'     -- Costos
                  OR c.Pk_Codigo_Cuenta LIKE '6%'     -- Gastos
                  OR c.Pk_Codigo_Cuenta LIKE '1.5%'   -- Activos fijos
                  OR c.Pk_Codigo_Cuenta LIKE '1.6%'   -- Depreciaciones
                THEN c.Cmp_CtaSaldoActual
                ELSE 0
            END AS Salida

        FROM Tbl_Catalogo_Cuentas c
        WHERE 
            (c.Pk_Codigo_Cuenta LIKE '1.1%' OR c.Pk_Codigo_Cuenta LIKE '1.2%' 
             OR c.Pk_Codigo_Cuenta LIKE '1.5%' OR c.Pk_Codigo_Cuenta LIKE '1.6%'
             OR c.Pk_Codigo_Cuenta LIKE '2.2%' OR c.Pk_Codigo_Cuenta LIKE '3.1%' 
             OR c.Pk_Codigo_Cuenta LIKE '3.2%' OR c.Pk_Codigo_Cuenta LIKE '4%' 
             OR c.Pk_Codigo_Cuenta LIKE '5%' OR c.Pk_Codigo_Cuenta LIKE '6%')
          AND c.Cmp_CtaSaldoActual <> 0
        ORDER BY c.Pk_Codigo_Cuenta;
    ";

            try
            {
                using (OdbcConnection gConn = gConexion.conexion())
                {
                    using (OdbcDataAdapter adp = new OdbcDataAdapter(sQuery, gConn))
                    {
                        adp.Fill(dts_Flujo);
                    }
                }

                if (dts_Flujo.Rows.Count == 0)
                    throw new Exception("No se encontraron registros relevantes para el flujo de efectivo.");
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los datos del flujo de efectivo: " + ex.Message);
            }

            return dts_Flujo;
        }


        // ---------------------------------------------------------------------------------
        // Método: fun_calcular_totales
        // Descripción:
        // Devuelve los totales globales de entrada y salida de efectivo.
        // ---------------------------------------------------------------------------------
        public DataTable fun_calcular_totales()
        {
            DataTable dts_Totales = new DataTable();

            string sQuery = @"
                SELECT 
                    SUM(CASE 
                        WHEN Pk_Codigo_Cuenta LIKE '4%' OR Pk_Codigo_Cuenta LIKE '2.2%' 
                             OR Pk_Codigo_Cuenta LIKE '3.1%' OR Pk_Codigo_Cuenta LIKE '3.2%' 
                        THEN Cmp_CtaSaldoActual ELSE 0 END) AS TotalEntradas,

                    SUM(CASE 
                        WHEN Pk_Codigo_Cuenta LIKE '5%' OR Pk_Codigo_Cuenta LIKE '6%' 
                             OR Pk_Codigo_Cuenta LIKE '1.5%' OR Pk_Codigo_Cuenta LIKE '1.6%'
                        THEN Cmp_CtaSaldoActual ELSE 0 END) AS TotalSalidas

                FROM Tbl_Catalogo_Cuentas
                WHERE (
                    Pk_Codigo_Cuenta LIKE '1.1%' OR Pk_Codigo_Cuenta LIKE '1.2%' OR
                    Pk_Codigo_Cuenta LIKE '1.5%' OR Pk_Codigo_Cuenta LIKE '1.6%' OR
                    Pk_Codigo_Cuenta LIKE '2.2%' OR Pk_Codigo_Cuenta LIKE '3.1%' OR
                    Pk_Codigo_Cuenta LIKE '3.2%' OR Pk_Codigo_Cuenta LIKE '4%' OR
                    Pk_Codigo_Cuenta LIKE '5%' OR Pk_Codigo_Cuenta LIKE '6%'
                );";

            try
            {
                using (OdbcConnection gConn = gConexion.conexion())
                {
                    using (OdbcDataAdapter adp = new OdbcDataAdapter(sQuery, gConn))
                    {
                        adp.Fill(dts_Totales);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al calcular los totales del flujo de efectivo: " + ex.Message);
            }

            return dts_Totales;
        }

        // ---------------------------------------------------------------------------------
        // Método: fun_consultar_flujo_efectivo_historico
        // Descripción: Devuelve las cuentas del flujo de efectivo filtradas por año y mes
        // desde la tabla tbl_historico_catalogo_cuentas.
        // ---------------------------------------------------------------------------------
        public DataTable fun_consultar_flujo_efectivo_historico(int iAnio, int iMes)
        {
            DataTable dts_FlujoHist = new DataTable();

            string sQuery = $@"
            SELECT 
                h.Pk_Codigo_Cuenta AS Cuenta,
                h.Cmp_CtaNombre AS Nombre,

                CASE 
                    WHEN h.Pk_Codigo_Cuenta LIKE '1.1%' 
                      OR h.Pk_Codigo_Cuenta LIKE '1.2%' 
                      OR h.Pk_Codigo_Cuenta LIKE '4%' 
                      OR h.Pk_Codigo_Cuenta LIKE '2.2%' 
                      OR h.Pk_Codigo_Cuenta LIKE '3.1%' 
                      OR h.Pk_Codigo_Cuenta LIKE '3.2%' 
                    THEN h.Cmp_CtaSaldoActual
                    ELSE 0
                END AS Entrada,

                CASE 
                    WHEN h.Pk_Codigo_Cuenta LIKE '5%' 
                      OR h.Pk_Codigo_Cuenta LIKE '6%' 
                      OR h.Pk_Codigo_Cuenta LIKE '1.5%' 
                      OR h.Pk_Codigo_Cuenta LIKE '1.6%' 
                    THEN h.Cmp_CtaSaldoActual
                    ELSE 0
                END AS Salida

            FROM tbl_historico_catalogo_cuentas h
            WHERE 
                (h.Pk_Codigo_Cuenta LIKE '1.1%' OR h.Pk_Codigo_Cuenta LIKE '1.2%' 
                 OR h.Pk_Codigo_Cuenta LIKE '1.5%' OR h.Pk_Codigo_Cuenta LIKE '1.6%'
                 OR h.Pk_Codigo_Cuenta LIKE '2.2%' OR h.Pk_Codigo_Cuenta LIKE '3.1%' 
                 OR h.Pk_Codigo_Cuenta LIKE '3.2%' OR h.Pk_Codigo_Cuenta LIKE '4%' 
                 OR h.Pk_Codigo_Cuenta LIKE '5%' OR h.Pk_Codigo_Cuenta LIKE '6%')
              AND h.Cmp_CtaSaldoActual <> 0
              AND h.Cmp_Anio = {iAnio} 
              AND h.Cmp_Mes = {iMes}
            ORDER BY h.Pk_Codigo_Cuenta;";

            try
            {
                using (OdbcConnection gConn = gConexion.conexion())
                {
                    using (OdbcDataAdapter adp = new OdbcDataAdapter(sQuery, gConn))
                    {
                        adp.Fill(dts_FlujoHist);
                    }
                }

                if (dts_FlujoHist.Rows.Count == 0)
                    throw new Exception($"No hay registros en el mes {iMes} del año {iAnio}.");
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los datos históricos del flujo de efectivo: " + ex.Message);
            }

            return dts_FlujoHist;
        }

        // ---------------------------------------------------------------------------------
        // Método: fun_calcular_totales_historico
        // Descripción: Calcula totales de entrada y salida del flujo histórico.
        // ---------------------------------------------------------------------------------
        public DataTable fun_calcular_totales_historico(int iAnio, int iMes)
        {
            DataTable dts_TotalesHist = new DataTable();

            string sQuery = $@"
                SELECT 
                    SUM(CASE 
                        WHEN Pk_Codigo_Cuenta LIKE '4%' OR Pk_Codigo_Cuenta LIKE '2.2%' 
                             OR Pk_Codigo_Cuenta LIKE '3.1%' OR Pk_Codigo_Cuenta LIKE '3.2%' 
                        THEN Cmp_CtaSaldoActual ELSE 0 END) AS TotalEntradas,

                    SUM(CASE 
                        WHEN Pk_Codigo_Cuenta LIKE '5%' OR Pk_Codigo_Cuenta LIKE '6%' 
                             OR Pk_Codigo_Cuenta LIKE '1.5%' OR Pk_Codigo_Cuenta LIKE '1.6%'
                        THEN Cmp_CtaSaldoActual ELSE 0 END) AS TotalSalidas
                FROM tbl_historico_catalogo_cuentas
                WHERE 
                    (Pk_Codigo_Cuenta LIKE '1.1%' OR Pk_Codigo_Cuenta LIKE '1.2%' 
                     OR Pk_Codigo_Cuenta LIKE '1.5%' OR Pk_Codigo_Cuenta LIKE '1.6%' 
                     OR Pk_Codigo_Cuenta LIKE '2.2%' OR Pk_Codigo_Cuenta LIKE '3.1%' 
                     OR Pk_Codigo_Cuenta LIKE '3.2%' OR Pk_Codigo_Cuenta LIKE '4%' 
                     OR Pk_Codigo_Cuenta LIKE '5%' OR Pk_Codigo_Cuenta LIKE '6%')
                  AND Cmp_Anio = {iAnio} 
                  AND Cmp_Mes = {iMes};";

            try
            {
                using (OdbcConnection gConn = gConexion.conexion())
                {
                    using (OdbcDataAdapter adp = new OdbcDataAdapter(sQuery, gConn))
                    {
                        adp.Fill(dts_TotalesHist);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al calcular los totales históricos del flujo de efectivo: " + ex.Message);
            }

            return dts_TotalesHist;
        }

    }
}

// Fin de código de Arón Ricardo Esquit Silva
// =====================================================================================
