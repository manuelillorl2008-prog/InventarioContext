using System.Collections.Generic;
using System.Data.Entity;
using System.Runtime.Remoting.Contexts;
using InventarioProductos.Models;

namespace InventarioProductos
{
	public class InventarioContext : DbContext
	{
		public DbSet<Productos> Productos { get; set; }
	}

}
