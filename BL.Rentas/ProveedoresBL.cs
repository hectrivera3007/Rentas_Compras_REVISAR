using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Rentas
{
    public class ProveedoresBL
    {

        Contexto _contexto;
        public BindingList<Proveedores> ListaProveedores { get; set; }

        public ProveedoresBL()
        {
            _contexto = new Contexto();
            ListaProveedores = new BindingList<Proveedores>();
        }
        public BindingList<Proveedores> ObtenerProveedores()
        {
            _contexto.Proveedores.Load();
            ListaProveedores = _contexto.Proveedores.Local.ToBindingList();
            return ListaProveedores;
        }
        public void CancelarCambios()
        {
            foreach (var item in _contexto.ChangeTracker.Entries())
            {
                item.State = EntityState.Unchanged;
                item.Reload();
            }
        }
        public Resultado GuardarProveedor(Proveedores proveedor)
        {
            var resultado = Validar(proveedor);
            if (resultado.Exitoso == false)
            {
                return resultado;
            }
            _contexto.SaveChanges();
            resultado.Exitoso = true;
            return resultado;
        }
        public void AgregarProveedor()
        {
            var nuevoProveedor = new Proveedores();
            ListaProveedores.Add(nuevoProveedor);
        }
        public bool EliminarProveedor(int id)
        {
            foreach (var proveedor in ListaProveedores.ToList())
            {
                if (proveedor.Id == id)
                {
                    ListaProveedores.Remove(proveedor);
                    _contexto.SaveChanges();
                    return true;
                }
            }

            return false;
        }

        private Resultado Validar(Proveedores proveedor)
        {
            var resultado = new Resultado();
            resultado.Exitoso = true;

            if (proveedor.Nombre == " ")
            {
                resultado.Mensaje = "Ingrese un nombre";
                resultado.Exitoso = false;
            }
            if (proveedor.Id != 0 )
            {
                resultado.Mensaje = "El Proveedor ya fue Guardado";
                resultado.Exitoso = false;
                return resultado;
            }
            if (proveedor.Email == " ")
            {
                resultado.Mensaje = "Ingrese un Correo Electronico";
                resultado.Exitoso = false;
            }
            if (proveedor.Telefono <= 0)
            {
                resultado.Mensaje = "Ingrese un Numero de telefono";
                resultado.Exitoso = false;
            }
            if (proveedor.Direccion == " ")
            {
                resultado.Mensaje = "Ingrese una Direccion";
                resultado.Exitoso = false;
            }

            return resultado;
        }
    }
    public class Proveedores
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public long Telefono { get; set; }
        public string Direccion { get; set; }
        public int ServicioId { get; set; }
        public Servicio Servicio { get; set; }

    }
    public class Resul
    {
        public bool Exito { get; set; }
        public string Mensaje { get; set; }
    }
}
