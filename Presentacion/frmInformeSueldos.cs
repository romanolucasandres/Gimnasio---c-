using BE;
using BLL;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Presentacion
{
    public partial class frmInformeSueldos : Form
    {
        BEEntrenador beEntrenador;
        BLLEntrenador bllEntrenador;

        public frmInformeSueldos()
        {
            InitializeComponent();

            beEntrenador = new BEEntrenador();
            bllEntrenador = new BLLEntrenador();

            try
            {
                // busco la lista y la paso al diccionario
                List<BEEntrenador> lista = bllEntrenador.Listar();

                if (lista != null)
                {
                    Dictionary<string, decimal> diccionario = new Dictionary<string, decimal>();

                    foreach (BEEntrenador entrenador in lista)
                    {
                        diccionario.Add(entrenador.ToString(), entrenador.Sueldo);
                    }

                    chart1.Titles.Clear();
                    chart1.Series.Clear();

                    chart1.Titles.Add("Entrenadores de mayor sueldo");

                    Series serie = new Series("Sueldo");
                    serie.ChartType = SeriesChartType.Column;
                    serie.Points.DataBindXY(diccionario.Keys, diccionario.Values);
                    chart1.Series.Add(serie);
                }
                else
                    throw new Exception("No se encontraron empleados en el sistema.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frmInformeSueldos_Load(object sender, EventArgs e)
        {

        }
    }
}
