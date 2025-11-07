using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Text;

namespace Capa_modelo
{
    public class Cls_Activo_FijoDAO
    {
        private Cls_Conexion conexion = new Cls_Conexion();

        private static readonly string sSQL_SELECT_ACTIVOS = @"
            SELECT Pk_Activo_ID, Cmp_Nombre_Activo, Cmp_Descripcion, Cmp_Grupo_Activo,
                   Cmp_Costo_Adquisicion, Cmp_Valor_Residual, Cmp_Vida_Util, Cmp_Estado
            FROM Tbl_ActivosFijos
            WHERE Cmp_Estado = 1";

        private static readonly string sSQL_SELECT_ACTIVO_COMPLETO = @"
            SELECT Pk_Activo_ID, Cmp_Nombre_Activo, Cmp_Descripcion, Cmp_Grupo_Activo,
                   Cmp_Fecha_Adquisicion, Cmp_Costo_Adquisicion, 
                   Cmp_Valor_Residual, Cmp_Vida_Util, Cmp_Estado,
                   Cmp_CtaActivo, Cmp_CtaDepreciacion, Cmp_CtaGastoDepreciacion
            FROM Tbl_ActivosFijos 
            WHERE Pk_Activo_ID = ? AND Cmp_Estado = 1";

        private static readonly string sSQL_INSERT_ACTIVO = @"
    INSERT INTO Tbl_ActivosFijos 
    (Cmp_Nombre_Activo, Cmp_Descripcion, Cmp_Grupo_Activo, Cmp_Fecha_Adquisicion,
     Cmp_Costo_Adquisicion, Cmp_Valor_Residual, Cmp_Vida_Util, Cmp_Estado,
     Cmp_CtaActivo, Cmp_CtaDepreciacion, Cmp_CtaGastoDepreciacion)
    VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

        private static readonly string sSQL_INSERT_DEP = @"
            INSERT INTO Tbl_DepreciacionActivos
            (Fk_Activo_ID, Cmp_Anio, Cmp_Valor_En_Libros, 
             Cmp_Depreciacion_Anual, Cmp_Depreciacion_Acumulada)
            VALUES (?, ?, ?, ?, ?)";


