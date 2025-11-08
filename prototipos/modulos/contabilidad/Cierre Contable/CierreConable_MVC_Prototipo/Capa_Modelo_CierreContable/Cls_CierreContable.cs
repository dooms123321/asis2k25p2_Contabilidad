using System;
<<<<<<< HEAD
using System.Data;
using System.Data.Odbc;
=======
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;
using System.Data;


>>>>>>> e42e22a9edf48ad572659449d0ea79beb894f129


namespace Capa_Modelo_CierreContable
{
<<<<<<< HEAD
    public class Cls_CierreContable
    {
        private readonly Cls_conexión conexion = new Cls_conexión();
=======

    public class Cls_CierreContable
    {
        Cls_conexión conexion = new Cls_conexión();
>>>>>>> e42e22a9edf48ad572659449d0ea79beb894f129

        public DataTable ObtenerCuentas()
        {
            DataTable dt = new DataTable();
            string query = "SELECT Pk_EncCodigo_Poliza, PK_Fecha_Poliza, Cmp_Concepto_Poliza, Cmp_Valor_Poliza, Cmp_Estado_Poliza FROM Tbl_EncabezadoPoliza";

            using (OdbcConnection conn = conexion.conexion())
<<<<<<< HEAD
            using (OdbcDataAdapter da = new OdbcDataAdapter(query, conn))
            {
                da.Fill(dt);
            }

            return dt;
        }

      
        public void ActualizarSaldoCuenta(string codigoCuenta, decimal nuevoSaldo)
        {
            try
            {
                string query = "UPDATE Tbl_Catalogo_Cuentas SET Cmp_CtaSaldoActual = ? WHERE Pk_Codigo_Cuenta = ?";

                using (OdbcConnection conn = conexion.conexion())
                using (OdbcCommand cmd = new OdbcCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("", nuevoSaldo);
                    cmd.Parameters.AddWithValue("", codigoCuenta);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (OdbcException ex)
            {
                Console.WriteLine("Error al actualizar el saldo: " + ex.Message);
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
                FROM Tbl_encabezadopoliza e
                INNER JOIN Tbl_DetallePoliza d ON e.Pk_EncCodigo_Poliza = d.PkFk_EncCodigo_Poliza
                INNER JOIN Tbl_Catalogo_Cuentas c ON d.PkFk_Codigo_Cuenta = c.Pk_Codigo_Cuenta
                ORDER BY e.Pk_EncCodigo_Poliza;";

            using (OdbcConnection conn = conexion.conexion())
            using (OdbcDataAdapter da = new OdbcDataAdapter(query, conn))
            {
                da.Fill(dt);
            }

            return dt;
        }


       



        public DataTable ObtenerPolizasPorFechas(DateTime fechaDesde, DateTime fechaHasta)
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
FROM tbl_encabezadopoliza e
INNER JOIN tbl_detallepoliza d ON e.Pk_EncCodigo_Poliza = d.PkFk_EncCodigo_Poliza
INNER JOIN tbl_catalogo_cuentas c ON d.PkFk_Codigo_Cuenta = c.Pk_Codigo_Cuenta
WHERE e.Pk_Fecha_Poliza BETWEEN ? AND ?
ORDER BY e.Pk_EncCodigo_Poliza";




            using (OdbcConnection conn = conexion.conexion())
            using (OdbcCommand cmd = new OdbcCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("", fechaDesde);
                cmd.Parameters.AddWithValue("", fechaHasta);

                using (OdbcDataAdapter da = new OdbcDataAdapter(cmd))
=======
            {
                using (OdbcDataAdapter da = new OdbcDataAdapter(query, conn))
>>>>>>> e42e22a9edf48ad572659449d0ea79beb894f129
                {
                    da.Fill(dt);
                }
            }

            return dt;
        }

<<<<<<< HEAD


        public void InsertarCierreContable(
    DateTime fechaCierre,
    string periodo,
    DateTime fechaDesde,
    DateTime fechaHasta,
    decimal debe,
    decimal haber,
    decimal saldoAnterior,
    decimal saldoFinal,
    string observaciones)
        {
            string query = @"
        INSERT INTO tbl_registro_cierre_contable
        (Fk_Codigo_de_Cuenta, Cmp_Fecha_de_Cierre, Cmp_Periodo_de_Tiempo,
         Cmp_Fecha_Desde, Cmp_Fecha_Hasta, Cmp_Movimiento_Debe,
         Cmp_Movimiento_Haber, Saldo_Anterior, Saldo_Final, Observaciones)
        VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

            using (OdbcConnection conn = conexion.conexion())
            using (OdbcCommand cmd = new OdbcCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("", "3.2.3");
                cmd.Parameters.AddWithValue("", fechaCierre);
                cmd.Parameters.AddWithValue("", periodo);
                cmd.Parameters.AddWithValue("", fechaDesde);
                cmd.Parameters.AddWithValue("", fechaHasta);
                cmd.Parameters.AddWithValue("", debe);
                cmd.Parameters.AddWithValue("", haber);
                cmd.Parameters.AddWithValue("", saldoAnterior);
                cmd.Parameters.AddWithValue("", saldoFinal);
                cmd.Parameters.AddWithValue("", observaciones);

                cmd.ExecuteNonQuery();
=======
        public void InsertarCierre(string codigoCuenta, DateTime fecha, string periodo, decimal debe, decimal haber, decimal saldoAnterior, decimal saldoFinal, string observaciones)
        {
            string query = @"INSERT INTO Tbl_Registro_Cierre_Contable 
                        (Fk_Codigo_de_Cuenta, Cmp_Fecha_de_Cierre, Cmp_Periodo_de_Tiempo, 
                         Cmp_Movimiento_Debe, Cmp_Movimiento_Haber, 
                         Saldo_Anterior, Saldo_Final, Observaciones)
                         VALUES (?,?,?,?,?,?,?,?)";

            using (OdbcConnection conn = conexion.conexion())
            {
                using (OdbcCommand cmd = new OdbcCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@codigoCuenta", codigoCuenta);
                    cmd.Parameters.AddWithValue("@fecha", fecha);
                    cmd.Parameters.AddWithValue("@periodo", periodo);
                    cmd.Parameters.AddWithValue("@debe", debe);
                    cmd.Parameters.AddWithValue("@haber", haber);
                    cmd.Parameters.AddWithValue("@saldoAnterior", saldoAnterior);
                    cmd.Parameters.AddWithValue("@saldoFinal", saldoFinal);
                    cmd.Parameters.AddWithValue("@observaciones", observaciones);

                    cmd.ExecuteNonQuery();
                }
            }
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
        INNER JOIN Tbl_Detalle_Poliza d ON e.Pk_EncCodigo_Poliza = d.PkFk_EncCodigo_Poliza
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




        public void InsertarCierreContable(string codigoCuenta, DateTime fechaCierre, string periodo,
                                   DateTime fechaDesde, DateTime fechaHasta,
                                   decimal debe, decimal haber, string observaciones)
        {
            string query = @"INSERT INTO Tbl_Registro_Cierre_Contable 
        (Fk_Codigo_Cuenta, Cmp_Fecha_de_Cierre, Cmp_Periodo_de_Tiempo, 
         Cmp_Fecha_Desde, Cmp_Fecha_Hasta, Cmp_Movimiento_Debe, 
         Cmp_Movimiento_Haber, Observaciones)
         VALUES (?,?,?,?,?,?,?,?)";

            using (OdbcConnection conn = conexion.conexion())
            {
                using (OdbcCommand cmd = new OdbcCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("Fk_Codigo_Cuenta", codigoCuenta);
                    cmd.Parameters.AddWithValue("Cmp_Fecha_de_Cierre", fechaCierre);
                    cmd.Parameters.AddWithValue("Cmp_Periodo_de_Tiempo", periodo);
                    cmd.Parameters.AddWithValue("Cmp_Fecha_Desde", fechaDesde);
                    cmd.Parameters.AddWithValue("Cmp_Fecha_Hasta", fechaHasta);
                    cmd.Parameters.AddWithValue("Cmp_Movimiento_Debe", debe);
                    cmd.Parameters.AddWithValue("Cmp_Movimiento_Haber", haber);
                    cmd.Parameters.AddWithValue("Observaciones", observaciones);

                    cmd.ExecuteNonQuery();
                }
>>>>>>> e42e22a9edf48ad572659449d0ea79beb894f129
            }
        }





<<<<<<< HEAD
    }

=======



    }



>>>>>>> e42e22a9edf48ad572659449d0ea79beb894f129
}
