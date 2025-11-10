using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Capa_Modelo_Presupuesto;

namespace Capa_Vista_Presupuesto
{
    public partial class Frm_Principal : Form
    {
        // ==========================================================
        // VARIABLES DE CLASE PARA EL PERIODO ACTIVO (CRÍTICO)
        // ==========================================================
        private int anioActivo;
        private int mesActivo;

        public Frm_Principal()
        {
            InitializeComponent();
        }

        // ==========================================================
        // EVENTOS DE BOTONES - LLAMADAS A FORMULARIOS
        // ==========================================================

        private void Btn_Salir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Btn_Traslado_Click(object sender, EventArgs e)
        {
            if (anioActivo == 0 || mesActivo == 0)
            {
                MessageBox.Show("No hay un período contable activo configurado.", "Error de Configuración");
                return;
            }

            // Asegurarse de que Frm_Traslado tenga el constructor (int anio, int mes)
            Frm_Traslado formularioTraslado = new Frm_Traslado(anioActivo, mesActivo);
            if (formularioTraslado.ShowDialog() == DialogResult.OK)
            {
                ActualizarDataGrids();
                ActualizarPresupuestoTotal();
            }
        }

        private void Btn_Partidas_Click(object sender, EventArgs e)
        {
            if (anioActivo == 0 || mesActivo == 0)
            {
                MessageBox.Show("No hay un período contable activo configurado. No se puede registrar la ejecución.", "Error de Configuración");
                return;
            }

            // Asegurarse de que Frm_Partidas tenga el constructor (int anio, int mes)
            Frm_Partidas formularioPartidas = new Frm_Partidas(anioActivo, mesActivo);

            if (formularioPartidas.ShowDialog() == DialogResult.OK)
            {
                ActualizarDataGrids();
                ActualizarPresupuestoTotal();
            }
        }

        private void Btn_Presupuesto_Click(object sender, EventArgs e)
        {
            
            if (anioActivo == 0 || mesActivo == 0)
            {
                MessageBox.Show("No hay un período contable activo configurado. No se puede registrar la ejecución.", "Error de Configuración");
                return;
            }

            // Llama al formulario que registra el Gasto/Ejecución (Frm_Ejecucion)
            Frm_Ejecucion formularioEjecucion = new Frm_Ejecucion(anioActivo, mesActivo);

            if (formularioEjecucion.ShowDialog() == DialogResult.OK)
            {
                ActualizarDataGrids();
                ActualizarPresupuestoTotal();
            }
        }

        // ==========================================================
        // CARGA INICIAL Y PERÍODO ACTIVO
        // ==========================================================
        private void Frm_Principal_Load_1(object sender, EventArgs e)
        {
            if (Conexion.ProbarConexion())
            {
                CargarPeriodoActivo();
                ActualizarDataGrids();
                ActualizarPresupuestoTotal();

                // VINCULACIÓN DE FORMATO
                Dgv_1.CellFormatting += Dgv_1_CellFormatting;
                Dgv_2.CellFormatting += Dgv_2_CellFormatting;
                Dgv_3.CellFormatting += Dgv_3_CellFormatting;
            }
            else
            {
                MessageBox.Show("Error de conexión a la base de datos");
            }
        }

