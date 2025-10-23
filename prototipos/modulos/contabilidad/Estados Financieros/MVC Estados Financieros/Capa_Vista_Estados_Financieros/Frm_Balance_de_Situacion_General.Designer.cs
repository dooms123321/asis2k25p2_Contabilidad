
namespace Capa_Vista_Estados_Financieros
{
    partial class Frm_Balance_de_Situacion_General
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
            this.Btn_Generar_PDF = new System.Windows.Forms.Button();
            this.Dgv_EstadoResultados = new System.Windows.Forms.DataGridView();
            this.Col_No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col_Cuentas = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col_Monto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col_Suma_Cuentas = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col_Suma_Total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Btn_Salir = new System.Windows.Forms.Button();
            this.Btm_Limpiar = new System.Windows.Forms.Button();
            this.Btn_Generar = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_EstadoResultados)).BeginInit();
            this.SuspendLayout();
            // 
            // Btn_Generar_PDF
            // 
            this.Btn_Generar_PDF.BackColor = System.Drawing.Color.White;
            this.Btn_Generar_PDF.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_Generar_PDF.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Generar_PDF.Location = new System.Drawing.Point(9, 373);
            this.Btn_Generar_PDF.Margin = new System.Windows.Forms.Padding(4);
            this.Btn_Generar_PDF.Name = "Btn_Generar_PDF";
            this.Btn_Generar_PDF.Size = new System.Drawing.Size(160, 74);
            this.Btn_Generar_PDF.TabIndex = 17;
            this.Btn_Generar_PDF.Text = "Generar";
            this.Btn_Generar_PDF.UseVisualStyleBackColor = false;
            // 
            // Dgv_EstadoResultados
            // 
            this.Dgv_EstadoResultados.AllowUserToResizeRows = false;
            this.Dgv_EstadoResultados.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.Dgv_EstadoResultados.BackgroundColor = System.Drawing.Color.White;
            this.Dgv_EstadoResultados.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.Dgv_EstadoResultados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dgv_EstadoResultados.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Col_No,
            this.Col_Cuentas,
            this.Col_Monto,
            this.Col_Suma_Cuentas,
            this.Col_Suma_Total});
            this.Dgv_EstadoResultados.Location = new System.Drawing.Point(209, 137);
            this.Dgv_EstadoResultados.Margin = new System.Windows.Forms.Padding(4);
            this.Dgv_EstadoResultados.Name = "Dgv_EstadoResultados";
            this.Dgv_EstadoResultados.RowHeadersVisible = false;
            this.Dgv_EstadoResultados.RowHeadersWidth = 51;
            this.Dgv_EstadoResultados.RowTemplate.Height = 24;
            this.Dgv_EstadoResultados.Size = new System.Drawing.Size(1239, 493);
            this.Dgv_EstadoResultados.TabIndex = 16;
            // 
            // Col_No
            // 
            this.Col_No.HeaderText = "No.";
            this.Col_No.MinimumWidth = 6;
            this.Col_No.Name = "Col_No";
            // 
            // Col_Cuentas
            // 
            this.Col_Cuentas.HeaderText = "Cuentas";
            this.Col_Cuentas.MinimumWidth = 6;
            this.Col_Cuentas.Name = "Col_Cuentas";
            // 
            // Col_Monto
            // 
            this.Col_Monto.HeaderText = "Montos";
            this.Col_Monto.MinimumWidth = 6;
            this.Col_Monto.Name = "Col_Monto";
            // 
            // Col_Suma_Cuentas
            // 
            this.Col_Suma_Cuentas.HeaderText = "Suma de cuentas";
            this.Col_Suma_Cuentas.MinimumWidth = 6;
            this.Col_Suma_Cuentas.Name = "Col_Suma_Cuentas";
            // 
            // Col_Suma_Total
            // 
            this.Col_Suma_Total.HeaderText = "Suma Total";
            this.Col_Suma_Total.MinimumWidth = 6;
            this.Col_Suma_Total.Name = "Col_Suma_Total";
            // 
            // Btn_Salir
            // 
            this.Btn_Salir.BackColor = System.Drawing.Color.White;
            this.Btn_Salir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_Salir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Salir.ForeColor = System.Drawing.Color.Black;
            this.Btn_Salir.Location = new System.Drawing.Point(1471, 529);
            this.Btn_Salir.Margin = new System.Windows.Forms.Padding(4);
            this.Btn_Salir.Name = "Btn_Salir";
            this.Btn_Salir.Size = new System.Drawing.Size(148, 85);
            this.Btn_Salir.TabIndex = 15;
            this.Btn_Salir.Text = "Salir";
            this.Btn_Salir.UseVisualStyleBackColor = false;
            // 
            // Btm_Limpiar
            // 
            this.Btm_Limpiar.BackColor = System.Drawing.Color.White;
            this.Btm_Limpiar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btm_Limpiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btm_Limpiar.Location = new System.Drawing.Point(9, 270);
            this.Btm_Limpiar.Margin = new System.Windows.Forms.Padding(4);
            this.Btm_Limpiar.Name = "Btm_Limpiar";
            this.Btm_Limpiar.Size = new System.Drawing.Size(160, 76);
            this.Btm_Limpiar.TabIndex = 14;
            this.Btm_Limpiar.Text = "Limpiar";
            this.Btm_Limpiar.UseVisualStyleBackColor = false;
            // 
            // Btn_Generar
            // 
            this.Btn_Generar.BackColor = System.Drawing.Color.White;
            this.Btn_Generar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_Generar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Generar.ForeColor = System.Drawing.Color.Black;
            this.Btn_Generar.Location = new System.Drawing.Point(9, 159);
            this.Btn_Generar.Margin = new System.Windows.Forms.Padding(4);
            this.Btn_Generar.Name = "Btn_Generar";
            this.Btn_Generar.Size = new System.Drawing.Size(160, 74);
            this.Btn_Generar.TabIndex = 13;
            this.Btn_Generar.Text = "Generar";
            this.Btn_Generar.UseVisualStyleBackColor = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Rockwell", 12F);
            this.label4.Location = new System.Drawing.Point(724, 90);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(295, 22);
            this.label4.TabIndex = 12;
            this.label4.Text = "Cifras expresadas en quetzales";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Rockwell", 12F);
            this.label3.Location = new System.Drawing.Point(653, 68);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(411, 22);
            this.label3.TabIndex = 11;
            this.label3.Text = "Del 01 de enero al 31 de diciembre de 2025";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Rockwell", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(650, 8);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(433, 38);
            this.label2.TabIndex = 10;
            this.label2.Text = "Estado de Situación General";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(804, 46);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(161, 22);
            this.label1.TabIndex = 9;
            this.label1.Text = "Hotel San Carlos";
            // 
            // Frm_Balance_de_Situacion_General
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1635, 652);
            this.Controls.Add(this.Btn_Generar_PDF);
            this.Controls.Add(this.Dgv_EstadoResultados);
            this.Controls.Add(this.Btn_Salir);
            this.Controls.Add(this.Btm_Limpiar);
            this.Controls.Add(this.Btn_Generar);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Frm_Balance_de_Situacion_General";
            this.Text = "Balance de Situación General";
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_EstadoResultados)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Btn_Generar_PDF;
        private System.Windows.Forms.DataGridView Dgv_EstadoResultados;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col_No;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col_Cuentas;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col_Monto;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col_Suma_Cuentas;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col_Suma_Total;
        private System.Windows.Forms.Button Btn_Salir;
        private System.Windows.Forms.Button Btm_Limpiar;
        private System.Windows.Forms.Button Btn_Generar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}