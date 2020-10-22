using RegistroPedidos.BLL;
using RegistroPedidos.Entidades;
using System.Collections.Generic;
using System.Windows;

namespace RegistroPedidos.UI.Consultas
{
    /// <summary>
    /// Interaction logic for cOrdenes.xaml
    /// </summary>
    public partial class cOrdenes : Window
    {
        public cOrdenes()
        {
            InitializeComponent();
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            var listado = new List<Ordenes>();
            listado = OrdenesBLL.GetList(c => true);
            DatosDataGrid.ItemsSource = null;
            DatosDataGrid.ItemsSource = listado;
        }
    }
}
