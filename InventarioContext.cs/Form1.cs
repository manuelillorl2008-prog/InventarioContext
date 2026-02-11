
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InventarioProductos.Models;

namespace InventarioProductos

{
	public partial class Form1 : Form
    {
		private int id;
		public Form1()
        {
            InitializeComponent();

			btnGuardar.Click += btnGuardar_Click;
		}

        private void Form1_Load(object sender, EventArgs e)
        {

        }

		private bool ejecutandoGuardar = false;
		private void btnGuardar_Click(object sender, EventArgs e)
        {
			if (ejecutandoGuardar) return; 
			ejecutandoGuardar = true;

			try
			{
				using (var db = new InventarioContext())
				{
					var producto = new Productos
					{
						Producto = txtProducto.Text,
						Precio = Convert.ToDecimal(txtPrecio.Text),
						Stock = Convert.ToInt32(txtStock.Text)
					};

					db.Productos.Add(producto);
					db.SaveChanges();
					CargarProductos();

				}

				MessageBox.Show("Producto guardado");

				using (var db = new InventarioContext())
				{
					dgvProductos.DataSource = db.Productos.ToList();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			txtProducto.Clear();
			txtPrecio.Clear();
			txtStock.Clear();

			ejecutandoGuardar = false;
		}

        private void btnActualizar_Click(object sender, EventArgs e)
        {
			if (dgvProductos.CurrentRow == null)
			{
				MessageBox.Show("Selecciona un producto");
				return;
			}

			dgvProductos.CurrentRow.Cells["Producto"].Value = txtProducto.Text;
			dgvProductos.CurrentRow.Cells["Precio"].Value = txtPrecio.Text;
			dgvProductos.CurrentRow.Cells["Stock"].Value = txtStock.Text;

			txtProducto.Clear();
			txtPrecio.Clear();
			txtStock.Clear();

			MessageBox.Show("Producto actualizado");
		}
		

        private void btnBorrar_Click(object sender, EventArgs e)
        {
			if (dgvProductos.CurrentRow == null)
			{
				MessageBox.Show("Selecciona un producto");
				return;
			}

			int id = (int)dgvProductos.CurrentRow.Cells["id"].Value;

			using (var db = new InventarioContext())
			{
				var producto = db.Productos.Find(id);

				if (producto != null)
				{
					db.Productos.Remove(producto);
					db.SaveChanges();
					CargarProductos();

				}
			}

			CargarProductos();
		}

	
	private void CargarProductos()
		{
			using (var db = new InventarioContext())
			{
				dgvProductos.DataSource = db.Productos.ToList();
			}
		}

        private void dgvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
			if (e.RowIndex >= 0)
			{
				id = (int)dgvProductos.Rows[e.RowIndex].Cells["id"].Value;
				txtProducto.Text = dgvProductos.Rows[e.RowIndex].Cells["Producto"].Value.ToString();
				txtPrecio.Text = dgvProductos.Rows[e.RowIndex].Cells["Precio"].Value.ToString();
				txtStock.Text = dgvProductos.Rows[e.RowIndex].Cells["Stock"].Value.ToString();
			}
		}

        private void btnLeer_Click(object sender, EventArgs e)
        {
			using (var db = new InventarioContext())
			{
				var productos = db.Productos.ToList();

				if (productos.Count == 0)
				{
					MessageBox.Show("No hay productos en la base de datos.");
					return;
				}

				string mensaje = "Lista de productos:\n";
				foreach (var p in productos)
				{
					mensaje += $"Id: {p.Id}, Nombre: {p.Producto}, Precio: {p.Precio}, Stock: {p.Stock}\n";
				}

				MessageBox.Show(mensaje, "Productos");
			}
		}
	}
    }




