// =====================================================================================
// Autor: Arón Ricardo Esquit Silva
// Carné: 0901-22-13036
// Fecha: 11/11/2025
// Descripción: DAO para guardar el Estado de Resultados (niveles 1, 2, 3) con validación de duplicados
// =====================================================================================
using System;
using System.Data;
using System.Data.Odbc;

namespace Capa_Modelo_Estados_Financieros
{
    public class Cls_Reporte_EstadoResultados_Dao
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
                Console.WriteLine("Error al obtener periodo activo: " + ex.Message);
            }
            return sPeriodo;
        }

        // ---------------------------------------------------------------------------------
        // Método: Fun_Guardar_Reporte
        // Descripción: Guarda los datos del Estado de Resultados
        // ---------------------------------------------------------------------------------
        public string Fun_Guardar_Reporte(DataTable dtsEstado, string sPeriodo, bool esHistorico)
        {
            try
            {
                if (esHistorico)
                    return "Modo Histórico: no se permite guardar";

                if (dtsEstado == null || dtsEstado.Rows.Count == 0)
                    return "No hay datos para guardar";

                using (OdbcConnection gConn = gConexion.conexion())
                {
                    // Verificar si ya existe un reporte para este periodo
                    string sVerificar = "SELECT COUNT(*) FROM Tbl_Reporte_Estado_Resultados WHERE Cmp_Periodo = ?";
                    using (OdbcCommand cmdVerificar = new OdbcCommand(sVerificar, gConn))
                    {
                        cmdVerificar.Parameters.AddWithValue("?", sPeriodo);
                        int iExiste = Convert.ToInt32(cmdVerificar.ExecuteScalar());
                        if (iExiste > 0)
                            return "No se guardó el reporte, ya existe uno idéntico para este periodo.";
                    }

                    // Insertar los registros
                    int iInsertados = 0;
                    foreach (DataRow fila in dtsEstado.Rows)
                    {
                        string sCodigo = fila["Codigo"].ToString().Trim();
                        string sNombre = fila["Nombre"].ToString().Trim();
                        string sTipo = fila.Table.Columns.Contains("Tipo") ? fila["Tipo"].ToString() : "General";
                        string sSaldo = fila.Table.Columns.Contains("Saldo") ? fila["Saldo"].ToString() : "0";

                        if (string.IsNullOrWhiteSpace(sCodigo) || string.IsNullOrWhiteSpace(sNombre))
                            continue;

                        decimal deSaldo = Fun_Limpiar_Valor(sSaldo);

                        string sInsert = @"INSERT INTO Tbl_Reporte_Estado_Resultados
                                           (Fk_Codigo_Cuenta, Cmp_Nombre_Cuenta, Cmp_Tipo_Cuenta, Cmp_Valor, Cmp_Periodo)
                                           VALUES (?, ?, ?, ?, ?)";
                        using (OdbcCommand cmdInsert = new OdbcCommand(sInsert, gConn))
                        {
                            cmdInsert.Parameters.AddWithValue("?", sCodigo);
                            cmdInsert.Parameters.AddWithValue("?", sNombre);
                            cmdInsert.Parameters.AddWithValue("?", sTipo);
                            cmdInsert.Parameters.AddWithValue("?", deSaldo);
                            cmdInsert.Parameters.AddWithValue("?", sPeriodo);
                            cmdInsert.ExecuteNonQuery();
                            iInsertados++;
                        }
                    }

                    return iInsertados > 0
                        ? "Reporte del Estado de Resultados guardado correctamente."
                        : "No se insertaron registros.";
                }
            }
            catch (Exception ex)
            {
                return "Error al guardar: " + ex.Message;
            }
        }

        // ---------------------------------------------------------------------------------
        // Limpia valores a decimal
        // ---------------------------------------------------------------------------------
        private decimal Fun_Limpiar_Valor(string sValor)
        {
            if (string.IsNullOrEmpty(sValor)) return 0;
            string sLimpio = sValor.Replace("Q", "").Replace(",", "").Trim();
            return decimal.TryParse(sLimpio, out decimal deValor) ? deValor : 0;
        }
    }
}
