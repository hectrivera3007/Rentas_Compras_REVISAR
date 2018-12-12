using BL.Rentas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Rentas
{
    public class ComprasBL
    {
        Contexto _contexto;
        public BindingList<Compras> ListaCompras { get; set; }

        public ComprasBL()
        {
            _contexto = new Contexto();
        }
        public BindingList<Compras> ObtenerCompras()
        {
            _contexto.Compras.Include("ComprasDetalle").Load();
            ListaCompras = _contexto.Compras.Local.ToBindingList();
            return ListaCompras;
        }

        public void AgregarCompra()
        {
            var nuevaCompra = new Compras();
            _contexto.Compras.Add(nuevaCompra);
        }

        public void AgregarCompraDetalle(Compras compra)
        {
            if(compra != null)
            {
                var nuevoDetalle = new ComprasDetalle();
                compra.ComprasDetalle.Add(nuevoDetalle);
            }
        }

        public void RemoverCompraDetalle(Compras compra, ComprasDetalle compraDetalle)
        {
            if(compra!=null && compraDetalle!=null)
            {
                compra.ComprasDetalle.Remove(compraDetalle);
            }
        }

        public void CancelarCambios()
        {
            foreach (var item in _contexto.ChangeTracker.Entries())
            {
                item.State = EntityState.Unchanged;
                item.Reload();
            }
        }
        public Resultado GuardarCompra(Compras compra)
        {
            var resultado = Validar(compra);
            if (resultado.Exitoso==false)
            {
                return resultado;
            }
            CalcularExistencia(compra);

            _contexto.SaveChanges();
            resultado.Exitoso = true;
            return resultado;
        }

        private void CalcularExistencia(Compras compra)
        {
            foreach (var detalle in compra.ComprasDetalle)
            {
                var producto = _contexto.Productos.Find(detalle.ProductoId);
                if (producto != null)
                {
                    if (compra.Activo==true)
                    {
                        producto.Existencia = producto.Existencia + detalle.Cantidad;
                    }
                    else
                    {
                        producto.Existencia = producto.Existencia - detalle.Cantidad;
                    }
                   
                }
            }
        }

        private Resultado Validar(Compras compra)
        {
            var resultado = new Resultado();
            resultado.Exitoso = true;

            if (compra==null)
            {
                resultado.Mensaje = "Agregue una Compra para poderla Guardar ";
                resultado.Exitoso = false;
                return resultado;

            }
            if (compra.Activo == false)
            {
                resultado.Mensaje = "La Compra ha sido anulada, no se puede guardar";
                resultado.Exitoso = false;
                return resultado;
            }
            if (compra.Id != 0 && compra.Activo == true)
            {
                resultado.Mensaje = "La Compra ya fue realizada y no se pueden realizar cambios en ella";
                resultado.Exitoso = false;
                return resultado;
            }
           
            if (compra.ComprasDetalle.Count==0)
            {
                resultado.Mensaje = "Agregue Productos a la Compra";
                resultado.Exitoso = false;
                return resultado;
            }

            foreach (var detalle in compra.ComprasDetalle)
            {
                if (detalle.ProductoId==0)
                {
                    resultado.Mensaje = "Seleccione Productos Válidos";
                    resultado.Exitoso = false;
                }
            }
            return resultado;
        }

        public void CalcularCompra(Compras compra)
        {
            if (compra!=null)
            {
                double subtotal = 0;
                foreach (var detalle in compra.ComprasDetalle)
                {
                    var producto = _contexto.Productos.Find(detalle.ProductoId);

                    if (producto!=null)
                    {
                        detalle.Precio = producto.Precio;
                        detalle.Total = detalle.Cantidad * producto.Precio;
                        subtotal += detalle.Total;
                    }
                }
                compra.Subtotal = subtotal;
                compra.Impuesto = subtotal * 0.15;
                compra.Total = subtotal + compra.Impuesto;
            }
        }
        public bool AnularCompra(int id)
        {
            foreach (var compra in ListaCompras)
            {
                if (compra.Id==id)
                {
                    compra.Activo = false;
                    CalcularExistencia(compra);
                    _contexto.SaveChanges();
                    return true;
                }
            }
            return false;
        }
    }

    public class Compras
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int NumCompra{ get; set; }
        public int ProveedorId { get; set; }
        public Proveedores Proveedor { get; set; }
        public BindingList<ComprasDetalle> ComprasDetalle { get; set; }
        public Double Subtotal { get; set; }
        public double Impuesto { get; set; }
        public double Total { get; set; }
        public bool  Activo { get; set; }

        public Compras()
        {
            Fecha = DateTime.Now;
            ComprasDetalle = new BindingList<ComprasDetalle>();
            Activo = true;
        }
    }

    public class ComprasDetalle
    {
        public int Id { get; set; }
        public int ProductoId { get; set; }
        public Producto Producto { get; set; }
        public int Cantidad { get; set; }
        public double Precio { get; set; }
        public double Total { get; set; }

        public ComprasDetalle()
        {
            Cantidad = 1;
        }
    }
}
