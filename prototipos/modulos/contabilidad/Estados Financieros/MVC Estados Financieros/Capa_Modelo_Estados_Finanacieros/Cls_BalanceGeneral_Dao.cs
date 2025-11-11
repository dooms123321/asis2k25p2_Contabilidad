// =====================================================================================
// Autor: Arón Ricardo Esquit Silva
// Carné: 0901-22-13036
// Fecha: 10/11/2025
// Descripción: DAO del Balance General (modo actual e histórico)
// =====================================================================================

using System;
using System.Data;
using System.Data.Odbc;

namespace Capa_Modelo_Estados_Financieros
{
    public class Cls_BalanceGeneral_Dao
    {
        private readonly Cls_Conexion gConexion = new Cls_Conexion();

        // -------------------------------------------------------------------------
        // =========================   MODO ACTUAL   ================================
        // -------------------------------------------------------------------------

        // Consulta general de cuentas contables (nivel 1, 2 o 3)
        public DataTable Fun_Consultar_Balance_General(int iNivel)
        {
            DataTable dts_Cuentas = new DataTable();
            try
            {
                string sComando = @"
                    SELECT 
                        Pk_Codigo_Cuenta AS Codigo,
                        Cmp_CtaNombre AS Nombre,
                        Cmp_CtaTipo AS Tipo,
                        Cmp_CtaNaturaleza AS Naturaleza,
                        Cmp_CtaSaldoActual AS SALDO_ACTUAL
                    FROM Tbl_Catalogo_Cuentas
                    WHERE LEFT(Pk_Codigo_Cuenta,1) IN ('1','2','3')
                    ORDER BY Pk_Codigo_Cuenta;
                ";

                using (OdbcConnection conn = gConexion.AbrirConexion())
                {
                    using (OdbcCommand cmd = new OdbcCommand(sComando, conn))
                    {
                        OdbcDataAdapter adapter = new OdbcDataAdapter(cmd);
                        adapter.Fill(dts_Cuentas);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al consultar el balance general: " + ex.Message);
            }
            return dts_Cuentas;
        }

        // Consulta de totales por tipo de cuenta (Activo, Pasivo, Capital)
        public DataTable Fun_Obtener_Totales_Balance()
        {
            DataTable dts_Totales = new DataTable();
            try
            {
                string sComando = @"
                    SELECT 
                        SUM(CASE WHEN LEFT(Pk_Codigo_Cuenta,1)='1' THEN Cmp_CtaSaldoActual ELSE 0 END) AS Total_Activo,
                        SUM(CASE WHEN LEFT(Pk_Codigo_Cuenta,1)='2' THEN Cmp_CtaSaldoActual ELSE 0 END) AS Total_Pasivo,
                        SUM(CASE WHEN LEFT(Pk_Codigo_Cuenta,1)='3' THEN Cmp_CtaSaldoActual ELSE 0 END) AS Total_Capital
                    FROM Tbl_Catalogo_Cuentas
                    WHERE LEFT(Pk_Codigo_Cuenta,1) IN ('1','2','3');
                ";

                using (OdbcConnection conn = gConexion.AbrirConexion())
                {
                    using (OdbcCommand cmd = new OdbcCommand(sComando, conn))
                    {
                        OdbcDataAdapter adapter = new OdbcDataAdapter(cmd);
                        adapter.Fill(dts_Totales);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener totales del balance general: " + ex.Message);
            }
            return dts_Totales;
        }

        // Consulta jerárquica de saldos acumulados por cuenta
        public DataTable Fun_Consultar_SaldosJerarquicos()
        {
            DataTable dts_Saldos = new DataTable();
            try
            {
                string sComando = @"
                    WITH RECURSIVE CuentasJerarquia AS (
                        SELECT 
                            c.Pk_Codigo_Cuenta,
                            c.Cmp_CtaNombre,
                            c.Cmp_CtaMadre,
                            c.Cmp_CtaSaldoActual
                        FROM Tbl_Catalogo_Cuentas c
                
                        UNION ALL
                
                        SELECT 
                            h.Pk_Codigo_Cuenta,
                            h.Cmp_CtaNombre,
                            h.Cmp_CtaMadre,
                            h.Cmp_CtaSaldoActual
                        FROM Tbl_Catalogo_Cuentas h
                        INNER JOIN CuentasJerarquia cj 
                            ON h.Cmp_CtaMadre = cj.Pk_Codigo_Cuenta
                    ),
                    SaldosJerarquicos AS (
                        SELECT 
                            c1.Pk_Codigo_Cuenta,
                            COALESCE(SUM(c2.Cmp_CtaSaldoActual), 0) AS Saldo_Acumulado
                        FROM Tbl_Catalogo_Cuentas c1
                        LEFT JOIN Tbl_Catalogo_Cuentas c2
                            ON c2.Pk_Codigo_Cuenta LIKE CONCAT(c1.Pk_Codigo_Cuenta, '%')
                        GROUP BY c1.Pk_Codigo_Cuenta
                 )
                    SELECT 
                        c.Pk_Codigo_Cuenta AS Codigo,
                        c.Cmp_CtaNombre AS Nombre,
                        c.Cmp_CtaTipo AS Tipo,
                        c.Cmp_CtaNaturaleza AS Naturaleza,
                        s.Saldo_Acumulado AS SALDO_ACTUAL
                    FROM Tbl_Catalogo_Cuentas c
                    JOIN SaldosJerarquicos s ON c.Pk_Codigo_Cuenta = s.Pk_Codigo_Cuenta
                    WHERE LEFT(c.Pk_Codigo_Cuenta,1) IN ('1','2','3')
                    ORDER BY c.Pk_Codigo_Cuenta;
               ";

                using (OdbcConnection conn = gConexion.AbrirConexion())
                {
                    using (OdbcCommand cmd = new OdbcCommand(sComando, conn))
                    {
                        OdbcDataAdapter adapter = new OdbcDataAdapter(cmd);
                        adapter.Fill(dts_Saldos);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al consultar saldos jerárquicos: " + ex.Message);
            }
            return dts_Saldos;
        }


        // -------------------------------------------------------------------------
        // =========================   MODO HISTÓRICO   =============================
        // -------------------------------------------------------------------------

        // Consulta general de cuentas contables históricas (nivel 1, 2 o 3)
        public DataTable Fun_Consultar_Balance_General_Historico(int iNivel, int iAnio, int iMes)
        {
            DataTable dts_Cuentas = new DataTable();
            try
            {
                string sComando = @"
                    SELECT 
                        Pk_Codigo_Cuenta AS Codigo,
                        Cmp_CtaNombre AS Nombre,
                        Cmp_CtaTipo AS Tipo,
                        Cmp_CtaNaturaleza AS Naturaleza,
                        Cmp_CtaSaldoActual AS SALDO_ACTUAL
                    FROM Tbl_Historico_Catalogo_Cuentas
                    WHERE LEFT(Pk_Codigo_Cuenta,1) IN ('1','2','3')
                      AND Cmp_Anio = " + iAnio + @"
                      AND Cmp_Mes  = " + iMes + @"
                    ORDER BY Pk_Codigo_Cuenta;
                ";

                using (OdbcConnection conn = gConexion.AbrirConexion())
                {
                    using (OdbcCommand cmd = new OdbcCommand(sComando, conn))
                    {
                        OdbcDataAdapter adapter = new OdbcDataAdapter(cmd);
                        adapter.Fill(dts_Cuentas);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al consultar el balance general histórico: " + ex.Message);
            }
            return dts_Cuentas;
        }

        // Consulta de totales históricos (Activo, Pasivo, Capital)
        public DataTable Fun_Obtener_Totales_Balance_Historico(int iAnio, int iMes)
        {
            DataTable dts_Totales = new DataTable();
            try
            {
                string sComando = @"
                    SELECT 
                        SUM(CASE WHEN LEFT(Pk_Codigo_Cuenta,1)='1' THEN Cmp_CtaSaldoActual ELSE 0 END) AS Total_Activo,
                        SUM(CASE WHEN LEFT(Pk_Codigo_Cuenta,1)='2' THEN Cmp_CtaSaldoActual ELSE 0 END) AS Total_Pasivo,
                        SUM(CASE WHEN LEFT(Pk_Codigo_Cuenta,1)='3' THEN Cmp_CtaSaldoActual ELSE 0 END) AS Total_Capital
                    FROM Tbl_Historico_Catalogo_Cuentas
                    WHERE LEFT(Pk_Codigo_Cuenta,1) IN ('1','2','3')
                      AND Cmp_Anio = " + iAnio + @"
                      AND Cmp_Mes  = " + iMes + @";
                ";

                using (OdbcConnection conn = gConexion.AbrirConexion())
                {
                    using (OdbcCommand cmd = new OdbcCommand(sComando, conn))
                    {
                        OdbcDataAdapter adapter = new OdbcDataAdapter(cmd);
                        adapter.Fill(dts_Totales);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener totales del balance general histórico: " + ex.Message);
            }
            return dts_Totales;
        }

        // Consulta jerárquica histórica de saldos acumulados por cuenta
        public DataTable Fun_Consultar_SaldosJerarquicos_Historico(int iAnio, int iMes)
        {
            DataTable dts_Saldos = new DataTable();
            try
            {
                string sComando = @"
                    WITH RECURSIVE CuentasJerarquia AS (
                        SELECT 
                            c.Pk_Codigo_Cuenta,
                            c.Cmp_CtaNombre,
                            c.Cmp_CtaMadre,
                            c.Cmp_CtaSaldoActual
                        FROM Tbl_Historico_Catalogo_Cuentas c
                        WHERE Cmp_Anio = " + iAnio + @" 
                          AND Cmp_Mes  = " + iMes + @"
                
                        UNION ALL
                
                        SELECT 
                            h.Pk_Codigo_Cuenta,
                            h.Cmp_CtaNombre,
                            h.Cmp_CtaMadre,
                            h.Cmp_CtaSaldoActual
                        FROM Tbl_Historico_Catalogo_Cuentas h
                        INNER JOIN CuentasJerarquia cj 
                            ON h.Cmp_CtaMadre = cj.Pk_Codigo_Cuenta
                            AND h.Cmp_Anio = " + iAnio + @"
                            AND h.Cmp_Mes  = " + iMes + @"
                    ),
                    SaldosJerarquicos AS (
                        SELECT 
                            c1.Pk_Codigo_Cuenta,
                            COALESCE(SUM(c2.Cmp_CtaSaldoActual), 0) AS Saldo_Acumulado
                        FROM Tbl_Historico_Catalogo_Cuentas c1
                        LEFT JOIN Tbl_Historico_Catalogo_Cuentas c2
                            ON c2.Pk_Codigo_Cuenta LIKE CONCAT(c1.Pk_Codigo_Cuenta, '%')
                           AND c2.Cmp_Anio = " + iAnio + @"
                           AND c2.Cmp_Mes  = " + iMes + @"
                        WHERE c1.Cmp_Anio = " + iAnio + @"
                          AND c1.Cmp_Mes  = " + iMes + @"
                        GROUP BY c1.Pk_Codigo_Cuenta
                 )
                    SELECT 
                        c.Pk_Codigo_Cuenta AS Codigo,
                        c.Cmp_CtaNombre AS Nombre,
                        c.Cmp_CtaTipo AS Tipo,
                        c.Cmp_CtaNaturaleza AS Naturaleza,
                        s.Saldo_Acumulado AS SALDO_ACTUAL
                    FROM Tbl_Historico_Catalogo_Cuentas c
                    JOIN SaldosJerarquicos s 
                        ON c.Pk_Codigo_Cuenta = s.Pk_Codigo_Cuenta
                       AND c.Cmp_Anio = " + iAnio + @"
                       AND c.Cmp_Mes  = " + iMes + @"
                    WHERE LEFT(c.Pk_Codigo_Cuenta,1) IN ('1','2','3')
                    ORDER BY c.Pk_Codigo_Cuenta;
               ";

                using (OdbcConnection conn = gConexion.AbrirConexion())
                {
                    using (OdbcCommand cmd = new OdbcCommand(sComando, conn))
                    {
                        OdbcDataAdapter adapter = new OdbcDataAdapter(cmd);
                        adapter.Fill(dts_Saldos);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al consultar saldos jerárquicos históricos: " + ex.Message);
            }
            return dts_Saldos;
        }
    }
}

// Fin del código de Arón Ricardo Esquit Silva
// =====================================================================================
