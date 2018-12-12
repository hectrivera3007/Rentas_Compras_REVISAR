using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Rentas
{
   public  class ClientesBL
    {

        Contexto _contexto;
        public BindingList<Cliente> ListaClientes { get; set; }

        public ClientesBL()
        {
            _contexto = new Contexto();
            ListaClientes = new BindingList<Cliente>();
        }
        public BindingList<Cliente> ObtenerClientes()
        {
            _contexto.Clientes.Load();
            ListaClientes = _contexto.Clientes.Local.ToBindingList();
           return ListaClientes;
        }
        public void CancelarCambios()
        {
            foreach (var item in _contexto.ChangeTracker.Entries())
            {
                item.State = EntityState.Unchanged;
                item.Reload();
            }
        }
        public Resultado GuardarCliente(Cliente cliente)
        {
            var resultado = Validar(cliente);
            if(resultado.Exitoso == false)
            {
                return resultado;
            }
            _contexto.SaveChanges();
            resultado.Exitoso = true;
            return resultado;
        }
        public void AgregarCliente()
        {
            var nuevoCliente = new Cliente();
            ListaClientes.Add(nuevoCliente);
        }
        public bool EliminarCliente(int id)
        {
            foreach (var cliente in ListaClientes.ToList())
            {
                if (cliente.Id == id)
                {
                    ListaClientes.Remove(cliente);
                    _contexto.SaveChanges();
                    return true;
                }
            }
            
            return false;
        }

        private Resultado Validar(Cliente cliente)
        {
            var resultado = new Resultado();
            resultado.Exitoso = true;

            if (cliente == null)
            {
                resultado.Mensaje = "Agregue un Cliente Válido";
                resultado.Exitoso = false;
                return resultado;
            }
            if (cliente.Id != 0 && cliente.Activo == true)
            {
                resultado.Mensaje = "Cliente ya fue Guardado";
                resultado.Exitoso = false;
                return resultado;
            }
            if (cliente.Nombre == " ")
            {
                resultado.Mensaje = "Ingrese un nombre";
                resultado.Exitoso = false;
            }
            if (cliente.Email == " ")
            {
                resultado.Mensaje = "Ingrese un Correo Electronico";
                resultado.Exitoso = false;
            }
            if (cliente.Telefono <= 0)
            {
                resultado.Mensaje = "Ingrese un Numero de telefono";
                resultado.Exitoso = false;
            }
            if (cliente.Direccion == " ")
            {
                resultado.Mensaje = "Ingrese una Direccion";
                resultado.Exitoso = false;
            }

            return resultado;
        }
    }
    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public long Telefono { get; set; }
        public string Direccion { get; set; }
        public int CiudadId { get; set; }
        public Ciudad Ciudad { get; set; }
        public byte[] Foto { get; set; }
        public bool Activo { get; set; }

    }
    //public class Resul
    //{
    //    public bool Exito { get; set; }
    //    public string Mensaj { get; set; }
    //}
}
