using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Capa_Vista_Contabilida
{
    public partial class Depreciacion : Form
    {
        private DataGridView dataGridViewDepreciacion;
        private GroupBox groupBoxCaracteristicas;
        private Label lblCosto;
        private Label lblValorResidual;
        private Label lblVidaUtil;
        private Label lblDepreciacion;
        private TextBox txtCosto;
        private TextBox txtValorResidual;
        private TextBox txtVidaUtil;
        private TextBox txtDepreciacion;
        private Button btnCalcular;
        private Button btnLimpiar;
        private Label lblTitulo;

        public Depreciacion()
        {
            InitializeComponent();
            CrearControles();
            CalcularDepreciacion();
        }

        private void CrearControles()
        {
            // Configuración del formulario
            this.Text = "Sistema de Depreciación - Método de Línea Recta";
            this.Size = new Size(900, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;

            // Título
            lblTitulo = new Label();
            lblTitulo.Text = "MÉTODO DE DEPRECIACIÓN - LÍNEA RECTA";
            lblTitulo.Font = new Font("Arial", 14, FontStyle.Bold);
            lblTitulo.ForeColor = Color.DarkBlue;
            lblTitulo.AutoSize = true;
            lblTitulo.Location = new Point(250, 20);

            // GroupBox para características
            groupBoxCaracteristicas = new GroupBox();
            groupBoxCaracteristicas.Text = "Características del Activo";
            groupBoxCaracteristicas.Font = new Font("Arial", 10, FontStyle.Bold);
            groupBoxCaracteristicas.Location = new Point(50, 60);
            groupBoxCaracteristicas.Size = new Size(350, 180);
            groupBoxCaracteristicas.BackColor = Color.LightGray;

            // Labels para características
            lblCosto = new Label();
            lblCosto.Text = "Costo total:";
            lblCosto.Location = new Point(20, 40);
            lblCosto.Size = new Size(120, 20);
            lblCosto.Font = new Font("Arial", 9, FontStyle.Regular);

            lblValorResidual = new Label();
            lblValorResidual.Text = "Valor residual:";
            lblValorResidual.Location = new Point(20, 70);
            lblValorResidual.Size = new Size(120, 20);
            lblValorResidual.Font = new Font("Arial", 9, FontStyle.Regular);

            lblVidaUtil = new Label();
            lblVidaUtil.Text = "Vida útil (años):";
            lblVidaUtil.Location = new Point(20, 100);
            lblVidaUtil.Size = new Size(120, 20);
            lblVidaUtil.Font = new Font("Arial", 9, FontStyle.Regular);

            lblDepreciacion = new Label();
            lblDepreciacion.Text = "Depreciación anual:";
            lblDepreciacion.Location = new Point(20, 130);
            lblDepreciacion.Size = new Size(120, 20);
            lblDepreciacion.Font = new Font("Arial", 9, FontStyle.Regular);

            // TextBoxes para características
            txtCosto = new TextBox();
            txtCosto.Location = new Point(150, 37);
            txtCosto.Size = new Size(150, 25);
            txtCosto.Font = new Font("Arial", 9, FontStyle.Regular);
            txtCosto.Text = "16000.00";
            txtCosto.TextChanged += new EventHandler(ActualizarCalculo);

            txtValorResidual = new TextBox();
            txtValorResidual.Location = new Point(150, 67);
            txtValorResidual.Size = new Size(150, 25);
            txtValorResidual.Font = new Font("Arial", 9, FontStyle.Regular);
            txtValorResidual.Text = "1000.00";
            txtValorResidual.TextChanged += new EventHandler(ActualizarCalculo);

            txtVidaUtil = new TextBox();
            txtVidaUtil.Location = new Point(150, 97);
            txtVidaUtil.Size = new Size(150, 25);
            txtVidaUtil.Font = new Font("Arial", 9, FontStyle.Regular);
            txtVidaUtil.Text = "8";
            txtVidaUtil.TextChanged += new EventHandler(ActualizarCalculo);

            txtDepreciacion = new TextBox();
            txtDepreciacion.Location = new Point(150, 127);
            txtDepreciacion.Size = new Size(150, 25);
            txtDepreciacion.Font = new Font("Arial", 9, FontStyle.Regular);
            txtDepreciacion.Text = "1875.00";
            txtDepreciacion.ReadOnly = true;
            txtDepreciacion.BackColor = Color.WhiteSmoke;

            // Agregar controles al GroupBox
            groupBoxCaracteristicas.Controls.AddRange(new Control[] {
                lblCosto, lblValorResidual, lblVidaUtil, lblDepreciacion,
                txtCosto, txtValorResidual, txtVidaUtil, txtDepreciacion
            });

            // Botones
            btnCalcular = new Button();
            btnCalcular.Text = "&Calcular Depreciación";
            btnCalcular.Location = new Point(450, 100);
            btnCalcular.Size = new Size(150, 35);
            btnCalcular.Font = new Font("Arial", 10, FontStyle.Bold);
            btnCalcular.BackColor = Color.SteelBlue;
            btnCalcular.ForeColor = Color.White;
            btnCalcular.Click += new EventHandler(btnCalcular_Click);

            btnLimpiar = new Button();
            btnLimpiar.Text = "&Limpiar Campos";
            btnLimpiar.Location = new Point(450, 150);
            btnLimpiar.Size = new Size(150, 35);
            btnLimpiar.Font = new Font("Arial", 10, FontStyle.Regular);
            btnLimpiar.BackColor = Color.LightGray;
            btnLimpiar.Click += new EventHandler(btnLimpiar_Click);

            // DataGridView para la tabla
            dataGridViewDepreciacion = new DataGridView();
            dataGridViewDepreciacion.Location = new Point(50, 270);
            dataGridViewDepreciacion.Size = new Size(800, 300);
            dataGridViewDepreciacion.Font = new Font("Arial", 9, FontStyle.Regular);
            dataGridViewDepreciacion.BackgroundColor = Color.White;
            dataGridViewDepreciacion.BorderStyle = BorderStyle.Fixed3D;
            dataGridViewDepreciacion.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewDepreciacion.ReadOnly = true;
            dataGridViewDepreciacion.RowHeadersVisible = false;
            dataGridViewDepreciacion.AllowUserToAddRows = false;

            // Configurar columnas del DataGridView
            ConfigurarDataGridView();

            // Agregar todos los controles al formulario
            this.Controls.AddRange(new Control[] {
                lblTitulo,
                groupBoxCaracteristicas,
                btnCalcular,
                btnLimpiar,
                dataGridViewDepreciacion
            });
        }

        private void ConfigurarDataGridView()
        {
            // Limpiar columnas existentes
            dataGridViewDepreciacion.Columns.Clear();

            // Agregar columnas
            dataGridViewDepreciacion.Columns.Add("Metodo", "Método de línea recta");
            dataGridViewDepreciacion.Columns.Add("Anio", "Año");
            dataGridViewDepreciacion.Columns.Add("ValorLibros", "Valor en libros");
            dataGridViewDepreciacion.Columns.Add("Depreciacion", "Depreciación");
            dataGridViewDepreciacion.Columns.Add("DepreciacionAcumulada", "Depreciación acumulada");

            // Configurar propiedades de las columnas
            dataGridViewDepreciacion.Columns["Metodo"].Width = 250;
            dataGridViewDepreciacion.Columns["Anio"].Width = 80;
            dataGridViewDepreciacion.Columns["ValorLibros"].Width = 150;
            dataGridViewDepreciacion.Columns["Depreciacion"].Width = 150;
            dataGridViewDepreciacion.Columns["DepreciacionAcumulada"].Width = 170;

            // Centrar texto en columnas
            dataGridViewDepreciacion.Columns["Anio"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewDepreciacion.Columns["ValorLibros"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewDepreciacion.Columns["Depreciacion"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewDepreciacion.Columns["DepreciacionAcumulada"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            // Formato de moneda
            dataGridViewDepreciacion.Columns["ValorLibros"].DefaultCellStyle.Format = "C2";
            dataGridViewDepreciacion.Columns["Depreciacion"].DefaultCellStyle.Format = "C2";
            dataGridViewDepreciacion.Columns["DepreciacionAcumulada"].DefaultCellStyle.Format = "C2";
        }

        private void CalcularDepreciacion()
        {
            try
            {
                decimal costo = decimal.Parse(txtCosto.Text);
                decimal valorResidual = decimal.Parse(txtValorResidual.Text);
                int vidaUtil = int.Parse(txtVidaUtil.Text);

                // Calcular depreciación anual
                decimal depreciacionAnual = (costo - valorResidual) / vidaUtil;
                txtDepreciacion.Text = depreciacionAnual.ToString("F2");

                // Limpiar y recalcular la tabla
                dataGridViewDepreciacion.Rows.Clear();

                // Agregar fila con fórmula
                dataGridViewDepreciacion.Rows.Add(
                    "D = (Costo - Valor residual) / Vida útil",
                    "", "", "", ""
                );

                // Calcular valores para cada año
                decimal valorLibros = costo;
                decimal depreciacionAcumulada = 0;

                // Año 0
                dataGridViewDepreciacion.Rows.Add("", "0", valorLibros, "0.00", "0.00");

                for (int anio = 1; anio <= vidaUtil; anio++)
                {
                    depreciacionAcumulada += depreciacionAnual;
                    valorLibros = costo - depreciacionAcumulada;

                    // Ajustar en el último año para que coincida con el valor residual
                    if (anio == vidaUtil)
                    {
                        valorLibros = valorResidual;
                        // Ajustar la depreciación del último año si es necesario
                        decimal depreciacionAjustada = costo - depreciacionAcumulada + depreciacionAnual - valorResidual;
                        if (depreciacionAjustada != depreciacionAnual)
                        {
                            depreciacionAnual = depreciacionAjustada;
                            depreciacionAcumulada = costo - valorResidual;
                        }
                    }

                    string metodoColumna = (anio == 3) ? "Características" : "";

                    dataGridViewDepreciacion.Rows.Add(
                        metodoColumna,
                        anio.ToString(),
                        valorLibros,
                        depreciacionAnual,
                        depreciacionAcumulada
                    );
                }

                // Aplicar formato especial a algunas filas
                AplicarFormatoEspecial();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error en los datos: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AplicarFormatoEspecial()
        {
            if (dataGridViewDepreciacion.Rows.Count > 0)
            {
                // Fila de fórmula - fondo diferente
                dataGridViewDepreciacion.Rows[0].DefaultCellStyle.BackColor = Color.LightYellow;
                dataGridViewDepreciacion.Rows[0].DefaultCellStyle.Font =
                    new Font("Arial", 9, FontStyle.Italic);

                // Fila de características (año 3) - fondo diferente
                if (dataGridViewDepreciacion.Rows.Count > 4)
                {
                    dataGridViewDepreciacion.Rows[4].DefaultCellStyle.BackColor = Color.LightCyan;
                }

                // Alternar colores en las filas
                for (int i = 1; i < dataGridViewDepreciacion.Rows.Count; i++)
                {
                    if (i % 2 == 0)
                    {
                        dataGridViewDepreciacion.Rows[i].DefaultCellStyle.BackColor = Color.WhiteSmoke;
                    }
                }
            }
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            CalcularDepreciacion();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtCosto.Text = "16000.00";
            txtValorResidual.Text = "1000.00";
            txtVidaUtil.Text = "8";
            CalcularDepreciacion();
        }

        private void ActualizarCalculo(object sender, EventArgs e)
        {
            // Recalcular automáticamente cuando cambien los valores
            if (!string.IsNullOrEmpty(txtCosto.Text) &&
                !string.IsNullOrEmpty(txtValorResidual.Text) &&
                !string.IsNullOrEmpty(txtVidaUtil.Text))
            {
                try
                {
                    decimal costo = decimal.Parse(txtCosto.Text);
                    decimal valorResidual = decimal.Parse(txtValorResidual.Text);
                    int vidaUtil = int.Parse(txtVidaUtil.Text);

                    if (vidaUtil > 0)
                    {
                        decimal depreciacionAnual = (costo - valorResidual) / vidaUtil;
                        txtDepreciacion.Text = depreciacionAnual.ToString("F2");
                    }
                }
                catch
                {
                    // Ignorar errores durante la escritura
                }
            }
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Depreciacion());
        }
    }
}

