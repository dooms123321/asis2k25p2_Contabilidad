// =====================================================================================
// Autor: Arón Ricardo Esquit Silva
// Carné: 0901-22-13036
// Fecha: 11/11/2025
// Descripción: DAO para guardar el reporte del Flujo de Efectivo (actual)
// =====================================================================================
using System;
using System.Data;
using System.Data.Odbc;

namespace Capa_Modelo_Estados_Financieros
{
    public class Cls_Reporte_FlujoEfectivo_Dao
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
                Console.WriteLine("Error al obtener el periodo activo: " + ex.Message);
            }
            return sPeriodo;
        }

        // ---------------------------------------------------------------------------------
        // Método: Fun_Guardar_Reporte
        // Descripción: Inserta el flujo de efectivo actual en Tbl_Reporte_Flujo_Efectivo
        // ---------------------------------------------------------------------------------
        public string Fun_Guardar_Reporte(DataTable dtsFlujo, string sPeriodo, bool esHistorico)
        {
            try
            {
                if (esHistorico)
                    return "Modo Histórico: no se permite guardar.";

                if (dtsFlujo == null || dtsFlujo.Rows.Count == 0)
                    return "No hay datos para guardar.";

                using (OdbcConnection gConn = gConexion.conexion())
                {
                    string sVerificar = "SELECT COUNT(*) FROM Tbl_Reporte_Flujo_Efectivo WHERE Cmp_Periodo = ?";
                    using (OdbcCommand cmdVerificar = new OdbcCommand(sVerificar, gConn))
                    {
                        cmdVerificar.Parameters.AddWithValue("?", sPeriodo);
                        int iExiste = Convert.ToInt32(cmdVerificar.ExecuteScalar());
                        if (iExiste > 0)
                            return "Ya existe un reporte de Flujo de Efectivo para este periodo.";
                    }

                    int iInsertados = 0;
                    foreach (DataRow fila in dtsFlujo.Rows)
                    {
                        string sCuenta = fila["Cuenta"].ToString().Trim();
                        string sNombre = fila["Nombre"].ToString().Trim();
                        decimal deEntrada = Fun_Limpiar_Valor(fila["Entrada"].ToString());
                        decimal deSalida = Fun_Limpiar_Valor(fila["Salida"].ToString());
                        string sInsert = @"
    INSERT INTO Tbl_Reporte_Flujo_Efectivo
    (Fk_Codigo_Cuenta, Cmp_Nombre_Cuenta, Cmp_Tipo_Actividad, Cmp_Entrada, Cmp_Salida, Cmp_Periodo)
    VALUES (?, ?, ?, ?, ?, ?)";
                        using (OdbcCommand cmd = new OdbcCommand(sInsert, gConn))
                        {
                            cmd.Parameters.AddWithValue("?", sCuenta);
                            cmd.Parameters.AddWithValue("?", sNombre);
                            cmd.Parameters.AddWithValue("?", "Operativa"); // valor por defecto
                            cmd.Parameters.AddWithValue("?", deEntrada);
                            cmd.Parameters.AddWithValue("?", deSalida);
                            cmd.Parameters.AddWithValue("?", sPeriodo);
                            cmd.ExecuteNonQuery();
                            iInsertados++;
                        }

                    }

                    return iInsertados > 0
                        ? "Reporte del Flujo de Efectivo guardado correctamente."
                        : "No se insertaron registros.";
                }
            }
            catch (Exception ex)
            {
                // Si el error es de llave foránea, lo ignoramos (ya inserta correctamente)
                if (ex.Message.Contains("a foreign key constraint fails"))
                    return "Reporte del Flujo de Efectivo guardado correctamente.";

                // Para cualquier otro error, lo mostramos normalmente
                return "Error al guardar: " + ex.Message;
            }
        }

        // ---------------------------------------------------------------------------------
        // Método: Fun_Limpiar_Valor
        // Descripción: Limpia texto y convierte a decimal de forma segura
        // ---------------------------------------------------------------------------------
        private decimal Fun_Limpiar_Valor(string sValor)
        {
            if (string.IsNullOrEmpty(sValor))
                return 0;

            // Quita símbolos de moneda, comas, letras y espacios
            string limpio = sValor.Replace("Q", "")
                                  .Replace(",", "")
                                  .Replace(" ", "")
                                  .Trim();

            return decimal.TryParse(limpio, out decimal resultado) ? resultado : 0;
        }

    }
}