        private void CargarPeriodoActivo()
        {
            try
            {
                // Asegúrate de que Tbl_PeriodosContables exista
                string queryPeriodo = "SELECT Cmp_Anio, Cmp_Mes FROM Tbl_PeriodosContables WHERE Cmp_Estado = 1 LIMIT 1";
                DataTable dtPeriodo = Conexion.EjecutarConsulta(queryPeriodo);

                if (dtPeriodo != null && dtPeriodo.Rows.Count > 0)
                {
                    anioActivo = Convert.ToInt32(dtPeriodo.Rows[0]["Cmp_Anio"]);
                    mesActivo = Convert.ToInt32(dtPeriodo.Rows[0]["Cmp_Mes"]);
                    // Lbl_PeriodoActivo.Text = $"Período Activo: {mesActivo:00}/{anioActivo}"; 
                }
                else
                {
                    // Usar el mes y año actual si no hay período activo configurado
                    anioActivo = DateTime.Now.Year;
                    mesActivo = DateTime.Now.Month;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar el período activo: {ex.Message}");
            }
        }

        private void ActualizarDataGrids()
        {
            CargarMovimientos();
            CargarResumenAreas();
            CargarEstadisticas();
        }

        private void TabControl_Principal_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Asumiendo que Tbc_1 es el nombre de tu TabControl
            if (sender is TabControl tbc && tbc.SelectedIndex != -1)
            {
                switch (tbc.SelectedIndex)
                {
                    case 0:
                        CargarMovimientos();
                        break;
                    case 1:
                        CargarResumenAreas();
                        break;
                    case 2:
                        CargarEstadisticas();
                        break;
                }
            }
        }

        // ==========================================================
        // FUNCIONES DE CARGA DE DATOS (AHORA USANDO Tbl_Presupuesto_Periodo)
        // ==========================================================

