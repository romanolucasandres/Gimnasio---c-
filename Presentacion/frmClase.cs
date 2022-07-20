// referencias a BE y BLL
using BE;
using BLL;
using System;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class frmClase : Form
    {
        // declaro los objetos y los instancio en el constructor
        BLLClase bllClase;
        BEClase beClase;
        // para cargar los cmbBox y realizar las agregaciones
        BLLActividad bllActividad;
        BLLEntrenador bllEntrenador;

        public frmClase()
        {
            InitializeComponent();

            bllClase = new BLLClase();
            beClase = new BEClase();
            bllActividad = new BLLActividad();
            bllEntrenador = new BLLEntrenador();
        }

        private void FrmClase_Load(object sender, EventArgs e)
        {
            dgv.MultiSelect = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

            // para que el usuario no pueda editar los cmbBox
            cmbAct.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAula.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEntrenador.DropDownStyle = ComboBoxStyle.DropDownList;

            // formato para poder seleccionar hora
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd/MM/yyyy HH:mm:ss";

            // traigo las actividades y entrenadores al cmbBox para poder asignarlas a la clase
            cmbAct.DataSource = bllActividad.Listar();
            cmbEntrenador.DataSource = bllEntrenador.Listar();
            // le asigno la propiedad que debe mostrar y el valor que devuelve al ser seleccionado
            cmbAct.DisplayMember = "Nombre";

            CargarDgv();
        }
        #region ABM
        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                beClase.Codigo = 0;
                CargarDatos();

                bllClase.Guardar(beClase);
                CargarDgv();
                Limpiar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error");
            }
        }

        private void BtnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                beClase.Codigo = int.Parse(txtCod.Text);
                CargarDatos();

                bllClase.Guardar(beClase);
                CargarDgv();
                Limpiar();
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
                beClase.Codigo = int.Parse(txtCod.Text);
                DialogResult resultado = MessageBox.Show("¿Está seguro que desea eliminar la clase?",
                    "Confirmar", MessageBoxButtons.YesNo);

                if(resultado == DialogResult.Yes)
                {
                    bllClase.Baja(beClase);
                    CargarDgv();
                }

                Limpiar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
        #endregion ABM
        #region metodos varios
        private void CargarDgv()
        {
            dgv.DataSource = null;
            dgv.DataSource = bllClase.Listar();

            if(dgv.DataSource != null)
            {
                dgv.Columns["Fecha_hora"].HeaderText = "Fecha y hora";
            }
        }

        // para no repetir codigo en alta y modificar
        private void CargarDatos()
        {
            beClase.Actividad = (BEActividad)cmbAct.SelectedItem;
            beClase.Entrenador = (BEEntrenador)cmbEntrenador.SelectedItem;
            beClase.Aula = int.Parse(cmbAula.Text);
            if (dateTimePicker1.Value > DateTime.Today)
            {
                beClase.Fecha_hora = dateTimePicker1.Value;
            }
            else
                throw new Exception("Seleccionar una fecha válida");
            beClase.Cupo = int.Parse(txtCupo.Text);
        }

        private void Limpiar()
        {
            txtCod.Text = "";
            txtCupo.Text = "";
            cmbAct.DataSource = bllActividad.Listar();
            cmbEntrenador.DataSource = bllEntrenador.Listar();
            cmbAula.SelectedIndex = 0;
            dateTimePicker1.Value = DateTime.Today;
        }

        private void Dgv_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                beClase = (BEClase)dgv.SelectedRows[0].DataBoundItem;

                txtCod.Text = beClase.Codigo.ToString();
                cmbAula.Text = beClase.Aula.ToString();
                txtCupo.Text = beClase.Cupo.ToString();
                cmbAct.SelectedItem = beClase.Actividad;
                cmbEntrenador.SelectedItem = beClase.Entrenador;
                dateTimePicker1.Value = beClase.Fecha_hora;
            }
            catch (Exception) { }
        }

        // verifico que en los txtbox solo entren numeros
        private void TxtCod_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!(char.IsNumber(e.KeyChar)) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void TxtCupo_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!(char.IsNumber(e.KeyChar)) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }
        #endregion metodos varios
    }
}
