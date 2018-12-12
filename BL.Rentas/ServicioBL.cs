using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Rentas
{
    public class ServicioBL
    {
        Contexto _contexto;

        public BindingList<Servicio> ListaServicios { get; set; }

        public ServicioBL()
        {
            _contexto = new Contexto();
            ListaServicios = new BindingList<Servicio>();
        }
        public BindingList<Servicio> ObtenerServicio()
        {
            _contexto.Servicios.Load();
            ListaServicios = _contexto.Servicios.Local.ToBindingList();

            return ListaServicios;
        }
    }
    public class Servicio
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
    }
}
