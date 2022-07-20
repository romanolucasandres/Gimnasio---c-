namespace Presentacion
{
    partial class frmClienteClase
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
            this.dgvClases = new System.Windows.Forms.DataGridView();
            this.dgvClientes = new System.Windows.Forms.DataGridView();
            this.btnAsignar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvClienteClase = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.btnDesAsignar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClases)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClientes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClienteClase)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvClases
            // 
            this.dgvClases.AllowUserToAddRows = false;
            this.dgvClases.AllowUserToDeleteRows = false;
            this.dgvClases.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvClases.Location = new System.Drawing.Point(15, 257);
            this.dgvClases.Name = "dgvClases";
            this.dgvClases.ReadOnly = true;
            this.dgvClases.Size = new System.Drawing.Size(435, 158);
            this.dgvClases.TabIndex = 2;
            this.dgvClases.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvClases_RowEnter);
            // 
            // dgvClientes
            // 
            this.dgvClientes.AllowUserToAddRows = false;
            this.dgvClientes.AllowUserToDeleteRows = false;
            this.dgvClientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvClientes.Location = new System.Drawing.Point(15, 48);
            this.dgvClientes.Name = "dgvClientes";
            this.dgvClientes.ReadOnly = true;
            this.dgvClientes.Size = new System.Drawing.Size(435, 158);
            this.dgvClientes.TabIndex = 3;
            this.dgvClientes.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvClientes_RowEnter);
            // 
            // btnAsignar
            // 
            this.btnAsignar.Location = new System.Drawing.Point(354, 220);
            this.btnAsignar.Name = "btnAsignar";
            this.btnAsignar.Size = new System.Drawing.Size(75, 23);
            this.btnAsignar.TabIndex = 4;
            this.btnAsignar.Text = "Asignar";
            this.btnAsignar.UseVisualStyleBackColor = true;
            this.btnAsignar.Click += new System.EventHandler(this.BtnAsignar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Clientes:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 231);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Clases:";
            // 
            // dgvClienteClase
            // 
            this.dgvClienteClase.AllowUserToAddRows = false;
            this.dgvClienteClase.AllowUserToDeleteRows = false;
            this.dgvClienteClase.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvClienteClase.Location = new System.Drawing.Point(490, 48);
            this.dgvClienteClase.Name = "dgvClienteClase";
            this.dgvClienteClase.ReadOnly = true;
            this.dgvClienteClase.Size = new System.Drawing.Size(365, 367);
            this.dgvClienteClase.TabIndex = 7;
            this.dgvClienteClase.Enter += new System.EventHandler(this.DgvClienteClase_Enter);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(487, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(200, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Listado de clientes registrados a la clase:";
            // 
            // btnDesAsignar
            // 
            this.btnDesAsignar.Location = new System.Drawing.Point(635, 431);
            this.btnDesAsignar.Name = "btnDesAsignar";
            this.btnDesAsignar.Size = new System.Drawing.Size(75, 23);
            this.btnDesAsignar.TabIndex = 9;
            this.btnDesAsignar.Text = "Desasignar";
            this.btnDesAsignar.UseVisualStyleBackColor = true;
            this.btnDesAsignar.Click += new System.EventHandler(this.BtnDesAsignar_Click);
            // 
            // frmClienteClase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(887, 473);
            this.Controls.Add(this.btnDesAsignar);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dgvClienteClase);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAsignar);
            this.Controls.Add(this.dgvClientes);
            this.Controls.Add(this.dgvClases);
            this.Name = "frmClienteClase";
            this.Text = "Asignacion cliente-clase";
            this.Load += new System.EventHandler(this.FrmClienteClase_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvClases)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClientes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClienteClase)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvClases;
        private System.Windows.Forms.DataGridView dgvClientes;
        private System.Windows.Forms.Button btnAsignar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvClienteClase;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnDesAsignar;
    }
}