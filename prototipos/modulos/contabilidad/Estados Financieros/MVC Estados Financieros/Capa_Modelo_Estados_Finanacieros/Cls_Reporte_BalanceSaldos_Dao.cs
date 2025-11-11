// =====================================================================================
// Autor: Arón Ricardo Esquit Silva
// Carné: 0901-22-13036
// Fecha: 11/11/2025
// Descripción: DAO para guardar el Balance de Saldos (niveles 1, 2, 3)
// =====================================================================================
using System;
using System.Data;
using System.Data.Odbc;

namespace Capa_Modelo_Estados_Financieros
{
    public class Cls_Reporte_BalanceSaldos_Dao
    {
        private readonly Cls_Conexion gConexion = new Cls_Conexion();

        // ---------------------------------------------------------------------------------
        // Método: Fun_Obtener_Periodo_Activo
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
                    // Validar si ya existe un reporte para el periodo
                    string sVerificar = "SELECT COUNT(*) FROM Tbl_Reporte_Balance_Saldos WHERE Cmp_Periodo = ?";
                    using (OdbcCommand cmdVerificar = new OdbcCommand(sVerificar, gConn))
                    {
                        cmdVerificar.Parameters.AddWithValue("?", sPeriodo);
                        int iExiste = Convert.ToInt32(cmdVerificar.ExecuteScalar());
                        if (iExiste > 0)
                            return "No se guardó el reporte, ya existe uno idéntico para este periodo.";
                    }

                    // Insertar datos
                    int iInsertados = 0;
                    foreach (DataRow fila in dtsBalance.Rows)
                    {
                        string sCuenta = fila["Cuenta"].ToString().Trim();
                        string sNombre = fila["Nombre"].ToString().Trim();
                        string sDebe = fila["Debe"].ToString();
                        string sHaber = fila["Haber"].ToString();
                        string sSaldo = fila["Saldo"].ToString();

                        if (string.IsNullOrWhiteSpace(sCuenta) || string.IsNullOrWhiteSpace(sNombre))
                            continue;

                        decimal deDebe = Fun_Limpiar_Valor(sDebe);
                        decimal deHaber = Fun_Limpiar_Valor(sHaber);
                        decimal deSaldo = Fun_Limpiar_Valor(sSaldo);

                        string sInsert = @"INSERT INTO Tbl_Reporte_Balance_Saldos
                                           (Fk_Codigo_Cuenta, Cmp_Nombre_Cuenta, Cmp_Debe, Cmp_Haber, Cmp_Saldo, Cmp_Periodo)
                                           VALUES (?, ?, ?, ?, ?, ?)";
                        using (OdbcCommand cmdInsert = new OdbcCommand(sInsert, gConn))
                        {
                            cmdInsert.Parameters.AddWithValue("?", sCuenta);
                            cmdInsert.Parameters.AddWithValue("?", sNombre);
                            cmdInsert.Parameters.AddWithValue("?", deDebe);
                            cmdInsert.Parameters.AddWithValue("?", deHaber);
                            cmdInsert.Parameters.AddWithValue("?", deSaldo);
                            cmdInsert.Parameters.AddWithValue("?", sPeriodo);
                            cmdInsert.ExecuteNonQuery();
                            iInsertados++;
                        }
                    }

                    return iInsertados > 0
                        ? "Reporte del Balance de Saldos guardado correctamente."
                        : "No se insertaron registros.";
                }
            }
            catch (Exception ex)
            {
                return "Error al guardar: " + ex.Message;
            }
        }

        // ---------------------------------------------------------------------------------
        // Método auxiliar: Limpia y convierte valores a decimal
        // ---------------------------------------------------------------------------------
        private decimal Fun_Limpiar_Valor(string sValor)
        {
            if (string.IsNullOrEmpty(sValor))
                return 0;

            string sLimpio = sValor.Replace("Q", "").Replace("X", "").Replace(",", "")
                                   .Replace("[", "").Replace("]", "")
                                   .Replace("Aceptar", "").Replace("Acaptar", "").Trim();

            return decimal.TryParse(sLimpio, out decimal deValor) ? deValor : 0;
        }
    }
}