        public DataTable fun_ObtenerActivos()
        {
            DataTable dt = new DataTable();
            try
            {
                using (OdbcConnection conn = conexion.conexion())
                {
                    OdbcDataAdapter da = new OdbcDataAdapter(sSQL_SELECT_ACTIVOS, conn);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener activos: {ex.Message}");
            }
            return dt;
        }


        public Cls_Activo_Fijo fun_ObtenerDatosActivo(int iIdActivo)
        {
            Cls_Activo_Fijo activo = null;

            try
            {
                using (OdbcConnection conn = conexion.conexion())
                {
                    OdbcCommand cmd = new OdbcCommand(sSQL_SELECT_ACTIVO_COMPLETO, conn);
                    cmd.Parameters.AddWithValue("@Pk_Activo_ID", iIdActivo);
                    OdbcDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        activo = new Cls_Activo_Fijo
                        {
                            iPkActivoId = reader.GetInt32(0),
                            sNombreActivo = reader.GetString(1),
                            sDescripcion = reader.IsDBNull(2) ? "" : reader.GetString(2),
                            sGrupoActivo = reader.IsDBNull(3) ? "" : reader.GetString(3),
                            dFechaAdquisicion = reader.GetDateTime(4),
                            dCostoAdquisicion = reader.GetDecimal(5),
                            dValorResidual = reader.GetDecimal(6),
                            iVidaUtil = reader.GetInt32(7),
                            bEstadoActivo = reader.GetInt32(8) == 1,
                            sCuentaActivo = reader.IsDBNull(9) ? "" : reader.GetString(9),
                            sCuentaDepreciacionAcumulada = reader.IsDBNull(10) ? "" : reader.GetString(10),
                            sCuentaGastoDepreciacion = reader.IsDBNull(11) ? "" : reader.GetString(11)
                        };
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener datos del activo: {ex.Message}");
            }
            return activo;
        }
        private bool ValidarCuentaExistente(OdbcConnection conn, string codigoCuenta)
        {
            try
            {
                // QUITAR Cmp_Estado = 1 porque no existe
                string sql = "SELECT COUNT(*) FROM Tbl_Catalogo_Cuentas WHERE Pk_Codigo_Cuenta = ?";
                OdbcCommand cmd = new OdbcCommand(sql, conn);
                cmd.Parameters.AddWithValue("?", codigoCuenta);

                int count = Convert.ToInt32(cmd.ExecuteScalar());
                Console.WriteLine($"Cuenta {codigoCuenta} existe: {count > 0}");
                return count > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error validando cuenta {codigoCuenta}: {ex.Message}");
                return false;
            }
        }

        public bool pro_GuardarNuevoActivo(string sNombre, string sDescripcion, string sGrupo,
     DateTime dFechaAdquisicion, decimal deCosto, decimal deValorResidual,
     int iVidaUtil, string sCuentaActivo, string sCuentaDepreciacion, string sCuentaGasto, bool estadoActivo)
        {
            OdbcConnection conn = null;
            try
            {
                Console.WriteLine("=== INICIANDO INSERT DIRECTO SQL ===");

                conn = conexion.conexion();
                Console.WriteLine($"✅ Conexión abierta: {conn.State}");

                // CONSTRUIR SQL CON VALORES LITERALES (para diagnóstico)
                string sqlDirecto = $@"
            INSERT INTO Tbl_ActivosFijos 
            (Cmp_Nombre_Activo, Cmp_Descripcion, Cmp_Grupo_Activo, Cmp_Fecha_Adquisicion,
             Cmp_Costo_Adquisicion, Cmp_Valor_Residual, Cmp_Vida_Util, Cmp_Estado,
             Cmp_CtaActivo, Cmp_CtaDepreciacion, Cmp_CtaGastoDepreciacion)
            VALUES (
                '{sNombre?.Replace("'", "''")}',
                '{sDescripcion?.Replace("'", "''")}',
                '{sGrupo?.Replace("'", "''")}',
                '{dFechaAdquisicion:yyyy-MM-dd}',
                {deCosto},
                {deValorResidual},
                {iVidaUtil},
                {(estadoActivo ? 1 : 0)},
                '{sCuentaActivo?.Replace("'", "''")}',
                '{sCuentaDepreciacion?.Replace("'", "''")}',
                '{sCuentaGasto?.Replace("'", "''")}'
            )";

                Console.WriteLine("SQL generado:");
                Console.WriteLine(sqlDirecto);

                OdbcCommand cmd = new OdbcCommand(sqlDirecto, conn);

                Console.WriteLine("Ejecutando INSERT directo...");
                int result = cmd.ExecuteNonQuery();
                Console.WriteLine($"✅ INSERT directo exitoso. Filas afectadas: {result}");

                return result > 0;
            }
            catch (OdbcException odbcEx)
            {
                Console.WriteLine($"🔴 ERROR ODBC DIRECTO: {odbcEx.Message}");
                if (odbcEx.Errors != null && odbcEx.Errors.Count > 0)
                {
                    foreach (OdbcError error in odbcEx.Errors)
                    {
                        Console.WriteLine($"   🔴 Native: {error.NativeError}, State: {error.SQLState}");
                    }
                }
                throw new Exception($"Error ODBC: {odbcEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ ERROR: {ex.Message}");
                throw;
            }
            finally
            {
                conexion.desconexion(conn);
            }
        }

        // Método auxiliar para validar cuentas
        private bool ValidarCuentaConConsulta(OdbcConnection conn, string codigoCuenta)
        {
            try
            {
                string sql = "SELECT COUNT(*) FROM Tbl_Catalogo_Cuentas WHERE Pk_Codigo_Cuenta = ?";
                OdbcCommand cmd = new OdbcCommand(sql, conn);
                cmd.Parameters.AddWithValue("?", codigoCuenta);

                int count = Convert.ToInt32(cmd.ExecuteScalar());
                Console.WriteLine($"   Cuenta {codigoCuenta}: {(count > 0 ? "EXISTE" : "NO EXISTE")}");
                return count > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"   ❌ Error validando cuenta {codigoCuenta}: {ex.Message}");
                return false;
            }
        }


        public string ProbarConexionYTablas()
        {
            OdbcConnection conn = null;
            try
            {
                conn = conexion.conexion();
                string resultado = $"Conexión: {conn.State}\n";

                // Verificar si la tabla existe
                string checkTableSQL = "SELECT COUNT(*) FROM information_schema.tables WHERE table_schema = 'bd_pruebaContabilidad' AND table_name = 'Tbl_ActivosFijos'";
                OdbcCommand cmdTable = new OdbcCommand(checkTableSQL, conn);
                object tableCount = cmdTable.ExecuteScalar();
                resultado += $"Tabla Tbl_ActivosFijos existe: {Convert.ToInt32(tableCount) > 0}\n";

                // Verificar si hay cuentas contables
                string checkCuentasSQL = "SELECT COUNT(*) FROM Tbl_Catalogo_Cuentas";
                OdbcCommand cmdCuentas = new OdbcCommand(checkCuentasSQL, conn);
                object cuentasCount = cmdCuentas.ExecuteScalar();
                resultado += $"Cuentas contables: {cuentasCount}\n";

                return resultado;
            }
            catch (Exception ex)
            {
                return $"Error en prueba: {ex.Message}";
            }
            finally
            {
                if (conn != null) conn.Close();
            }
        }

        public int pro_GuardarDepreciacion(int iIdActivo, int iAnio, decimal deValorLibros,
                                         decimal depAnual, decimal depAcumulada)
        {
            try
            {
                using (OdbcConnection conn = conexion.conexion())
                {
                    OdbcCommand cmd = new OdbcCommand(sSQL_INSERT_DEP, conn);
                    cmd.Parameters.AddWithValue("@Fk_Activo_ID", iIdActivo);
                    cmd.Parameters.AddWithValue("@Cmp_Anio", iAnio);
                    cmd.Parameters.AddWithValue("@Cmp_Valor_En_Libros", deValorLibros);
                    cmd.Parameters.AddWithValue("@Cmp_Depreciacion_Anual", depAnual);
                    cmd.Parameters.AddWithValue("@Cmp_Depreciacion_Acumulada", depAcumulada);

                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al guardar depreciación: {ex.Message}");
            }
        }

        public int EliminarDepreciacionesPrevias(int iIdActivo)
        {
            try
            {
                using (OdbcConnection conn = conexion.conexion())
                {
                    string sqlDelete = "DELETE FROM Tbl_DepreciacionActivos WHERE Fk_Activo_ID = ?";
                    OdbcCommand cmd = new OdbcCommand(sqlDelete, conn);
                    cmd.Parameters.AddWithValue("@Fk_Activo_ID", iIdActivo);
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar depreciaciones previas: {ex.Message}");
            }
        }
        public string ProbarDSNCompleto()
        {
            StringBuilder resultado = new StringBuilder();
            OdbcConnection conn = null;

            try
            {
                resultado.AppendLine("=== DIAGNÓSTICO COMPLETO DSN ===");

                // 1. Probar conexión básica
                conn = conexion.conexion();
                resultado.AppendLine($"✅ Conexión ODBC: {conn.State}");
                resultado.AppendLine($"🔗 Cadena conexión: {conn.ConnectionString}");
                resultado.AppendLine($"⏱️  Timeout: {conn.ConnectionTimeout}");

                // 2. Probar consulta simple
                OdbcCommand cmdTest = new OdbcCommand("SELECT 1 as Test", conn);
                object testResult = cmdTest.ExecuteScalar();
                resultado.AppendLine($"✅ Consulta básica: {testResult}");

                // 3. Verificar tablas específicas
                string[] tablas = { "Tbl_ActivosFijos", "Tbl_Catalogo_Cuentas", "Tbl_DepreciacionActivos" };

                foreach (string tabla in tablas)
                {
                    try
                    {
                        OdbcCommand cmd = new OdbcCommand($"SELECT COUNT(*) FROM {tabla}", conn);
                        object count = cmd.ExecuteScalar();
                        resultado.AppendLine($"✅ Tabla {tabla}: {count} registros");
                    }
                    catch (Exception ex)
                    {
                        resultado.AppendLine($"❌ Tabla {tabla}: ERROR - {ex.Message}");
                    }
                }

                // 4. Verificar claves foráneas específicas
                resultado.AppendLine("\n🔍 VERIFICANDO CLAVES FORÁNEAS:");
                string[] cuentas = { "1.5.1", "1.6.1", "6.1.5" };

                foreach (string cuenta in cuentas)
                {
                    try
                    {
                        OdbcCommand cmd = new OdbcCommand(
                            "SELECT Cmp_CtaNombre FROM Tbl_Catalogo_Cuentas WHERE Pk_Codigo_Cuenta = ?",
                            conn);
                        cmd.Parameters.AddWithValue("?", cuenta);
                        object nombre = cmd.ExecuteScalar();
                        resultado.AppendLine($"✅ Cuenta {cuenta}: {(nombre ?? "NO ENCONTRADA")}");
                    }
                    catch (Exception ex)
                    {
                        resultado.AppendLine($"❌ Cuenta {cuenta}: ERROR - {ex.Message}");
                    }
                }

                return resultado.ToString();
            }
            catch (Exception ex)
            {
                resultado.AppendLine($"❌ ERROR GENERAL: {ex.Message}");
                resultado.AppendLine($"📋 Tipo: {ex.GetType().FullName}");
                if (ex.InnerException != null)
                {
                    resultado.AppendLine($"🔍 Inner: {ex.InnerException.Message}");
                }
                return resultado.ToString();
            }
            finally
            {
                conexion.desconexion(conn);
            }
        }
    }
}