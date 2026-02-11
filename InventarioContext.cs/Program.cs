using System;
using System.Windows.Forms;

namespace InventarioProductos
{
	internal static class Program
	{
		[STAThread]
		static void Main()
		{
			Console.WriteLine("hola mundo desde Program.cs");

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Form1());
		}
	}
}

