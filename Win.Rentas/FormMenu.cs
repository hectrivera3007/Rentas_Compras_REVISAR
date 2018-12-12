using BL.Rentas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Win.Rentas;

namespace Win.Rentas
{
    public partial class FormMenu : Form
    {
        
        public FormMenu()
        {
            InitializeComponent();
        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Login();
        }

        public void Autorizar(Usuario usuario)
        {
            productosToolStripMenuItem.Visible = usuario.PuedeVerProductos;
            clientesToolStripMenuItem.Visible = usuario.PuedeVerClientes;
            proveedoresToolStripMenuItem.Visible = usuario.PuedeVerProveedores;
            comprasToolStripMenuItem.Visible = usuario.PuedeVerCompras;
            facturaToolStripMenuItem.Visible = usuario.PuedeVerFacturas;
            reportesToolStripMenuItem.Visible = usuario.PuedeVerReportes;
        }

        private void Login()
        {
            var formLogin = new FormLogin();
            formLogin.MenuPrincipal = this;
            formLogin.ShowDialog();
        }

        private void productosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var formProductos = new FormProductos();
            formProductos.MdiParent = this;
            formProductos.Show();
        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var formClientes = new FormClientes();
            formClientes.MdiParent = this;
            formClientes.Show();
        }

        private void rentarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var formProveedores = new Proveedores();
            formProveedores.MdiParent = this;
            formProveedores.Show();
        }

        private void FormMenu_Load(object sender, EventArgs e)
        {
            Login();
            
        }

        private void comprasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var formCompras = new FormCompras();
            formCompras.MdiParent = this;
            formCompras.Show();
        }

        private void facturasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var formFacturas = new FormFactura();
            formFacturas.MdiParent = this;
            formFacturas.Show();
        }
        private void reporteDeProductosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var formReporteProductos = new ReportedeProductos();
            formReporteProductos.MdiParent = this;
            formReporteProductos.Show();
        }
    }
}
