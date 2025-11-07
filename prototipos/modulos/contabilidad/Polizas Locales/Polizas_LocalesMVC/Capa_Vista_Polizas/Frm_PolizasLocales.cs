using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Capa_Controlador_Polizas;

namespace Capa_Vista_Polizas
{
    public partial class Frm_PolizasLocales : Form
    {

        private Cls_PolizaControlador cControlador = new Cls_PolizaControlador();

        public Frm_PolizasLocales()
        {
            InitializeComponent();
            ConfigurarBotonesInicio();

            // Maximiza el formulario al abrir
            this.WindowState = FormWindowState.Maximized;
        }

        private void Frm_PolizasLocales_Load(object sender, EventArgs e)
        {
            cControlador.AsegurarPeriodoActivo();
            SincronizarModoUI();
            CargarEncabezados();
        }

        private void SincronizarModoUI()
        {
            var modo = cControlador.SincronizarModoConBD();

            if (modo == Cls_PolizaControlador.ModoActualizacion.EnLinea)
            {
                Lbl_ModoActual.Text = "Modo actual: En línea (automático)";
                Lbl_ModoActual.ForeColor = Color.DarkGreen;
                Btn_ActualizarSaldos.Visible = false;
                Btn_CierreMes.Visible = false;
                Btn_CierreAnio.Visible = false;
            }
            else
            {
                Lbl_ModoActual.Text = "Modo actual: Batch (manual)";
                Lbl_ModoActual.ForeColor = Color.DarkOrange;
                Btn_ActualizarSaldos.Visible = true;
                Btn_CierreMes.Visible = true;
                Btn_CierreAnio.Visible = true;
            }
        }




        // configuracion botones
        private void ConfigurarBotonesInicio()
        {
            Btn_Ingresar.Enabled = true;
            Btn_Editar.Enabled = true;
            Btn_Borrar.Enabled = true;
            Btn_Refrescar.Enabled = true;
            Btn_Salir.Enabled = true;
        }

        private void HabilitarModoEdicion()
        {
            Btn_Ingresar.Enabled = false;
            Btn_Editar.Enabled = true;
            Btn_Borrar.Enabled = true;
            Btn_Refrescar.Enabled = true;
            Btn_Imprimir.Enabled = false;
            Btn_Filtrar.Enabled = false;
            Btn_Todos.Enabled = false;
            Btn_Salir.Enabled = true;
        }

        private void HabilitarModoConsulta()
        {
            Btn_Ingresar.Enabled = true;
            Btn_Editar.Enabled = true;
            Btn_Borrar.Enabled = true;
            Btn_Refrescar.Enabled = true;
            Btn_Imprimir.Enabled = true;
            Btn_Filtrar.Enabled = true;
            Btn_Todos.Enabled = true;
            Btn_Salir.Enabled = true;
        }
       

        //cargar encabezados en el dgv
        private void CargarEncabezados()
        {
            try
            {
                Dgv_EncabezadoPolizas.DataSource = cControlador.ObtenerEncabezados();
                Dgv_EncabezadoPolizas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                Dgv_EncabezadoPolizas.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar encabezados: " + ex.Message);
            }
        }

        private void Btn_Cancelar_Click(object sender, EventArgs e)
        {

        }

