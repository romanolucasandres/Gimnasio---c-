// referencias a BE y BLL
using BE;
using BLL;
using System;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class frmClienteClase : Form
    {
        // declaro los objetos y luego los instancio
        BEClase beClase;
        BLLClase bllClase;
        BECliente beCliente;
        BLLCliente bllCliente;
        BECliente clienteBorrar;

        public frmClienteClase()
        {
            InitializeComponent();
            beCliente = new BECliente();
            bllCliente = new BLLCliente();
            beClase = new BEClase();
            bllClase = new BLLClase();
            clienteBorrar = new BECliente();
        }

        private void FrmClienteClase_Load(object sender, EventArgs e)
        {
            ConfigurarDgv(dgvClientes);
            ConfigurarDgv(dgvClases);
            ConfigurarDgv(dgvClienteClase);

            CargarDgv();

            MessageBox.Show("Antes de registrar un cliente, seleccione la clase deseada y también el " +
                "cliente a registrar de las dos grillas"); 
        }
        #region asignacion y desasignacion
        private void BtnAsignar_Click(object sender, EventArgs e)
        {
            try
            {
                if (beClase.Codigo != 0 && beCliente.Codigo != 0)   // para que no tire error por la seleccion
                {
                    if (beClase.ListaClientes != null)  // para que no tire error si la lista esta en null
                    {
                        // primero reviso si el cliente ya esta registrado en la clase
                        foreach (BECliente cliente in beClase.ListaClientes)
                        {
                            if (cliente.Codigo == beCliente.Codigo)
                            {
                                throw new Exception("El cliente ya se encuentra registrado a la clase.");
                            }
                        }
                    }

                    bllClase.RegistrarClienteClase(beCliente, beClase);
                    CargarDgv();
                    dgvClienteClase.DataSource = null;

                    MessageBox.Show("Se ha registrado el cliente a la clase.");
                }
                else
                    throw new Exception("Seleccionar los elementos correctamente");
                
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
                if (clienteBorrar.Codigo != 0 && beClase.Codigo != 0)
                {
                    bllClase.ElimnarClienteClase(clienteBorrar, beClase);
                    CargarDgv();
                    dgvClienteClase.DataSource = null;
                }
                else
                    throw new Exception("Seleccionar los elementos correctamente");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion asignacion y desasignacion

        #region metodos varios
        private void CargarDgv()
        {
            dgvClientes.DataSource = null;
            dgvClientes.DataSource = bllCliente.Listar();
            dgvClases.DataSource = null;
            dgvClases.DataSource = bllClase.Listar();
        }

        private void ConfigurarDgv(DataGridView dgv)
        {
            dgv.MultiSelect = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
        }

        private void DgvClientes_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                beCliente = (BECliente)dgvClientes.SelectedRows[0].DataBoundItem;
            }
            catch (Exception ex) { }
        }

        private void DgvClases_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                beClase = (BEClase)dgvClases.SelectedRows[0].DataBoundItem;

                // muestro los clientes registrados en la clase seleccionada
                dgvClienteClase.DataSource = null;
                dgvClienteClase.DataSource = beClase.ListaClientes;
            }
            catch (Exception ex) { }
        }

        private void DgvClienteClase_Enter(object sender, EventArgs e)
        {
            clienteBorrar = (BECliente)dgvClienteClase.SelectedRows[0].DataBoundItem;
        }
        #endregion metodos varios
    }
}
