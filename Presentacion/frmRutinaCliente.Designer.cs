namespace Presentacion
{
    partial class frmRutinaCliente
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
            this.dgvClientes = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvRutinas = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.btnDesAsignar = new System.Windows.Forms.Button();
            this.btnAsignar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClientes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRutinas)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvClientes
            // 
            this.dgvClientes.AllowUserToAddRows = false;
            this.dgvClientes.AllowUserToDeleteRows = false;
            this.dgvClientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvClientes.Location = new System.Drawing.Point(515, 39);
            this.dgvClientes.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvClientes.Name = "dgvClientes";
            this.dgvClientes.ReadOnly = true;
            this.dgvClientes.RowHeadersWidth = 51;
            this.dgvClientes.Size = new System.Drawing.Size(467, 207);
            this.dgvClientes.TabIndex = 1;
            this.dgvClientes.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvClientes_RowEnter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(515, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 16);
            this.label1.TabIndex = 44;
            this.label1.Text = "Clientes:";
            // 
            // dgvRutinas
            // 
            this.dgvRutinas.AllowUserToAddRows = false;
            this.dgvRutinas.AllowUserToDeleteRows = false;
            this.dgvRutinas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRutinas.Location = new System.Drawing.Point(16, 39);
            this.dgvRutinas.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvRutinas.Name = "dgvRutinas";
            this.dgvRutinas.ReadOnly = true;
            this.dgvRutinas.RowHeadersWidth = 51;
            this.dgvRutinas.Size = new System.Drawing.Size(467, 207);
            this.dgvRutinas.TabIndex = 45;
            this.dgvRutinas.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvRutinas_RowEnter);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 20);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 16);
            this.label2.TabIndex = 46;
            this.label2.Text = "Rutinas:";
            // 
            // btnDesAsignar
            // 
            this.btnDesAsignar.Location = new System.Drawing.Point(515, 265);
            this.btnDesAsignar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnDesAsignar.Name = "btnDesAsignar";
            this.btnDesAsignar.Size = new System.Drawing.Size(100, 28);
            this.btnDesAsignar.TabIndex = 48;
            this.btnDesAsignar.Text = "Desasignar";
            this.btnDesAsignar.UseVisualStyleBackColor = true;
            this.btnDesAsignar.Click += new System.EventHandler(this.BtnDesAsignar_Click);
            // 
            // btnAsignar
            // 
            this.btnAsignar.Location = new System.Drawing.Point(16, 265);
            this.btnAsignar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAsignar.Name = "btnAsignar";
            this.btnAsignar.Size = new System.Drawing.Size(100, 28);
            this.btnAsignar.TabIndex = 47;
            this.btnAsignar.Text = "Asignar";
            this.btnAsignar.UseVisualStyleBackColor = true;
            this.btnAsignar.Click += new System.EventHandler(this.BtnAsignar_Click);
            // 
            // frmRutinaCliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(997, 309);
            this.Controls.Add(this.btnDesAsignar);
            this.Controls.Add(this.btnAsignar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgvRutinas);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvClientes);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "frmRutinaCliente";
            this.Text = "Asignar rutina";
            this.Load += new System.EventHandler(this.frmRutinaCliente_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvClientes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRutinas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvClientes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvRutinas;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnDesAsignar;
        private System.Windows.Forms.Button btnAsignar;
    }
}