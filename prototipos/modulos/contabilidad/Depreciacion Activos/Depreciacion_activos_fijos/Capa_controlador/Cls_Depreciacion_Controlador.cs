using System;
using System.Collections.Generic;
using System.Data;
using Capa_modelo;

namespace Capa_controlador
{
    public class Cls_Depreciacion_Controlador
    {
        private Cls_Activo_FijoDAO dao = new Cls_Activo_FijoDAO();
        private Cls_CuentaContableDAO cuentaDAO = new Cls_CuentaContableDAO();

        public System.Collections.Generic.List<string> ObtenerCuentasActivo()
        {
            try
            {
                var cuentas = new System.Collections.Generic.List<string>();
                DataTable dt = cuentaDAO.ObtenerCuentasActivo();

                foreach (System.Data.DataRow row in dt.Rows)
                {
                    string codigo = row["Pk_Codigo_Cuenta"].ToString();
                    string nombre = row["Cmp_CtaNombre"].ToString();
                    cuentas.Add($"{codigo} - {nombre}");
                }

                return cuentas;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener cuentas de activo: {ex.Message}");
            }
        }

        public System.Collections.Generic.List<string> ObtenerCuentasDepreciacion()
        {
            try
            {
                var cuentas = new System.Collections.Generic.List<string>();
                DataTable dt = cuentaDAO.ObtenerCuentasDepreciacion();

                foreach (System.Data.DataRow row in dt.Rows)
                {
                    string codigo = row["Pk_Codigo_Cuenta"].ToString();
                    string nombre = row["Cmp_CtaNombre"].ToString();
                    cuentas.Add($"{codigo} - {nombre}");
                }

                return cuentas;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener cuentas de depreciación: {ex.Message}");
            }
        }

        public System.Collections.Generic.List<string> ObtenerCuentasGastoDepreciacion()
        {
            try
            {
                var cuentas = new System.Collections.Generic.List<string>();
                DataTable dt = cuentaDAO.ObtenerCuentasGastoDepreciacion();

                foreach (System.Data.DataRow row in dt.Rows)
                {
                    string codigo = row["Pk_Codigo_Cuenta"].ToString();
                    string nombre = row["Cmp_CtaNombre"].ToString();
                    cuentas.Add($"{codigo} - {nombre}");
                }

                return cuentas;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener cuentas de gasto: {ex.Message}");
            }
        }

        public class ActivoFijoDTO
        {
            public decimal dCostoAdquisicion { get; set; }
            public decimal dValorResidual { get; set; }
            public int iVidaUtil { get; set; }
            public string sNombreActivo { get; set; }
            public string sGrupoActivo { get; set; }
        }

        public class DepreciacionDTO
        {
            public int iAño { get; set; }
            public decimal dValorEnLibros { get; set; }
            public decimal dDepreciacionAnual { get; set; }
            public decimal dDepreciacionAcumulada { get; set; }
        }

        public DataTable CargarActivos()
        {
            try
            {
                DataTable dt = dao.fun_ObtenerActivos();

                if (dt == null || dt.Rows.Count == 0)
                {
                    throw new Exception("No hay activos fijos disponibles para depreciar.");
                }

                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al cargar activos: {ex.Message}");
            }
        }

        public ActivoFijoDTO ObtenerDatosActivo(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("El ID del activo debe ser mayor a cero.");
            }

            Cls_Activo_Fijo activo = dao.fun_ObtenerDatosActivo(id);

            if (activo == null)
            {
                throw new Exception($"No se encontró el activo con ID: {id}");
            }

            if (activo.dCostoAdquisicion <= 0)
            {
                throw new Exception("El costo de adquisición debe ser mayor a cero.");
            }

            if (activo.dValorResidual < 0)
            {
                throw new Exception("El valor residual no puede ser negativo.");
            }

            if (activo.dValorResidual >= activo.dCostoAdquisicion)
            {
                throw new Exception("El valor residual no puede ser mayor o igual al costo de adquisición.");
            }

            if (activo.iVidaUtil <= 0)
            {
                throw new Exception("La vida útil debe ser mayor a cero.");
            }

            return new ActivoFijoDTO
            {
                dCostoAdquisicion = activo.dCostoAdquisicion,
                dValorResidual = activo.dValorResidual,
                iVidaUtil = activo.iVidaUtil,
                sNombreActivo = activo.sNombreActivo,
                sGrupoActivo = activo.sGrupoActivo
            };
        }

        public bool ValidarSeleccionActivo(object selectedValue)
        {
            if (selectedValue == null)
            {
                throw new Exception("Debe seleccionar un activo fijo.");
            }

            if (!int.TryParse(selectedValue.ToString(), out int idActivo) || idActivo <= 0)
            {
                throw new Exception("El ID del activo seleccionado no es válido.");
            }

            return true;
        }

