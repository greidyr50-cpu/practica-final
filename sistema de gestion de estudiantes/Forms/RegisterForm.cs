using System;
using System.Windows.Forms;
using sistema_de_gestion_de_estudiantes.Exceptions;

namespace sistema_de_gestion_de_estudiantes.Forms
{
    public class RegisterForm : Form
    {
        private TextBox txtId, txtNombre, txtEdad, txtCarrera;
        private ComboBox cbSexo, cbEstado;
        private DateTimePicker dtpFecha;
        private Button btnGuardar, btnCancelar;
        private Estudiante editing;

        public RegisterForm(Estudiante estudiante = null)
        {
            editing = estudiante;
            InitializeComponent();
            LoadEnums();
            if (editing != null) LoadForEdit();
        }

        private void InitializeComponent()
        {
            this.Text = editing == null ? "Registrar estudiante" : "Editar estudiante";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Width = 520;
            this.Height = 480;
            this.Font = new System.Drawing.Font("Segoe UI", 9F);

            // TableLayoutPanel para alinear etiquetas y controles
            var tlp = new TableLayoutPanel();
            tlp.ColumnCount = 2;
            tlp.RowCount = 7;
            tlp.Dock = DockStyle.Fill;
            tlp.Padding = new Padding(10);
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
            for (int i = 0; i < tlp.RowCount; i++) tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));

            var lblId = new Label { Text = "ID:", Anchor = AnchorStyles.Left | AnchorStyles.Top, AutoSize = true };
            txtId = new TextBox { Dock = DockStyle.Fill };

            var lblNombre = new Label { Text = "Nombre:", Anchor = AnchorStyles.Left | AnchorStyles.Top, AutoSize = true };
            txtNombre = new TextBox { Dock = DockStyle.Fill };

            var lblEdad = new Label { Text = "Edad:", Anchor = AnchorStyles.Left | AnchorStyles.Top, AutoSize = true };
            txtEdad = new TextBox { Dock = DockStyle.Fill };

            var lblSexo = new Label { Text = "Sexo:", Anchor = AnchorStyles.Left | AnchorStyles.Top, AutoSize = true };
            cbSexo = new ComboBox { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList }; 

            var lblCarrera = new Label { Text = "Carrera:", Anchor = AnchorStyles.Left | AnchorStyles.Top, AutoSize = true };
            txtCarrera = new TextBox { Dock = DockStyle.Fill };

            var lblEstado = new Label { Text = "Estado:", Anchor = AnchorStyles.Left | AnchorStyles.Top, AutoSize = true };
            cbEstado = new ComboBox { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList };

            var lblFecha = new Label { Text = "Fecha:", Anchor = AnchorStyles.Left | AnchorStyles.Top, AutoSize = true };
            dtpFecha = new DateTimePicker { Dock = DockStyle.Fill, Format = DateTimePickerFormat.Short };

            // Añadir controles al TableLayoutPanel
            tlp.Controls.Add(lblId, 0, 0); tlp.Controls.Add(txtId, 1, 0);
            tlp.Controls.Add(lblNombre, 0, 1); tlp.Controls.Add(txtNombre, 1, 1);
            tlp.Controls.Add(lblEdad, 0, 2); tlp.Controls.Add(txtEdad, 1, 2);
            tlp.Controls.Add(lblSexo, 0, 3); tlp.Controls.Add(cbSexo, 1, 3);
            tlp.Controls.Add(lblCarrera, 0, 4); tlp.Controls.Add(txtCarrera, 1, 4);
            tlp.Controls.Add(lblEstado, 0, 5); tlp.Controls.Add(cbEstado, 1, 5);
            tlp.Controls.Add(lblFecha, 0, 6); tlp.Controls.Add(dtpFecha, 1, 6);

            // Panel inferior para botones
            var pnlButtons = new FlowLayoutPanel { Dock = DockStyle.Bottom, Height = 50, FlowDirection = FlowDirection.RightToLeft, Padding = new Padding(10) };
            btnGuardar = new Button { Text = "Guardar", Width = 120, Height = 30, Margin = new Padding(6) };
            btnCancelar = new Button { Text = "Cancelar", Width = 120, Height = 30, Margin = new Padding(6) };
            btnGuardar.Click += BtnGuardar_Click;
            btnCancelar.Click += (s, e) => this.Close();
            pnlButtons.Controls.AddRange(new Control[] { btnCancelar, btnGuardar });

            this.Controls.Add(tlp);
            this.Controls.Add(pnlButtons);
        }

        private void LoadEnums()
        {
            cbSexo.Items.AddRange(Enum.GetNames(typeof(Sexo)));
            cbEstado.Items.AddRange(Enum.GetNames(typeof(EstadoAcademico)));
            cbSexo.SelectedIndex = 0;
            cbEstado.SelectedIndex = 0;
        }

        private void LoadForEdit()
        {
            txtId.Text = editing.Id;
            txtId.Enabled = false; // no cambiar ID
            txtNombre.Text = editing.Nombre;
            txtEdad.Text = editing.Edad.ToString();
            cbSexo.SelectedItem = editing.Sexo.ToString();
            txtCarrera.Text = editing.Carrera;
            cbEstado.SelectedItem = editing.Estado.ToString();
            dtpFecha.Value = editing.FechaInscripcion;
        }

        private void BtnGuardar_Click(object? sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtId.Text)) throw new ArgumentException("El ID es obligatorio.");
                if (string.IsNullOrWhiteSpace(txtNombre.Text)) throw new ArgumentException("El nombre es obligatorio.");
                if (!int.TryParse(txtEdad.Text, out int edad) || edad <= 0) throw new ArgumentException("La edad debe ser un número mayor que 0.");

                var estudiante = new Estudiante
                {
                    Id = txtId.Text.Trim(),
                    Nombre = txtNombre.Text.Trim(),
                    Edad = edad,
                    Sexo = Enum.Parse<Sexo>(cbSexo.SelectedItem.ToString()),
                    Carrera = txtCarrera.Text.Trim(),
                    Estado = Enum.Parse<EstadoAcademico>(cbEstado.SelectedItem.ToString()),
                    FechaInscripcion = dtpFecha.Value.Date
                };

                if (editing == null)
                {
                    GestorEstudiantes.Agregar(estudiante);
                    MessageBox.Show("Estudiante registrado correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    GestorEstudiantes.Actualizar(estudiante);
                    MessageBox.Show("Estudiante actualizado correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                var repetir = MessageBox.Show(editing == null ? "¿Desea registrar otro estudiante?" : "¿Desea editar otro estudiante?", "Continuar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (repetir == DialogResult.Yes)
                {
                    if (editing == null) ClearForm();
                    else this.Close();
                }
                else
                {
                    this.Close();
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearForm()
        {
            txtId.Text = "";
            txtNombre.Text = "";
            txtEdad.Text = "";
            txtCarrera.Text = "";
            cbSexo.SelectedIndex = 0;
            cbEstado.SelectedIndex = 0;
            dtpFecha.Value = DateTime.Now;
            txtId.Focus();
        }
    }
}
