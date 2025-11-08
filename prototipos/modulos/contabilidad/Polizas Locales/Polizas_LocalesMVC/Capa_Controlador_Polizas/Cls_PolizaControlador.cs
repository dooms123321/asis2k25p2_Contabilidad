using Capa_Modelo_Polizas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Capa_Controlador_Polizas
{
    public class Cls_PolizaControlador
    {
        //modos de batch o linea
        public enum ModoActualizacion
        {
            Batch,
            EnLinea
        }

        private Cls_PolizasDAO cPolizaDAO = new Cls_PolizasDAO();
        private ModoActualizacion modoActual = ModoActualizacion.Batch;

        //cambia si se actualiza en linea o en batch
        public void CambiarModoActualizacion(ModoActualizacion nuevoModo)
        {
            modoActual = nuevoModo;
            MessageBox.Show(
                $"Modo de actualización cambiado a: {(modoActual == ModoActualizacion.EnLinea ? "En línea (automático)" : "Batch (manual)")} ",
                "Modo Actualización",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        //cargar modo desde la base de datos
        public void CargarModoDesdeBD()
        {
            try
            {
                bool bModo = cPolizaDAO.ObtenerModoOperacion();
                modoActual = bModo ? ModoActualizacion.EnLinea : ModoActualizacion.Batch;

                MessageBox.Show(
                    $"Modo actual de operación: {(modoActual == ModoActualizacion.EnLinea ? "En línea (automático)" : "Batch (manual)")}",
                    "Modo Contable", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar modo contable: " + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //cambiar modo en la base de datos
        public void CambiarModoContable()
        {
            bool bNuevo = (modoActual != ModoActualizacion.EnLinea); // invertir
            if (cPolizaDAO.CambiarModoOperacion(bNuevo))
            {
                modoActual = bNuevo ? ModoActualizacion.EnLinea : ModoActualizacion.Batch;
                MessageBox.Show($"Modo cambiado a {(bNuevo ? "En Línea" : "Batch")}.",
                    "Modo Contable", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No se pudo cambiar el modo en la base de datos.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public ModoActualizacion SincronizarModoConBD()
        {
            try
            {
                bool bModo = cPolizaDAO.ObtenerModoOperacion();
                modoActual = bModo ? ModoActualizacion.EnLinea : ModoActualizacion.Batch;
                return modoActual;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al sincronizar modo contable: " + ex.Message);
                return modoActual;
            }
        }


        //modo actual
        public ModoActualizacion GetModoActual() => modoActual;




        //obtener siguiente ID de encabezado - otros modulos utilizaran este metodo
        public int ObtenerSiguienteIdEncabezado(DateTime dFecha)
        {
            return cPolizaDAO.ObtenerSiguienteIdEncabezado(dFecha);
        }

        // Validar que todas las cuentas sean de tipo detalle
        private bool ValidarSoloCuentasDetalle(List<(string sCodigoCuenta, bool bTipo, decimal dValor)> lDetalles)
        {
            DataTable dtCuentas = cPolizaDAO.ConsultarCuentasContables(); // Solo tipo 1 (detalle)
            var lCuentasValidas = dtCuentas.AsEnumerable()
                .Select(row => row["Pk_Codigo_Cuenta"].ToString().Trim())
                .ToList();

            foreach (var det in lDetalles)
            {
                if (!lCuentasValidas.Contains(det.sCodigoCuenta.Trim()))
                {
                    MessageBox.Show($"La cuenta '{det.sCodigoCuenta}' no es de tipo DETALLE y no puede usarse en la póliza.",
                                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return true;
        }


        //validacion para que un encabezado sea editable
        private bool EsEditable(int iIdPoliza, DateTime dFecha)
        {
            DataTable dt = cPolizaDAO.ConsultarEncabezados();
            DataRow[] rows = dt.Select($"Codigo = {iIdPoliza} AND Fecha = #{dFecha:yyyy-MM-dd}#");
            if (rows.Length == 0) return false;

            bool bEstado = Convert.ToBoolean(rows[0]["Estado"]);
            return bEstado;
        }

        //validar campos de encabezado
        private bool ValidarEncabezado(DateTime dFecha, string sConcepto)
        {
            if (string.IsNullOrWhiteSpace(sConcepto))
            {
                MessageBox.Show("El concepto de la póliza no puede estar vacío.",
                                "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (dFecha > DateTime.Now)
            {
                MessageBox.Show("La fecha no puede ser mayor a la actual.",
                                "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        //validaciones de detalles
        private bool ValidarDetalles(List<(string sCodigoCuenta, bool bTipo, decimal dValor)> lDetalles)
        {
            if (lDetalles == null || lDetalles.Count == 0)
            {
                MessageBox.Show("Debe ingresar al menos una cuenta de detalle.",
                                "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            foreach (var det in lDetalles)
            {
                if (string.IsNullOrWhiteSpace(det.sCodigoCuenta))
                {
                    MessageBox.Show("Existe un detalle con código de cuenta vacío.",
                                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (det.dValor <= 0)
                {
                    MessageBox.Show($"La cuenta '{det.sCodigoCuenta}' tiene un valor inválido.",
                                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            decimal dCargos = lDetalles.Where(x => x.bTipo).Sum(x => x.dValor);
            decimal dAbonos = lDetalles.Where(x => !x.bTipo).Sum(x => x.dValor);

            if (dCargos != dAbonos)
            {
                DialogResult dr = MessageBox.Show(
                    $"La póliza no está cuadrada.\nCargos: {dCargos}\nAbonos: {dAbonos}\n\n¿Desea continuar de todas formas?",
                    "Advertencia de Balance",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (dr == DialogResult.No) return false;
            }

            return true;
        }

        private void RefrescarModoActual()
        {
            try
            {
                bool bModo = cPolizaDAO.ObtenerModoOperacion();
                modoActual = bModo ? ModoActualizacion.EnLinea : ModoActualizacion.Batch;
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo sincronizar el modo contable: " + ex.Message,
                                "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }



        //insertar nueva poliza de encabezado y detalle
        //actualizacion de saldos cuando se inserte en linea o batch
        public bool InsertarPoliza(DateTime dFecha, string sConcepto, List<(string sCodigoCuenta, bool bTipo, decimal dValor)> lDetalles)
        {
            try
            {
                //sincroniza modo actual desde BD
                RefrescarModoActual();

                if (!ValidarEncabezado(dFecha, sConcepto)) return false;
                if (!ValidarDetalles(lDetalles)) return false;
                if (!ValidarSoloCuentasDetalle(lDetalles)) return false;

                bool exito = cPolizaDAO.InsertarPoliza(dFecha, sConcepto, lDetalles);
                if (!exito)
                {
                    MessageBox.Show("Error al insertar la póliza. No se realizaron cambios.",
                                    "Error de inserción", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                Console.WriteLine($"[{DateTime.Now}] Inserción de póliza exitosa. Concepto: {sConcepto}, Fecha: {dFecha:yyyy-MM-dd}, Detalles: {lDetalles.Count}");

                if (modoActual == ModoActualizacion.EnLinea)
                {
                    try
                    {
                        if (cPolizaDAO.ActualizarSaldosContables())
                        {
                            MessageBox.Show("Póliza insertada y saldos contables actualizados (modo En línea).",
                                            "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Póliza insertada, pero no se pudieron actualizar los saldos contables.",
                                            "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Póliza insertada, pero ocurrió un error al actualizar saldos: " + ex.Message,
                                        "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Póliza insertada correctamente (modo Batch). Los saldos se actualizarán manualmente más tarde.",
                                    "Modo Batch", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al insertar póliza: " + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"[{DateTime.Now}] Error InsertarPoliza(): {ex.Message}");
                return false;
            }
        }

        //actualiza poliza
        public bool ActualizarPoliza(int iIdPoliza, DateTime dFecha, string sConcepto, List<(string sCodigoCuenta, bool bTipo, decimal dValor)> lDetalles)
        {
            try
            {
                RefrescarModoActual();

                if (!EsEditable(iIdPoliza, dFecha))
                {
                    MessageBox.Show("No se puede modificar una póliza inactiva.",
                                    "Restricción", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if (!ValidarEncabezado(dFecha, sConcepto)) return false;
                if (!ValidarDetalles(lDetalles)) return false;
                if (!ValidarSoloCuentasDetalle(lDetalles)) return false;

                bool exito = cPolizaDAO.ActualizarPoliza(iIdPoliza, dFecha, sConcepto, lDetalles);
                if (!exito)
                {
                    MessageBox.Show("Error al actualizar la póliza.",
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                Console.WriteLine($"[{DateTime.Now}] Póliza {iIdPoliza} actualizada correctamente. Concepto: {sConcepto}");

                if (modoActual == ModoActualizacion.EnLinea)
                {
                    if (cPolizaDAO.ActualizarSaldosContables())
                    {
                        MessageBox.Show("Póliza actualizada y saldos contables recalculados (modo En línea).",
                                        "Actualización Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Póliza actualizada, pero no se pudieron recalcular los saldos.",
                                        "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Póliza actualizada correctamente (modo Batch). Los saldos se recalcularán manualmente más tarde.",
                                    "Modo Batch", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar póliza: " + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }



        //eliminar una póliza completa
        public bool EliminarPoliza(int iIdPoliza, DateTime dFecha)
        {
            if (!EsEditable(iIdPoliza, dFecha))
            {
                MessageBox.Show("No se puede eliminar una póliza inactiva.",
                                "Restricción", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            DialogResult dr = MessageBox.Show("¿Está seguro de eliminar esta póliza completa?",
                                              "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.No) return false;

            bool exito = cPolizaDAO.EliminarPoliza(iIdPoliza, dFecha);
            if (exito)
            {
                if (modoActual == ModoActualizacion.EnLinea)
                    cPolizaDAO.ActualizarSaldosContables();

                MessageBox.Show("Póliza eliminada correctamente.",
                                "Eliminación", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return exito;
        }

        //eliminar un detalle específico
        public bool EliminarDetalle(int iIdPoliza, DateTime dFecha, string sCodigoCuenta)
        {
            if (!EsEditable(iIdPoliza, dFecha))
            {
                MessageBox.Show("No se puede eliminar un detalle de una póliza inactiva.",
                                "Restricción", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (string.IsNullOrWhiteSpace(sCodigoCuenta))
            {
                MessageBox.Show("Debe seleccionar una cuenta de detalle para eliminar.",
                                "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            DialogResult dr = MessageBox.Show($"¿Desea eliminar el detalle con cuenta {sCodigoCuenta}?",
                                              "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.No) return false;

            bool exito = cPolizaDAO.EliminarDetalle(iIdPoliza, dFecha, sCodigoCuenta);
            if (exito)
            {
                if (modoActual == ModoActualizacion.EnLinea)
                    cPolizaDAO.ActualizarSaldosContables();

                MessageBox.Show("Detalle eliminado correctamente.",
                                "Eliminación de Detalle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return exito;
        }

        //---- consultas varias
        //consultar encabezados para dgv
        public DataTable ObtenerEncabezados()
        {
            return cPolizaDAO.ConsultarEncabezados();
        }

        public DataTable ObtenerDetalles(int iIdPoliza, DateTime dFecha)
        {
            return cPolizaDAO.ConsultarDetalle(iIdPoliza, dFecha);
        }

        public DataTable ObtenerCuentasContables()
        {
            return cPolizaDAO.ConsultarCuentasContables();
        }

        public decimal ObtenerTotalCargos(int iIdPoliza, DateTime dFecha)
        {
            return cPolizaDAO.ObtenerTotalCargos(iIdPoliza, dFecha);
        }

        public decimal ObtenerTotalAbonos(int iIdPoliza, DateTime dFecha)
        {
            return cPolizaDAO.ObtenerTotalAbonos(iIdPoliza, dFecha);
        }

        public decimal ObtenerDiferencial(int iIdPoliza, DateTime dFecha)
        {
            return cPolizaDAO.ObtenerDiferencial(iIdPoliza, dFecha);
        }

        public void ActualizarSaldosManualmente()
        {
            if (cPolizaDAO.ActualizarSaldosContables())
                MessageBox.Show("Saldos contables actualizados correctamente (modo Batch).",
                                "Actualización de Saldos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("No se pudieron actualizar los saldos contables.",
                                "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        //cerrar mes contable
        public bool CerrarMesContable(DateTime fechaFin)
        {
            try
            {
                // Asegurar que el período actual exista y esté activo
                cPolizaDAO.AsegurarPeriodoActivo(DateTime.Now);

                if (GetModoActual() != ModoActualizacion.Batch)
                {
                    MessageBox.Show("El cierre mensual solo puede realizarse en modo Batch.",
                                    "Restricción de Modo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                DialogResult confirmar = MessageBox.Show(
                    $"¿Desea cerrar el mes contable actual ({fechaFin:MMMM yyyy})?\n\n" +
                    "Esta acción inactivará todas las pólizas activas hasta esta fecha y recalculará los saldos contables.",
                    "Confirmación de Cierre Mensual", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (confirmar == DialogResult.No)
                    return false;

                int registros = cPolizaDAO.CerrarMesContable(fechaFin);

                if (registros > 0)
                {
                    cPolizaDAO.ActualizarSaldosContables();
                    MessageBox.Show($"Mes contable cerrado correctamente.\n" +
                                    $"Se inactivaron {registros} pólizas hasta {fechaFin:dd/MM/yyyy}.",
                                    "Cierre Contable", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
                else
                {
                    MessageBox.Show("No se encontraron pólizas activas para cerrar en el mes actual.",
                                    "Cierre Contable", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cerrar mes contable: " + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        //cerrar año contable
        public bool CerrarAnioContable(DateTime fechaFin)
        {
            try
            {
                cPolizaDAO.AsegurarPeriodoActivo(DateTime.Now);
                // Verificar modo contable
                if (GetModoActual() != ModoActualizacion.Batch)
                {
                    MessageBox.Show("El cierre anual solo puede realizarse en modo Batch.",
                                    "Restricción de Modo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                // Confirmar acción
                DialogResult confirmar = MessageBox.Show(
                    $"¿Desea cerrar el año contable {fechaFin.Year}?\n\n" +
                    "Esta acción inactivará todas las pólizas activas del año y recalculará los saldos finales.",
                    "Confirmación de Cierre Anual", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (confirmar == DialogResult.No)
                    return false;

                // Ejecutar cierre
                int registros = cPolizaDAO.CerrarAnioContable(fechaFin);

                if (registros > 0)
                {
                    // Recalcular saldos solo una vez
                    cPolizaDAO.ActualizarSaldosContables();

                    MessageBox.Show($" Año contable {fechaFin.Year} cerrado correctamente.\n" +
                                    $"Se inactivaron {registros} pólizas activas del año.",
                                    "Cierre Anual", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return true;
                }
                else
                {
                    MessageBox.Show($" No se encontraron pólizas activas del año {fechaFin.Year} para cerrar.",
                                    "Cierre Anual", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(" Error al cerrar año contable: " + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        //asegurar periodo activo
        public void AsegurarPeriodoActivo()
        {
            try
            {
                cPolizaDAO.AsegurarPeriodoActivo(DateTime.Now);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al verificar período contable activo: " + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
