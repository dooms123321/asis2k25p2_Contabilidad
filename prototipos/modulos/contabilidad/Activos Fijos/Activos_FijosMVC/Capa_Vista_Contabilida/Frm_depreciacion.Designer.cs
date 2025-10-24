
namespace Capa_Vista_Contabilida
{
    partial class Frm_depreciacion
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Gpb_caracteristicas_activo_fijo = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Cbo_buscar_activo_fijo = new System.Windows.Forms.ComboBox();
            this.Btn_buscar_activo_fijo = new System.Windows.Forms.Button();
            this.Lbl_costo_activo_fijo = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Lbl_vida_activo_fijo = new System.Windows.Forms.Label();
            this.Lbl_deprecioacion_anual = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.Btn_calcular = new System.Windows.Forms.Button();
            this.Btn_limpiar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.Gpb_caracteristicas_activo_fijo.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(56, 439);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.Size = new System.Drawing.Size(992, 214);
            this.dataGridView1.TabIndex = 0;
            // 
            // Gpb_caracteristicas_activo_fijo
            // 
            this.Gpb_caracteristicas_activo_fijo.Controls.Add(this.textBox4);
            this.Gpb_caracteristicas_activo_fijo.Controls.Add(this.textBox3);
            this.Gpb_caracteristicas_activo_fijo.Controls.Add(this.textBox2);
            this.Gpb_caracteristicas_activo_fijo.Controls.Add(this.textBox1);
            this.Gpb_caracteristicas_activo_fijo.Controls.Add(this.Lbl_deprecioacion_anual);
            this.Gpb_caracteristicas_activo_fijo.Controls.Add(this.Lbl_vida_activo_fijo);
            this.Gpb_caracteristicas_activo_fijo.Controls.Add(this.label2);
            this.Gpb_caracteristicas_activo_fijo.Controls.Add(this.Lbl_costo_activo_fijo);
            this.Gpb_caracteristicas_activo_fijo.Controls.Add(this.Btn_buscar_activo_fijo);
            this.Gpb_caracteristicas_activo_fijo.Controls.Add(this.Cbo_buscar_activo_fijo);
            this.Gpb_caracteristicas_activo_fijo.Font = new System.Drawing.Font("Rockwell", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Gpb_caracteristicas_activo_fijo.Location = new System.Drawing.Point(56, 147);
            this.Gpb_caracteristicas_activo_fijo.Name = "Gpb_caracteristicas_activo_fijo";
            this.Gpb_caracteristicas_activo_fijo.Size = new System.Drawing.Size(554, 275);
            this.Gpb_caracteristicas_activo_fijo.TabIndex = 1;
            this.Gpb_caracteristicas_activo_fijo.TabStop = false;
            this.Gpb_caracteristicas_activo_fijo.Text = "Datos para el calculo";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Rockwell", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(246, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(647, 41);
            this.label1.TabIndex = 2;
            this.label1.Text = "Depreciacion Linea Recta - Activo fijo";
            // 
            // Cbo_buscar_activo_fijo
            // 
            this.Cbo_buscar_activo_fijo.FormattingEnabled = true;
            this.Cbo_buscar_activo_fijo.Location = new System.Drawing.Point(233, 33);
            this.Cbo_buscar_activo_fijo.Name = "Cbo_buscar_activo_fijo";
            this.Cbo_buscar_activo_fijo.Size = new System.Drawing.Size(291, 33);
            this.Cbo_buscar_activo_fijo.TabIndex = 0;
            // 
            // Btn_buscar_activo_fijo
            // 
            this.Btn_buscar_activo_fijo.Location = new System.Drawing.Point(6, 32);
            this.Btn_buscar_activo_fijo.Name = "Btn_buscar_activo_fijo";
            this.Btn_buscar_activo_fijo.Size = new System.Drawing.Size(221, 44);
            this.Btn_buscar_activo_fijo.TabIndex = 1;
            this.Btn_buscar_activo_fijo.Text = "Buscar Activo Fijo";
            this.Btn_buscar_activo_fijo.UseVisualStyleBackColor = true;
            // 
            // Lbl_costo_activo_fijo
            // 
            this.Lbl_costo_activo_fijo.AutoSize = true;
            this.Lbl_costo_activo_fijo.Location = new System.Drawing.Point(38, 85);
            this.Lbl_costo_activo_fijo.Name = "Lbl_costo_activo_fijo";
            this.Lbl_costo_activo_fijo.Size = new System.Drawing.Size(135, 25);
            this.Lbl_costo_activo_fijo.TabIndex = 2;
            this.Lbl_costo_activo_fijo.Text = "Costo Total:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 123);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(166, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "Valor Residual:";
            // 
            // Lbl_vida_activo_fijo
            // 
            this.Lbl_vida_activo_fijo.AutoSize = true;
            this.Lbl_vida_activo_fijo.Location = new System.Drawing.Point(7, 162);
            this.Lbl_vida_activo_fijo.Name = "Lbl_vida_activo_fijo";
            this.Lbl_vida_activo_fijo.Size = new System.Drawing.Size(173, 25);
            this.Lbl_vida_activo_fijo.TabIndex = 4;
            this.Lbl_vida_activo_fijo.Text = "Vida util (años):";
            // 
            // Lbl_deprecioacion_anual
            // 
            this.Lbl_deprecioacion_anual.AutoSize = true;
            this.Lbl_deprecioacion_anual.Location = new System.Drawing.Point(6, 229);
            this.Lbl_deprecioacion_anual.Name = "Lbl_deprecioacion_anual";
            this.Lbl_deprecioacion_anual.Size = new System.Drawing.Size(219, 25);
            this.Lbl_deprecioacion_anual.TabIndex = 5;
            this.Lbl_deprecioacion_anual.Text = "Depreciacion Anual:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(197, 82);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(327, 33);
            this.textBox1.TabIndex = 6;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(197, 120);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(327, 33);
            this.textBox2.TabIndex = 7;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(197, 159);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(327, 33);
            this.textBox3.TabIndex = 8;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(231, 229);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(293, 33);
            this.textBox4.TabIndex = 9;
            // 
            // Btn_calcular
            // 
            this.Btn_calcular.Font = new System.Drawing.Font("Rockwell", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_calcular.Location = new System.Drawing.Point(708, 239);
            this.Btn_calcular.Name = "Btn_calcular";
            this.Btn_calcular.Size = new System.Drawing.Size(340, 82);
            this.Btn_calcular.TabIndex = 4;
            this.Btn_calcular.Text = "Calcular";
            this.Btn_calcular.UseVisualStyleBackColor = true;
            // 
            // Btn_limpiar
            // 
            this.Btn_limpiar.Font = new System.Drawing.Font("Rockwell", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_limpiar.Location = new System.Drawing.Point(823, 389);
            this.Btn_limpiar.Name = "Btn_limpiar";
            this.Btn_limpiar.Size = new System.Drawing.Size(225, 44);
            this.Btn_limpiar.TabIndex = 5;
            this.Btn_limpiar.Text = "Limpiar campos";
            this.Btn_limpiar.UseVisualStyleBackColor = true;
            // 
            // Frm_depreciacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 674);
            this.Controls.Add(this.Btn_limpiar);
            this.Controls.Add(this.Btn_calcular);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Gpb_caracteristicas_activo_fijo);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Frm_depreciacion";
            this.Text = "Frm_depreciacion";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.Gpb_caracteristicas_activo_fijo.ResumeLayout(false);
            this.Gpb_caracteristicas_activo_fijo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox Gpb_caracteristicas_activo_fijo;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label Lbl_deprecioacion_anual;
        private System.Windows.Forms.Label Lbl_vida_activo_fijo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label Lbl_costo_activo_fijo;
        private System.Windows.Forms.Button Btn_buscar_activo_fijo;
        private System.Windows.Forms.ComboBox Cbo_buscar_activo_fijo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Btn_calcular;
        private System.Windows.Forms.Button Btn_limpiar;
    }
}