using System.ComponentModel;
using System.Data.Entity;
using System.Linq;

namespace BL.Rentas
{
    public class ProductosBL
    {
        Contexto _contexto;

        public BindingList<Producto> ListaProductos { get; set; }

        public ProductosBL()
        {
            _contexto = new Contexto();
            ListaProductos = new BindingList<Producto>();
        }

        public BindingList<Producto> ObtenerProductos()
        {
            _contexto.Productos.Load();
            ListaProductos = _contexto.Productos.Local.ToBindingList();

            return ListaProductos;
        }

        public BindingList<Producto> ObtenerProductos(Producto producto)
        {
            _contexto.Productos.Load();
            ListaProductos = _contexto.Productos.Local.ToBindingList();

            return ListaProductos;
        }
        //Método Obtener Productos Activos
        public BindingList<Producto> ObtenerProductos(bool Activo)
        {
            _contexto.Productos.Where(producto=> producto.Activo==Activo).ToList();
            ListaProductos = _contexto.Productos.Local.ToBindingList();
            return ListaProductos;
        }

        //Método Buscar Productos
        public BindingList<Producto> ObtenerProductos(string Descripcion)
        {
            _contexto.Productos.Where(producto => producto.Descripcion.Contains(Descripcion) == true).ToList();
            ListaProductos = _contexto.Productos.Local.ToBindingList();
            return ListaProductos;
        }

        //Método Cancelar Cambios
        public void CancelarCambios()
        {
            foreach (var item in _contexto.ChangeTracker.Entries())
            {
                item.State = EntityState.Unchanged;
                item.Reload();
            }
        }

        //Método Actualizar Formulario
        public void RefrescarDatos(int productoId)
        {
            var producto = _contexto.Productos.Find(productoId);
            if (producto!=null)
            {
                _contexto.Entry(producto).Reload();
            }
        }

        //Método Guardar Producto
        public Resultado GuardarProducto(Producto producto)
        {
            var resultado = Validar(producto);
            if (resultado.Exitoso == false)
            {
                return resultado;
            }

            _contexto.SaveChanges();
            resultado.Exitoso = true;
            return resultado;
        }

        //Método Agregar Producto
        public void AgregarProducto()
        {
            var nuevoProducto = new Producto();
            _contexto.Productos.Add(nuevoProducto);
        }

        //Método Eliminar Producto
        public bool EliminarProducto(int id)
        {
            foreach (var producto in ListaProductos.ToList())
            {
                if (producto.Id == id)
                {
                    ListaProductos.Remove(producto);
                    _contexto.SaveChanges();
                    return true;
                }
            }

            return false;
        }

        //Haciendo Validaciones
        private Resultado Validar(Producto producto)
        {
            var resultado = new Resultado();
            resultado.Exitoso = true;

            if (producto==null)
            {
                resultado.Mensaje = "Agregue un Producto Válido";
                resultado.Exitoso = false;
                return resultado;
            }
            if (producto.Id != 0 && producto.Activo == true)
            {
                resultado.Mensaje = "El Producto ya fue Guardado";
                resultado.Exitoso = false;
                return resultado;
            }
            if (string.IsNullOrEmpty(producto.Descripcion) == true)
            {
                resultado.Mensaje = "Ingrese una descripción";
                resultado.Exitoso = false;
            }

            if (producto.Existencia < 0)
            {
                resultado.Mensaje = "La existencia debe ser mayor que cero";
                resultado.Exitoso = false;
            }

            if (producto.Precio < 0)
            {
                resultado.Mensaje = "El precio debe ser mayor que cero";
                resultado.Exitoso = false;
            }

            if (producto.CategoriaId == 0)
            {
                resultado.Mensaje = "Seleccione una categoria";
                resultado.Exitoso = false;
            }

            return resultado;
        }
    }

    public class Producto
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public double Precio { get; set; }
        public int Existencia { get; set; }
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
        public int TipoId { get; set; }
        public Tipo Tipo { get; set; }
        public byte[] Foto { get; set; }
        public bool Activo { get; set; }

        public Producto()
        {
            Activo = true;
        }
    }
}