        public (List<DepreciacionDTO> resultados, decimal depreciacionAnual) CalcularDepreciacionLineaRecta(int idActivo)
        {
            try
            {
                Cls_Activo_Fijo activo = dao.fun_ObtenerDatosActivo(idActivo);

                if (activo == null)
                    throw new Exception("Activo no encontrado.");

                if (activo.dCostoAdquisicion <= 0)
                    throw new Exception("El costo de adquisición debe ser mayor a cero.");

                if (activo.dValorResidual < 0)
                    throw new Exception("El valor residual no puede ser negativo.");

                if (activo.dValorResidual >= activo.dCostoAdquisicion)
                    throw new Exception("El valor residual no puede ser mayor o igual al costo de adquisición.");

                if (activo.iVidaUtil <= 0)
                    throw new Exception("La vida útil debe ser mayor a cero.");

                decimal depAnual = (activo.dCostoAdquisicion - activo.dValorResidual) / activo.iVidaUtil;
                decimal depAcumulada = 0;
                var resultados = new List<DepreciacionDTO>();

                for (int i = 1; i <= activo.iVidaUtil; i++)
                {
                    depAcumulada += depAnual;
                    decimal valorLibros = activo.dCostoAdquisicion - depAcumulada;

                    if (valorLibros < activo.dValorResidual)
                    {
                        valorLibros = activo.dValorResidual;
                        depAcumulada = activo.dCostoAdquisicion - activo.dValorResidual;
                    }

                    resultados.Add(new DepreciacionDTO
                    {
                        iAño = i,
                        dValorEnLibros = Math.Max(valorLibros, activo.dValorResidual),
                        dDepreciacionAnual = depAnual,
                        dDepreciacionAcumulada = Math.Min(depAcumulada, activo.dCostoAdquisicion - activo.dValorResidual)
                    });
                }

                return (resultados, depAnual);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en cálculo de depreciación: {ex.Message}");
            }
        }

        public bool GuardarDepreciacionCompleta(int idActivo, List<DepreciacionDTO> depreciaciones, decimal depreciacionAnual)
        {
            try
            {
                Cls_Activo_Fijo activo = dao.fun_ObtenerDatosActivo(idActivo);

                if (activo == null)
                    throw new Exception("Activo no encontrado");

                dao.EliminarDepreciacionesPrevias(idActivo);

                foreach (var dep in depreciaciones)
                {
                    dao.pro_GuardarDepreciacion(
                        idActivo,
                        dep.iAño,
                        dep.dValorEnLibros,
                        dep.dDepreciacionAnual,
                        dep.dDepreciacionAcumulada
                    );
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al guardar depreciación completa: {ex.Message}");
            }
        }

        public bool GuardarNuevoActivo(
             string nombre,
             string descripcion,
             string grupo,
             DateTime fechaAdquisicion,
             decimal valorAdquisicion,
             decimal valorResidual,
             int vidaUtil,
             string cuentaActivo,
             string cuentaDepreciacion,
             string cuentaGasto,
             bool estadoActivo)
        {
            try
            {
                Console.WriteLine("=== CONTROLADOR: Iniciando guardado ===");

                // Validaciones
                if (string.IsNullOrEmpty(nombre?.Trim()))
                    throw new Exception("El nombre del activo es requerido");

                if (string.IsNullOrEmpty(grupo?.Trim()))
                    throw new Exception("El grupo del activo es requerido");

                if (valorAdquisicion <= 0)
                    throw new Exception("El costo debe ser mayor a cero");

                if (valorResidual < 0)
                    throw new Exception("El valor residual no puede ser negativo");

                if (valorResidual >= valorAdquisicion)
                    throw new Exception("El valor residual no puede ser mayor o igual al costo");

                if (vidaUtil <= 0)
                    throw new Exception("La vida útil debe ser mayor a cero");

                Console.WriteLine("Validaciones pasadas, llamando al DAO...");

                // LLAMAR AL DAO REAL
                return dao.pro_GuardarNuevoActivo(
                    nombre.Trim(),
                    descripcion?.Trim() ?? "",
                    grupo.Trim(),
                    fechaAdquisicion,
                    valorAdquisicion,
                    valorResidual,
                    vidaUtil,
                    cuentaActivo,
                    cuentaDepreciacion,
                    cuentaGasto,
                    estadoActivo
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR EN CONTROLADOR: {ex.ToString()}");
                throw new Exception($"Error en controlador al guardar activo: {ex.Message}");
            }
        }
        public string ProbarSistema()
        {
            try
            {
                return dao.ProbarConexionYTablas();
            }
            catch (Exception ex)
            {
                return $"Error probando sistema: {ex.Message}";
            }
        }
        public string ProbarDSNCompleto()
        {
            try
            {
                return dao.ProbarDSNCompleto();
            }
            catch (Exception ex)
            {
                return $"Error en prueba DSN: {ex.Message}";
            }
        }
        public bool GuardarNuevoActivo_SQLDirecto(
     string nombre, string descripcion, string grupo,
     DateTime fechaAdquisicion, decimal valorAdquisicion,
     decimal valorResidual, int vidaUtil,
     string cuentaActivo, string cuentaDepreciacion,
     string cuentaGasto, bool estadoActivo)
        {
            try
            {
                Console.WriteLine("=== CONTROLADOR: Usando SQL directo ===");
                return dao.pro_GuardarNuevoActivo(
                    nombre.Trim(),
                    descripcion?.Trim() ?? "",
                    grupo.Trim(),
                    fechaAdquisicion,
                    valorAdquisicion,
                    valorResidual,
                    vidaUtil,
                    cuentaActivo,
                    cuentaDepreciacion,
                    cuentaGasto,
                    estadoActivo
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR EN CONTROLADOR (SQL directo): {ex.ToString()}");
                throw new Exception($"Error en controlador (SQL directo): {ex.Message}");
            }
        }
    }
}