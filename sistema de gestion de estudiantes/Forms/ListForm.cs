using System;
using System.Linq;
using System.Windows.Forms;

namespace sistema_de_gestion_de_estudiantes.Forms
{
    public class ListForm : Form
    {
        private DataGridView dgv;
        private BindingSource bs;
        private TextBox txtBuscar;
        private Button btnBuscar, btnRefrescar, btnEditar, btnEliminar;

        public ListForm()
        {
            InitializeComponent();
            CargarDatos();
        }

        private void InitializeComponent()
        {
            this.Text = "Listado de estudiantes";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Width = 900;
            this.Height = 650;

            this.Font = new System.Drawing.Font("Segoe UI", 9F);

            // Panel superior con búsqueda
            var topPanel = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 50, Padding = new Padding(10), FlowDirection = FlowDirection.LeftToRight, WrapContents = false };
            txtBuscar = new TextBox { Width = 420, Height = 26, Margin = new Padding(0, 6, 6, 6) };
            btnBuscar = new Button { Text = "Buscar", Width = 100, Height = 28, Margin = new Padding(0, 6, 6, 6) };
            btnRefrescar = new Button { Text = "Refrescar", Width = 100, Height = 28, Margin = new Padding(0, 6, 6, 6) };
            topPanel.Controls.AddRange(new Control[] { txtBuscar, btnBuscar, btnRefrescar });

            // DataGridView ocupa el resto
            dgv = new DataGridView { Dock = DockStyle.Fill, ReadOnly = true, SelectionMode = DataGridViewSelectionMode.FullRowSelect, AutoGenerateColumns = true, AllowUserToAddRows = false };
            bs = new BindingSource();
            dgv.DataSource = bs;

            // Panel inferior con acciones
            var bottomPanel = new FlowLayoutPanel { Dock = DockStyle.Bottom, Height = 50, Padding = new Padding(10), FlowDirection = FlowDirection.LeftToRight }; 
            btnEditar = new Button { Text = "Editar seleccionado", Width = 180, Height = 30, Margin = new Padding(6) };
            btnEliminar = new Button { Text = "Eliminar seleccionado", Width = 180, Height = 30, Margin = new Padding(6) };
            bottomPanel.Controls.AddRange(new Control[] { btnEditar, btnEliminar });

            // Eventos
            btnBuscar.Click += BtnBuscar_Click;
            btnRefrescar.Click += (s, e) => CargarDatos();
            btnEditar.Click += BtnEditar_Click;
            btnEliminar.Click += BtnEliminar_Click;

            // Añadir controles al formulario
            this.Controls.Add(dgv);
            this.Controls.Add(bottomPanel);
            this.Controls.Add(topPanel);
        }

        private void CargarDatos()
        {
            bs.DataSource = GestorEstudiantes.ObtenerTodos().Select(x => new
            {
                x.Id,
                x.Nombre,
                x.Edad,
                Sexo = x.Sexo.ToString(),
                x.Carrera,
                Estado = x.Estado.ToString(),
                FechaInscripcion = x.FechaInscripcion.ToShortDateString()
            }).ToList();
        }

        private void BtnBuscar_Click(object? sender, EventArgs e)
        {
            var texto = txtBuscar.Text.Trim();
            if (string.IsNullOrWhiteSpace(texto))
            {
                CargarDatos();
                return;
            }

            // Buscar por ID exacto o por nombre parcial
            try
            {
                var lista = GestorEstudiantes.BuscarPorNombre(texto).Select(x => new
                {
                    x.Id,
                    x.Nombre,
                    x.Edad,
                    Sexo = x.Sexo.ToString(),
                    x.Carrera,
                    Estado = x.Estado.ToString(),
                    FechaInscripcion = x.FechaInscripcion.ToShortDateString()
                }).ToList();

                // Si no hay resultados por nombre, intentar por ID exacto
                if (!lista.Any())
                {
                    try
                    {
                        var porId = GestorEstudiantes.BuscarPorId(texto);
                        lista = new[] { porId }.Select(x => new
                        {
                            x.Id,
                            x.Nombre,
                            x.Edad,
                            Sexo = x.Sexo.ToString(),
                            x.Carrera,
                            Estado = x.Estado.ToString(),
                            FechaInscripcion = x.FechaInscripcion.ToShortDateString()
                        }).ToList();
                    }
                    catch { }
                }

                bs.DataSource = lista;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnEditar_Click(object? sender, EventArgs e)
        {
            if (dgv.SelectedRows.Count == 0) { MessageBox.Show("Seleccione una fila.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            var id = dgv.SelectedRows[0].Cells[0].Value.ToString();
            try
            {
                var estudiante = GestorEstudiantes.BuscarPorId(id);
                using (var f = new RegisterForm(estudiante))
                {
                    f.ShowDialog();
                }
                CargarDatos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnEliminar_Click(object? sender, EventArgs e)
        {
            if (dgv.SelectedRows.Count == 0) { MessageBox.Show("Seleccione una fila.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            var id = dgv.SelectedRows[0].Cells[0].Value.ToString();
            var confirm = MessageBox.Show($"¿Desea eliminar el estudiante con ID {id}?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            try
            {
                GestorEstudiantes.EliminarPorId(id);
                MessageBox.Show("Estudiante eliminado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarDatos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
