using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;
using System.Data;
using System.Windows.Forms;

namespace Capa_Modelo_CierreContable
{

    public class Cls_CierreContable
    {
        Cls_conexión conexion = new Cls_conexión();

        public DataTable ObtenerCuentas()
        {
            DataTable dt = new DataTable();
            string query = @"
        SELECT 
            Pk_Codigo_Cuenta,
            Cmp_CtaNombre,
            Cmp_CtaMadre,
            Cmp_CtaSaldoInicial,
            Cmp_CtaCargoMes,
            Cmp_CtaAbonoMes,
            Cmp_CtaSaldoActual,
            Cmp_CtaCargoActual,
            Cmp_CtaAbonoActual,
            Cmp_CtaTipo,
            Cmp_CtaNaturaleza
        FROM Tbl_Catalogo_Cuentas
        ORDER BY Pk_Codigo_Cuenta;
    ";

            using (OdbcConnection conn = conexion.conexion())
            {
                using (OdbcDataAdapter da = new OdbcDataAdapter(query, conn))
                {
                    da.Fill(dt);
                }
            }

            return dt;
        }




        public void ActualizarSaldoCuenta(string codigoCuenta, decimal nuevoSaldo)
        {
            try
            {
                using (OdbcConnection conn = conexion.conexion())
                {
                    string query = "UPDATE Tbl_Catalogo_Cuentas SET Cmp_CtaSaldoActual = ? WHERE Pk_Codigo_Cuenta = ?";
                    using (OdbcCommand cmd = new OdbcCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Cmp_CtaSaldoActual", nuevoSaldo);
                        cmd.Parameters.AddWithValue("@Pk_Codigo_Cuenta", codigoCuenta);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (OdbcException ex)
            {
                Console.WriteLine("Error al actualizar el saldo: " + ex.Message);
            }
        }

        public bool ExisteHistoricoPeriodo(int anio, int mes)
        {
            using (OdbcConnection conn = conexion.AbrirConexion())
            {
                string query = "SELECT COUNT(*) FROM tbl_historico_catalogo_cuentas WHERE Cmp_Anio = ? AND Cmp_Mes = ?";
                using (OdbcCommand cmd = new OdbcCommand(query, conn))
                {
                    cmd.Parameters.Add("?", OdbcType.Int).Value = anio;
                    cmd.Parameters.Add("?", OdbcType.Int).Value = mes;

                    int total = Convert.ToInt32(cmd.ExecuteScalar());
                    return total > 0;
                }
            }
        }


        public DataTable ObtenerPolizas()
        {
            DataTable dt = new DataTable();
            string query = @"
        SELECT 
            e.Pk_EncCodigo_Poliza,
            c.Pk_Codigo_Cuenta,
            c.Cmp_CtaNombre,
            e.Cmp_Concepto_Poliza,
            e.Pk_Fecha_Poliza,
            
            
            
            CASE WHEN d.Cmp_Tipo_Poliza = 1 THEN d.Cmp_Valor_Poliza ELSE 0 END AS Debe,
            CASE WHEN d.Cmp_Tipo_Poliza = 0 THEN d.Cmp_Valor_Poliza ELSE 0 END AS Haber
        FROM Tbl_EncabezadoPoliza e
        INNER JOIN Tbl_DetallePoliza d ON e.Pk_EncCodigo_Poliza = d.PkFk_EncCodigo_Poliza
        INNER JOIN Tbl_Catalogo_Cuentas c ON d.PkFk_Codigo_Cuenta = c.Pk_Codigo_Cuenta
        ORDER BY e.Pk_EncCodigo_Poliza;
    ";

            using (OdbcConnection conn = conexion.AbrirConexion())
            {
                OdbcDataAdapter da = new OdbcDataAdapter(query, conn);
                da.Fill(dt);
            }

            return dt;
        }





        public DataTable ObtenerPolizasPorFechas(DateTime desde, DateTime hasta)
        {
            DataTable dt = new DataTable();
            string query = @"
        SELECT 
            e.Pk_EncCodigo_Poliza,
            c.Pk_Codigo_Cuenta,
            c.Cmp_CtaNombre,
            e.Cmp_Concepto_Poliza,
            e.Pk_Fecha_Poliza,
            CASE WHEN d.Cmp_Tipo_Poliza = 1 THEN d.Cmp_Valor_Poliza ELSE 0 END AS Debe,
            CASE WHEN d.Cmp_Tipo_Poliza = 0 THEN d.Cmp_Valor_Poliza ELSE 0 END AS Haber
        FROM Tbl_EncabezadoPoliza e
        INNER JOIN Tbl_DetallePoliza d ON e.Pk_EncCodigo_Poliza = d.PkFk_EncCodigo_Poliza
        INNER JOIN Tbl_Catalogo_Cuentas c ON d.PkFk_Codigo_Cuenta = c.Pk_Codigo_Cuenta
        WHERE e.Pk_Fecha_Poliza BETWEEN ? AND ?
        ORDER BY e.Pk_EncCodigo_Poliza;
    ";

            using (OdbcConnection conn = conexion.AbrirConexion())
            {
                OdbcDataAdapter da = new OdbcDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@desde", desde);
                da.SelectCommand.Parameters.AddWithValue("@hasta", hasta);
                da.Fill(dt);
            }

            return dt;
        }





        public bool GuardarHistorico(int anio, int mes)
        {
            // Evita duplicados
            if (ExisteCierre(anio, mes))
                return false;

            using (OdbcConnection conn = conexion.AbrirConexion())
            {
                string query = @"
            INSERT INTO Tbl_Historico_Catalogo_Cuentas
            (
                Cmp_Anio, Cmp_Mes, Pk_Codigo_Cuenta,
                Cmp_CtaNombre, Cmp_CtaMadre, Cmp_CtaSaldoInicial,
                Cmp_CtaCargoMes, Cmp_CtaAbonoMes, Cmp_CtaSaldoActual,
                Cmp_CtaCargoActual, Cmp_CtaAbonoActual,
                Cmp_CtaTipo, Cmp_CtaNaturaleza
            )
            SELECT
                ?, ?, Pk_Codigo_Cuenta,
                Cmp_CtaNombre, Cmp_CtaMadre, Cmp_CtaSaldoInicial,
                Cmp_CtaCargoMes, Cmp_CtaAbonoMes, Cmp_CtaSaldoActual,
                Cmp_CtaCargoActual, Cmp_CtaAbonoActual,
                Cmp_CtaTipo, Cmp_CtaNaturaleza
            FROM Tbl_Catalogo_Cuentas";

                using (OdbcCommand cmd = new OdbcCommand(query, conn))
                {
                    cmd.Parameters.Add("?", OdbcType.Int).Value = anio;
                    cmd.Parameters.Add("?", OdbcType.Int).Value = mes;

                    int filas = cmd.ExecuteNonQuery();
                    return filas > 0;
                }
            }
        }













        public bool ExisteCierre(int anio, int mes)
        {
            using (OdbcConnection conn = conexion.AbrirConexion())
            {
                string query = @"SELECT COUNT(*) 
                         FROM Tbl_Historico_Catalogo_Cuentas
                         WHERE Cmp_Anio = ? AND Cmp_Mes = ?";

                using (OdbcCommand cmd = new OdbcCommand(query, conn))
                {
                    cmd.Parameters.Add("?", OdbcType.Int).Value = anio;
                    cmd.Parameters.Add("?", OdbcType.Int).Value = mes;

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
        }



        public bool ExistePeriodoContable(int anio, int mes)
        {
            using (OdbcConnection conn = conexion.AbrirConexion())
            {
                string query = "SELECT COUNT(*) FROM tbl_periodoscontables WHERE Cmp_Anio = ? AND Cmp_Mes = ?";
                using (OdbcCommand cmd = new OdbcCommand(query, conn))
                {
                    cmd.Parameters.Add("?", OdbcType.Int).Value = anio;
                    cmd.Parameters.Add("?", OdbcType.Int).Value = mes;

                    int total = Convert.ToInt32(cmd.ExecuteScalar());
                    return total > 0;
                }
            }
        }






        public bool RegistrarPeriodo(int anio, int mes, DateTime inicio, DateTime fin, int estado, int modo)
        {
            using (OdbcConnection conn = conexion.AbrirConexion())
            {
                string query = @"
            INSERT INTO tbl_periodoscontables
            (Cmp_Anio, Cmp_Mes, Cmp_FechaInicio, Cmp_FechaFin, Cmp_Estado, Cmp_ModoActualizacion)
            VALUES (?, ?, ?, ?, ?, ?)";

                using (OdbcCommand cmd = new OdbcCommand(query, conn))
                {
                    cmd.Parameters.Add("?", OdbcType.Int).Value = anio;
                    cmd.Parameters.Add("?", OdbcType.Int).Value = mes;
                    cmd.Parameters.Add("?", OdbcType.Date).Value = inicio;
                    cmd.Parameters.Add("?", OdbcType.Date).Value = fin;
                    cmd.Parameters.Add("?", OdbcType.Int).Value = estado;
                    cmd.Parameters.Add("?", OdbcType.Int).Value = modo;

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }



        public void CerrarPeriodosAnteriores(int anio, int mes)
        {
            using (OdbcConnection conn = conexion.AbrirConexion())
            {
                string query = @"
            UPDATE tbl_periodoscontables
            SET Cmp_Estado = 0
            WHERE (Cmp_Anio < ?)
               OR (Cmp_Anio = ? AND Cmp_Mes < ?)";

                using (OdbcCommand cmd = new OdbcCommand(query, conn))
                {
                    cmd.Parameters.Add("?", OdbcType.Int).Value = anio;
                    cmd.Parameters.Add("?", OdbcType.Int).Value = anio;
                    cmd.Parameters.Add("?", OdbcType.Int).Value = mes;
                    cmd.ExecuteNonQuery();
                }
            }
        }





        public void ActualizarSaldosMensuales()
        {
            using (OdbcConnection conn = conexion.AbrirConexion())
            {
                string query = @"
            UPDATE Tbl_Catalogo_Cuentas 
            SET 
                Cmp_CtaCargoActual = Cmp_CtaCargoActual + Cmp_CtaCargoMes,
                Cmp_CtaAbonoActual = Cmp_CtaAbonoActual + Cmp_CtaAbonoMes,
                Cmp_CtaSaldoActual = Cmp_CtaSaldoActual + (Cmp_CtaCargoMes - Cmp_CtaAbonoMes),
                Cmp_CtaCargoMes = 0,
                Cmp_CtaAbonoMes = 0";

                using (OdbcCommand cmd = new OdbcCommand(query, conn))
                    cmd.ExecuteNonQuery();
            }
        }


        public void ActualizarSaldosAnuales()
        {
            using (OdbcConnection conn = conexion.AbrirConexion())
            {
                string query = @"
            UPDATE Tbl_Catalogo_Cuentas
            SET 
                Cmp_CtaSaldoInicial = Cmp_CtaSaldoActual,
                Cmp_CtaCargoMes = 0,
                Cmp_CtaAbonoMes = 0,
                Cmp_CtaCargoActual = 0,
                Cmp_CtaAbonoActual = 0,
                Cmp_CtaSaldoActual = 0";

                using (OdbcCommand cmd = new OdbcCommand(query, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public bool GuardarHistoricoDesdeLista(int anio, int mes, List<string> codigosCuentas)
        {
            using (OdbcConnection conn = conexion.AbrirConexion())
            using (OdbcTransaction trans = conn.BeginTransaction())
            {
                try
                {
                    foreach (string codigo in codigosCuentas)
                    {
                        // 1️⃣ Obtener saldo del mes anterior
                        decimal saldoAnterior = 0;
                        using (OdbcCommand cmdPrev = new OdbcCommand(@"
                    SELECT Cmp_CtaSaldoActual 
                    FROM Tbl_Historico_Catalogo_Cuentas
                    WHERE Pk_Codigo_Cuenta = ? 
                      AND Cmp_Anio = ? 
                      AND Cmp_Mes = ?", conn, trans))
                        {
                            cmdPrev.Parameters.Add("?", OdbcType.VarChar).Value = codigo;
                            cmdPrev.Parameters.Add("?", OdbcType.Int).Value = anio;
                            cmdPrev.Parameters.Add("?", OdbcType.Int).Value = mes - 1;

                            object result = cmdPrev.ExecuteScalar();
                            if (result != null && result != DBNull.Value)
                                saldoAnterior = Convert.ToDecimal(result);
                        }

                        // 2️⃣ Obtener movimientos del mes actual
                        decimal totalCargos = 0, totalAbonos = 0;
                        using (OdbcCommand cmdMov = new OdbcCommand(@"
                    SELECT 
                        SUM(CASE WHEN d.Cmp_Tipo_Poliza = 1 THEN d.Cmp_Valor_Poliza ELSE 0 END) AS TotalCargos,
                        SUM(CASE WHEN d.Cmp_Tipo_Poliza = 0 THEN d.Cmp_Valor_Poliza ELSE 0 END) AS TotalAbonos
                    FROM Tbl_DetallePoliza d
                    INNER JOIN Tbl_EncabezadoPoliza e 
                        ON e.Pk_EncCodigo_Poliza = d.PkFk_EncCodigo_Poliza
                    WHERE d.PkFk_Codigo_Cuenta = ?
                      AND MONTH(e.Pk_Fecha_Poliza) = ?
                      AND YEAR(e.Pk_Fecha_Poliza) = ?", conn, trans))
                        {
                            cmdMov.Parameters.Add("?", OdbcType.VarChar).Value = codigo;
                            cmdMov.Parameters.Add("?", OdbcType.Int).Value = mes;
                            cmdMov.Parameters.Add("?", OdbcType.Int).Value = anio;

                            using (OdbcDataReader dr = cmdMov.ExecuteReader())
                            {
                                if (dr.Read())
                                {
                                    totalCargos = dr.IsDBNull(0) ? 0 : dr.GetDecimal(0);
                                    totalAbonos = dr.IsDBNull(1) ? 0 : dr.GetDecimal(1);
                                }
                            }
                        }

                        // 3️⃣ Calcular nuevo saldo
                        decimal nuevoSaldo = saldoAnterior + (totalCargos - totalAbonos);

                        // 4️⃣ Insertar histórico
                        using (OdbcCommand cmdInsert = new OdbcCommand(@"
                    INSERT INTO Tbl_Historico_Catalogo_Cuentas
                    (Cmp_Anio, Cmp_Mes, Pk_Codigo_Cuenta,
                     Cmp_CtaNombre, Cmp_CtaMadre, 
                     Cmp_CtaSaldoInicial, Cmp_CtaCargoMes, Cmp_CtaAbonoMes, 
                     Cmp_CtaSaldoActual, Cmp_CtaTipo, Cmp_CtaNaturaleza)
                    SELECT ?, ?, Pk_Codigo_Cuenta, Cmp_CtaNombre, Cmp_CtaMadre,
                           ?, ?, ?, ?, Cmp_CtaTipo, Cmp_CtaNaturaleza
                    FROM Tbl_Catalogo_Cuentas
                    WHERE Pk_Codigo_Cuenta = ?", conn, trans))
                        {
                            cmdInsert.Parameters.Add("?", OdbcType.Int).Value = anio;
                            cmdInsert.Parameters.Add("?", OdbcType.Int).Value = mes;
                            cmdInsert.Parameters.Add("?", OdbcType.Decimal).Value = saldoAnterior;
                            cmdInsert.Parameters.Add("?", OdbcType.Decimal).Value = totalCargos;
                            cmdInsert.Parameters.Add("?", OdbcType.Decimal).Value = totalAbonos;
                            cmdInsert.Parameters.Add("?", OdbcType.Decimal).Value = nuevoSaldo;
                            cmdInsert.Parameters.Add("?", OdbcType.VarChar).Value = codigo;

                            cmdInsert.ExecuteNonQuery();
                        }

                        // 5️⃣ Actualizar saldo actual en el catálogo
                        using (OdbcCommand cmdUpdate = new OdbcCommand(@"
                    UPDATE Tbl_Catalogo_Cuentas
                    SET Cmp_CtaSaldoActual = ?
                    WHERE Pk_Codigo_Cuenta = ?", conn, trans))
                        {
                            cmdUpdate.Parameters.Add("?", OdbcType.Decimal).Value = nuevoSaldo;
                            cmdUpdate.Parameters.Add("?", OdbcType.VarChar).Value = codigo;
                            cmdUpdate.ExecuteNonQuery();
                        }
                    }

                    trans.Commit();
                    MessageBox.Show("✅ Cierre contable guardado con acumulación correcta.");
                    return true;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    MessageBox.Show("❌ ERROR en GuardarHistoricoDesdeLista:\n" + ex.Message);
                    return false;
                }
            }
        }






















    }


}




