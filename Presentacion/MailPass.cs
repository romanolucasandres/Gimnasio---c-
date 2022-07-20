using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Presentacion
{
    public partial class MailPass : UserControl
    {
        public MailPass()
        {
            InitializeComponent();
        }
        
        public string GetTextoMail()
        {
            return txtMail.Text;
        }

        public string GetTextoPass()
        {
            return txtPw.Text;
        }

        public void SetTextoMail(string texto)
        {
            txtMail.Text = texto;
        }

        public void SetTextoPass(string texto)
        {
            txtPw.Text = texto;
        }

        public bool Validar()
        {
            // debe tener el formato algo@algo.com
            bool user = Regex.IsMatch(txtMail.Text, @"\A[a-zA-Z0-9_.]+\@[a-zA-Z0-9_.]+(\.com)\Z");

            // debe tener entre mas de 8 caracteres
            bool pw = Regex.IsMatch(txtMail.Text, "^.{8,}$");

            if (user && pw)
                return true;
            else
                return false;
        }

        public void Limpiar()
        {
            txtPw.Text = "";
            txtMail.Text = "";
        }
    }
}
