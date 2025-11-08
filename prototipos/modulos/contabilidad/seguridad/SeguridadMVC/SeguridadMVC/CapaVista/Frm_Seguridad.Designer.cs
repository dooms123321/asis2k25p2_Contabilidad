namespace Capa_Vista_Seguridad
{
    partial class Frm_Seguridad
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.archivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cerrarSesiónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.catálogosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cuentasToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.usuariosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.perfilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modulosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Btn_Aplicacion = new System.Windows.Forms.ToolStripMenuItem();
            this.procesosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Btn_Bitacora = new System.Windows.Forms.ToolStripMenuItem();
            this.herramientasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cambiarContraseñaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.asignacionesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.asignacionDeAplicacionAUsuarioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.asignacionPerfilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.asignacionDeAplicacionAPerfilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Pnl_Superior = new System.Windows.Forms.Panel();
            this.Pic_Cerrar = new System.Windows.Forms.PictureBox();
            this.gestiónFinancieraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cierreContableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.presupuestosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.activosFijosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.estadosFinancierosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.polizasLocalesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.Pnl_Superior.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Pic_Cerrar)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 477);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(3, 0, 19, 0);
            this.statusStrip.Size = new System.Drawing.Size(1069, 26);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "StatusStrip";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(54, 20);
            this.toolStripStatusLabel.Text = "Estado";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivoToolStripMenuItem,
            this.catálogosToolStripMenuItem,
            this.procesosToolStripMenuItem,
            this.herramientasToolStripMenuItem,
            this.asignacionesToolStripMenuItem,
            this.gestiónFinancieraToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 47);
            this.menuStrip1.MaximumSize = new System.Drawing.Size(0, 503);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 1069, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1792, 28);
            this.menuStrip1.TabIndex = 24;
            this.menuStrip1.Text = "MenuStrip";
            // 
            // archivoToolStripMenuItem
            // 
            this.archivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cerrarSesiónToolStripMenuItem});
            this.archivoToolStripMenuItem.Font = new System.Drawing.Font("Rockwell", 10F);
            this.archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            this.archivoToolStripMenuItem.Size = new System.Drawing.Size(85, 24);
            this.archivoToolStripMenuItem.Text = "Archivo";
            // 
            // cerrarSesiónToolStripMenuItem
            // 
            this.cerrarSesiónToolStripMenuItem.Name = "cerrarSesiónToolStripMenuItem";
            this.cerrarSesiónToolStripMenuItem.Size = new System.Drawing.Size(127, 26);
            this.cerrarSesiónToolStripMenuItem.Text = "Salir";
            this.cerrarSesiónToolStripMenuItem.Click += new System.EventHandler(this.cerrarSesiónToolStripMenuItem_Click);
            // 
            // catálogosToolStripMenuItem
            // 
            this.catálogosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cuentasToolStripMenuItem1,
            this.usuariosToolStripMenuItem,
            this.perfilesToolStripMenuItem,
            this.modulosToolStripMenuItem,
            this.Btn_Aplicacion});
            this.catálogosToolStripMenuItem.Font = new System.Drawing.Font("Rockwell", 10F);
            this.catálogosToolStripMenuItem.Name = "catálogosToolStripMenuItem";
            this.catálogosToolStripMenuItem.Size = new System.Drawing.Size(102, 24);
            this.catálogosToolStripMenuItem.Text = "Catálogos";
            this.catálogosToolStripMenuItem.Click += new System.EventHandler(this.catálogosToolStripMenuItem_Click);
            // 
            // cuentasToolStripMenuItem1
            // 
            this.cuentasToolStripMenuItem1.Name = "cuentasToolStripMenuItem1";
            this.cuentasToolStripMenuItem1.Size = new System.Drawing.Size(176, 26);
            this.cuentasToolStripMenuItem1.Text = "Cuentas";
            this.cuentasToolStripMenuItem1.Click += new System.EventHandler(this.cuentasToolStripMenuItem1_Click);
            // 
            // usuariosToolStripMenuItem
            // 
            this.usuariosToolStripMenuItem.Name = "usuariosToolStripMenuItem";
            this.usuariosToolStripMenuItem.Size = new System.Drawing.Size(176, 26);
            this.usuariosToolStripMenuItem.Text = "Usuarios";
            this.usuariosToolStripMenuItem.Click += new System.EventHandler(this.usuariosToolStripMenuItem_Click);
            // 
            // perfilesToolStripMenuItem
            // 
            this.perfilesToolStripMenuItem.Name = "perfilesToolStripMenuItem";
            this.perfilesToolStripMenuItem.Size = new System.Drawing.Size(176, 26);
            this.perfilesToolStripMenuItem.Text = "Perfiles";
            this.perfilesToolStripMenuItem.Click += new System.EventHandler(this.perfilesToolStripMenuItem_Click_1);
            // 
            // modulosToolStripMenuItem
            // 
            this.modulosToolStripMenuItem.Name = "modulosToolStripMenuItem";
            this.modulosToolStripMenuItem.Size = new System.Drawing.Size(176, 26);
            this.modulosToolStripMenuItem.Text = "Modulos";
            this.modulosToolStripMenuItem.Click += new System.EventHandler(this.modulosToolStripMenuItem_Click);
            // 
            // Btn_Aplicacion
            // 
            this.Btn_Aplicacion.Name = "Btn_Aplicacion";
            this.Btn_Aplicacion.Size = new System.Drawing.Size(176, 26);
            this.Btn_Aplicacion.Text = "Aplicacion";
            this.Btn_Aplicacion.Click += new System.EventHandler(this.Btn_Aplicacion_Click_1);
            // 
            // procesosToolStripMenuItem
            // 
            this.procesosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Btn_Bitacora});
            this.procesosToolStripMenuItem.Font = new System.Drawing.Font("Rockwell", 10F);
            this.procesosToolStripMenuItem.Name = "procesosToolStripMenuItem";
            this.procesosToolStripMenuItem.Size = new System.Drawing.Size(95, 24);
            this.procesosToolStripMenuItem.Text = "Procesos";
            // 
            // Btn_Bitacora
            // 
            this.Btn_Bitacora.Name = "Btn_Bitacora";
            this.Btn_Bitacora.Size = new System.Drawing.Size(156, 26);
            this.Btn_Bitacora.Text = "Bitacora";
            this.Btn_Bitacora.Click += new System.EventHandler(this.Btn_Bitacora_Click);
            // 
            // herramientasToolStripMenuItem
            // 
            this.herramientasToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem,
            this.cambiarContraseñaToolStripMenuItem});
            this.herramientasToolStripMenuItem.Font = new System.Drawing.Font("Rockwell", 10F);
            this.herramientasToolStripMenuItem.Name = "herramientasToolStripMenuItem";
            this.herramientasToolStripMenuItem.Size = new System.Drawing.Size(130, 24);
            this.herramientasToolStripMenuItem.Text = "&Herramientas";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(251, 26);
            this.optionsToolStripMenuItem.Text = "&Opciones";
            // 
            // cambiarContraseñaToolStripMenuItem
            // 
            this.cambiarContraseñaToolStripMenuItem.Name = "cambiarContraseñaToolStripMenuItem";
            this.cambiarContraseñaToolStripMenuItem.Size = new System.Drawing.Size(251, 26);
            this.cambiarContraseñaToolStripMenuItem.Text = "Cambiar contraseña";
            this.cambiarContraseñaToolStripMenuItem.Click += new System.EventHandler(this.cambiarContraseñaToolStripMenuItem_Click);
            // 
            // asignacionesToolStripMenuItem
            // 
            this.asignacionesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.asignacionDeAplicacionAUsuarioToolStripMenuItem,
            this.asignacionPerfilesToolStripMenuItem,
            this.asignacionDeAplicacionAPerfilesToolStripMenuItem});
            this.asignacionesToolStripMenuItem.Font = new System.Drawing.Font("Rockwell", 10F);
            this.asignacionesToolStripMenuItem.Name = "asignacionesToolStripMenuItem";
            this.asignacionesToolStripMenuItem.Size = new System.Drawing.Size(129, 24);
            this.asignacionesToolStripMenuItem.Text = "Asignaciones";
            // 
            // asignacionDeAplicacionAUsuarioToolStripMenuItem
            // 
            this.asignacionDeAplicacionAUsuarioToolStripMenuItem.Name = "asignacionDeAplicacionAUsuarioToolStripMenuItem";
            this.asignacionDeAplicacionAUsuarioToolStripMenuItem.Size = new System.Drawing.Size(372, 26);
            this.asignacionDeAplicacionAUsuarioToolStripMenuItem.Text = "Asignacion De Aplicacion a Usuario";
            this.asignacionDeAplicacionAUsuarioToolStripMenuItem.Click += new System.EventHandler(this.asignacionDeAplicacionAUsuarioToolStripMenuItem_Click);
            // 
            // asignacionPerfilesToolStripMenuItem
            // 
            this.asignacionPerfilesToolStripMenuItem.Name = "asignacionPerfilesToolStripMenuItem";
            this.asignacionPerfilesToolStripMenuItem.Size = new System.Drawing.Size(372, 26);
            this.asignacionPerfilesToolStripMenuItem.Text = "Asignacion de Perfil a Usuario";
            this.asignacionPerfilesToolStripMenuItem.Click += new System.EventHandler(this.asignacionPerfilesToolStripMenuItem_Click);
            // 
            // asignacionDeAplicacionAPerfilesToolStripMenuItem
            // 
            this.asignacionDeAplicacionAPerfilesToolStripMenuItem.Name = "asignacionDeAplicacionAPerfilesToolStripMenuItem";
            this.asignacionDeAplicacionAPerfilesToolStripMenuItem.Size = new System.Drawing.Size(372, 26);
            this.asignacionDeAplicacionAPerfilesToolStripMenuItem.Text = "Asignacion De Aplicacion a Perfiles";
            this.asignacionDeAplicacionAPerfilesToolStripMenuItem.Click += new System.EventHandler(this.asignacionDeAplicacionAPerfilesToolStripMenuItem_Click);
            // 
            // Pnl_Superior
            // 
            this.Pnl_Superior.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(142)))), ((int)(((byte)(24)))));
            this.Pnl_Superior.Controls.Add(this.Pic_Cerrar);
            this.Pnl_Superior.Dock = System.Windows.Forms.DockStyle.Top;
            this.Pnl_Superior.Location = new System.Drawing.Point(0, 0);
            this.Pnl_Superior.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Pnl_Superior.Name = "Pnl_Superior";
            this.Pnl_Superior.Size = new System.Drawing.Size(1069, 44);
            this.Pnl_Superior.TabIndex = 96;
            this.Pnl_Superior.Paint += new System.Windows.Forms.PaintEventHandler(this.Pnl_Superior_Paint);
            // 
            // Pic_Cerrar
            // 
            this.Pic_Cerrar.BackColor = System.Drawing.Color.Transparent;
            this.Pic_Cerrar.Dock = System.Windows.Forms.DockStyle.Right;
            this.Pic_Cerrar.Image = global::Capa_Vista_Seguridad.Properties.Resources.Cancel_icon_icons_com_73703;
            this.Pic_Cerrar.Location = new System.Drawing.Point(1032, 0);
            this.Pic_Cerrar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Pic_Cerrar.Name = "Pic_Cerrar";
            this.Pic_Cerrar.Size = new System.Drawing.Size(37, 44);
            this.Pic_Cerrar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Pic_Cerrar.TabIndex = 0;
            this.Pic_Cerrar.TabStop = false;
            this.Pic_Cerrar.Click += new System.EventHandler(this.Pic_Cerrar_Click);
            // 
            // gestiónFinancieraToolStripMenuItem
            // 
            this.gestiónFinancieraToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cierreContableToolStripMenuItem,
            this.presupuestosToolStripMenuItem,
            this.activosFijosToolStripMenuItem,
            this.estadosFinancierosToolStripMenuItem,
            this.polizasLocalesToolStripMenuItem});
            this.gestiónFinancieraToolStripMenuItem.Font = new System.Drawing.Font("Rockwell", 10F);
            this.gestiónFinancieraToolStripMenuItem.Name = "gestiónFinancieraToolStripMenuItem";
            this.gestiónFinancieraToolStripMenuItem.Size = new System.Drawing.Size(172, 24);
            this.gestiónFinancieraToolStripMenuItem.Text = "Gestión Financiera";
            // 
            // cierreContableToolStripMenuItem
            // 
            this.cierreContableToolStripMenuItem.Name = "cierreContableToolStripMenuItem";
            this.cierreContableToolStripMenuItem.Size = new System.Drawing.Size(249, 26);
            this.cierreContableToolStripMenuItem.Text = "Cierre Contable";
            this.cierreContableToolStripMenuItem.Click += new System.EventHandler(this.cierreContableToolStripMenuItem_Click);
            // 
            // presupuestosToolStripMenuItem
            // 
            this.presupuestosToolStripMenuItem.Name = "presupuestosToolStripMenuItem";
            this.presupuestosToolStripMenuItem.Size = new System.Drawing.Size(249, 26);
            this.presupuestosToolStripMenuItem.Text = "Presupuestos";
            // 
            // activosFijosToolStripMenuItem
            // 
            this.activosFijosToolStripMenuItem.Name = "activosFijosToolStripMenuItem";
            this.activosFijosToolStripMenuItem.Size = new System.Drawing.Size(249, 26);
            this.activosFijosToolStripMenuItem.Text = "Activos Fijos";
            // 
            // estadosFinancierosToolStripMenuItem
            // 
            this.estadosFinancierosToolStripMenuItem.Name = "estadosFinancierosToolStripMenuItem";
            this.estadosFinancierosToolStripMenuItem.Size = new System.Drawing.Size(249, 26);
            this.estadosFinancierosToolStripMenuItem.Text = "Estados Financieros";
            // 
            // polizasLocalesToolStripMenuItem
            // 
            this.polizasLocalesToolStripMenuItem.Name = "polizasLocalesToolStripMenuItem";
            this.polizasLocalesToolStripMenuItem.Size = new System.Drawing.Size(249, 26);
            this.polizasLocalesToolStripMenuItem.Text = "Polizas Locales";
            // 
            // Frm_Seguridad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1069, 503);
            this.Controls.Add(this.Pnl_Superior);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.statusStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.IsMdiContainer = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "Frm_Seguridad";
            this.Text = "frmSeguridad";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Frm_Seguridad_Load_1);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.Pnl_Superior.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Pic_Cerrar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem archivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cerrarSesiónToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem catálogosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem procesosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem herramientasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem asignacionesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem asignacionDeAplicacionAUsuarioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem asignacionPerfilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem perfilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Btn_Aplicacion;
        private System.Windows.Forms.ToolStripMenuItem Btn_Bitacora;
        private System.Windows.Forms.ToolStripMenuItem cuentasToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem usuariosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cambiarContraseñaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modulosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem asignacionDeAplicacionAPerfilesToolStripMenuItem;
        private System.Windows.Forms.Panel Pnl_Superior;
        private System.Windows.Forms.PictureBox Pic_Cerrar;
        private System.Windows.Forms.ToolStripMenuItem gestiónFinancieraToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cierreContableToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem presupuestosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem activosFijosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem estadosFinancierosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem polizasLocalesToolStripMenuItem;
    }
}