        //doble click para ver detalle de poliza
        private void Dgv_EncabezadoPolizas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    int iIdPoliza = Convert.ToInt32(Dgv_EncabezadoPolizas.Rows[e.RowIndex].Cells["Codigo"].Value);
                    Frm_DetallePolizas frmDetalle = new Frm_DetallePolizas(iIdPoliza, "lectura");
                    frmDetalle.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir detalle: " + ex.Message);
            }
        }

        private void Dgv_EncabezadoPolizas_SelectionChanged(object sender, EventArgs e)
        {
            Btn_Editar.Enabled = Dgv_EncabezadoPolizas.SelectedRows.Count > 0;
            Btn_Borrar.Enabled = Dgv_EncabezadoPolizas.SelectedRows.Count > 0;
        }

        //ingresar nueva poliza

        private void Btn_Ingresar_Click(object sender, EventArgs e)
        {
            try
            {
                int iNuevoId = cControlador.ObtenerSiguienteIdEncabezado(DateTime.Now);
                Frm_DetallePolizas frmDetalle = new Frm_DetallePolizas(iNuevoId, "insertar");
                frmDetalle.ShowDialog();
                CargarEncabezados();
                SincronizarModoUI(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al ingresar póliza: " + ex.Message);
            }
        }

        //editar poliza existente
        private void Btn_Editar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Dgv_EncabezadoPolizas.CurrentRow == null)
                {
                    MessageBox.Show("Seleccione una póliza para editar.");
                    return;
                }

                int iIdPoliza = Convert.ToInt32(Dgv_EncabezadoPolizas.CurrentRow.Cells["Codigo"].Value);

                Frm_DetallePolizas frmDetalle = new Frm_DetallePolizas(iIdPoliza, "editar");
                frmDetalle.ShowDialog();
                CargarEncabezados();
                SincronizarModoUI(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al editar póliza: " + ex.Message);
            }
        }

        //eliminar poliza existente
        private void Btn_Borrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Dgv_EncabezadoPolizas.CurrentRow == null)
                {
                    MessageBox.Show("Seleccione una póliza para eliminar.");
                    return;
                }

                int iIdPoliza = Convert.ToInt32(Dgv_EncabezadoPolizas.CurrentRow.Cells["Codigo"].Value);
                DateTime dFecha = Convert.ToDateTime(Dgv_EncabezadoPolizas.CurrentRow.Cells["Fecha"].Value);

                DialogResult r = MessageBox.Show("¿Desea eliminar esta póliza y todos sus detalles?",
                                                 "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (r == DialogResult.Yes)
                {
                    bool eliminado = cControlador.EliminarPoliza(iIdPoliza, dFecha);
                    if (eliminado)
                        MessageBox.Show("Póliza eliminada correctamente.");
                    else
                        MessageBox.Show("No se pudo eliminar la póliza.");
                }

                CargarEncabezados();
                SincronizarModoUI(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar póliza: " + ex.Message);
            }
        }

        //refrescar dgv
        private void Btn_Refrescar_Click(object sender, EventArgs e)
        {
            CargarEncabezados();
        }

        //salir del formulario
        private void Btn_Salir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Btn_ActualizarSaldos_Click(object sender, EventArgs e)
        {
            try
            {
                cControlador.ActualizarSaldosManualmente();
                CargarEncabezados();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar saldos manualmente: " + ex.Message);
            }
        }

        private void Btn_CambiarModo_Click(object sender, EventArgs e)
        {
            try
            {
                cControlador.CambiarModoContable();
                SincronizarModoUI();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cambiar modo contable: " + ex.Message);
            }
        }

        private void Btn_CierreMes_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea cerrar el mes contable actual?", "Confirmar Cierre Mensual",
         MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            try
            {
                // Verificar modo contable
                if (cControlador.GetModoActual() != Cls_PolizaControlador.ModoActualizacion.Batch)
                {
                    MessageBox.Show("El cierre mensual solo puede realizarse en modo Batch.",
                                    "Restricción de Modo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Verificar si hay pólizas activas este mes
                DataTable dtPolizas = cControlador.ObtenerEncabezados();
                int mesActual = DateTime.Now.Month;
                bool existenActivas = dtPolizas.AsEnumerable()
                    .Any(row => Convert.ToDateTime(row["Fecha"]).Month == mesActual &&
                                Convert.ToBoolean(row["Estado"]) == true);

                if (!existenActivas)
                {
                    MessageBox.Show("No existen pólizas activas en el mes actual para cerrar.",
                                    "Cierre Mensual", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Cerrar mes
                DateTime fechaFin = DateTime.Now;
                bool cerrado = cControlador.CerrarMesContable(fechaFin);

                if (cerrado)
                    CargarEncabezados();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cerrar mes contable: " + ex.Message);
            }
        }

        private void Btn_CierreAnio_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult confirmar = MessageBox.Show(
                    $"¿Está seguro de cerrar el año contable {DateTime.Now.Year}?\n\n" +
                    "Esta acción inactivará todas las pólizas activas y recalculará los saldos finales.",
                    "Confirmar Cierre Anual", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (confirmar == DialogResult.No) return;

                // Validar modo contable
                if (cControlador.GetModoActual() != Cls_PolizaControlador.ModoActualizacion.Batch)
                {
                    MessageBox.Show("El cierre anual solo puede realizarse en modo Batch.",
                                    "Restricción de Modo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Verificar si hay pólizas activas este año
                DataTable dtPolizas = cControlador.ObtenerEncabezados();
                int anioActual = DateTime.Now.Year;
                bool existenActivas = dtPolizas.AsEnumerable()
                    .Any(row => Convert.ToDateTime(row["Fecha"]).Year == anioActual &&
                                Convert.ToBoolean(row["Estado"]) == true);

                if (!existenActivas)
                {
                    MessageBox.Show("No existen pólizas activas en el año actual para cerrar.",
                                    "Cierre Anual", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Ejecutar cierre
                DateTime fechaFin = DateTime.Now;
                bool cerrado = cControlador.CerrarAnioContable(fechaFin);
                if (cerrado) CargarEncabezados();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cerrar año contable: " + ex.Message);
            }
        }


        private void Btn_SincronizarModo_Click(object sender, EventArgs e)
        {
            try
            {
                SincronizarModoUI(); 
                MessageBox.Show("Modo contable sincronizado correctamente con la base de datos.",
                                "Sincronización Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al sincronizar modo contable: " + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



    }
}
