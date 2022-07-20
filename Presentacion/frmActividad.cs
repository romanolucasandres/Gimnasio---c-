// presentacion tiene referencias a BE y BLL
using BE;
using BLL;
using System;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class frmActividad : Form
    {
        // declaro los objetos y los instancio en el constructor
        BEActividad beActividad;
        BLLActividad bllActividad;

        public frmActividad()
        {
            InitializeComponent();

            beActividad = new BEActividad();
            bllActividad = new BLLActividad();
        }

        private void FrmActividad_Load(object sender, EventArgs e)
        {
            dgv.MultiSelect = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            CargarDgv();
        }

        #region ABM
        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                beActividad.Codigo = 0;
                GuardarDatos();
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
                beActividad.Codigo = int.Parse(txtCod.Text);
                GuardarDatos();
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
                beActividad.Codigo = int.Parse(txtCod.Text);

                DialogResult resultado = MessageBox.Show("¿Está seguro que quiere eliminar esta actividad?",
                    "Confirmar", MessageBoxButtons.YesNo);

                if(resultado == DialogResult.Yes)
                {
                    bllActividad.Baja(beActividad);
                    CargarDgv();
                }

                txtCod.Text = "";
                txtNombre.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
        #endregion ABM
        #region metodos varios
        // metodo para no repetir el mismo codigo en alta y modificacion
        private void GuardarDatos()
        {
            beActividad.Nombre = txtNombre.Text;
            bllActividad.Guardar(beActividad);
            txtCod.Text = "";
            txtNombre.Text = "";
            CargarDgv();
        }
        private void CargarDgv()
        {
            dgv.DataSource = null;
            dgv.DataSource = bllActividad.Listar();
        }

        private void Dgv_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                beActividad = (BEActividad)dgv.SelectedRows[0].DataBoundItem;

                txtCod.Text = beActividad.Codigo.ToString();
                txtNombre.Text = beActividad.Nombre;
            }
            catch (Exception) { }
        }
        #endregion metodos varios
    }
}
