namespace Capa_Vista_Presupuesto
{
    partial class Frm_Partidas
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
            this.Lbl_Cantidad = new System.Windows.Forms.Label();
            this.Lbl_Descripcion = new System.Windows.Forms.Label();
            this.Txt_1 = new System.Windows.Forms.TextBox();
            this.Txt_2 = new System.Windows.Forms.TextBox();
            this.Btn_1 = new System.Windows.Forms.Button();
            this.Btn_2 = new System.Windows.Forms.Button();
            this.Llb_Area = new System.Windows.Forms.Label();
            this.Cmb_1 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // Lbl_Cantidad
            // 
            this.Lbl_Cantidad.AutoSize = true;
            this.Lbl_Cantidad.Location = new System.Drawing.Point(109, 122);
            this.Lbl_Cantidad.Name = "Lbl_Cantidad";
            this.Lbl_Cantidad.Size = new System.Drawing.Size(49, 13);
            this.Lbl_Cantidad.TabIndex = 0;
            this.Lbl_Cantidad.Text = "Cantidad";
            // 
            // Lbl_Descripcion
            // 
            this.Lbl_Descripcion.AutoSize = true;
            this.Lbl_Descripcion.Location = new System.Drawing.Point(95, 153);
            this.Lbl_Descripcion.Name = "Lbl_Descripcion";
            this.Lbl_Descripcion.Size = new System.Drawing.Size(63, 13);
            this.Lbl_Descripcion.TabIndex = 1;
            this.Lbl_Descripcion.Text = "Descripción";
            // 
            // Txt_1
            // 
            this.Txt_1.Location = new System.Drawing.Point(164, 119);
            this.Txt_1.Name = "Txt_1";
            this.Txt_1.Size = new System.Drawing.Size(100, 20);
            this.Txt_1.TabIndex = 2;
            // 
            // Txt_2
            // 
            this.Txt_2.Location = new System.Drawing.Point(164, 150);
            this.Txt_2.Name = "Txt_2";
            this.Txt_2.Size = new System.Drawing.Size(100, 20);
            this.Txt_2.TabIndex = 3;
            // 
            // Btn_1
            // 
            this.Btn_1.Location = new System.Drawing.Point(98, 223);
            this.Btn_1.Name = "Btn_1";
            this.Btn_1.Size = new System.Drawing.Size(75, 23);
            this.Btn_1.TabIndex = 4;
            this.Btn_1.Text = "Confirmar";
            this.Btn_1.UseVisualStyleBackColor = true;
            this.Btn_1.Click += new System.EventHandler(this.Btn_1_Click);
            // 
            // Btn_2
            // 
            this.Btn_2.Location = new System.Drawing.Point(208, 223);
            this.Btn_2.Name = "Btn_2";
            this.Btn_2.Size = new System.Drawing.Size(75, 23);
            this.Btn_2.TabIndex = 5;
            this.Btn_2.Text = "Cancelar";
            this.Btn_2.UseVisualStyleBackColor = true;
            this.Btn_2.Click += new System.EventHandler(this.Btn_2_Click);
            // 
            // Llb_Area
            // 
            this.Llb_Area.AutoSize = true;
            this.Llb_Area.Location = new System.Drawing.Point(75, 88);
            this.Llb_Area.Name = "Llb_Area";
            this.Llb_Area.Size = new System.Drawing.Size(83, 13);
            this.Llb_Area.TabIndex = 6;
            this.Llb_Area.Text = "Area a Designar";
            // 
            // Cmb_1
            // 
            this.Cmb_1.FormattingEnabled = true;
            this.Cmb_1.Location = new System.Drawing.Point(164, 85);
            this.Cmb_1.Name = "Cmb_1";
            this.Cmb_1.Size = new System.Drawing.Size(121, 21);
            this.Cmb_1.TabIndex = 7;
            // 
            // Frm_Partidas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(367, 340);
            this.Controls.Add(this.Cmb_1);
            this.Controls.Add(this.Llb_Area);
            this.Controls.Add(this.Btn_2);
            this.Controls.Add(this.Btn_1);
            this.Controls.Add(this.Txt_2);
            this.Controls.Add(this.Txt_1);
            this.Controls.Add(this.Lbl_Descripcion);
            this.Controls.Add(this.Lbl_Cantidad);
            this.Name = "Frm_Partidas";
            this.Text = "Frm_Partidas";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Lbl_Cantidad;
        private System.Windows.Forms.Label Lbl_Descripcion;
        private System.Windows.Forms.TextBox Txt_1;
        private System.Windows.Forms.TextBox Txt_2;
        private System.Windows.Forms.Button Btn_1;
        private System.Windows.Forms.Button Btn_2;
        private System.Windows.Forms.Label Llb_Area;
        private System.Windows.Forms.ComboBox Cmb_1;
    }
}