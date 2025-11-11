namespace Capa_Vista_Presupuesto
{
    partial class Frm_Traslado
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
            this.Cmb_1 = new System.Windows.Forms.ComboBox();
            this.Cmb_2 = new System.Windows.Forms.ComboBox();
            this.Lbl_1 = new System.Windows.Forms.Label();
            this.Txt_1 = new System.Windows.Forms.TextBox();
            this.Btn_1 = new System.Windows.Forms.Button();
            this.Btn_2 = new System.Windows.Forms.Button();
            this.Lbl_2 = new System.Windows.Forms.Label();
            this.Txt_2 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Cmb_1
            // 
            this.Cmb_1.FormattingEnabled = true;
            this.Cmb_1.Location = new System.Drawing.Point(67, 116);
            this.Cmb_1.Name = "Cmb_1";
            this.Cmb_1.Size = new System.Drawing.Size(121, 21);
            this.Cmb_1.TabIndex = 0;
            // 
            // Cmb_2
            // 
            this.Cmb_2.FormattingEnabled = true;
            this.Cmb_2.Location = new System.Drawing.Point(241, 115);
            this.Cmb_2.Name = "Cmb_2";
            this.Cmb_2.Size = new System.Drawing.Size(121, 21);
            this.Cmb_2.TabIndex = 1;
            // 
            // Lbl_1
            // 
            this.Lbl_1.AutoSize = true;
            this.Lbl_1.Location = new System.Drawing.Point(64, 184);
            this.Lbl_1.Name = "Lbl_1";
            this.Lbl_1.Size = new System.Drawing.Size(178, 13);
            this.Lbl_1.TabIndex = 2;
            this.Lbl_1.Text = "Establecer el presupesuto a cambiar";
            // 
            // Txt_1
            // 
            this.Txt_1.Location = new System.Drawing.Point(248, 181);
            this.Txt_1.Name = "Txt_1";
            this.Txt_1.Size = new System.Drawing.Size(100, 20);
            this.Txt_1.TabIndex = 3;
            // 
            // Btn_1
            // 
            this.Btn_1.Location = new System.Drawing.Point(113, 270);
            this.Btn_1.Name = "Btn_1";
            this.Btn_1.Size = new System.Drawing.Size(75, 23);
            this.Btn_1.TabIndex = 5;
            this.Btn_1.Text = "Confirmar";
            this.Btn_1.UseVisualStyleBackColor = true;
            this.Btn_1.Click += new System.EventHandler(this.Btn_2_Click);
            // 
            // Btn_2
            // 
            this.Btn_2.Location = new System.Drawing.Point(248, 270);
            this.Btn_2.Name = "Btn_2";
            this.Btn_2.Size = new System.Drawing.Size(75, 23);
            this.Btn_2.TabIndex = 6;
            this.Btn_2.Text = "Cancelar";
            this.Btn_2.UseVisualStyleBackColor = true;
            this.Btn_2.Click += new System.EventHandler(this.Btn_1_Click);
            // 
            // Lbl_2
            // 
            this.Lbl_2.AutoSize = true;
            this.Lbl_2.Location = new System.Drawing.Point(179, 229);
            this.Lbl_2.Name = "Lbl_2";
            this.Lbl_2.Size = new System.Drawing.Size(63, 13);
            this.Lbl_2.TabIndex = 7;
            this.Lbl_2.Text = "Descripcion";
            // 
            // Txt_2
            // 
            this.Txt_2.Location = new System.Drawing.Point(248, 222);
            this.Txt_2.Name = "Txt_2";
            this.Txt_2.Size = new System.Drawing.Size(100, 20);
            this.Txt_2.TabIndex = 8;
            // 
            // Frm_Traslado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 435);
            this.Controls.Add(this.Txt_2);
            this.Controls.Add(this.Lbl_2);
            this.Controls.Add(this.Btn_2);
            this.Controls.Add(this.Btn_1);
            this.Controls.Add(this.Txt_1);
            this.Controls.Add(this.Lbl_1);
            this.Controls.Add(this.Cmb_2);
            this.Controls.Add(this.Cmb_1);
            this.Name = "Frm_Traslado";
            this.Text = "Frm_Traslado";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox Cmb_1;
        private System.Windows.Forms.ComboBox Cmb_2;
        private System.Windows.Forms.Label Lbl_1;
        private System.Windows.Forms.TextBox Txt_1;
        private System.Windows.Forms.Button Btn_1;
        private System.Windows.Forms.Button Btn_2;
        private System.Windows.Forms.Label Lbl_2;
        private System.Windows.Forms.TextBox Txt_2;
    }
}