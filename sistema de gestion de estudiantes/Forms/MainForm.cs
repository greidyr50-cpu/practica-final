using System;
using System.Windows.Forms;

namespace sistema_de_gestion_de_estudiantes.Forms
{
    public class MainForm : Form
    {
        private MenuStrip menuStrip1;

        public MainForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.menuStrip1 = new MenuStrip();
            var menuRegistro = new ToolStripMenuItem("Registrar");
            var menuListar = new ToolStripMenuItem("Listar");
            var menuSalir = new ToolStripMenuItem("Salir");

            menuRegistro.Click += MenuRegistro_Click;
            menuListar.Click += MenuListar_Click;
            menuSalir.Click += MenuSalir_Click;

            this.menuStrip1.Items.AddRange(new ToolStripItem[] { menuRegistro, menuListar, menuSalir });

            this.MainMenuStrip = this.menuStrip1;
            this.Controls.Add(this.menuStrip1);
            this.Text = "Sistema de gestión de estudiantes";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Width = 800;
            this.Height = 600;
        }

        private void MenuRegistro_Click(object? sender, EventArgs e)
        {
            using (var f = new RegisterForm())
            {
                f.ShowDialog();
            }
        }

        private void MenuListar_Click(object? sender, EventArgs e)
        {
            using (var f = new ListForm())
            {
                f.ShowDialog();
            }
        }

        private void MenuSalir_Click(object? sender, EventArgs e)
        {
            this.Close();
        }
    }
}
