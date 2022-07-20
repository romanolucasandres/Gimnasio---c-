using BE;
using BLL;
using System;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class frmRutinaCliente : Form
    {
        BECliente beCliente;
        BLLCliente bllCliente;
        BERutina beRutina;
        BLLRutina bllRutina;

        public frmRutinaCliente()
        {
            InitializeComponent();

            beCliente = new BECliente();
            bllCliente = new BLLCliente();
            beRutina = new BERutina();
            bllRutina = new BLLRutina();

            dgvClientes.MultiSelect = false;
            dgvClientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvClientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

            dgvRutinas.MultiSelect = false;
            dgvRutinas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRutinas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

            CargarDgv();
        }

        private void BtnAsignar_Click(object sender, EventArgs e)
        {
            try
            {
                bllCliente.AgregarRutina(beCliente, beRutina);
                CargarDgv();

                MessageBox.Show("Se ha asignado la rutina al cliente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnDesAsignar_Click(object sender, EventArgs e)
        {
            try
            {
                bllCliente.EliminarRutina(beCliente);
                CargarDgv();

                MessageBox.Show("Se ha quitado la rutina al cliente y se la eliminó del sistema.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CargarDgv()
        {
            dgvClientes.DataSource = null;
            dgvClientes.DataSource = bllCliente.Listar();

            if (dgvClientes.DataSource != null)
            {
                dgvClientes.Columns["RutinaPersonalizada"].HeaderText = "Rutina personalizada";
                dgvClientes.Columns["Fecha_nacimiento"].HeaderText = "Fecha de nacimiento";
                dgvClientes.Columns["Codigo"].DisplayIndex = 0;
            }

            dgvRutinas.DataSource = null;
            dgvRutinas.DataSource = bllRutina.Listar();
        }

        private void DgvClientes_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                beCliente = (BECliente)dgvClientes.SelectedRows[0].DataBoundItem;
            }
            catch (Exception) { }
        }

        private void DgvRutinas_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                beRutina = (BERutina)dgvRutinas.SelectedRows[0].DataBoundItem;
            }
            catch (Exception) { }
        }

        private void frmRutinaCliente_Load(object sender, EventArgs e)
        {

        }
    }
}
