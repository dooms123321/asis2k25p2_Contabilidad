
namespace Capa_Vista_Estados_Financieros
{
    partial class Frm_EstadosFinancieros
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
            this.Msp_EstadosFinancieros = new System.Windows.Forms.MenuStrip();
            this.Tsm_EstadosFianacieros = new System.Windows.Forms.ToolStripMenuItem();
            this.Tsm_EstadoBalanceDeSaldos = new System.Windows.Forms.ToolStripMenuItem();
            this.Tsm_EstadoResultados = new System.Windows.Forms.ToolStripMenuItem();
            this.Tsm_BalanceGeneral = new System.Windows.Forms.ToolStripMenuItem();
            this.Tsm_FlujoEfectivo = new System.Windows.Forms.ToolStripMenuItem();
            this.Msp_EstadosFinancieros.SuspendLayout();
            this.SuspendLayout();
            // 
            // Msp_EstadosFinancieros
            // 
            this.Msp_EstadosFinancieros.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.Msp_EstadosFinancieros.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Tsm_EstadosFianacieros});
            this.Msp_EstadosFinancieros.Location = new System.Drawing.Point(0, 0);
            this.Msp_EstadosFinancieros.Name = "Msp_EstadosFinancieros";
            this.Msp_EstadosFinancieros.Size = new System.Drawing.Size(1233, 28);
            this.Msp_EstadosFinancieros.TabIndex = 0;
            this.Msp_EstadosFinancieros.Text = "Estados Financieros";
            // 
            // Tsm_EstadosFianacieros
            // 
            this.Tsm_EstadosFianacieros.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Tsm_EstadoBalanceDeSaldos,
            this.Tsm_EstadoResultados,
            this.Tsm_BalanceGeneral,
            this.Tsm_FlujoEfectivo});
            this.Tsm_EstadosFianacieros.Name = "Tsm_EstadosFianacieros";
            this.Tsm_EstadosFianacieros.Size = new System.Drawing.Size(152, 24);
            this.Tsm_EstadosFianacieros.Text = "Estados Financieros";
            // 
            // Tsm_EstadoBalanceDeSaldos
            // 
            this.Tsm_EstadoBalanceDeSaldos.Name = "Tsm_EstadoBalanceDeSaldos";
            this.Tsm_EstadoBalanceDeSaldos.Size = new System.Drawing.Size(283, 26);
            this.Tsm_EstadoBalanceDeSaldos.Text = "Estado de Balance de Saldos";
            this.Tsm_EstadoBalanceDeSaldos.Click += new System.EventHandler(this.Tsm_EstadoBalanceDeSaldos_Click);
            // 
            // Tsm_EstadoResultados
            // 
            this.Tsm_EstadoResultados.Name = "Tsm_EstadoResultados";
            this.Tsm_EstadoResultados.Size = new System.Drawing.Size(283, 26);
            this.Tsm_EstadoResultados.Text = "Estado de Resultados";
            this.Tsm_EstadoResultados.Click += new System.EventHandler(this.Tsm_EstadoResultados_Click);
            // 
            // Tsm_BalanceGeneral
            // 
            this.Tsm_BalanceGeneral.Name = "Tsm_BalanceGeneral";
            this.Tsm_BalanceGeneral.Size = new System.Drawing.Size(283, 26);
            this.Tsm_BalanceGeneral.Text = "Estado de Balanace General";
            this.Tsm_BalanceGeneral.Click += new System.EventHandler(this.Tsm_BalanceGeneral_Click);
            // 
            // Tsm_FlujoEfectivo
            // 
            this.Tsm_FlujoEfectivo.Name = "Tsm_FlujoEfectivo";
            this.Tsm_FlujoEfectivo.Size = new System.Drawing.Size(283, 26);
            this.Tsm_FlujoEfectivo.Text = "Estado de Flujo de Efectivo";
            this.Tsm_FlujoEfectivo.Click += new System.EventHandler(this.Tsm_FlujoEfectivo_Click);
            // 
            // Frm_EstadosFinancieros
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1233, 544);
            this.Controls.Add(this.Msp_EstadosFinancieros);
            this.MainMenuStrip = this.Msp_EstadosFinancieros;
            this.Name = "Frm_EstadosFinancieros";
            this.Text = "Frm_EstadosFinancieros";
            this.Msp_EstadosFinancieros.ResumeLayout(false);
            this.Msp_EstadosFinancieros.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip Msp_EstadosFinancieros;
        private System.Windows.Forms.ToolStripMenuItem Tsm_EstadosFianacieros;
        private System.Windows.Forms.ToolStripMenuItem Tsm_EstadoBalanceDeSaldos;
        private System.Windows.Forms.ToolStripMenuItem Tsm_EstadoResultados;
        private System.Windows.Forms.ToolStripMenuItem Tsm_BalanceGeneral;
        private System.Windows.Forms.ToolStripMenuItem Tsm_FlujoEfectivo;
    }
}