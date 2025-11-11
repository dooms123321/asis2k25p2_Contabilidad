// =====================================================================================
// Autor: Arón Ricardo Esquit Silva
// Carné: 0901-22-13036
// Fecha: 11/11/2025
// Descripción: DAO para guardar el Balance General (niveles 1, 2, 3) con mensajes
// =====================================================================================
using System;
using System.Data;
using System.Data.Odbc;

namespace Capa_Modelo_Estados_Financieros
{
    public class Cls_Reporte_BalanceGeneral_Dao
    {
        private readonly Cls_Conexion gConexion = new Cls_Conexion();

        // ---------------------------------------------------------------------------------
        // Método: Fun_Obtener_Periodo_Activo
        // Descripción: Devuelve el periodo activo (año-mes)
        // ---------------------------------------------------------------------------------
        public string Fun_Obtener_Periodo_Activo()
        {
            string sPeriodo = "0000-00";
            try
            {
                using (OdbcConnection gConn = gConexion.conexion())
                {
                    string sQuery = "SELECT CONCAT(Cmp_Anio, '-', LPAD(Cmp_Mes, 2, '0')) FROM Tbl_PeriodosContables WHERE Cmp_Estado = 1 LIMIT 1;";
                    using (OdbcCommand cmd = new OdbcCommand(sQuery, gConn))
                    {
                        object result = cmd.ExecuteScalar();
                        if (result != null)
                            sPeriodo = result.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener periodo: " + ex.Message);
            }
            return sPeriodo;
        }

        // ---------------------------------------------------------------------------------
        // Método: Fun_Guardar_Reporte
        // Descripción: Guarda los datos del Balance General (niveles 1, 2, 3)
        // ---------------------------------------------------------------------------------
        public string Fun_Guardar_Reporte(DataTable dtsBalance, string sPeriodo, bool esHistorico)
        {
            try
            {
                if (esHistorico)
                    return "Modo Histórico: no se permite guardar";

                if (dtsBalance == null || dtsBalance.Rows.Count == 0)
                    return "No hay datos para guardar";

                using (OdbcConnection gConn = gConexion.conexion())
                {
                    // Verificar si ya existe un reporte idéntico
                    // 🔹 Validar si ya existe un reporte guardado para este periodo
                    string sVerificar = "SELECT COUNT(*) FROM Tbl_Reporte_Balance_General WHERE Cmp_Periodo = ?";
                    using (OdbcCommand cmdVerificar = new OdbcCommand(sVerificar, gConn))
                    {
                        cmdVerificar.Parameters.AddWithValue("?", sPeriodo);
                        int iExiste = Convert.ToInt32(cmdVerificar.ExecuteScalar());

                        if (iExiste > 0)
                            return "No se guardó el reporte, ya existe uno idéntico para este periodo.";
                    }


                    // Eliminar reporte existente del periodo
                    string sDelete = "DELETE FROM Tbl_Reporte_Balance_General WHERE Cmp_Periodo = ?";
                    using (OdbcCommand cmdDel = new OdbcCommand(sDelete, gConn))
                    {
                        cmdDel.Parameters.AddWithValue("?", sPeriodo);
                        cmdDel.ExecuteNonQuery();
                    }

                    // 🔹 Insertar todos los niveles (1, 2, 3)
                    int iInsertados = 0;
                    foreach (DataRow fila in dtsBalance.Rows)
                    {
                        string sCuenta = fila["Cuenta"].ToString().Trim();
                        string sNombre = fila["Nombre"].ToString().Trim();
                        string sDebe = fila.Table.Columns.Contains("Debe") ? fila["Debe"].ToString() : "0";
                        string sHaber = fila.Table.Columns.Contains("Haber") ? fila["Haber"].ToString() : "0";

                        // Saltar filas vacías o totales de diferencia
                        if (string.IsNullOrWhiteSpace(sCuenta) || string.IsNullOrWhiteSpace(sNombre))
                            continue;

                        // Limpiar valores
                        decimal deValor = Fun_Limpiar_Valor(sDebe, sHaber);
                        string sTipo = Fun_Obtener_Tipo_Cuenta(sCuenta);

                        string sInsert = @"INSERT INTO Tbl_Reporte_Balance_General
                                            (Fk_Codigo_Cuenta, Cmp_Nombre_Cuenta, Cmp_Tipo_Cuenta, Cmp_Valor, Cmp_Periodo)
                                           VALUES (?, ?, ?, ?, ?)";
                        using (OdbcCommand cmdInsert = new OdbcCommand(sInsert, gConn))
                        {
                            cmdInsert.Parameters.AddWithValue("?", sCuenta);
                            cmdInsert.Parameters.AddWithValue("?", sNombre);
                            cmdInsert.Parameters.AddWithValue("?", sTipo);
                            cmdInsert.Parameters.AddWithValue("?", deValor);
                            cmdInsert.Parameters.AddWithValue("?", sPeriodo);
                            cmdInsert.ExecuteNonQuery();
                            iInsertados++;
                        }
                    }

                    return iInsertados > 0
                        ? "Reporte del Balance General guardado correctamente."
                        : "No se insertaron registros.";
                }
            }
            catch (Exception ex)
            {
                return "Error al guardar: " + ex.Message;
            }
        }

        // ---------------------------------------------------------------------------------
        // Método: Fun_Verificar_Reporte_Identico
        // Descripción: Verifica si ya existe un reporte idéntico para el periodo
        // ---------------------------------------------------------------------------------
        private bool Fun_Verificar_Reporte_Identico(DataTable dtsNuevo, string sPeriodo, OdbcConnection gConn)
        {
            try
            {
                DataTable dtsExistente = new DataTable();
                string sSelect = "SELECT Fk_Codigo_Cuenta, Cmp_Nombre_Cuenta, Cmp_Valor FROM Tbl_Reporte_Balance_General WHERE Cmp_Periodo = ?";
                using (OdbcCommand cmdSel = new OdbcCommand(sSelect, gConn))
                {
                    cmdSel.Parameters.AddWithValue("?", sPeriodo);
                    using (OdbcDataAdapter da = new OdbcDataAdapter(cmdSel))
                    {
                        da.Fill(dtsExistente);
                    }
                }

                if (dtsExistente.Rows.Count == 0)
                    return false;

                if (dtsExistente.Rows.Count != dtsNuevo.Rows.Count)
                    return false;

                foreach (DataRow fila in dtsNuevo.Rows)
                {
                    string sCuenta = fila["Cuenta"].ToString();
                    string sNombre = fila["Nombre"].ToString();
                    decimal deValor = Fun_Limpiar_Valor(fila["Debe"].ToString(), fila["Haber"].ToString());

                    DataRow[] filaExistente = dtsExistente.Select($"Fk_Codigo_Cuenta = '{sCuenta}'");
                    if (filaExistente.Length == 0)
                        return false;

                    decimal deValorExistente = Convert.ToDecimal(filaExistente[0]["Cmp_Valor"]);
                    if (filaExistente[0]["Cmp_Nombre_Cuenta"].ToString() != sNombre ||
                        Math.Abs(deValorExistente - deValor) > 0.01m)
                        return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        // ---------------------------------------------------------------------------------
        // Método: Fun_Limpiar_Valor
        // Descripción: Limpia texto y convierte a decimal
        // ---------------------------------------------------------------------------------
        private decimal Fun_Limpiar_Valor(string sDebe, string sHaber)
        {
            string sValorTexto = !string.IsNullOrEmpty(sDebe) ? sDebe : sHaber;
            if (string.IsNullOrEmpty(sValorTexto))
                return 0;

            string sLimpio = sValorTexto.Replace("Q", "").Replace("X", "").Replace(",", "")
                                       .Replace("[", "").Replace("]", "")
                                       .Replace("Aceptar", "").Replace("Acaptar", "").Trim();

            return decimal.TryParse(sLimpio, out decimal deValor) ? deValor : 0;
        }

        // ---------------------------------------------------------------------------------
        // Método: Fun_Obtener_Tipo_Cuenta
        // Descripción: Devuelve tipo (Activo, Pasivo, Capital)
        // ---------------------------------------------------------------------------------
        private string Fun_Obtener_Tipo_Cuenta(string sCuenta)
        {
            return sCuenta.StartsWith("1") ? "Activo"
                 : sCuenta.StartsWith("2") ? "Pasivo"
                 : sCuenta.StartsWith("3") ? "Capital"
                 : "Otro";
        }
    }
}
