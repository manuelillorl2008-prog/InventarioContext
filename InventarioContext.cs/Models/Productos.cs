namespace InventarioProductos.Models
{
	public class Productos
	{
		public int Id { get; set; }
		public string Producto { get; set; }
		public decimal Precio { get; set; }
		public int Stock { get; set; }
	}
}
