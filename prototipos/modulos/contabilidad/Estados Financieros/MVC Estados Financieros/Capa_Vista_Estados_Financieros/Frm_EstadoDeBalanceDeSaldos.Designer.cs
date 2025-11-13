
namespace Capa_Vista_Estados_Financieros
{
    partial class Frm_EstadoDeBalanceDeSaldos
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
            this.label2 = new System.Windows.Forms.Label();
            this.Dgv_EstadoBalanceDeSaldos = new System.Windows.Forms.DataGridView();
            this.Lbl_Diferencia = new System.Windows.Forms.Label();
            this.Lbl_Nivel = new System.Windows.Forms.Label();
            this.Nud_Nivel = new System.Windows.Forms.NumericUpDown();
            this.Cbo_TipoOrigen = new System.Windows.Forms.ComboBox();
            this.Lbl_OrigenDatos = new System.Windows.Forms.Label();
            this.Lbl_Mes = new System.Windows.Forms.Label();
            this.Lbl_Anio = new System.Windows.Forms.Label();
            this.Nud_Mes = new System.Windows.Forms.NumericUpDown();
            this.Nud_Anio = new System.Windows.Forms.NumericUpDown();
            this.Btn_Generar_Reportes = new System.Windows.Forms.Button();
            this.Btn_Generar = new System.Windows.Forms.Button();
            this.Btn_Limpiar = new System.Windows.Forms.Button();
            this.Btn_Salir = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Btn_Ver_Reporte = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_EstadoBalanceDeSaldos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Nud_Nivel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Nud_Mes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Nud_Anio)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Rockwell", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(50, 18);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(407, 38);
            this.label2.TabIndex = 13;
            this.label2.Text = "Reporte Balance de Saldos";
            // 
            // Dgv_EstadoBalanceDeSaldos
            // 
            this.Dgv_EstadoBalanceDeSaldos.AllowUserToResizeRows = false;
            this.Dgv_EstadoBalanceDeSaldos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Dgv_EstadoBalanceDeSaldos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.Dgv_EstadoBalanceDeSaldos.BackgroundColor = System.Drawing.Color.White;
            this.Dgv_EstadoBalanceDeSaldos.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.Dgv_EstadoBalanceDeSaldos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dgv_EstadoBalanceDeSaldos.Location = new System.Drawing.Point(183, 192);
            this.Dgv_EstadoBalanceDeSaldos.Margin = new System.Windows.Forms.Padding(4);
            this.Dgv_EstadoBalanceDeSaldos.Name = "Dgv_EstadoBalanceDeSaldos";
            this.Dgv_EstadoBalanceDeSaldos.RowHeadersVisible = false;
            this.Dgv_EstadoBalanceDeSaldos.RowHeadersWidth = 51;
            this.Dgv_EstadoBalanceDeSaldos.RowTemplate.Height = 24;
            this.Dgv_EstadoBalanceDeSaldos.Size = new System.Drawing.Size(900, 400);
            this.Dgv_EstadoBalanceDeSaldos.TabIndex = 18;
            // 
            // Lbl_Diferencia
            // 
            this.Lbl_Diferencia.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Lbl_Diferencia.Font = new System.Drawing.Font("Rockwell", 13F, System.Drawing.FontStyle.Bold);
            this.Lbl_Diferencia.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.Lbl_Diferencia.Location = new System.Drawing.Point(0, 623);
            this.Lbl_Diferencia.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Lbl_Diferencia.Name = "Lbl_Diferencia";
            this.Lbl_Diferencia.Size = new System.Drawing.Size(1182, 30);
            this.Lbl_Diferencia.TabIndex = 19;
            this.Lbl_Diferencia.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Lbl_Nivel
            // 
            this.Lbl_Nivel.AutoSize = true;
            this.Lbl_Nivel.Font = new System.Drawing.Font("Rockwell", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_Nivel.Location = new System.Drawing.Point(302, 71);
            this.Lbl_Nivel.Name = "Lbl_Nivel";
            this.Lbl_Nivel.Size = new System.Drawing.Size(54, 20);
            this.Lbl_Nivel.TabIndex = 23;
            this.Lbl_Nivel.Text = "Nivel";
            this.Lbl_Nivel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Nud_Nivel
            // 
            this.Nud_Nivel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Nud_Nivel.BackColor = System.Drawing.Color.White;
            this.Nud_Nivel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Nud_Nivel.Font = new System.Drawing.Font("Rockwell", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Nud_Nivel.Location = new System.Drawing.Point(283, 94);
            this.Nud_Nivel.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.Nud_Nivel.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Nud_Nivel.Name = "Nud_Nivel";
            this.Nud_Nivel.Size = new System.Drawing.Size(100, 27);
            this.Nud_Nivel.TabIndex = 25;
            this.Nud_Nivel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Nud_Nivel.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // Cbo_TipoOrigen
            // 
            this.Cbo_TipoOrigen.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Cbo_TipoOrigen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(204)))), ((int)(((byte)(153)))));
            this.Cbo_TipoOrigen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cbo_TipoOrigen.Font = new System.Drawing.Font("Rockwell", 10.2F, System.Drawing.FontStyle.Bold);
            this.Cbo_TipoOrigen.FormattingEnabled = true;
            this.Cbo_TipoOrigen.Items.AddRange(new object[] {
            "Actual",
            "Histórico"});
            this.Cbo_TipoOrigen.Location = new System.Drawing.Point(57, 97);
            this.Cbo_TipoOrigen.Name = "Cbo_TipoOrigen";
            this.Cbo_TipoOrigen.Size = new System.Drawing.Size(160, 28);
            this.Cbo_TipoOrigen.TabIndex = 26;
            // 
            // Lbl_OrigenDatos
            // 
            this.Lbl_OrigenDatos.AutoSize = true;
            this.Lbl_OrigenDatos.Font = new System.Drawing.Font("Rockwell", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_OrigenDatos.Location = new System.Drawing.Point(69, 74);
            this.Lbl_OrigenDatos.Name = "Lbl_OrigenDatos";
            this.Lbl_OrigenDatos.Size = new System.Drawing.Size(148, 20);
            this.Lbl_OrigenDatos.TabIndex = 27;
            this.Lbl_OrigenDatos.Text = "Origen de Datos";
            this.Lbl_OrigenDatos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Lbl_Mes
            // 
            this.Lbl_Mes.AutoSize = true;
            this.Lbl_Mes.Font = new System.Drawing.Font("Rockwell", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_Mes.Location = new System.Drawing.Point(310, 128);
            this.Lbl_Mes.Name = "Lbl_Mes";
            this.Lbl_Mes.Size = new System.Drawing.Size(46, 20);
            this.Lbl_Mes.TabIndex = 45;
            this.Lbl_Mes.Text = "Mes";
            this.Lbl_Mes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Lbl_Anio
            // 
            this.Lbl_Anio.AutoSize = true;
            this.Lbl_Anio.Font = new System.Drawing.Font("Rockwell", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_Anio.Location = new System.Drawing.Point(116, 128);
            this.Lbl_Anio.Name = "Lbl_Anio";
            this.Lbl_Anio.Size = new System.Drawing.Size(42, 20);
            this.Lbl_Anio.TabIndex = 44;
            this.Lbl_Anio.Text = "Año";
            this.Lbl_Anio.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Nud_Mes
            // 
            this.Nud_Mes.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Nud_Mes.BackColor = System.Drawing.Color.White;
            this.Nud_Mes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Nud_Mes.Font = new System.Drawing.Font("Rockwell", 10.2F, System.Drawing.FontStyle.Bold);
            this.Nud_Mes.Location = new System.Drawing.Point(283, 151);
            this.Nud_Mes.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.Nud_Mes.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Nud_Mes.Name = "Nud_Mes";
            this.Nud_Mes.Size = new System.Drawing.Size(100, 27);
            this.Nud_Mes.TabIndex = 43;
            this.Nud_Mes.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Nud_Mes.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // Nud_Anio
            // 
            this.Nud_Anio.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Nud_Anio.BackColor = System.Drawing.Color.White;
            this.Nud_Anio.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Nud_Anio.Font = new System.Drawing.Font("Rockwell", 10.2F, System.Drawing.FontStyle.Bold);
            this.Nud_Anio.Location = new System.Drawing.Point(84, 151);
            this.Nud_Anio.Maximum = new decimal(new int[] {
            2100,
            0,
            0,
            0});
            this.Nud_Anio.Minimum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.Nud_Anio.Name = "Nud_Anio";
            this.Nud_Anio.Size = new System.Drawing.Size(100, 27);
            this.Nud_Anio.TabIndex = 42;
            this.Nud_Anio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Nud_Anio.Value = new decimal(new int[] {
            2025,
            0,
            0,
            0});
            // 
            // Btn_Generar_Reportes
            // 
            this.Btn_Generar_Reportes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.Btn_Generar_Reportes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_Generar_Reportes.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.Btn_Generar_Reportes.FlatAppearance.BorderSize = 2;
            this.Btn_Generar_Reportes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Generar_Reportes.Font = new System.Drawing.Font("Rockwell", 10.2F, System.Drawing.FontStyle.Bold);
            this.Btn_Generar_Reportes.Location = new System.Drawing.Point(13, 376);
            this.Btn_Generar_Reportes.Margin = new System.Windows.Forms.Padding(4);
            this.Btn_Generar_Reportes.Name = "Btn_Generar_Reportes";
            this.Btn_Generar_Reportes.Size = new System.Drawing.Size(150, 55);
            this.Btn_Generar_Reportes.TabIndex = 20;
            this.Btn_Generar_Reportes.Text = "Capturar Cuentas ​​✉";
            this.Btn_Generar_Reportes.UseVisualStyleBackColor = false;
            this.Btn_Generar_Reportes.Click += new System.EventHandler(this.Btn_Ver_Reporte_Click);
            // 
            // Btn_Generar
            // 
            this.Btn_Generar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.Btn_Generar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_Generar.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.Btn_Generar.FlatAppearance.BorderSize = 2;
            this.Btn_Generar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Generar.Font = new System.Drawing.Font("Rockwell", 10.2F, System.Drawing.FontStyle.Bold);
            this.Btn_Generar.ForeColor = System.Drawing.Color.Black;
            this.Btn_Generar.Location = new System.Drawing.Point(13, 192);
            this.Btn_Generar.Margin = new System.Windows.Forms.Padding(4);
            this.Btn_Generar.Name = "Btn_Generar";
            this.Btn_Generar.Size = new System.Drawing.Size(150, 45);
            this.Btn_Generar.TabIndex = 9;
            this.Btn_Generar.Text = "Generar 🔎​";
            this.Btn_Generar.UseVisualStyleBackColor = false;
            this.Btn_Generar.Click += new System.EventHandler(this.Btn_Generar_Click);
            // 
            // Btn_Limpiar
            // 
            this.Btn_Limpiar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.Btn_Limpiar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_Limpiar.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.Btn_Limpiar.FlatAppearance.BorderSize = 2;
            this.Btn_Limpiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Limpiar.Font = new System.Drawing.Font("Rockwell", 10.2F, System.Drawing.FontStyle.Bold);
            this.Btn_Limpiar.Location = new System.Drawing.Point(13, 287);
            this.Btn_Limpiar.Margin = new System.Windows.Forms.Padding(4);
            this.Btn_Limpiar.Name = "Btn_Limpiar";
            this.Btn_Limpiar.Size = new System.Drawing.Size(150, 45);
            this.Btn_Limpiar.TabIndex = 10;
            this.Btn_Limpiar.Text = "Limpiar ​🧹​";
            this.Btn_Limpiar.UseVisualStyleBackColor = false;
            this.Btn_Limpiar.Click += new System.EventHandler(this.Btn_Limpiar_Click);
            // 
            // Btn_Salir
            // 
            this.Btn_Salir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.Btn_Salir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_Salir.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.Btn_Salir.FlatAppearance.BorderSize = 2;
            this.Btn_Salir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Salir.Font = new System.Drawing.Font("Rockwell", 10.2F, System.Drawing.FontStyle.Bold);
            this.Btn_Salir.ForeColor = System.Drawing.Color.Black;
            this.Btn_Salir.Location = new System.Drawing.Point(13, 462);
            this.Btn_Salir.Margin = new System.Windows.Forms.Padding(4);
            this.Btn_Salir.Name = "Btn_Salir";
            this.Btn_Salir.Size = new System.Drawing.Size(150, 45);
            this.Btn_Salir.TabIndex = 17;
            this.Btn_Salir.Text = "Salir ↩︎";
            this.Btn_Salir.UseVisualStyleBackColor = false;
            this.Btn_Salir.Click += new System.EventHandler(this.Btn_Salir_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Lbl_Mes);
            this.groupBox1.Controls.Add(this.Lbl_Anio);
            this.groupBox1.Controls.Add(this.Nud_Mes);
            this.groupBox1.Controls.Add(this.Nud_Anio);
            this.groupBox1.Controls.Add(this.Lbl_OrigenDatos);
            this.groupBox1.Controls.Add(this.Cbo_TipoOrigen);
            this.groupBox1.Controls.Add(this.Nud_Nivel);
            this.groupBox1.Controls.Add(this.Lbl_Nivel);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(393, -4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(504, 189);
            this.groupBox1.TabIndex = 46;
            this.groupBox1.TabStop = false;
            // 
            // Btn_Ver_Reporte
            // 
            this.Btn_Ver_Reporte.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.Btn_Ver_Reporte.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_Ver_Reporte.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.Btn_Ver_Reporte.FlatAppearance.BorderSize = 2;
            this.Btn_Ver_Reporte.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Ver_Reporte.Font = new System.Drawing.Font("Rockwell", 10.2F, System.Drawing.FontStyle.Bold);
            this.Btn_Ver_Reporte.Location = new System.Drawing.Point(11, 22);
            this.Btn_Ver_Reporte.Margin = new System.Windows.Forms.Padding(4);
            this.Btn_Ver_Reporte.Name = "Btn_Ver_Reporte";
            this.Btn_Ver_Reporte.Size = new System.Drawing.Size(150, 55);
            this.Btn_Ver_Reporte.TabIndex = 47;
            this.Btn_Ver_Reporte.Text = "Ver reporte 🔎​";
            this.Btn_Ver_Reporte.UseVisualStyleBackColor = false;
            this.Btn_Ver_Reporte.Click += new System.EventHandler(this.Btn_Ver_Reporte_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Btn_Ver_Reporte);
            this.groupBox2.Location = new System.Drawing.Point(977, 40);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(168, 93);
            this.groupBox2.TabIndex = 48;
            this.groupBox2.TabStop = false;
            // 
            // Frm_EstadoDeBalanceDeSaldos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.ClientSize = new System.Drawing.Size(1182, 653);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Btn_Salir);
            this.Controls.Add(this.Btn_Generar_Reportes);
            this.Controls.Add(this.Btn_Limpiar);
            this.Controls.Add(this.Btn_Generar);
            this.Controls.Add(this.Lbl_Diferencia);
            this.Controls.Add(this.Dgv_EstadoBalanceDeSaldos);
            this.Name = "Frm_EstadoDeBalanceDeSaldos";
            this.Text = "Frm_EstadoDeBalanceDeSaldos";
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_EstadoBalanceDeSaldos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Nud_Nivel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Nud_Mes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Nud_Anio)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView Dgv_EstadoBalanceDeSaldos;
        private System.Windows.Forms.Label Lbl_Diferencia;
        private System.Windows.Forms.Label Lbl_Nivel;
        private System.Windows.Forms.NumericUpDown Nud_Nivel;
        private System.Windows.Forms.ComboBox Cbo_TipoOrigen;
        private System.Windows.Forms.Label Lbl_OrigenDatos;
        private System.Windows.Forms.Label Lbl_Mes;
        private System.Windows.Forms.Label Lbl_Anio;
        private System.Windows.Forms.NumericUpDown Nud_Mes;
        private System.Windows.Forms.NumericUpDown Nud_Anio;
        private System.Windows.Forms.Button Btn_Generar_Reportes;
        private System.Windows.Forms.Button Btn_Generar;
        private System.Windows.Forms.Button Btn_Limpiar;
        private System.Windows.Forms.Button Btn_Salir;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button Btn_Ver_Reporte;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}