using BE;
using BLL;
using System;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class frmRutina : Form
    {
        BLLRutina bllRutina;
        BERutina beRutina;

        public frmRutina()
        {
            InitializeComponent();

            bllRutina = new BLLRutina();
            beRutina = new BERutina();
        }

        private void FrmRutina_Load(object sender, EventArgs e)
        {
            dgv.MultiSelect = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.DataSource = null;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

            dgvBusqueda.MultiSelect = false;
            dgvBusqueda.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBusqueda.DataSource = null;
            dgvBusqueda.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

            dgv.DataSource = bllRutina.Listar();
        }

        #region abm
        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                // verifico el codigo
                if (bllRutina.Listar().Exists(x => x.Codigo == Convert.ToInt32(txtCod.Text)))
                    throw new Exception("Ya existe una rutina con ese código.");

                if (Convert.ToInt32(txtCod.Text) <= 0)
                    throw new Exception("El código debe ser mayor a 0.");

                beRutina.Codigo = Convert.ToInt32(txtCod.Text);
                beRutina.Ejercicio = txtEjercicio.Text;
                beRutina.Series = Convert.ToInt32(txtSeries.Text);
                beRutina.Descanso = Convert.ToInt32(txtDescanso.Text);

                bllRutina.Guardar(beRutina);
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
                beRutina.Ejercicio = txtEjercicio.Text;
                beRutina.Series = Convert.ToInt32(txtSeries.Text);
                beRutina.Descanso = Convert.ToInt32(txtDescanso.Text);

                bllRutina.Guardar(beRutina);
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
                DialogResult resultado = MessageBox.Show("¿Desea borrar esta rutina?", "Alerta", MessageBoxButtons.YesNo);

                if(resultado == DialogResult.Yes)
                {
                    bllRutina.Baja(beRutina);
                    CargarDgv();
                    Limpiar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    #endregion abm
        #region metodos varios
        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                dgvBusqueda.DataSource = null;
                dgvBusqueda.DataSource = bllRutina.Buscar(txtBuscar.Text);
                Limpiar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CargarDgv()
        {
            dgv.DataSource = null;
            dgv.DataSource = bllRutina.Listar();
        }

        private void Limpiar()
        {
            txtCod.Text = "";
            txtEjercicio.Text = "";
            txtSeries.Text = "";
            txtDescanso.Text = "";
            txtBuscar.Text = "";
        }

        private void Dgv_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                beRutina = (BERutina)dgv.SelectedRows[0].DataBoundItem;

                txtCod.Text = beRutina.Codigo.ToString();
                txtEjercicio.Text = beRutina.Ejercicio;
                txtSeries.Text = beRutina.Series.ToString();
                txtDescanso.Text = beRutina.Descanso.ToString();
                txtBuscar.Text = beRutina.Ejercicio;
            }
            catch (Exception) { }
        }

        private void TxtCod_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void TxtSeries_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void TxtDescanso_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }
        #endregion metodos varios
    }
}
