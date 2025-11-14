
namespace Capa_Vista_CierreContable
{
    partial class Frm_CierreContable
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_CierreContable));
            this.Dgv_Cuentas = new System.Windows.Forms.DataGridView();
            this.Cbo_periodo = new System.Windows.Forms.ComboBox();
            this.Gbp_datos = new System.Windows.Forms.GroupBox();
            this.Cbo_actualizacion = new System.Windows.Forms.ComboBox();
            this.Lbl_actualizacion = new System.Windows.Forms.Label();
            this.Dtp_fecha_cierre = new System.Windows.Forms.DateTimePicker();
            this.Dtp_Fecha_Hasta = new System.Windows.Forms.DateTimePicker();
            this.Dtp_fecha_desde = new System.Windows.Forms.DateTimePicker();
            this.Lbl_hasta = new System.Windows.Forms.Label();
            this.Lbl_fecha_desde = new System.Windows.Forms.Label();
            this.Lbl_fecha_de_cierre = new System.Windows.Forms.Label();
            this.Lbl_periodo = new System.Windows.Forms.Label();
            this.Lbl_saldos_totales = new System.Windows.Forms.Label();
            this.Lbl_Debe = new System.Windows.Forms.Label();
            this.Lbl_cierre_contable = new System.Windows.Forms.Label();
            this.Lbl_haber = new System.Windows.Forms.Label();
            this.Lbl_TotalDebe = new System.Windows.Forms.Label();
            this.Lbl_TotalHaber = new System.Windows.Forms.Label();
            this.Lbl_SaldosTotales = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.Btn_salir = new System.Windows.Forms.Button();
            this.Btn_imprimir = new System.Windows.Forms.Button();
            this.Btn_cargar = new System.Windows.Forms.Button();
            this.Btn_Guardar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_Cuentas)).BeginInit();
            this.Gbp_datos.SuspendLayout();
            this.SuspendLayout();
            // 
            // Dgv_Cuentas
            // 
            this.Dgv_Cuentas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dgv_Cuentas.Location = new System.Drawing.Point(46, 267);
            this.Dgv_Cuentas.Name = "Dgv_Cuentas";
            this.Dgv_Cuentas.RowHeadersWidth = 51;
            this.Dgv_Cuentas.RowTemplate.Height = 24;
            this.Dgv_Cuentas.Size = new System.Drawing.Size(1020, 219);
            this.Dgv_Cuentas.TabIndex = 0;
            this.Dgv_Cuentas.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dgv_Cuentas_CellContentClick);
            // 
            // Cbo_periodo
            // 
            this.Cbo_periodo.FormattingEnabled = true;
            this.Cbo_periodo.Location = new System.Drawing.Point(108, 27);
            this.Cbo_periodo.Name = "Cbo_periodo";
            this.Cbo_periodo.Size = new System.Drawing.Size(134, 24);
            this.Cbo_periodo.TabIndex = 1;
            this.Cbo_periodo.SelectedIndexChanged += new System.EventHandler(this.Cbo_periodo_SelectedIndexChanged);
            // 
            // Gbp_datos
            // 
            this.Gbp_datos.Controls.Add(this.Cbo_actualizacion);
            this.Gbp_datos.Controls.Add(this.Lbl_actualizacion);
            this.Gbp_datos.Controls.Add(this.Dtp_fecha_cierre);
            this.Gbp_datos.Controls.Add(this.Dtp_Fecha_Hasta);
            this.Gbp_datos.Controls.Add(this.Dtp_fecha_desde);
            this.Gbp_datos.Controls.Add(this.Lbl_hasta);
            this.Gbp_datos.Controls.Add(this.Lbl_fecha_desde);
            this.Gbp_datos.Controls.Add(this.Lbl_fecha_de_cierre);
            this.Gbp_datos.Controls.Add(this.Lbl_periodo);
            this.Gbp_datos.Controls.Add(this.Cbo_periodo);
            this.Gbp_datos.Location = new System.Drawing.Point(22, 133);
            this.Gbp_datos.Name = "Gbp_datos";
            this.Gbp_datos.Size = new System.Drawing.Size(956, 128);
            this.Gbp_datos.TabIndex = 2;
            this.Gbp_datos.TabStop = false;
            this.Gbp_datos.Enter += new System.EventHandler(this.Gbp_datos_Enter);
            // 
            // Cbo_actualizacion
            // 
            this.Cbo_actualizacion.FormattingEnabled = true;
            this.Cbo_actualizacion.Location = new System.Drawing.Point(817, 27);
            this.Cbo_actualizacion.Name = "Cbo_actualizacion";
            this.Cbo_actualizacion.Size = new System.Drawing.Size(121, 24);
            this.Cbo_actualizacion.TabIndex = 10;
            this.Cbo_actualizacion.SelectedIndexChanged += new System.EventHandler(this.Cbo_actualizacion_SelectedIndexChanged);
            // 
            // Lbl_actualizacion
            // 
            this.Lbl_actualizacion.AutoSize = true;
            this.Lbl_actualizacion.Font = new System.Drawing.Font("Rockwell", 10F);
            this.Lbl_actualizacion.Location = new System.Drawing.Point(681, 27);
            this.Lbl_actualizacion.Name = "Lbl_actualizacion";
            this.Lbl_actualizacion.Size = new System.Drawing.Size(119, 20);
            this.Lbl_actualizacion.TabIndex = 9;
            this.Lbl_actualizacion.Text = "Actualización ";
            // 
            // Dtp_fecha_cierre
            // 
            this.Dtp_fecha_cierre.Location = new System.Drawing.Point(450, 29);
            this.Dtp_fecha_cierre.Name = "Dtp_fecha_cierre";
            this.Dtp_fecha_cierre.Size = new System.Drawing.Size(200, 22);
            this.Dtp_fecha_cierre.TabIndex = 8;
            this.Dtp_fecha_cierre.ValueChanged += new System.EventHandler(this.Dtp_fecha_cierre_ValueChanged);
            // 
            // Dtp_Fecha_Hasta
            // 
            this.Dtp_Fecha_Hasta.Location = new System.Drawing.Point(505, 79);
            this.Dtp_Fecha_Hasta.Name = "Dtp_Fecha_Hasta";
            this.Dtp_Fecha_Hasta.Size = new System.Drawing.Size(200, 22);
            this.Dtp_Fecha_Hasta.TabIndex = 7;
            this.Dtp_Fecha_Hasta.ValueChanged += new System.EventHandler(this.Dtp_Fecha_Hasta_ValueChanged);
            // 
            // Dtp_fecha_desde
            // 
            this.Dtp_fecha_desde.Location = new System.Drawing.Point(152, 76);
            this.Dtp_fecha_desde.Name = "Dtp_fecha_desde";
            this.Dtp_fecha_desde.Size = new System.Drawing.Size(200, 22);
            this.Dtp_fecha_desde.TabIndex = 6;
            // 
            // Lbl_hasta
            // 
            this.Lbl_hasta.AutoSize = true;
            this.Lbl_hasta.Font = new System.Drawing.Font("Rockwell", 10F);
            this.Lbl_hasta.Location = new System.Drawing.Point(380, 76);
            this.Lbl_hasta.Name = "Lbl_hasta";
            this.Lbl_hasta.Size = new System.Drawing.Size(101, 20);
            this.Lbl_hasta.TabIndex = 5;
            this.Lbl_hasta.Text = "Fecha hasta";
            // 
            // Lbl_fecha_desde
            // 
            this.Lbl_fecha_desde.AutoSize = true;
            this.Lbl_fecha_desde.Font = new System.Drawing.Font("Rockwell", 10F);
            this.Lbl_fecha_desde.Location = new System.Drawing.Point(21, 79);
            this.Lbl_fecha_desde.Name = "Lbl_fecha_desde";
            this.Lbl_fecha_desde.Size = new System.Drawing.Size(108, 20);
            this.Lbl_fecha_desde.TabIndex = 4;
            this.Lbl_fecha_desde.Text = "Fecha desde";
            this.Lbl_fecha_desde.Click += new System.EventHandler(this.Lbl_fecha_desde_Click);
            // 
            // Lbl_fecha_de_cierre
            // 
            this.Lbl_fecha_de_cierre.AutoSize = true;
            this.Lbl_fecha_de_cierre.Font = new System.Drawing.Font("Rockwell", 10F);
            this.Lbl_fecha_de_cierre.Location = new System.Drawing.Point(282, 27);
            this.Lbl_fecha_de_cierre.Name = "Lbl_fecha_de_cierre";
            this.Lbl_fecha_de_cierre.Size = new System.Drawing.Size(136, 20);
            this.Lbl_fecha_de_cierre.TabIndex = 3;
            this.Lbl_fecha_de_cierre.Text = "Fecha de cierre ";
            // 
            // Lbl_periodo
            // 
            this.Lbl_periodo.AutoSize = true;
            this.Lbl_periodo.Font = new System.Drawing.Font("Rockwell", 10F);
            this.Lbl_periodo.Location = new System.Drawing.Point(18, 27);
            this.Lbl_periodo.Name = "Lbl_periodo";
            this.Lbl_periodo.Size = new System.Drawing.Size(71, 20);
            this.Lbl_periodo.TabIndex = 2;
            this.Lbl_periodo.Text = "Periodo";
            this.Lbl_periodo.Click += new System.EventHandler(this.label1_Click);
            // 
            // Lbl_saldos_totales
            // 
            this.Lbl_saldos_totales.AutoSize = true;
            this.Lbl_saldos_totales.Font = new System.Drawing.Font("Rockwell", 10F);
            this.Lbl_saldos_totales.Location = new System.Drawing.Point(578, 528);
            this.Lbl_saldos_totales.Name = "Lbl_saldos_totales";
            this.Lbl_saldos_totales.Size = new System.Drawing.Size(126, 20);
            this.Lbl_saldos_totales.TabIndex = 3;
            this.Lbl_saldos_totales.Text = "Saldos Totales:";
            this.Lbl_saldos_totales.Click += new System.EventHandler(this.label1_Click_1);
            // 
            // Lbl_Debe
            // 
            this.Lbl_Debe.AutoSize = true;
            this.Lbl_Debe.Font = new System.Drawing.Font("Rockwell", 10F);
            this.Lbl_Debe.Location = new System.Drawing.Point(42, 522);
            this.Lbl_Debe.Name = "Lbl_Debe";
            this.Lbl_Debe.Size = new System.Drawing.Size(99, 20);
            this.Lbl_Debe.TabIndex = 4;
            this.Lbl_Debe.Text = "Total Debe:";
            this.Lbl_Debe.Click += new System.EventHandler(this.Lbl_Debe_Click);
            // 
            // Lbl_cierre_contable
            // 
            this.Lbl_cierre_contable.AutoSize = true;
            this.Lbl_cierre_contable.Font = new System.Drawing.Font("Rockwell", 20F);
            this.Lbl_cierre_contable.Location = new System.Drawing.Point(167, 66);
            this.Lbl_cierre_contable.Name = "Lbl_cierre_contable";
            this.Lbl_cierre_contable.Size = new System.Drawing.Size(271, 38);
            this.Lbl_cierre_contable.TabIndex = 5;
            this.Lbl_cierre_contable.Text = "Cierre Contable";
            // 
            // Lbl_haber
            // 
            this.Lbl_haber.AutoSize = true;
            this.Lbl_haber.Font = new System.Drawing.Font("Rockwell", 10F);
            this.Lbl_haber.Location = new System.Drawing.Point(311, 525);
            this.Lbl_haber.Name = "Lbl_haber";
            this.Lbl_haber.Size = new System.Drawing.Size(109, 20);
            this.Lbl_haber.TabIndex = 6;
            this.Lbl_haber.Text = "Total  Haber:";
            // 
            // Lbl_TotalDebe
            // 
            this.Lbl_TotalDebe.AutoSize = true;
            this.Lbl_TotalDebe.Location = new System.Drawing.Point(205, 525);
            this.Lbl_TotalDebe.Name = "Lbl_TotalDebe";
            this.Lbl_TotalDebe.Size = new System.Drawing.Size(39, 16);
            this.Lbl_TotalDebe.TabIndex = 15;
            this.Lbl_TotalDebe.Text = "- - - - -";
            // 
            // Lbl_TotalHaber
            // 
            this.Lbl_TotalHaber.AutoSize = true;
            this.Lbl_TotalHaber.Location = new System.Drawing.Point(474, 525);
            this.Lbl_TotalHaber.Name = "Lbl_TotalHaber";
            this.Lbl_TotalHaber.Size = new System.Drawing.Size(39, 16);
            this.Lbl_TotalHaber.TabIndex = 16;
            this.Lbl_TotalHaber.Text = "- - - - -";
            this.Lbl_TotalHaber.Click += new System.EventHandler(this.Lbl_TotalHaber_Click);
            // 
            // Lbl_SaldosTotales
            // 
            this.Lbl_SaldosTotales.AutoSize = true;
            this.Lbl_SaldosTotales.Location = new System.Drawing.Point(747, 528);
            this.Lbl_SaldosTotales.Name = "Lbl_SaldosTotales";
            this.Lbl_SaldosTotales.Size = new System.Drawing.Size(39, 16);
            this.Lbl_SaldosTotales.TabIndex = 17;
            this.Lbl_SaldosTotales.Text = "- - - - -";
            // 
            // button1
            // 
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(947, 66);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(53, 46);
            this.button1.TabIndex = 18;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Btn_salir
            // 
            this.Btn_salir.Image = ((System.Drawing.Image)(resources.GetObject("Btn_salir.Image")));
            this.Btn_salir.Location = new System.Drawing.Point(1026, 65);
            this.Btn_salir.Name = "Btn_salir";
            this.Btn_salir.Size = new System.Drawing.Size(56, 50);
            this.Btn_salir.TabIndex = 14;
            this.Btn_salir.UseVisualStyleBackColor = true;
            this.Btn_salir.Click += new System.EventHandler(this.Btn_salir_Click);
            // 
            // Btn_imprimir
            // 
            this.Btn_imprimir.Image = ((System.Drawing.Image)(resources.GetObject("Btn_imprimir.Image")));
            this.Btn_imprimir.Location = new System.Drawing.Point(866, 67);
            this.Btn_imprimir.Name = "Btn_imprimir";
            this.Btn_imprimir.Size = new System.Drawing.Size(52, 48);
            this.Btn_imprimir.TabIndex = 11;
            this.Btn_imprimir.UseVisualStyleBackColor = true;
            this.Btn_imprimir.Click += new System.EventHandler(this.Btn_imprimir_Click);
            // 
            // Btn_cargar
            // 
            this.Btn_cargar.Image = ((System.Drawing.Image)(resources.GetObject("Btn_cargar.Image")));
            this.Btn_cargar.Location = new System.Drawing.Point(707, 65);
            this.Btn_cargar.Name = "Btn_cargar";
            this.Btn_cargar.Size = new System.Drawing.Size(51, 47);
            this.Btn_cargar.TabIndex = 10;
            this.Btn_cargar.UseVisualStyleBackColor = true;
            this.Btn_cargar.Click += new System.EventHandler(this.Btn_cargar_Click);
            // 
            // Btn_Guardar
            // 
            this.Btn_Guardar.Image = ((System.Drawing.Image)(resources.GetObject("Btn_Guardar.Image")));
            this.Btn_Guardar.Location = new System.Drawing.Point(779, 65);
            this.Btn_Guardar.Name = "Btn_Guardar";
            this.Btn_Guardar.Size = new System.Drawing.Size(64, 47);
            this.Btn_Guardar.TabIndex = 19;
            this.Btn_Guardar.UseVisualStyleBackColor = true;
            this.Btn_Guardar.Click += new System.EventHandler(this.Btn_Guardar_Click_1);
            // 
            // Frm_CierreContable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1131, 586);
            this.Controls.Add(this.Btn_Guardar);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Lbl_SaldosTotales);
            this.Controls.Add(this.Lbl_TotalHaber);
            this.Controls.Add(this.Lbl_TotalDebe);
            this.Controls.Add(this.Btn_salir);
            this.Controls.Add(this.Btn_imprimir);
            this.Controls.Add(this.Btn_cargar);
            this.Controls.Add(this.Lbl_haber);
            this.Controls.Add(this.Lbl_cierre_contable);
            this.Controls.Add(this.Lbl_Debe);
            this.Controls.Add(this.Lbl_saldos_totales);
            this.Controls.Add(this.Gbp_datos);
            this.Controls.Add(this.Dgv_Cuentas);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Frm_CierreContable";
            this.Text = "Frm_CierreContale";
            this.Load += new System.EventHandler(this.Frm_CierreContable_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_Cuentas)).EndInit();
            this.Gbp_datos.ResumeLayout(false);
            this.Gbp_datos.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView Dgv_Cuentas;
        private System.Windows.Forms.ComboBox Cbo_periodo;
        private System.Windows.Forms.GroupBox Gbp_datos;
        private System.Windows.Forms.Label Lbl_periodo;
        private System.Windows.Forms.DateTimePicker Dtp_fecha_desde;
        private System.Windows.Forms.Label Lbl_hasta;
        private System.Windows.Forms.Label Lbl_fecha_desde;
        private System.Windows.Forms.Label Lbl_fecha_de_cierre;
        private System.Windows.Forms.Label Lbl_saldos_totales;
        private System.Windows.Forms.Label Lbl_Debe;
        private System.Windows.Forms.Label Lbl_cierre_contable;
        private System.Windows.Forms.DateTimePicker Dtp_fecha_cierre;
        private System.Windows.Forms.DateTimePicker Dtp_Fecha_Hasta;
        private System.Windows.Forms.Label Lbl_haber;
        private System.Windows.Forms.Button Btn_cargar;
        private System.Windows.Forms.Button Btn_imprimir;
        private System.Windows.Forms.Button Btn_salir;
        private System.Windows.Forms.Label Lbl_TotalDebe;
        private System.Windows.Forms.Label Lbl_TotalHaber;
        private System.Windows.Forms.Label Lbl_SaldosTotales;
        private System.Windows.Forms.ComboBox Cbo_actualizacion;
        private System.Windows.Forms.Label Lbl_actualizacion;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button Btn_Guardar;
    }
}