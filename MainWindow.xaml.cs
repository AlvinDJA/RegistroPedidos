using RegistroPedidos.UI.Consultas;
using RegistroPedidos.UI.Registros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RegistroPedidos
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void rPedidosMenuItem_Click(object sender, RoutedEventArgs e)
        {
            new rOrdenes().ShowDialog();
        }

        private void cPedidosMenuItem_Click(object sender, RoutedEventArgs e)
        {
            new cOrdenes().ShowDialog();
        }

        private void cSuplidoresMenuItem_Click(object sender, RoutedEventArgs e)
        {
            new cSuplidores().ShowDialog();
        }

        private void cProductosMenuItem_Click(object sender, RoutedEventArgs e)
        {
            new cProductos().ShowDialog();
        }
    }
}
