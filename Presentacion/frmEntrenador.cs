using BE;
// presentacion tiene referencias a BE y BLL
using BLL;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class frmEntrenador : Form
    {
        // declaro los objetos y despues los instancio
        BEEntrenador beEntrenador;
        BLLEntrenador bllEntrenador;

        public frmEntrenador()
        {
            InitializeComponent();

            beEntrenador = new BEEntrenador();
            bllEntrenador = new BLLEntrenador();

            dgv.MultiSelect = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

            CargarDgv(); 
        }

        #region AMB
        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                beEntrenador.Codigo = 0;
                beEntrenador.Contrasenia = mailPass1.GetTextoPass();

                CargarDatos();

                bllEntrenador.Guardar(beEntrenador);
                CargarDgv();
                Limpiar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                beEntrenador.Codigo = int.Parse(txtCod.Text);
                CargarDatos();

                bllEntrenador.Guardar(beEntrenador);
                CargarDgv();
                Limpiar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnBorrar_Click(object sender, EventArgs e)
        {
            try
            {
                beEntrenador.Codigo = int.Parse(txtCod.Text);

                DialogResult resultado = MessageBox.Show("¿Está seguro que quiere borrar al entrenador?",
                    "Confirmar", MessageBoxButtons.YesNo);

                if(resultado == DialogResult.Yes)
                {
                    bllEntrenador.Baja(beEntrenador);
                    CargarDgv();
                }

                Limpiar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion AMB

        #region metodos varios
        private void CargarDgv()
        {
            dgv.DataSource = null;
            dgv.DataSource = bllEntrenador.Listar();

            if(dgv.DataSource != null)
            {
                dgv.Columns["Fecha_nacimiento"].HeaderText = "Fecha de nacimiento";
            }
        }

        private void CargarDatos()
        {
            if (!(mailPass1.Validar()))
                throw new Exception("El formato del mail o la contraseña son incorrectos. La contraseña debe tener " +
                    "al menos 8 caracteres.");

            if (!(Regex.IsMatch(txtDNI.Text, "^[0-9]{8}$")))
                throw new Exception("El formato del DNI es incorrecto.");

            if (!(Regex.IsMatch(txtTelefono.Text, "^([0-9]{10}|[0-9]{8})$")))
                throw new Exception("El formato del teléfono es incorrecto.");

            // para que sean solo numeros y no pueda poner solo 0 como legajo
            if (!(Regex.IsMatch(txtLegajo.Text, "^([1-9]|[1-9][0-9]+)$")))
                throw new Exception("El formato del legajo es incorrecto.");

            if(!(Regex.IsMatch(txtSueldo.Text, "^([0-9]+|[0-9]+\\.[0-9]+)$")))
                throw new Exception("El formato del sueldo es incorrecto.");

            beEntrenador.Legajo = int.Parse(txtLegajo.Text);
            beEntrenador.DNI = int.Parse(txtDNI.Text);
            beEntrenador.Nombre = txtNombre.Text;
            beEntrenador.Apellido = txtApe.Text;
            beEntrenador.Telefono = int.Parse(txtTelefono.Text);
            beEntrenador.Sueldo = decimal.Parse(txtSueldo.Text);
            beEntrenador.Mail = mailPass1.GetTextoMail();
            if (dateTimePicker1.Value.Year < DateTime.Today.Year)
                beEntrenador.Fecha_nacimiento = dateTimePicker1.Value;
            else
                throw new Exception("Por favor ingresar una fecha valida.");
            beEntrenador.CalcularEdad();
        }

        private void Limpiar()
        {
            txtCod.Text = "";
            txtLegajo.Text = "";
            txtDNI.Text = "";
            txtNombre.Text = "";
            txtApe.Text = "";
            txtSueldo.Text = "";
            txtTelefono.Text = "";
            txtEdad.Text = "";
            dateTimePicker1.Value = DateTime.Today;
            mailPass1.Limpiar();
        }

        private void Dgv_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                beEntrenador = (BEEntrenador)dgv.SelectedRows[0].DataBoundItem;

                txtCod.Text = beEntrenador.Codigo.ToString();
                txtDNI.Text = beEntrenador.DNI.ToString();
                txtNombre.Text = beEntrenador.Nombre;
                txtApe.Text = beEntrenador.Apellido;
                txtTelefono.Text = beEntrenador.Telefono.ToString();
                txtEdad.Text = beEntrenador.Edad.ToString();
                txtLegajo.Text = beEntrenador.Legajo.ToString();
                txtSueldo.Text = beEntrenador.Sueldo.ToString();
                dateTimePicker1.Value = beEntrenador.Fecha_nacimiento;
                mailPass1.SetTextoMail(beEntrenador.Mail);
                mailPass1.SetTextoPass(beEntrenador.Contrasenia);
            }
            catch (Exception ex) { }
        }
        #endregion metodos varios

        private void frmEntrenador_Load(object sender, EventArgs e)
        {

        }
    }
}