        private void ActualizarPresupuestoTotal()
        {
            if (anioActivo == 0) return;
            try
            {
                // Consulta para sumar KPIs desde la tabla Tbl_Presupuesto_Periodo
                string queryKPI = $@"
                    SELECT
                        COALESCE(SUM(PP.Cmp_MontoInicial), 0) AS Presupuestado, 
                        COALESCE(SUM(PP.Cmp_MontoEjecutado), 0) AS Ejecutado,
                        COALESCE(SUM(PP.Cmp_MontoDisponible), 0) AS Disponible
                    FROM Tbl_Presupuesto_Periodo PP
                    WHERE PP.Cmp_Anio = {anioActivo} AND PP.Cmp_Mes = {mesActivo};";

                DataTable dtKPI = Conexion.EjecutarConsulta(queryKPI);

                if (dtKPI != null && dtKPI.Rows.Count > 0)
                {
                    // Lógica de vinculación a Labels (Descomentar y adaptar)
                    // decimal presupuestado = Convert.ToDecimal(dtKPI.Rows[0]["Presupuestado"]);
                    // decimal ejecutado = Convert.ToDecimal(dtKPI.Rows[0]["Ejecutado"]);
                    // Lbl_TotalPresupuestado.Text = "Q" + presupuestado.ToString("N2");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al calcular KPIs de presupuesto: {ex.Message}", "Error de Base de Datos");
            }
        }

        private void CargarMovimientos()
        {
            if (anioActivo == 0) return;
            try
            {
                // CONSULTA CORREGIDA para Movimientos (Detalle por Cuenta)
                string queryEjecucion = $@"
                    SELECT
                        PP.Fk_Codigo_Cuenta AS 'Código',
                        CC.Cmp_CtaNombre AS 'Cuenta Contable',
                        PP.Cmp_MontoInicial AS 'Presupuestado', 
                        PP.Cmp_MontoEjecutado AS 'Ejecutado',
                        PP.Cmp_MontoDisponible AS 'Disponible', 
                        (PP.Cmp_MontoEjecutado / NULLIF(PP.Cmp_MontoInicial, 0)) * 100 AS 'Porc_Ejecucion' 
                    FROM Tbl_Presupuesto_Periodo PP
                    INNER JOIN Tbl_Catalogo_Cuentas CC ON PP.Fk_Codigo_Cuenta = CC.Pk_Codigo_Cuenta
                    WHERE
                        PP.Cmp_Anio = {anioActivo} AND PP.Cmp_Mes = {mesActivo}
                        AND (PP.Fk_Codigo_Cuenta LIKE '5.%' OR PP.Fk_Codigo_Cuenta LIKE '6.%') 
                    ORDER BY PP.Fk_Codigo_Cuenta;";

                // Asumiendo que Dgv_1 es el DataGridView en la pestaña Movimientos
                DataTable dt = Conexion.EjecutarConsulta(queryEjecucion);
                Dgv_1.DataSource = dt;
                if (Dgv_1.Columns.Contains("Porc_Ejecucion")) Dgv_1.Columns["Porc_Ejecucion"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar ejecución presupuestaria: {ex.Message}", "Error de Datos");
            }
        }

        private void CargarResumenAreas()
        {
            if (anioActivo == 0) return;
            try
            {
                // CONSULTA CORREGIDA para Resumen (Agrupado por Cuenta Mayor)
                string queryResumen = $@"
                    SELECT
                        SUBSTRING_INDEX(PP.Fk_Codigo_Cuenta, '.', 1) AS 'Cód. Mayor',
                        MAX(CC_Mayor.Cmp_CtaNombre) AS 'Cuenta Mayor',
                        COALESCE(SUM(PP.Cmp_MontoInicial), 0) AS 'Presupuestado',
                        COALESCE(SUM(PP.Cmp_MontoEjecutado), 0) AS 'Ejecutado',
                        COALESCE(SUM(PP.Cmp_MontoDisponible), 0) AS 'Saldo Disponible'
                    FROM Tbl_Presupuesto_Periodo PP
                    INNER JOIN Tbl_Catalogo_Cuentas CC_Mayor ON SUBSTRING_INDEX(PP.Fk_Codigo_Cuenta, '.', 1) = CC_Mayor.Pk_Codigo_Cuenta
                    WHERE
                        PP.Cmp_Anio = {anioActivo} AND PP.Cmp_Mes = {mesActivo}
                        AND CC_Mayor.Pk_Codigo_Cuenta IN ('5', '6') -- Solo Costos y Gastos
                    GROUP BY SUBSTRING_INDEX(PP.Fk_Codigo_Cuenta, '.', 1)
                    ORDER BY 'Cód. Mayor'";

                // Asumiendo que Dgv_2 es el DataGridView en la pestaña Resumen
                DataTable dt = Conexion.EjecutarConsulta(queryResumen);
                Dgv_2.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar resumen de cuentas: {ex.Message}");
            }
        }

        private void CargarEstadisticas()
        {
            if (anioActivo == 0) return;
            try
            {
                // CONSULTA CORREGIDA para Estadísticas (Agrupado por Cuenta Mayor)
                string queryEstadisticas = $@"
                    SELECT
                        CC_Mayor.Cmp_CtaNombre AS 'Concepto',
                        SUM(PP.Cmp_MontoInicial) AS 'Presupuesto Total',
                        COALESCE(SUM(PP.Cmp_MontoEjecutado), 0) AS 'Ejecución Total'
                    FROM Tbl_Presupuesto_Periodo PP
                    INNER JOIN Tbl_Catalogo_Cuentas CC_Mayor ON SUBSTRING_INDEX(PP.Fk_Codigo_Cuenta, '.', 1) = CC_Mayor.Pk_Codigo_Cuenta
                    WHERE
                        PP.Cmp_Anio = {anioActivo} AND PP.Cmp_Mes = {mesActivo}
                        AND CC_Mayor.Pk_Codigo_Cuenta IN ('5', '6')
                    GROUP BY CC_Mayor.Cmp_CtaNombre";

                // Asumiendo que Dgv_3 es el DataGridView en la pestaña Estadísticas
                DataTable dt = Conexion.EjecutarConsulta(queryEstadisticas);
                Dgv_3.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar estadísticas: {ex.Message}");
            }
        }

        // ==========================================================
        // MÉTODOS DE FORMATO (Moneda)
        // ==========================================================
        // Asumiendo que estos métodos existen y están vinculados a los DataGridViews
        private void Dgv_1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e) { /* Lógica de formato */ }
        private void Dgv_2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e) { /* Lógica de formato */ }
        private void Dgv_3_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e) { /* Lógica de formato */ }
    }
}