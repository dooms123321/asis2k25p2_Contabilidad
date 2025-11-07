namespace Capa_Vista_Polizas
{
    partial class Frm_PolizasLocales
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
            this.Dgv_EncabezadoPolizas = new System.Windows.Forms.DataGridView();
            this.Btn_Ingresar = new System.Windows.Forms.Button();
            this.Btn_Editar = new System.Windows.Forms.Button();
            this.Btn_Borrar = new System.Windows.Forms.Button();
            this.Btn_Imprimir = new System.Windows.Forms.Button();
            this.Btn_Refrescar = new System.Windows.Forms.Button();
            this.Btn_Salir = new System.Windows.Forms.Button();
            this.Btn_Filtrar = new System.Windows.Forms.Button();
            this.Btn_Todos = new System.Windows.Forms.Button();
            this.Lbl_ModoActual = new System.Windows.Forms.Label();
            this.Btn_CambiarModo = new System.Windows.Forms.Button();
            this.Btn_ActualizarSaldos = new System.Windows.Forms.Button();
            this.Btn_CierreMes = new System.Windows.Forms.Button();
            this.Btn_CierreAnio = new System.Windows.Forms.Button();
            this.Btn_SincronizarModo = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_EncabezadoPolizas)).BeginInit();
            this.SuspendLayout();
            // 
            // Dgv_EncabezadoPolizas
            // 
            this.Dgv_EncabezadoPolizas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dgv_EncabezadoPolizas.Location = new System.Drawing.Point(12, 138);
            this.Dgv_EncabezadoPolizas.Name = "Dgv_EncabezadoPolizas";
            this.Dgv_EncabezadoPolizas.RowHeadersWidth = 51;
            this.Dgv_EncabezadoPolizas.RowTemplate.Height = 24;
            this.Dgv_EncabezadoPolizas.Size = new System.Drawing.Size(1427, 572);
            this.Dgv_EncabezadoPolizas.TabIndex = 0;
            this.Dgv_EncabezadoPolizas.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dgv_EncabezadoPolizas_CellDoubleClick);
            // 
            // Btn_Ingresar
            // 
            this.Btn_Ingresar.Font = new System.Drawing.Font("Rockwell", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_Ingresar.Location = new System.Drawing.Point(12, 30);
            this.Btn_Ingresar.Name = "Btn_Ingresar";
            this.Btn_Ingresar.Size = new System.Drawing.Size(100, 80);
            this.Btn_Ingresar.TabIndex = 1;
            this.Btn_Ingresar.Text = "Ingresar";
            this.Btn_Ingresar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.Btn_Ingresar.UseVisualStyleBackColor = true;
            this.Btn_Ingresar.Click += new System.EventHandler(this.Btn_Ingresar_Click);
            // 
            // Btn_Editar
            // 
            this.Btn_Editar.Font = new System.Drawing.Font("Rockwell", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_Editar.Location = new System.Drawing.Point(118, 30);
            this.Btn_Editar.Name = "Btn_Editar";
            this.Btn_Editar.Size = new System.Drawing.Size(100, 80);
            this.Btn_Editar.TabIndex = 2;
            this.Btn_Editar.Text = "Editar";
            this.Btn_Editar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.Btn_Editar.UseVisualStyleBackColor = true;
            this.Btn_Editar.Click += new System.EventHandler(this.Btn_Editar_Click);
            // 
            // Btn_Borrar
            // 
            this.Btn_Borrar.Font = new System.Drawing.Font("Rockwell", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_Borrar.Location = new System.Drawing.Point(224, 30);
            this.Btn_Borrar.Name = "Btn_Borrar";
            this.Btn_Borrar.Size = new System.Drawing.Size(100, 80);
            this.Btn_Borrar.TabIndex = 3;
            this.Btn_Borrar.Text = "Borrar";
            this.Btn_Borrar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.Btn_Borrar.UseVisualStyleBackColor = true;
            this.Btn_Borrar.Click += new System.EventHandler(this.Btn_Borrar_Click);
            // 
            // Btn_Imprimir
            // 
            this.Btn_Imprimir.Font = new System.Drawing.Font("Rockwell", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_Imprimir.Location = new System.Drawing.Point(439, 30);
            this.Btn_Imprimir.Name = "Btn_Imprimir";
            this.Btn_Imprimir.Size = new System.Drawing.Size(100, 80);
            this.Btn_Imprimir.TabIndex = 4;
            this.Btn_Imprimir.Text = "Imprimir";
            this.Btn_Imprimir.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.Btn_Imprimir.UseVisualStyleBackColor = true;
            this.Btn_Imprimir.Click += new System.EventHandler(this.Btn_Cancelar_Click);
            // 
            // Btn_Refrescar
            // 
            this.Btn_Refrescar.Font = new System.Drawing.Font("Rockwell", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_Refrescar.Location = new System.Drawing.Point(333, 30);
            this.Btn_Refrescar.Name = "Btn_Refrescar";
            this.Btn_Refrescar.Size = new System.Drawing.Size(100, 80);
            this.Btn_Refrescar.TabIndex = 5;
            this.Btn_Refrescar.Text = "Refrescar";
            this.Btn_Refrescar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.Btn_Refrescar.UseVisualStyleBackColor = true;
            this.Btn_Refrescar.Click += new System.EventHandler(this.Btn_Refrescar_Click);
            // 
            // Btn_Salir
            // 
            this.Btn_Salir.Font = new System.Drawing.Font("Rockwell", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_Salir.Location = new System.Drawing.Point(757, 30);
            this.Btn_Salir.Name = "Btn_Salir";
            this.Btn_Salir.Size = new System.Drawing.Size(100, 80);
            this.Btn_Salir.TabIndex = 6;
            this.Btn_Salir.Text = "Salir";
            this.Btn_Salir.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.Btn_Salir.UseVisualStyleBackColor = true;
            this.Btn_Salir.Click += new System.EventHandler(this.Btn_Salir_Click);
            // 
            // Btn_Filtrar
            // 
            this.Btn_Filtrar.Font = new System.Drawing.Font("Rockwell", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_Filtrar.Location = new System.Drawing.Point(545, 30);
            this.Btn_Filtrar.Name = "Btn_Filtrar";
            this.Btn_Filtrar.Size = new System.Drawing.Size(100, 80);
            this.Btn_Filtrar.TabIndex = 7;
            this.Btn_Filtrar.Text = "Filtrar";
            this.Btn_Filtrar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.Btn_Filtrar.UseVisualStyleBackColor = true;
            // 
            // Btn_Todos
            // 
            this.Btn_Todos.Font = new System.Drawing.Font("Rockwell", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_Todos.Location = new System.Drawing.Point(651, 30);
            this.Btn_Todos.Name = "Btn_Todos";
            this.Btn_Todos.Size = new System.Drawing.Size(100, 80);
            this.Btn_Todos.TabIndex = 8;
            this.Btn_Todos.Text = "Todos";
            this.Btn_Todos.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.Btn_Todos.UseVisualStyleBackColor = true;
            // 
            // Lbl_ModoActual
            // 
            this.Lbl_ModoActual.AutoSize = true;
            this.Lbl_ModoActual.Font = new System.Drawing.Font("Rockwell", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_ModoActual.Location = new System.Drawing.Point(894, 25);
            this.Lbl_ModoActual.Name = "Lbl_ModoActual";
            this.Lbl_ModoActual.Size = new System.Drawing.Size(154, 28);
            this.Lbl_ModoActual.TabIndex = 9;
            this.Lbl_ModoActual.Text = "Modo Actual";
            // 
            // Btn_CambiarModo
            // 
            this.Btn_CambiarModo.Font = new System.Drawing.Font("Rockwell", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_CambiarModo.Location = new System.Drawing.Point(863, 56);
            this.Btn_CambiarModo.Name = "Btn_CambiarModo";
            this.Btn_CambiarModo.Size = new System.Drawing.Size(116, 54);
            this.Btn_CambiarModo.TabIndex = 10;
            this.Btn_CambiarModo.Text = "Cambiar Modo";
            this.Btn_CambiarModo.UseVisualStyleBackColor = true;
            this.Btn_CambiarModo.Click += new System.EventHandler(this.Btn_CambiarModo_Click);
            // 
            // Btn_ActualizarSaldos
            // 
            this.Btn_ActualizarSaldos.Font = new System.Drawing.Font("Rockwell", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_ActualizarSaldos.Location = new System.Drawing.Point(1107, 56);
            this.Btn_ActualizarSaldos.Name = "Btn_ActualizarSaldos";
            this.Btn_ActualizarSaldos.Size = new System.Drawing.Size(116, 54);
            this.Btn_ActualizarSaldos.TabIndex = 11;
            this.Btn_ActualizarSaldos.Text = "Actualizar Saldos";
            this.Btn_ActualizarSaldos.UseVisualStyleBackColor = true;
            this.Btn_ActualizarSaldos.Click += new System.EventHandler(this.Btn_ActualizarSaldos_Click);
            // 
            // Btn_CierreMes
            // 
            this.Btn_CierreMes.Font = new System.Drawing.Font("Rockwell", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_CierreMes.Location = new System.Drawing.Point(1229, 56);
            this.Btn_CierreMes.Name = "Btn_CierreMes";
            this.Btn_CierreMes.Size = new System.Drawing.Size(116, 54);
            this.Btn_CierreMes.TabIndex = 12;
            this.Btn_CierreMes.Text = "Cierre Mes";
            this.Btn_CierreMes.UseVisualStyleBackColor = true;
            this.Btn_CierreMes.Click += new System.EventHandler(this.Btn_CierreMes_Click);
            // 
            // Btn_CierreAnio
            // 
            this.Btn_CierreAnio.Font = new System.Drawing.Font("Rockwell", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_CierreAnio.Location = new System.Drawing.Point(1351, 56);
            this.Btn_CierreAnio.Name = "Btn_CierreAnio";
            this.Btn_CierreAnio.Size = new System.Drawing.Size(116, 54);
            this.Btn_CierreAnio.TabIndex = 13;
            this.Btn_CierreAnio.Text = "Cierre Año";
            this.Btn_CierreAnio.UseVisualStyleBackColor = true;
            this.Btn_CierreAnio.Click += new System.EventHandler(this.Btn_CierreAnio_Click);
            // 
            // Btn_SincronizarModo
            // 
            this.Btn_SincronizarModo.Font = new System.Drawing.Font("Rockwell", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_SincronizarModo.Location = new System.Drawing.Point(985, 56);
            this.Btn_SincronizarModo.Name = "Btn_SincronizarModo";
            this.Btn_SincronizarModo.Size = new System.Drawing.Size(116, 54);
            this.Btn_SincronizarModo.TabIndex = 14;
            this.Btn_SincronizarModo.Text = "Sincronizar Modo";
            this.Btn_SincronizarModo.UseVisualStyleBackColor = true;
            this.Btn_SincronizarModo.Click += new System.EventHandler(this.Btn_SincronizarModo_Click);
            // 
            // Frm_PolizasLocales
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1475, 722);
            this.Controls.Add(this.Btn_SincronizarModo);
            this.Controls.Add(this.Btn_CierreAnio);
            this.Controls.Add(this.Btn_CierreMes);
            this.Controls.Add(this.Btn_ActualizarSaldos);
            this.Controls.Add(this.Btn_CambiarModo);
            this.Controls.Add(this.Lbl_ModoActual);
            this.Controls.Add(this.Btn_Todos);
            this.Controls.Add(this.Btn_Filtrar);
            this.Controls.Add(this.Btn_Salir);
            this.Controls.Add(this.Btn_Refrescar);
            this.Controls.Add(this.Btn_Imprimir);
            this.Controls.Add(this.Btn_Borrar);
            this.Controls.Add(this.Btn_Editar);
            this.Controls.Add(this.Btn_Ingresar);
            this.Controls.Add(this.Dgv_EncabezadoPolizas);
            this.Name = "Frm_PolizasLocales";
            this.Text = "Frm_PolizasLocales";
            this.Load += new System.EventHandler(this.Frm_PolizasLocales_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_EncabezadoPolizas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView Dgv_EncabezadoPolizas;
        private System.Windows.Forms.Button Btn_Ingresar;
        private System.Windows.Forms.Button Btn_Editar;
        private System.Windows.Forms.Button Btn_Borrar;
        private System.Windows.Forms.Button Btn_Imprimir;
        private System.Windows.Forms.Button Btn_Refrescar;
        private System.Windows.Forms.Button Btn_Salir;
        private System.Windows.Forms.Button Btn_Filtrar;
        private System.Windows.Forms.Button Btn_Todos;
        private System.Windows.Forms.Label Lbl_ModoActual;
        private System.Windows.Forms.Button Btn_CambiarModo;
        private System.Windows.Forms.Button Btn_ActualizarSaldos;
        private System.Windows.Forms.Button Btn_CierreMes;
        private System.Windows.Forms.Button Btn_CierreAnio;
        private System.Windows.Forms.Button Btn_SincronizarModo;
    }
}