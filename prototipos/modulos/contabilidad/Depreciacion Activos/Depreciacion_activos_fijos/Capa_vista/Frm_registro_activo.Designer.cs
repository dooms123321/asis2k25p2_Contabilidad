
namespace Capa_vista
{
    partial class Frm_registro_activo
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
            this.Gpb_activo = new System.Windows.Forms.GroupBox();
            this.Txt_fecha_adquisicion = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Lbl_estado_activo = new System.Windows.Forms.Label();
            this.Rdb_inactivo = new System.Windows.Forms.RadioButton();
            this.Rdb_activo = new System.Windows.Forms.RadioButton();
            this.Txt_vida_util = new System.Windows.Forms.TextBox();
            this.Lbl_vida_util = new System.Windows.Forms.Label();
            this.Txt_adquisicion = new System.Windows.Forms.TextBox();
            this.Lbl_valor_residual = new System.Windows.Forms.Label();
            this.Txt_costo_adquisicion = new System.Windows.Forms.TextBox();
            this.Lbl_costo_adquisicion = new System.Windows.Forms.Label();
            this.Lbl_fecha = new System.Windows.Forms.Label();
            this.Lbl_Grupo = new System.Windows.Forms.Label();
            this.Cbo_grupo = new System.Windows.Forms.ComboBox();
            this.Txt_descripcion = new System.Windows.Forms.TextBox();
            this.Lbl_descripcion = new System.Windows.Forms.Label();
            this.Txt_activo_fijo = new System.Windows.Forms.TextBox();
            this.Lbl_activo_fijo = new System.Windows.Forms.Label();
            this.Gpb_cuentas_Activo = new System.Windows.Forms.GroupBox();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.Btn_limpiar = new System.Windows.Forms.Button();
            this.Btn_guardar = new System.Windows.Forms.Button();
            this.Lbl_cuneta_gastos = new System.Windows.Forms.Label();
            this.Lbl_cuenta_depreciacion = new System.Windows.Forms.Label();
            this.Lbl_cuenta_activo = new System.Windows.Forms.Label();
            this.Btn_diagnostico = new System.Windows.Forms.Button();
            this.Btn_diagnostico_indice = new System.Windows.Forms.Button();
            this.Gpb_activo.SuspendLayout();
            this.Gpb_cuentas_Activo.SuspendLayout();
            this.SuspendLayout();
            // 
            // Gpb_activo
            // 
            this.Gpb_activo.Controls.Add(this.Txt_fecha_adquisicion);
            this.Gpb_activo.Controls.Add(this.label1);
            this.Gpb_activo.Controls.Add(this.Lbl_estado_activo);
            this.Gpb_activo.Controls.Add(this.Rdb_inactivo);
            this.Gpb_activo.Controls.Add(this.Rdb_activo);
            this.Gpb_activo.Controls.Add(this.Txt_vida_util);
            this.Gpb_activo.Controls.Add(this.Lbl_vida_util);
            this.Gpb_activo.Controls.Add(this.Txt_adquisicion);
            this.Gpb_activo.Controls.Add(this.Lbl_valor_residual);
            this.Gpb_activo.Controls.Add(this.Txt_costo_adquisicion);
            this.Gpb_activo.Controls.Add(this.Lbl_costo_adquisicion);
            this.Gpb_activo.Controls.Add(this.Lbl_fecha);
            this.Gpb_activo.Controls.Add(this.Lbl_Grupo);
            this.Gpb_activo.Controls.Add(this.Cbo_grupo);
            this.Gpb_activo.Controls.Add(this.Txt_descripcion);
            this.Gpb_activo.Controls.Add(this.Lbl_descripcion);
            this.Gpb_activo.Controls.Add(this.Txt_activo_fijo);
            this.Gpb_activo.Controls.Add(this.Lbl_activo_fijo);
            this.Gpb_activo.Font = new System.Drawing.Font("Rockwell", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Gpb_activo.Location = new System.Drawing.Point(12, 21);
            this.Gpb_activo.Name = "Gpb_activo";
            this.Gpb_activo.Size = new System.Drawing.Size(1200, 346);
            this.Gpb_activo.TabIndex = 0;
            this.Gpb_activo.TabStop = false;
            this.Gpb_activo.Text = "Activos fijos";
            // 
            // Txt_fecha_adquisicion
            // 
            this.Txt_fecha_adquisicion.Location = new System.Drawing.Point(219, 184);
            this.Txt_fecha_adquisicion.Name = "Txt_fecha_adquisicion";
            this.Txt_fecha_adquisicion.Size = new System.Drawing.Size(384, 31);
            this.Txt_fecha_adquisicion.TabIndex = 18;
            this.Txt_fecha_adquisicion.TextChanged += new System.EventHandler(this.Txt_fecha_adquisicion_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Rockwell", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(816, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(229, 41);
            this.label1.TabIndex = 17;
            this.label1.Text = "Activos Fijos";
            // 
            // Lbl_estado_activo
            // 
            this.Lbl_estado_activo.AutoSize = true;
            this.Lbl_estado_activo.Location = new System.Drawing.Point(801, 292);
            this.Lbl_estado_activo.Name = "Lbl_estado_activo";
            this.Lbl_estado_activo.Size = new System.Drawing.Size(174, 22);
            this.Lbl_estado_activo.TabIndex = 16;
            this.Lbl_estado_activo.Text = "Estado del activo:";
            // 
            // Rdb_inactivo
            // 
            this.Rdb_inactivo.AutoSize = true;
            this.Rdb_inactivo.Location = new System.Drawing.Point(1074, 288);
            this.Rdb_inactivo.Name = "Rdb_inactivo";
            this.Rdb_inactivo.Size = new System.Drawing.Size(107, 26);
            this.Rdb_inactivo.TabIndex = 15;
            this.Rdb_inactivo.TabStop = true;
            this.Rdb_inactivo.Text = "Inactivo";
            this.Rdb_inactivo.UseVisualStyleBackColor = true;
            // 
            // Rdb_activo
            // 
            this.Rdb_activo.AutoSize = true;
            this.Rdb_activo.Location = new System.Drawing.Point(975, 288);
            this.Rdb_activo.Name = "Rdb_activo";
            this.Rdb_activo.Size = new System.Drawing.Size(93, 26);
            this.Rdb_activo.TabIndex = 14;
            this.Rdb_activo.TabStop = true;
            this.Rdb_activo.Text = "Activo";
            this.Rdb_activo.UseVisualStyleBackColor = true;
            // 
            // Txt_vida_util
            // 
            this.Txt_vida_util.Location = new System.Drawing.Point(219, 289);
            this.Txt_vida_util.Name = "Txt_vida_util";
            this.Txt_vida_util.Size = new System.Drawing.Size(385, 31);
            this.Txt_vida_util.TabIndex = 13;
            // 
            // Lbl_vida_util
            // 
            this.Lbl_vida_util.AutoSize = true;
            this.Lbl_vida_util.Location = new System.Drawing.Point(89, 292);
            this.Lbl_vida_util.Name = "Lbl_vida_util";
            this.Lbl_vida_util.Size = new System.Drawing.Size(97, 22);
            this.Lbl_vida_util.TabIndex = 12;
            this.Lbl_vida_util.Text = "Vida Util:";
            // 
            // Txt_adquisicion
            // 
            this.Txt_adquisicion.Location = new System.Drawing.Point(837, 230);
            this.Txt_adquisicion.Name = "Txt_adquisicion";
            this.Txt_adquisicion.Size = new System.Drawing.Size(344, 31);
            this.Txt_adquisicion.TabIndex = 11;
            // 
            // Lbl_valor_residual
            // 
            this.Lbl_valor_residual.AutoSize = true;
            this.Lbl_valor_residual.Location = new System.Drawing.Point(685, 233);
            this.Lbl_valor_residual.Name = "Lbl_valor_residual";
            this.Lbl_valor_residual.Size = new System.Drawing.Size(146, 22);
            this.Lbl_valor_residual.TabIndex = 10;
            this.Lbl_valor_residual.Text = "Valor residual:";
            // 
            // Txt_costo_adquisicion
            // 
            this.Txt_costo_adquisicion.Location = new System.Drawing.Point(219, 239);
            this.Txt_costo_adquisicion.Name = "Txt_costo_adquisicion";
            this.Txt_costo_adquisicion.Size = new System.Drawing.Size(384, 31);
            this.Txt_costo_adquisicion.TabIndex = 9;
            // 
            // Lbl_costo_adquisicion
            // 
            this.Lbl_costo_adquisicion.AutoSize = true;
            this.Lbl_costo_adquisicion.Location = new System.Drawing.Point(6, 239);
            this.Lbl_costo_adquisicion.Name = "Lbl_costo_adquisicion";
            this.Lbl_costo_adquisicion.Size = new System.Drawing.Size(208, 22);
            this.Lbl_costo_adquisicion.TabIndex = 8;
            this.Lbl_costo_adquisicion.Text = "Costo de adquisicion:";
            // 
            // Lbl_fecha
            // 
            this.Lbl_fecha.AutoSize = true;
            this.Lbl_fecha.Location = new System.Drawing.Point(15, 187);
            this.Lbl_fecha.Name = "Lbl_fecha";
            this.Lbl_fecha.Size = new System.Drawing.Size(186, 22);
            this.Lbl_fecha.TabIndex = 7;
            this.Lbl_fecha.Text = "Fecha adquisicion: ";
            // 
            // Lbl_Grupo
            // 
            this.Lbl_Grupo.AutoSize = true;
            this.Lbl_Grupo.Location = new System.Drawing.Point(111, 130);
            this.Lbl_Grupo.Name = "Lbl_Grupo";
            this.Lbl_Grupo.Size = new System.Drawing.Size(75, 22);
            this.Lbl_Grupo.TabIndex = 5;
            this.Lbl_Grupo.Text = "Grupo:";
            // 
            // Cbo_grupo
            // 
            this.Cbo_grupo.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.Cbo_grupo.FormattingEnabled = true;
            this.Cbo_grupo.Location = new System.Drawing.Point(207, 127);
            this.Cbo_grupo.Name = "Cbo_grupo";
            this.Cbo_grupo.Size = new System.Drawing.Size(244, 30);
            this.Cbo_grupo.TabIndex = 4;
            // 
            // Txt_descripcion
            // 
            this.Txt_descripcion.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.Txt_descripcion.Location = new System.Drawing.Point(207, 75);
            this.Txt_descripcion.Name = "Txt_descripcion";
            this.Txt_descripcion.Size = new System.Drawing.Size(244, 31);
            this.Txt_descripcion.TabIndex = 3;
            // 
            // Lbl_descripcion
            // 
            this.Lbl_descripcion.AutoSize = true;
            this.Lbl_descripcion.Location = new System.Drawing.Point(62, 78);
            this.Lbl_descripcion.Name = "Lbl_descripcion";
            this.Lbl_descripcion.Size = new System.Drawing.Size(126, 22);
            this.Lbl_descripcion.TabIndex = 2;
            this.Lbl_descripcion.Text = "Descripcion:";
            // 
            // Txt_activo_fijo
            // 
            this.Txt_activo_fijo.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.Txt_activo_fijo.Location = new System.Drawing.Point(207, 24);
            this.Txt_activo_fijo.Name = "Txt_activo_fijo";
            this.Txt_activo_fijo.Size = new System.Drawing.Size(244, 31);
            this.Txt_activo_fijo.TabIndex = 1;
            // 
            // Lbl_activo_fijo
            // 
            this.Lbl_activo_fijo.AutoSize = true;
            this.Lbl_activo_fijo.Location = new System.Drawing.Point(71, 27);
            this.Lbl_activo_fijo.Name = "Lbl_activo_fijo";
            this.Lbl_activo_fijo.Size = new System.Drawing.Size(115, 22);
            this.Lbl_activo_fijo.TabIndex = 0;
            this.Lbl_activo_fijo.Text = "Acrivo Fijo:";
            // 
            // Gpb_cuentas_Activo
            // 
            this.Gpb_cuentas_Activo.Controls.Add(this.Btn_diagnostico_indice);
            this.Gpb_cuentas_Activo.Controls.Add(this.Btn_diagnostico);
            this.Gpb_cuentas_Activo.Controls.Add(this.comboBox3);
            this.Gpb_cuentas_Activo.Controls.Add(this.comboBox2);
            this.Gpb_cuentas_Activo.Controls.Add(this.comboBox1);
            this.Gpb_cuentas_Activo.Controls.Add(this.Btn_limpiar);
            this.Gpb_cuentas_Activo.Controls.Add(this.Btn_guardar);
            this.Gpb_cuentas_Activo.Controls.Add(this.Lbl_cuneta_gastos);
            this.Gpb_cuentas_Activo.Controls.Add(this.Lbl_cuenta_depreciacion);
            this.Gpb_cuentas_Activo.Controls.Add(this.Lbl_cuenta_activo);
            this.Gpb_cuentas_Activo.Font = new System.Drawing.Font("Rockwell", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Gpb_cuentas_Activo.Location = new System.Drawing.Point(15, 402);
            this.Gpb_cuentas_Activo.Name = "Gpb_cuentas_Activo";
            this.Gpb_cuentas_Activo.Size = new System.Drawing.Size(1196, 279);
            this.Gpb_cuentas_Activo.TabIndex = 1;
            this.Gpb_cuentas_Activo.TabStop = false;
            this.Gpb_cuentas_Activo.Text = "Cuentas de depreciacion";
            // 
            // comboBox3
            // 
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new System.Drawing.Point(799, 153);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(321, 30);
            this.comboBox3.TabIndex = 7;
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(219, 153);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(282, 30);
            this.comboBox2.TabIndex = 6;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(216, 62);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(285, 30);
            this.comboBox1.TabIndex = 5;
            // 
            // Btn_limpiar
            // 
            this.Btn_limpiar.Location = new System.Drawing.Point(1048, 225);
            this.Btn_limpiar.Name = "Btn_limpiar";
            this.Btn_limpiar.Size = new System.Drawing.Size(130, 48);
            this.Btn_limpiar.TabIndex = 4;
            this.Btn_limpiar.Text = "Limpiar";
            this.Btn_limpiar.UseVisualStyleBackColor = true;
            // 
            // Btn_guardar
            // 
            this.Btn_guardar.Location = new System.Drawing.Point(916, 229);
            this.Btn_guardar.Name = "Btn_guardar";
            this.Btn_guardar.Size = new System.Drawing.Size(126, 44);
            this.Btn_guardar.TabIndex = 3;
            this.Btn_guardar.Text = "Guardar";
            this.Btn_guardar.UseVisualStyleBackColor = true;
            this.Btn_guardar.Click += new System.EventHandler(this.Btn_guardar_Click_1);
            // 
            // Lbl_cuneta_gastos
            // 
            this.Lbl_cuneta_gastos.AutoSize = true;
            this.Lbl_cuneta_gastos.Location = new System.Drawing.Point(522, 156);
            this.Lbl_cuneta_gastos.Name = "Lbl_cuneta_gastos";
            this.Lbl_cuneta_gastos.Size = new System.Drawing.Size(271, 22);
            this.Lbl_cuneta_gastos.TabIndex = 2;
            this.Lbl_cuneta_gastos.Text = "Cuenta gastos depreciacion:";
            // 
            // Lbl_cuenta_depreciacion
            // 
            this.Lbl_cuenta_depreciacion.AutoSize = true;
            this.Lbl_cuenta_depreciacion.Location = new System.Drawing.Point(6, 153);
            this.Lbl_cuenta_depreciacion.Name = "Lbl_cuenta_depreciacion";
            this.Lbl_cuenta_depreciacion.Size = new System.Drawing.Size(207, 22);
            this.Lbl_cuenta_depreciacion.TabIndex = 1;
            this.Lbl_cuenta_depreciacion.Text = "Cuenta depreciacion:";
            // 
            // Lbl_cuenta_activo
            // 
            this.Lbl_cuenta_activo.AutoSize = true;
            this.Lbl_cuenta_activo.Location = new System.Drawing.Point(33, 62);
            this.Lbl_cuenta_activo.Name = "Lbl_cuenta_activo";
            this.Lbl_cuenta_activo.Size = new System.Drawing.Size(170, 22);
            this.Lbl_cuenta_activo.TabIndex = 0;
            this.Lbl_cuenta_activo.Text = "Cuenta de activo:";
            // 
            // Btn_diagnostico
            // 
            this.Btn_diagnostico.Location = new System.Drawing.Point(725, 42);
            this.Btn_diagnostico.Name = "Btn_diagnostico";
            this.Btn_diagnostico.Size = new System.Drawing.Size(75, 23);
            this.Btn_diagnostico.TabIndex = 8;
            this.Btn_diagnostico.Text = "button1";
            this.Btn_diagnostico.UseVisualStyleBackColor = true;
            this.Btn_diagnostico.Click += new System.EventHandler(this.Btn_diagnostico_Click);
            // 
            // Btn_diagnostico_indice
            // 
            this.Btn_diagnostico_indice.Location = new System.Drawing.Point(987, 47);
            this.Btn_diagnostico_indice.Name = "Btn_diagnostico_indice";
            this.Btn_diagnostico_indice.Size = new System.Drawing.Size(75, 23);
            this.Btn_diagnostico_indice.TabIndex = 9;
            this.Btn_diagnostico_indice.Text = "c";
            this.Btn_diagnostico_indice.UseVisualStyleBackColor = true;
            this.Btn_diagnostico_indice.Click += new System.EventHandler(this.Btn_diagnostico_indice_Click);
            // 
            // Frm_registro_activo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(108)))), ((int)(((byte)(0)))));
            this.ClientSize = new System.Drawing.Size(1219, 724);
            this.Controls.Add(this.Gpb_cuentas_Activo);
            this.Controls.Add(this.Gpb_activo);
            this.Name = "Frm_registro_activo";
            this.Text = "Frm_registro_activo";
            this.Load += new System.EventHandler(this.Frm_registro_activo_Load);
            this.Gpb_activo.ResumeLayout(false);
            this.Gpb_activo.PerformLayout();
            this.Gpb_cuentas_Activo.ResumeLayout(false);
            this.Gpb_cuentas_Activo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox Gpb_activo;
        private System.Windows.Forms.TextBox Txt_adquisicion;
        private System.Windows.Forms.Label Lbl_valor_residual;
        private System.Windows.Forms.TextBox Txt_costo_adquisicion;
        private System.Windows.Forms.Label Lbl_costo_adquisicion;
        private System.Windows.Forms.Label Lbl_fecha;
        private System.Windows.Forms.Label Lbl_Grupo;
        private System.Windows.Forms.ComboBox Cbo_grupo;
        private System.Windows.Forms.TextBox Txt_descripcion;
        private System.Windows.Forms.Label Lbl_descripcion;
        private System.Windows.Forms.TextBox Txt_activo_fijo;
        private System.Windows.Forms.Label Lbl_activo_fijo;
        private System.Windows.Forms.Label Lbl_estado_activo;
        private System.Windows.Forms.RadioButton Rdb_inactivo;
        private System.Windows.Forms.RadioButton Rdb_activo;
        private System.Windows.Forms.TextBox Txt_vida_util;
        private System.Windows.Forms.Label Lbl_vida_util;
        private System.Windows.Forms.GroupBox Gpb_cuentas_Activo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button Btn_limpiar;
        private System.Windows.Forms.Button Btn_guardar;
        private System.Windows.Forms.Label Lbl_cuneta_gastos;
        private System.Windows.Forms.Label Lbl_cuenta_depreciacion;
        private System.Windows.Forms.Label Lbl_cuenta_activo;
        private System.Windows.Forms.TextBox Txt_fecha_adquisicion;
        private System.Windows.Forms.Button Btn_diagnostico;
        private System.Windows.Forms.Button Btn_diagnostico_indice;
    }
}