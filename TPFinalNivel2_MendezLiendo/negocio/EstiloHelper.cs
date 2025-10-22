using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;


namespace negocio
{
    public static class EstiloHelper
    {
        public static readonly Color FondoFormulario = ColorTranslator.FromHtml("#F2F2F2");
        public static readonly Color Blanco = Color.White;
        public static readonly Color GrisOscuro = ColorTranslator.FromHtml("#4D4D4D");
        public static readonly Color AzulPetroleo = ColorTranslator.FromHtml("#2C3E50");
        public static readonly Color AzulMedio = ColorTranslator.FromHtml("#2980B9");
        public static readonly Color RojoSuave = ColorTranslator.FromHtml("#E74C3C");
        public static readonly Color AzulClaro = ColorTranslator.FromHtml("#D6EAF8");

        public static void aplicarEstiloFormulario(Form form, TextBox camposTexto,
            IEnumerable<Label> etiquetas,           
            IEnumerable<ComboBox> combos)
        {
            form.BackColor = FondoFormulario;

            camposTexto.BackColor = Blanco;
            camposTexto.ForeColor = Color.Black;
            camposTexto.BorderStyle = BorderStyle.FixedSingle;
            foreach (var label in etiquetas)
            {
                label.ForeColor = GrisOscuro;
                label.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            }
            foreach (var combo in combos)
            {
                combo.BackColor = Blanco;
                combo.ForeColor = Color.Black;
                combo.FlatStyle = FlatStyle.Flat;
            }

        }

        public static void aplicarEstiloBotonPrincipal(Button btn)
        {
            btn.BackColor = AzulMedio;
            btn.ForeColor = Blanco;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        }

        public static void aplicarEstiloBotonEliminar(Button btn)
        {
            btn.BackColor = RojoSuave;
            btn.ForeColor = Blanco;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        }

        public static void aplicarEstiloDataGrid(DataGridView dgv)
        {
            dgv.BackgroundColor = Blanco;
            dgv.DefaultCellStyle.BackColor = Blanco;
            dgv.DefaultCellStyle.ForeColor = Color.Black;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = GrisOscuro;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Blanco;
            dgv.DefaultCellStyle.SelectionBackColor = AzulClaro;
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;
        }
        public static void aplicarEstiloFormulario(Form form,
            IEnumerable<Label> etiquetas,
            IEnumerable<TextBox> camposTexto,
            IEnumerable<ComboBox> combos,
            Button btnAceptar,
            Button btnCancelar,
            Button btnAgregarImagen)
        {
            form.BackColor = FondoFormulario;

            foreach (var label in etiquetas)
            {
                label.ForeColor = GrisOscuro;
                label.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            }

            foreach (var txt in camposTexto)
            {
                txt.BackColor = Blanco;
                txt.ForeColor = Color.Black;
                txt.BorderStyle = BorderStyle.FixedSingle;
            }

            foreach (var combo in combos)
            {
                combo.BackColor = Blanco;
                combo.ForeColor = Color.Black;
                combo.FlatStyle = FlatStyle.Flat;
            }

            aplicarEstiloBotonPrincipal(btnAceptar);
            aplicarEstiloBotonSecundario(btnCancelar);
            aplicarEstiloBotonSecundario(btnAgregarImagen);
        }

        public static void aplicarEstiloBotonSecundario(Button btn)
        {
            btn.BackColor = Blanco;
            btn.ForeColor = GrisOscuro;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderColor = GrisOscuro;
            btn.FlatAppearance.BorderSize = 1;
            btn.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
        }


    }
}
