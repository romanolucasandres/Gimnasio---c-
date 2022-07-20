using System;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }
         
        private void Menu_Load(object sender, EventArgs e)
        {
            try
            {
                // deshabilito el menuStrip hasta que se complete el login
                menuStrip1.Enabled = false;

                frmLogin login = new frmLogin();
                login.MdiParent = this;
                login.Show();

                // suscripcion al evento del login
                login.habilitarMenu += HabilitarMenuStrip;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // evento que se desencadenara cuando se haya completado el login
        private void HabilitarMenuStrip(object sender, EventArgs e)
        {
            menuStrip1.Enabled = true;
        }

        private void ClientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCliente clientes = new frmCliente();
            clientes.MdiParent = this;
            clientes.Show();
        }
        private void EntrenadoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEntrenador entrenadores = new frmEntrenador();
            entrenadores.MdiParent = this;
            entrenadores.Show();
        }

        private void ActividadesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmActividad actividades = new frmActividad();
            actividades.MdiParent = this;
            actividades.Show();
        }

        private void ClasesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmClase clases = new frmClase();
            clases.MdiParent = this;
            clases.Show();
        }

        private void RutinasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRutina rutinas = new frmRutina();
            rutinas.MdiParent = this;
            rutinas.Show();
        }

        private void RegistrarClienteAClaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmClienteClase cliente_clase = new frmClienteClase();
            cliente_clase.MdiParent = this;
            cliente_clase.Show();
        }

        private void AsignarRutinaAClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRutinaCliente rutina_cliente = new frmRutinaCliente();
            rutina_cliente.MdiParent = this;
            rutina_cliente.Show();
        }

        private void EntrenadoresYSueldosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInformeSueldos informeSueldos = new frmInformeSueldos();
            informeSueldos.MdiParent = this;
            informeSueldos.Show();
        }

        private void SalirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
