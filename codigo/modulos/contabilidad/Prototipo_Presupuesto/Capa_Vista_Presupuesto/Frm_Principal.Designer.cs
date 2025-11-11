namespace Capa_Vista_Presupuesto
{
    partial class Frm_Principal
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
            this.Lbl_1 = new System.Windows.Forms.Label();
            this.Dgv_1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.Btn_Partidas = new System.Windows.Forms.Button();
            this.Btn_Presupuesto = new System.Windows.Forms.Button();
            this.Btn_Traslado = new System.Windows.Forms.Button();
            this.Btn_Salir = new System.Windows.Forms.Button();
            this.Tbc_1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.Dgv_2 = new System.Windows.Forms.DataGridView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.Dgv_3 = new System.Windows.Forms.DataGridView();
            this.Lbl_PresupuestoTotal = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_1)).BeginInit();
            this.Tbc_1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_2)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_3)).BeginInit();
            this.SuspendLayout();
            // 
            // Lbl_1
            // 
            this.Lbl_1.AutoSize = true;
            this.Lbl_1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_1.Location = new System.Drawing.Point(12, 23);
            this.Lbl_1.Name = "Lbl_1";
            this.Lbl_1.Size = new System.Drawing.Size(162, 21);
            this.Lbl_1.TabIndex = 0;
            this.Lbl_1.Text = "Presupuesto Actual";
            // 
            // Dgv_1
            // 
            this.Dgv_1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dgv_1.Location = new System.Drawing.Point(6, 6);
            this.Dgv_1.Name = "Dgv_1";
            this.Dgv_1.Size = new System.Drawing.Size(670, 317);
            this.Dgv_1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(181, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 2;
            // 
            // Btn_Partidas
            // 
            this.Btn_Partidas.Location = new System.Drawing.Point(739, 137);
            this.Btn_Partidas.Name = "Btn_Partidas";
            this.Btn_Partidas.Size = new System.Drawing.Size(110, 48);
            this.Btn_Partidas.TabIndex = 3;
            this.Btn_Partidas.Text = "Partidas Presupuestarias";
            this.Btn_Partidas.UseVisualStyleBackColor = true;
            this.Btn_Partidas.Click += new System.EventHandler(this.Btn_Partidas_Click);
            // 
            // Btn_Presupuesto
            // 
            this.Btn_Presupuesto.Location = new System.Drawing.Point(739, 191);
            this.Btn_Presupuesto.Name = "Btn_Presupuesto";
            this.Btn_Presupuesto.Size = new System.Drawing.Size(110, 48);
            this.Btn_Presupuesto.TabIndex = 4;
            this.Btn_Presupuesto.Text = "Ejecucion";
            this.Btn_Presupuesto.UseVisualStyleBackColor = true;
            this.Btn_Presupuesto.Click += new System.EventHandler(this.Btn_Presupuesto_Click);
            // 
            // Btn_Traslado
            // 
            this.Btn_Traslado.Location = new System.Drawing.Point(739, 245);
            this.Btn_Traslado.Name = "Btn_Traslado";
            this.Btn_Traslado.Size = new System.Drawing.Size(110, 48);
            this.Btn_Traslado.TabIndex = 5;
            this.Btn_Traslado.Text = "Traslado";
            this.Btn_Traslado.UseVisualStyleBackColor = true;
            this.Btn_Traslado.Click += new System.EventHandler(this.Btn_Traslado_Click);
            // 
            // Btn_Salir
            // 
            this.Btn_Salir.Location = new System.Drawing.Point(739, 299);
            this.Btn_Salir.Name = "Btn_Salir";
            this.Btn_Salir.Size = new System.Drawing.Size(110, 48);
            this.Btn_Salir.TabIndex = 6;
            this.Btn_Salir.Text = "Salir";
            this.Btn_Salir.UseVisualStyleBackColor = true;
            this.Btn_Salir.Click += new System.EventHandler(this.Btn_Salir_Click);
            // 
            // Tbc_1
            // 
            this.Tbc_1.Controls.Add(this.tabPage1);
            this.Tbc_1.Controls.Add(this.tabPage2);
            this.Tbc_1.Controls.Add(this.tabPage3);
            this.Tbc_1.Location = new System.Drawing.Point(16, 71);
            this.Tbc_1.Name = "Tbc_1";
            this.Tbc_1.SelectedIndex = 0;
            this.Tbc_1.Size = new System.Drawing.Size(690, 355);
            this.Tbc_1.TabIndex = 7;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.Dgv_1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(682, 329);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Movimientos";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.Dgv_2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(682, 329);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Resumen Áreas";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // Dgv_2
            // 
            this.Dgv_2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dgv_2.Location = new System.Drawing.Point(6, 6);
            this.Dgv_2.Name = "Dgv_2";
            this.Dgv_2.Size = new System.Drawing.Size(608, 317);
            this.Dgv_2.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.Dgv_3);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(682, 329);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Estadísticas";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // Dgv_3
            // 
            this.Dgv_3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dgv_3.Location = new System.Drawing.Point(6, 6);
            this.Dgv_3.Name = "Dgv_3";
            this.Dgv_3.Size = new System.Drawing.Size(608, 317);
            this.Dgv_3.TabIndex = 0;
            // 
            // Lbl_PresupuestoTotal
            // 
            this.Lbl_PresupuestoTotal.AutoSize = true;
            this.Lbl_PresupuestoTotal.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_PresupuestoTotal.Location = new System.Drawing.Point(187, 23);
            this.Lbl_PresupuestoTotal.Name = "Lbl_PresupuestoTotal";
            this.Lbl_PresupuestoTotal.Size = new System.Drawing.Size(14, 21);
            this.Lbl_PresupuestoTotal.TabIndex = 8;
            this.Lbl_PresupuestoTotal.Text = " ";
            // 
            // Frm_Principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(878, 448);
            this.Controls.Add(this.Lbl_PresupuestoTotal);
            this.Controls.Add(this.Tbc_1);
            this.Controls.Add(this.Btn_Salir);
            this.Controls.Add(this.Btn_Traslado);
            this.Controls.Add(this.Btn_Presupuesto);
            this.Controls.Add(this.Btn_Partidas);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Lbl_1);
            this.Name = "Frm_Principal";
            this.Text = " ";
            this.Load += new System.EventHandler(this.Frm_Principal_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_1)).EndInit();
            this.Tbc_1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_2)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Lbl_1;
        private System.Windows.Forms.DataGridView Dgv_1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Btn_Partidas;
        private System.Windows.Forms.Button Btn_Presupuesto;
        private System.Windows.Forms.Button Btn_Traslado;
        private System.Windows.Forms.Button Btn_Salir;
        private System.Windows.Forms.TabControl Tbc_1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView Dgv_2;
        private System.Windows.Forms.DataGridView Dgv_3;
        private System.Windows.Forms.Label Lbl_PresupuestoTotal;
    }
}