
namespace Capa_Vista_Estados_Financieros
{
    partial class Frm_EstadoBalanceGeneral
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
            this.Dgv_EstadoBalanceGeneral = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Btn_Generar_PDF = new System.Windows.Forms.Button();
            this.Btn_Limpiar = new System.Windows.Forms.Button();
            this.Btn_Generar = new System.Windows.Forms.Button();
            this.Btn_Salir = new System.Windows.Forms.Button();
            this.Lbl_EcuacionContable = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_EstadoBalanceGeneral)).BeginInit();
            this.SuspendLayout();
            // 
            // Dgv_EstadoBalanceGeneral
            // 
            this.Dgv_EstadoBalanceGeneral.AllowUserToResizeRows = false;
            this.Dgv_EstadoBalanceGeneral.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.Dgv_EstadoBalanceGeneral.BackgroundColor = System.Drawing.Color.White;
            this.Dgv_EstadoBalanceGeneral.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.Dgv_EstadoBalanceGeneral.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dgv_EstadoBalanceGeneral.Location = new System.Drawing.Point(232, 164);
            this.Dgv_EstadoBalanceGeneral.Margin = new System.Windows.Forms.Padding(4);
            this.Dgv_EstadoBalanceGeneral.Name = "Dgv_EstadoBalanceGeneral";
            this.Dgv_EstadoBalanceGeneral.RowHeadersVisible = false;
            this.Dgv_EstadoBalanceGeneral.RowHeadersWidth = 51;
            this.Dgv_EstadoBalanceGeneral.RowTemplate.Height = 24;
            this.Dgv_EstadoBalanceGeneral.Size = new System.Drawing.Size(1239, 493);
            this.Dgv_EstadoBalanceGeneral.TabIndex = 26;
            this.Dgv_EstadoBalanceGeneral.Click += new System.EventHandler(this.Btn_Generar_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Rockwell", 12F);
            this.label4.Location = new System.Drawing.Point(799, 97);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(295, 22);
            this.label4.TabIndex = 25;
            this.label4.Text = "Cifras expresadas en quetzales";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Rockwell", 12F);
            this.label3.Location = new System.Drawing.Point(833, 75);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(261, 22);
            this.label3.TabIndex = 24;
            this.label3.Text = "al 31 de diciembre de 2025";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Rockwell", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(831, 15);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(263, 38);
            this.label2.TabIndex = 23;
            this.label2.Text = "Balance General";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(879, 53);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(161, 22);
            this.label1.TabIndex = 22;
            this.label1.Text = "Hotel San Carlos";
            // 
            // Btn_Generar_PDF
            // 
            this.Btn_Generar_PDF.BackColor = System.Drawing.Color.White;
            this.Btn_Generar_PDF.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_Generar_PDF.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Generar_PDF.Location = new System.Drawing.Point(20, 346);
            this.Btn_Generar_PDF.Margin = new System.Windows.Forms.Padding(4);
            this.Btn_Generar_PDF.Name = "Btn_Generar_PDF";
            this.Btn_Generar_PDF.Size = new System.Drawing.Size(160, 74);
            this.Btn_Generar_PDF.TabIndex = 21;
            this.Btn_Generar_PDF.Text = "Generar PDF";
            this.Btn_Generar_PDF.UseVisualStyleBackColor = false;
            this.Btn_Generar_PDF.Click += new System.EventHandler(this.Btn_Generar_PDF_Click);
            // 
            // Btn_Limpiar
            // 
            this.Btn_Limpiar.BackColor = System.Drawing.Color.White;
            this.Btn_Limpiar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_Limpiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Limpiar.Location = new System.Drawing.Point(20, 243);
            this.Btn_Limpiar.Margin = new System.Windows.Forms.Padding(4);
            this.Btn_Limpiar.Name = "Btn_Limpiar";
            this.Btn_Limpiar.Size = new System.Drawing.Size(160, 76);
            this.Btn_Limpiar.TabIndex = 20;
            this.Btn_Limpiar.Text = "Limpiar";
            this.Btn_Limpiar.UseVisualStyleBackColor = false;
            this.Btn_Limpiar.Click += new System.EventHandler(this.Btn_Limpiar_Click);
            // 
            // Btn_Generar
            // 
            this.Btn_Generar.BackColor = System.Drawing.Color.White;
            this.Btn_Generar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_Generar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Generar.ForeColor = System.Drawing.Color.Black;
            this.Btn_Generar.Location = new System.Drawing.Point(20, 132);
            this.Btn_Generar.Margin = new System.Windows.Forms.Padding(4);
            this.Btn_Generar.Name = "Btn_Generar";
            this.Btn_Generar.Size = new System.Drawing.Size(160, 74);
            this.Btn_Generar.TabIndex = 19;
            this.Btn_Generar.Text = "Generar";
            this.Btn_Generar.UseVisualStyleBackColor = false;
            this.Btn_Generar.Click += new System.EventHandler(this.Btn_Generar_Click);
            // 
            // Btn_Salir
            // 
            this.Btn_Salir.BackColor = System.Drawing.Color.White;
            this.Btn_Salir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_Salir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Salir.ForeColor = System.Drawing.Color.Black;
            this.Btn_Salir.Location = new System.Drawing.Point(1495, 616);
            this.Btn_Salir.Margin = new System.Windows.Forms.Padding(4);
            this.Btn_Salir.Name = "Btn_Salir";
            this.Btn_Salir.Size = new System.Drawing.Size(148, 85);
            this.Btn_Salir.TabIndex = 27;
            this.Btn_Salir.Text = "Salir";
            this.Btn_Salir.UseVisualStyleBackColor = false;
            this.Btn_Salir.Click += new System.EventHandler(this.Btn_Salir_Click);
            // 
            // Lbl_EcuacionContable
            // 
            this.Lbl_EcuacionContable.AutoSize = true;
            this.Lbl_EcuacionContable.Font = new System.Drawing.Font("Rockwell", 12F);
            this.Lbl_EcuacionContable.Location = new System.Drawing.Point(722, 679);
            this.Lbl_EcuacionContable.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Lbl_EcuacionContable.Name = "Lbl_EcuacionContable";
            this.Lbl_EcuacionContable.Size = new System.Drawing.Size(0, 22);
            this.Lbl_EcuacionContable.TabIndex = 28;
            // 
            // Frm_EstadoBalanceGeneral
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1656, 752);
            this.Controls.Add(this.Lbl_EcuacionContable);
            this.Controls.Add(this.Btn_Salir);
            this.Controls.Add(this.Dgv_EstadoBalanceGeneral);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Btn_Generar_PDF);
            this.Controls.Add(this.Btn_Limpiar);
            this.Controls.Add(this.Btn_Generar);
            this.Name = "Frm_EstadoBalanceGeneral";
            this.Text = "Frm_EstadoDeBalanceGeneral";
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_EstadoBalanceGeneral)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView Dgv_EstadoBalanceGeneral;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Btn_Generar_PDF;
        private System.Windows.Forms.Button Btn_Limpiar;
        private System.Windows.Forms.Button Btn_Generar;
        private System.Windows.Forms.Button Btn_Salir;
        private System.Windows.Forms.Label Lbl_EcuacionContable;
    }
}