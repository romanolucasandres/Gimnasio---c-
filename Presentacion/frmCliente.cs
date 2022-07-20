using BE;
// presentacion tiene referencias a BE y BLL
using BLL;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class frmCliente : Form
    {
        // declaro objetos BE y BLL
        BECliente beCliente;
        BLLCliente bllCliente;

        public frmCliente()
        {
            InitializeComponent();
            // los instancio para trabajar con ellos
            beCliente = new BECliente();
            bllCliente = new BLLCliente();
        }

        private void FrmCliente_Load(object sender, EventArgs e)
        {
            dgv.MultiSelect = false;    // para seleccionar solo una fila
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;  // para que se seleccione la fila completa
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;   // celdas autoajustables

            CargarDgv();
        } 

        #region ABM
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                // cargo todas las propiedades del objeto para luego pasarlo al método de la BLL
                beCliente.Codigo = 0;
                CargarDatos();

                bllCliente.Guardar(beCliente);
                CargarDgv();
                LimpiarTxt();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void BtnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                // guardo los valores nuevos y los paso a la BLL
                beCliente.Codigo = int.Parse(txtCod.Text);
                CargarDatos();

                bllCliente.Guardar(beCliente);
                CargarDgv();
                LimpiarTxt();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void BtnBorrar_Click(object sender, EventArgs e)
        {
            try
            {
                //busco el codigo del cliente porque es lo unico que necesito para borrarlo
                beCliente.Codigo = int.Parse(txtCod.Text);

                DialogResult resultado = MessageBox.Show("¿Está seguro que quiere eliminar el cliente?",
                    "Confirmar", MessageBoxButtons.YesNo);

                if (resultado == DialogResult.Yes)
                {
                    bllCliente.Baja(beCliente);
                    CargarDgv();
                }

                LimpiarTxt();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
        #endregion ABM
        #region metodos varios
        // para cargar los datos en los txtBox cuando selecciono una fila
        private void Dgv_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // busco el objeto para obtener sus propiedades
                beCliente = (BECliente)dgv.SelectedRows[0].DataBoundItem;

                txtCod.Text = beCliente.Codigo.ToString();
                txtNombre.Text = beCliente.Nombre;
                txtApe.Text = beCliente.Apellido;
                txtDNI.Text = beCliente.DNI.ToString();
                txtTelefono.Text = beCliente.Telefono.ToString();
                txtEdad.Text = beCliente.Edad.ToString();
                dateTimePicker1.Value = beCliente.Fecha_nacimiento;
            }
            catch (Exception) { }
        }

        private void CargarDgv()
        {
            dgv.DataSource = null;
            dgv.DataSource = bllCliente.Listar();

            if(dgv.DataSource != null)
            {
                dgv.Columns["Fecha_nacimiento"].HeaderText = "Fecha de nacimiento";
                dgv.Columns["RutinaPersonalizada"].HeaderText = "Rutina personalizada";
                dgv.Columns["Codigo"].DisplayIndex = 0;
            }
        }

        // para no repetir codigo en alta y modificar
        private void CargarDatos()
        {
            // para que el DNI contenga 8 dígitos
            if (!(Regex.IsMatch(txtDNI.Text, "^[0-9]{8}$")))
                throw new Exception("El formato del DNI es incorrecto.");

            // para que el telefono sea un telefono celular o fijo
            if(!(Regex.IsMatch(txtTelefono.Text, "^([0-9]{10}|[0-9]{8})$")))
                throw new Exception("El formato del teléfono es incorrecto.");


            beCliente.DNI = int.Parse(txtDNI.Text);
            beCliente.Nombre = txtNombre.Text;
            beCliente.Apellido = txtApe.Text;
            beCliente.Telefono = int.Parse(txtTelefono.Text);
            if (dateTimePicker1.Value.Year < DateTime.Today.Year)
                beCliente.Fecha_nacimiento = dateTimePicker1.Value;
            else
                throw new Exception("Por favor ingresar una fecha valida.");
            beCliente.CalcularEdad();
        }

        private void LimpiarTxt()
        {
            txtCod.Text = "";
            txtNombre.Text = "";
            txtApe.Text = "";
            txtDNI.Text = "";
            txtTelefono.Text = "";
            txtEdad.Text = "";
            dateTimePicker1.Value = DateTime.Today;
        }
        #endregion metodos varios        
    }
}
