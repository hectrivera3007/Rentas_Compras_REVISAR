using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;



namespace BL.Rentas
{
    public class Contexto: DbContext
    {
        public Contexto(): base(@"Server=DESKTOP-E30O3CK\SQLEXPRESS2014;Database=Rentas;Trusted_Connection=True;")
        {
            
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            Database.SetInitializer(new DatosdeInicio());
        }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Ciudad> Ciudades { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Proveedores> Proveedores { get; set; }
        public DbSet<Servicio> Servicios { get; set; }
        public DbSet<Compras> Compras { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Tipo> Tipos { get; set; }
        public DbSet<Factura> Facturas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}