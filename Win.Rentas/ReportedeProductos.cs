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

namespace Win.Rentas
{
    public partial class ReportedeProductos : Form
    {
        public ReportedeProductos()
        {
            InitializeComponent();

            var _productsBL = new ProductosBL();
            var bindingSource = new BindingSource();
            bindingSource.DataSource = _productsBL.ObtenerProductos();

            var reporte = new ReporteProductos();
            reporte.SetDataSource(bindingSource);

            crystalReportViewer1.ReportSource = reporte;
            crystalReportViewer1.RefreshReport();
        }
    }
}
