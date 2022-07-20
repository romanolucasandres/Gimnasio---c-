using BLL;
using System;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class frmLogin : Form
    {
        // evento para que el form menu se entere que ya puede habilitar el menuStrip
        public event EventHandler habilitarMenu;

        BLLEntrenador bllEntrenador;

        public frmLogin()
        {
            InitializeComponent();

            bllEntrenador = new BLLEntrenador();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // valido si la contraseña ingresada esta bien o no
                if (bllEntrenador.PassCorrecta(mailPass1.GetTextoMail(), mailPass1.GetTextoPass()))
                {
                    habilitarMenu?.Invoke(this, null);
                    this.Close();
                }
                else
                    throw new Exception("Contraseña incorrecta.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmEntrenador entrenador = new frmEntrenador();
            entrenador.MdiParent = this.MdiParent;
            entrenador.Show();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
