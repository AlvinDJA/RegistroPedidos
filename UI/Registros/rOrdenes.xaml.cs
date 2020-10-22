using RegistroPedidos.BLL;
using RegistroPedidos.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RegistroPedidos.UI.Registros
{
    /// <summary>
    /// Interaction logic for rOrdenes.xaml
    /// </summary>
    /// <summary>
    /// Interaction logic for rMoras.xaml
    /// </summary>
    public partial class rOrdenes : Window
    {
        private Ordenes Orden = new Ordenes();

        public rOrdenes()
        {
            InitializeComponent();
            IniciarComboboxes();
        }
        private void IniciarComboboxes()
        {
            SuplidorComboBox.ItemsSource = SuplidoresBLL.GetList();
            SuplidorComboBox.SelectedValuePath = "SuplidorId";
            SuplidorComboBox.DisplayMemberPath = "Nombres";

            ProductoComboBox.ItemsSource = ProductosBLL.GetList();
            ProductoComboBox.SelectedValuePath = "ProductoId";
            ProductoComboBox.DisplayMemberPath = "Descripcion";
            Limpiar();
            MontoTextBox.Text = "0";
        }
        private void Cargar()
        {
            SuplidorComboBox.SelectedIndex = Orden.SuplidorId;
            this.DataContext = null;
            this.DataContext = Orden;
        }
        private void Limpiar()
        {
            this.Orden = new Ordenes();
            this.Orden.Fecha = DateTime.Now;
            this.DataContext = Orden;
        }
        private bool ValidarAgregar()
        {
            var prod  = ProductosBLL.Buscar(Convert.ToInt32(ProductoComboBox.SelectedValue));
            bool esValido = true;
            if (ProductoComboBox.Text.Length == 0)
            {
                esValido = false;
                MessageBox.Show("Seleccione un producto", "Advertencia",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (CantidadTextBox.Text.Length == 0)
            {
                esValido = false;
                MessageBox.Show("Inserte la cantidad", "Advertencia",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (CostoTextBox.Text.Length == 0)
            {
                esValido = false;
                MessageBox.Show("Inserte el costo", "Advertencia",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            return esValido;
        }
        private bool ValidarGuardar()
        {
            bool esValido = true;
            if (OrdenesDataGrid.Items.Count == 0)
            {
                esValido = false;
                MessageBox.Show("Debe agregar ordenes", "Advertencia",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            return esValido;
        }
        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            Ordenes encontrado = OrdenesBLL.Buscar(Orden.OrdenId);

            if (encontrado != null)
            {
                Orden = encontrado;
                Cargar();
            }
            else
            {
                Limpiar();
                MessageBox.Show("No existe en la base de datos", "Mensaje",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            Ordenes existe = OrdenesBLL.Buscar(Orden.OrdenId);

            if (existe == null)
            {
                MessageBox.Show("No existe la mora en la base de datos", "Mensaje",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            else
            {
                OrdenesBLL.Eliminar(Orden.OrdenId);
                MessageBox.Show("Eliminado", "Exito",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                Limpiar();
            }
        }
        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidarGuardar())
                return;
            bool paso = false;

            if (Orden.OrdenId == 0)
                paso = OrdenesBLL.Guardar(Orden);
            else
            {
                if (ExisteEnLaBaseDeDatos())
                {
                    paso = OrdenesBLL.Guardar(Orden);
                }
                    
                else
                    MessageBox.Show("No existe en la base de datos", "Información");
            }
            if (paso)
            {
                Limpiar();
                MessageBox.Show("Guardado!", "Exito",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("Fallo al guardar", "Fallo",
                    MessageBoxButton.OK, MessageBoxImage.Error);
        }
        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }
        private bool ExisteEnLaBaseDeDatos()
        {
            Ordenes esValido = OrdenesBLL.Buscar(Orden.OrdenId);

            return (esValido != null);
        }
        private void Remover_Click(object sender, RoutedEventArgs e)
        {
            if (OrdenesDataGrid.Items.Count >= 1 && OrdenesDataGrid.SelectedIndex <= OrdenesDataGrid.Items.Count - 1)
            {
                OrdenesDetalle m = (OrdenesDetalle)OrdenesDataGrid.SelectedValue;
                Orden.Monto -= m.Costo*m.Cantidad;
                Orden.Detalle.RemoveAt(OrdenesDataGrid.SelectedIndex);
                Cargar();
            }
        }
        private void Agregar_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidarAgregar())
                return;
            Orden.Monto += Convert.ToSingle(CostoTextBox.Text)*Convert.ToSingle(CantidadTextBox.Text);
            Orden.Detalle.Add(new OrdenesDetalle(Orden.OrdenId,
                Convert.ToInt32(ProductoComboBox.SelectedValue),
                Convert.ToSingle(CantidadTextBox.Text), 
                Convert.ToSingle(CostoTextBox.Text)));
            Cargar();
            CostoTextBox.Clear();
            CantidadTextBox.Clear();
        }

        private void ProductoComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var busc = ProductosBLL.Buscar(Convert.ToInt32(ProductoComboBox.SelectedValue));
            if(busc != null)
                CostoTextBox.Text = busc.Costo.ToString();
        }
    }
}
