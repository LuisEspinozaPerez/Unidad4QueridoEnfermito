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
using MiQueridoEnfermito.BIZ;
using MiQueridoEnfermito.COMMON.Entidades;
using MiQueridoEnfermito.COMMON.Interfaces;
using MiQueridoEnfermito.DAL;

namespace MiQueridoEnfermito.GUI.Administrador
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        enum accion
        {
            Nuevo,
            Editar
        }
        IManejadorEmpleado manejadorEmpleados;
        accion accionEmpleados;

        IManejadorCliente manejadorClientes;
        accion accionClientes;

        IManejadorProductos manejadorProductos;
        accion accionProductos;

        RepositoriodeVentas manejadorVentas;

        Venta venta;
        accion accionVenta;

        public MainWindow()
        {
            InitializeComponent();
            manejadorEmpleados = new ManejadorEmpleados(new RepositoriodeEmpleados());
            manejadorClientes = new ManejadorClientes(new RepositoriodeClientes());
            manejadorProductos = new ManejadorProductos(new RepositoriodeProductos());
            manejadorVentas= new RepositoriodeVentas();
            PonerBotonesEmpleadosEdicion(false);
            LimpiarCamposDeEmpleados();
            ActualizarTablaEmpleados();
            PonerBotonesClientesEdicion(false);
            LimpiarCamposDeClientes();
            ActualizarTablaClientes();
            PonerBotonesProductosEdicion(false);
            LimpiarCamposDeProductos();
            ActualizarTablaProductos();

        }

        

        //Metodos de Productos
        private void ActualizarTablaProductos()
        {
            dtgProductos.ItemsSource = null;
            dtgProductos.ItemsSource = manejadorProductos.Listar;
        }

        private void LimpiarCamposDeProductos()
        {
            txbProductosCategoria.Clear();
            txbProductosDescripcion.Clear();
            txbProductosId.Text = "";
            txbProductosNombreProduc.Clear();
            txbProductosPrecio.Clear();
            txbProductosPresentacion.Clear();
        }

        private void PonerBotonesProductosEdicion(bool value)
        {
            btnProductosCancelar.IsEnabled = value;
            btnProductosEditar.IsEnabled = !value;
            btnProductosEliminar.IsEnabled = !value;
            btnProductosGuardar.IsEnabled = value;
            btnProductosNuevo.IsEnabled = !value;
        }

        //Metodos de Clientes
        private void PonerBotonesClientesEdicion(bool value)
        {
            btnClientesCancelar.IsEnabled = value;
            btnClientesEditar.IsEnabled = !value;
            btnClientesEliminar.IsEnabled = !value;
            btnClientesGuardar.IsEnabled = value;
            btnClientesNuevo.IsEnabled = !value;
        }

        private void ActualizarTablaClientes()
        {
            dtgClientes.ItemsSource = null;
            dtgClientes.ItemsSource = manejadorClientes.Listar;
        }

        private void LimpiarCamposDeClientes()
        {
            txbClientesNombre.Clear();
            txbClientesCorreo.Clear();
            txbClientesDireccion.Clear();
            txbClientesRfc.Clear();
            txbClientesTelefono.Clear();
            txbClientesId.Text = "";
        }
        //Metodos de Empleados
        private void ActualizarTablaEmpleados()
        {
            dtgEmpleados.ItemsSource = null;
            dtgEmpleados.ItemsSource =manejadorEmpleados.Listar;
        }

        private void LimpiarCamposDeEmpleados()
        {
            txbEmpleadosNombre.Clear();
            txbEmpleadosId.Text = "";
        }

        private void PonerBotonesEmpleadosEdicion(bool value)
        {
            btnEmpleadosCancelar.IsEnabled = value;
            btnEmpleadosEditar.IsEnabled = !value;
            btnEmpleadosEliminar.IsEnabled = !value;
            btnEmpleadosGuardar.IsEnabled = value;
            btnEmpleadosNuevo.IsEnabled = !value;
        }
 
        //Botones de Empleados

        private void btnEmpleadosNuevo_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCamposDeEmpleados();
            PonerBotonesEmpleadosEdicion(true);
            accionEmpleados = accion.Nuevo;
        }

        private void btnEmpleadosEditar_Click(object sender, RoutedEventArgs e)
        {
            Empleado emp = dtgEmpleados.SelectedItem as Empleado;
            if (emp != null)
            {
                txbEmpleadosId.Text = emp.Id;
                txbEmpleadosNombre.Text = emp.NombreEmpleado;
                accionEmpleados = accion.Editar;
                PonerBotonesEmpleadosEdicion(true);
            }
        }

        private void btnEmpleadosGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (accionEmpleados== accion.Nuevo)
            {
                Empleado emp = new Empleado()
                {
                    NombreEmpleado = txbEmpleadosNombre.Text
                };
                if (manejadorEmpleados.Agregar(emp))
                {
                    MessageBox.Show("Empleado agregado Correctamente", "Mi Querido Enfermito", MessageBoxButton.OK, MessageBoxImage.Information);
                    LimpiarCamposDeEmpleados();
                    ActualizarTablaEmpleados();
                    PonerBotonesEmpleadosEdicion(false);
                }
                else
                {
                    MessageBox.Show("Error al agregar empleado", "Mi Querido Enfermito", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                Empleado emp = dtgEmpleados.SelectedItem as Empleado;
                emp.NombreEmpleado = txbEmpleadosNombre.Text;
                if (manejadorEmpleados.Modificar(emp))
                {
                    MessageBox.Show("Empleado modificado Correctamente", "Mi Querido Enfermito", MessageBoxButton.OK, MessageBoxImage.Information);
                    LimpiarCamposDeEmpleados();
                    ActualizarTablaEmpleados();
                    PonerBotonesEmpleadosEdicion(false);
                }
                else
                {
                    MessageBox.Show("Error al modificar empleado", "Mi Querido Enfermito", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnEmpleadosEliminar_Click(object sender, RoutedEventArgs e)
        {
            Empleado emp = dtgEmpleados.SelectedItem as Empleado;
            if (emp != null)
            {
                if(MessageBox.Show("Realmente deseas eliminar este empleado?", "Mi Querido Enfermito", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    if (manejadorEmpleados.Eliminar(emp.Id))
                    {
                        MessageBox.Show("Empleado Eliminado", "Mi Querido Enfermito", MessageBoxButton.OK, MessageBoxImage.Information);
                        ActualizarTablaEmpleados();
                    }
                    else
                    {
                        MessageBox.Show("Error al eliminar empleado", "Mi Querido Enfermito", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
        }

        private void btnEmpleadosCancelar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCamposDeEmpleados();
            PonerBotonesEmpleadosEdicion(false);
        }

        //Botones de Clientes
        private void btnClientesNuevo_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCamposDeClientes();
            PonerBotonesClientesEdicion(true);
            accionClientes = accion.Nuevo;
        }

        private void btnClientesEditar_Click(object sender, RoutedEventArgs e)
        {
            Cliente cliente = dtgClientes.SelectedItem as Cliente;
            if(cliente != null)
            {
                txbClientesId.Text = cliente.Id;
                txbClientesCorreo.Text = cliente.Correo;
                txbClientesDireccion.Text = cliente.Direccion;
                txbClientesNombre.Text = cliente.NombreCliente;
                txbClientesRfc.Text = cliente.RFC;
                txbClientesTelefono.Text = cliente.Telefono;
                accionClientes = accion.Editar;
                PonerBotonesClientesEdicion(true);
            }
        }

        private void btnClientesGuardar_Click(object sender, RoutedEventArgs e)
        {
            if(accionClientes== accion.Nuevo)
            {
                Cliente cliente = new Cliente()
                {
                    NombreCliente = txbClientesNombre.Text,
                    Direccion = txbClientesDireccion.Text,
                    RFC= txbClientesRfc.Text,
                    Telefono=txbClientesTelefono.Text,
                    Correo= txbClientesCorreo.Text
                };
                if (manejadorClientes.Agregar(cliente))
                {
                    MessageBox.Show("Cliente agregado Correctamente", "Mi Querido Enfermito", MessageBoxButton.OK, MessageBoxImage.Information);
                    LimpiarCamposDeClientes();
                    ActualizarTablaClientes();
                    PonerBotonesClientesEdicion(false);
                }
                else
                {
                    MessageBox.Show("Error al agregar cliente", "Mi Querido Enfermito", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                Cliente cliente = dtgClientes.SelectedItem as Cliente;
                cliente.Correo = txbClientesCorreo.Text;
                cliente.Direccion = txbClientesDireccion.Text;
                cliente.NombreCliente = txbClientesNombre.Text;
                cliente.RFC = txbClientesRfc.Text;
                cliente.Telefono = txbClientesTelefono.Text;
                if (manejadorClientes.Modificar(cliente))
                {
                    MessageBox.Show("Cliente modificado Correctamente", "Mi Querido Enfermito", MessageBoxButton.OK, MessageBoxImage.Information);
                    LimpiarCamposDeClientes();
                    ActualizarTablaClientes();
                    PonerBotonesClientesEdicion(false);
                }
                else
                {
                    MessageBox.Show("Error al modificar cliente", "Mi Querido Enfermito", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnClientesEliminar_Click(object sender, RoutedEventArgs e)
        {
            Cliente cliente = dtgClientes.SelectedItem as Cliente;
            if(cliente!= null)
            {
                if(MessageBox.Show("Realmente deseas eliminar este cliente?", "Mi Querido Enfermito", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    if (manejadorClientes.Eliminar(cliente.Id))
                    {
                        MessageBox.Show("Cliente Eliminado", "Mi Querido Enfermito", MessageBoxButton.OK, MessageBoxImage.Information);
                        ActualizarTablaClientes();
                    }
                    else
                    {
                        MessageBox.Show("Error al eliminar cliente", "Mi Querido Enfermito", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
        }

        private void btnClientesCancelar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCamposDeClientes();
            PonerBotonesClientesEdicion(false);
        }
        //Botones de Clientes
        private void btnProductosNuevo_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCamposDeProductos();
            PonerBotonesProductosEdicion(true);
            accionProductos = accion.Nuevo;
        }

        private void btnProductosEditar_Click(object sender, RoutedEventArgs e)
        {
            Producto producto = dtgProductos.SelectedItem as Producto;
            if(producto!= null)
            {
                txbProductosCategoria.Text = producto.Categoria;
                txbProductosDescripcion.Text = producto.Descripcion;
                txbProductosId.Text = producto.Id;
                txbProductosNombreProduc.Text = producto.NombreProducto;
                txbProductosPrecio.Text = producto.Precio.ToString();

            }
        }

        private void btnProductosGuardar_Click(object sender, RoutedEventArgs e)
        {
            if(accionProductos== accion.Nuevo)
            {
                Producto producto = new Producto()
                {
                    NombreProducto = txbProductosNombreProduc.Text,
                    Descripcion = txbProductosDescripcion.Text,
                    Precio = double.Parse(txbProductosPrecio.Text),
                    Presentacion = txbProductosPresentacion.Text,
                    Categoria = txbProductosCategoria.Text
                };
                if (manejadorProductos.Agregar(producto))
                {
                    MessageBox.Show("Producto agregado Correctamente", "Mi Querido Enfermito", MessageBoxButton.OK, MessageBoxImage.Information);
                    LimpiarCamposDeProductos();
                    ActualizarTablaProductos();
                    PonerBotonesProductosEdicion(false);
                }
                else
                {
                    MessageBox.Show("Error al agregar producto", "Mi Querido Enfermito", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                Producto producto = dtgProductos.SelectedItem as Producto;
                producto.Categoria = txbProductosCategoria.Text;
                producto.Descripcion = txbProductosDescripcion.Text;
                producto.NombreProducto = txbProductosNombreProduc.Text;
                producto.Precio = double.Parse(txbProductosPrecio.Text);
                producto.Presentacion = txbProductosPresentacion.Text;
                if (manejadorProductos.Modificar(producto))
                {
                    MessageBox.Show("Producto modificado Correctamente", "Mi Querido Enfermito", MessageBoxButton.OK, MessageBoxImage.Information);
                    LimpiarCamposDeProductos();
                    ActualizarTablaProductos();
                    PonerBotonesProductosEdicion(false);
                }
                else
                {
                        MessageBox.Show("Error al modificar producto", "Mi Querido Enfermito", MessageBoxButton.OK, MessageBoxImage.Error);
                }
               
            }
        }

        private void btnProductosEliminar_Click(object sender, RoutedEventArgs e)
        {
             Producto producto = dtgProductos.SelectedItem as Producto;
            if(producto!= null)
            {
                if(MessageBox.Show("Realmente deseas eliminar este producto?", "Mi Querido Enfermito", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    if (manejadorProductos.Eliminar(producto.Id))
                    {
                        MessageBox.Show("Producto Eliminado", "Mi Querido Enfermito", MessageBoxButton.OK, MessageBoxImage.Information);
                        ActualizarTablaProductos();
                    }
                    else
                    {
                        MessageBox.Show("Error al eliminar producto", "Mi Querido Enfermito", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
        }

        private void btnProductosCancelar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCamposDeProductos();
            PonerBotonesProductosEdicion(false);
        }

        private void btnVentasGuardar_Click(object sender, RoutedEventArgs e)
        {
            RepositoriodeVentas archivo = new RepositoriodeVentas();
            if (manejadorVentas.GenerarTicket(cmbCajero.Text, cmbComprador.Text, int.Parse(txbProductosPrecio.Text), cmbProductosVendidos.Text, int.Parse(txbTotalPagar.Text), cmbComprador.Text + ".poo"))
            {
                MessageBox.Show("Venta exitosa", "Ticket Generado", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("No se pudo realizar la venta", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void btnVentasNuevo_Click(object sender, RoutedEventArgs e)
        {
        
        
            venta = new Venta();
            venta.ProductosVendidos = new List<Producto>();
            
        }

        private void btnVentasCancelar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAgregarMaterial_Click(object sender, RoutedEventArgs e)
        {
            Producto m = cmbProductosVendidos.SelectedItem as Producto;
            if (m != null)
            {
                venta.ProductosVendidos.Add(m);
                
            }
        }

        private void btnEliminarMaterial_Click(object sender, RoutedEventArgs e)
        {
            Producto m = dtgProductosEnVenta.SelectedItem as Producto;
            if (m != null)
            {
                venta.ProductosVendidos.Remove(m);
                
            }
        }

        private void btnTotalPagar_Click(object sender, RoutedEventArgs e)
        {
            Venta venta = new Venta();
            double costoProducto;
            double total;
            costoProducto = double.Parse(txbProductosPrecio.Text);
            total = costoProducto + costoProducto * 0.16;
        }
    }
}
