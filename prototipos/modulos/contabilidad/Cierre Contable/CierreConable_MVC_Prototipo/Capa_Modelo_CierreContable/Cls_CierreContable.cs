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
            {
                using (OdbcTransaction trans = conn.BeginTransaction())
                {


                    try
                    {

                        OdbcCommand check = new OdbcCommand(
    "SELECT COUNT(*) FROM tbl_historico_catalogo_cuentas WHERE Cmp_Anio = ? AND Cmp_Mes = ?", conn, trans);

                        check.Parameters.Add("?", OdbcType.Int).Value = anio;
                        check.Parameters.Add("?", OdbcType.Int).Value = mes;

                        int total = Convert.ToInt32(check.ExecuteScalar());
                        MessageBox.Show("Registros existentes para este periodo: " + total);



                        foreach (string codigo in codigosCuentas)
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
                    SELECT ?, ?, Pk_Codigo_Cuenta,
                           Cmp_CtaNombre, Cmp_CtaMadre, Cmp_CtaSaldoInicial,
                           Cmp_CtaCargoMes, Cmp_CtaAbonoMes, Cmp_CtaSaldoActual,
                           Cmp_CtaCargoActual, Cmp_CtaAbonoActual,
                           Cmp_CtaTipo, Cmp_CtaNaturaleza
                    FROM Tbl_Catalogo_Cuentas
                    WHERE Pk_Codigo_Cuenta = ?";

                            using (OdbcCommand cmd = new OdbcCommand(query, conn, trans))
                            {
                                cmd.Parameters.Add("?", OdbcType.Int).Value = anio;
                                cmd.Parameters.Add("?", OdbcType.Int).Value = mes;
                                cmd.Parameters.Add("?", OdbcType.VarChar).Value = codigo;
                                cmd.ExecuteNonQuery();
                            }
                        }

                        trans.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        MessageBox.Show("❌ ERROR SQL:\n" + ex.Message, "Error en inserción",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                }
            }
            }
        }




    }




