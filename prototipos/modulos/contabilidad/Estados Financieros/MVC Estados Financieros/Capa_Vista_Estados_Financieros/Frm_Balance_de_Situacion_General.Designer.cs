
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
            this.Ggv_Estado = new System.Windows.Forms.DataGridView();
            this.Btn_Salir = new System.Windows.Forms.Button();
            this.Btm_Limpiar = new System.Windows.Forms.Button();
            this.Btn_Generar = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Ggv_Estado)).BeginInit();
            this.SuspendLayout();
            // 
            // Ggv_Estado
            // 
            this.Ggv_Estado.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Ggv_Estado.Location = new System.Drawing.Point(255, 150);
            this.Ggv_Estado.Name = "Ggv_Estado";
            this.Ggv_Estado.RowHeadersWidth = 51;
            this.Ggv_Estado.RowTemplate.Height = 24;
            this.Ggv_Estado.Size = new System.Drawing.Size(795, 349);
            this.Ggv_Estado.TabIndex = 23;
            // 
            // Btn_Salir
            // 
            this.Btn_Salir.Location = new System.Drawing.Point(1164, 424);
            this.Btn_Salir.Name = "Btn_Salir";
            this.Btn_Salir.Size = new System.Drawing.Size(162, 65);
            this.Btn_Salir.TabIndex = 22;
            this.Btn_Salir.Text = "Salir";
            this.Btn_Salir.UseVisualStyleBackColor = true;
            // 
            // Btm_Limpiar
            // 
            this.Btm_Limpiar.Location = new System.Drawing.Point(47, 226);
            this.Btm_Limpiar.Name = "Btm_Limpiar";
            this.Btm_Limpiar.Size = new System.Drawing.Size(121, 59);
            this.Btm_Limpiar.TabIndex = 21;
            this.Btm_Limpiar.Text = "Limpiar";
            this.Btm_Limpiar.UseVisualStyleBackColor = true;
            // 
            // Btn_Generar
            // 
            this.Btn_Generar.Location = new System.Drawing.Point(47, 137);
            this.Btn_Generar.Name = "Btn_Generar";
            this.Btn_Generar.Size = new System.Drawing.Size(128, 59);
            this.Btn_Generar.TabIndex = 20;
            this.Btn_Generar.Text = "Generar";
            this.Btn_Generar.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(547, 113);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(206, 17);
            this.label4.TabIndex = 19;
            this.label4.Text = "Cifras expresadas en quetzales";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(556, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(180, 17);
            this.label3.TabIndex = 18;
            this.label3.Text = "al 31 de diciembre de 2025";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(556, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(189, 17);
            this.label2.TabIndex = 17;
            this.label2.Text = "Estado de Situación General";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(599, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 17);
            this.label1.TabIndex = 16;
            this.label1.Text = "Hotel San Carlos";
            // 
            // Frm_Balance_de_Situacion_General
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1353, 529);
            this.Controls.Add(this.Ggv_Estado);
            this.Controls.Add(this.Btn_Salir);
            this.Controls.Add(this.Btm_Limpiar);
            this.Controls.Add(this.Btn_Generar);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Frm_Balance_de_Situacion_General";
            this.Text = "Frm_Balance_de_Situacion_General";
            ((System.ComponentModel.ISupportInitialize)(this.Ggv_Estado)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView Ggv_Estado;
        private System.Windows.Forms.Button Btn_Salir;
        private System.Windows.Forms.Button Btm_Limpiar;
        private System.Windows.Forms.Button Btn_Generar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}