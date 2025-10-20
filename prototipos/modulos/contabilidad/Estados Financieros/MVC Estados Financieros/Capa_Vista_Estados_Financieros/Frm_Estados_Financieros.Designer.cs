
namespace Capa_Vista_Estados_Financieros
{
    partial class Frm_Estados_Financieros
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.estadosFinancierosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.estadoDeResultadosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.estadosDeCambiosEnElPatrimonioNetoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.balanceDeSituaciónGeneralToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.estadoDeFlujoDeEjectivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.estadosFinancierosToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1330, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // estadosFinancierosToolStripMenuItem
            // 
            this.estadosFinancierosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.estadoDeResultadosToolStripMenuItem,
            this.estadosDeCambiosEnElPatrimonioNetoToolStripMenuItem,
            this.balanceDeSituaciónGeneralToolStripMenuItem,
            this.estadoDeFlujoDeEjectivoToolStripMenuItem});
            this.estadosFinancierosToolStripMenuItem.Name = "estadosFinancierosToolStripMenuItem";
            this.estadosFinancierosToolStripMenuItem.Size = new System.Drawing.Size(152, 24);
            this.estadosFinancierosToolStripMenuItem.Text = "Estados Financieros";
            // 
            // estadoDeResultadosToolStripMenuItem
            // 
            this.estadoDeResultadosToolStripMenuItem.Name = "estadoDeResultadosToolStripMenuItem";
            this.estadoDeResultadosToolStripMenuItem.Size = new System.Drawing.Size(375, 26);
            this.estadoDeResultadosToolStripMenuItem.Text = "Estado de Resultados";
            this.estadoDeResultadosToolStripMenuItem.Click += new System.EventHandler(this.estadoDeResultadosToolStripMenuItem_Click);
            // 
            // estadosDeCambiosEnElPatrimonioNetoToolStripMenuItem
            // 
            this.estadosDeCambiosEnElPatrimonioNetoToolStripMenuItem.Name = "estadosDeCambiosEnElPatrimonioNetoToolStripMenuItem";
            this.estadosDeCambiosEnElPatrimonioNetoToolStripMenuItem.Size = new System.Drawing.Size(375, 26);
            this.estadosDeCambiosEnElPatrimonioNetoToolStripMenuItem.Text = "Estados de Cambios en el Patrimonio Neto";
            this.estadosDeCambiosEnElPatrimonioNetoToolStripMenuItem.Click += new System.EventHandler(this.estadosDeCambiosEnElPatrimonioNetoToolStripMenuItem_Click);
            // 
            // balanceDeSituaciónGeneralToolStripMenuItem
            // 
            this.balanceDeSituaciónGeneralToolStripMenuItem.Name = "balanceDeSituaciónGeneralToolStripMenuItem";
            this.balanceDeSituaciónGeneralToolStripMenuItem.Size = new System.Drawing.Size(375, 26);
            this.balanceDeSituaciónGeneralToolStripMenuItem.Text = "Balance de Situación General";
            this.balanceDeSituaciónGeneralToolStripMenuItem.Click += new System.EventHandler(this.balanceDeSituaciónGeneralToolStripMenuItem_Click);
            // 
            // estadoDeFlujoDeEjectivoToolStripMenuItem
            // 
            this.estadoDeFlujoDeEjectivoToolStripMenuItem.Name = "estadoDeFlujoDeEjectivoToolStripMenuItem";
            this.estadoDeFlujoDeEjectivoToolStripMenuItem.Size = new System.Drawing.Size(375, 26);
            this.estadoDeFlujoDeEjectivoToolStripMenuItem.Text = "Estado de Flujo de Ejectivo";
            this.estadoDeFlujoDeEjectivoToolStripMenuItem.Click += new System.EventHandler(this.estadoDeFlujoDeEjectivoToolStripMenuItem_Click);
            // 
            // Frm_Estados_Financieros
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1330, 465);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Frm_Estados_Financieros";
            this.Text = "Frm_Estados_Financieros";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem estadosFinancierosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem estadoDeResultadosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem estadosDeCambiosEnElPatrimonioNetoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem balanceDeSituaciónGeneralToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem estadoDeFlujoDeEjectivoToolStripMenuItem;
    }
}