using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dominio;

namespace negocio
{
    public static class AppHelper
    {    
        public static void cargar(ArticuloNegocio negocio, DataGridView dgv, ref List<Articulo> listaArticulos, PictureBox pb)
        {
            try
            {
                listaArticulos = negocio.listar();
                dgv.DataSource = listaArticulos;
                cargarImagen(listaArticulos[0].ImagenUrl, pb);               
                columnas(dgv, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void cargarImagen(string imagen, PictureBox pb)
        {
            try
            {   
                pb.Load(imagen);
            }
            catch
            {
                pb.Load("https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQmNYw9e9Y7rhqAKRQ3Lv7pFw0JJWdXj13WJA&s");
            }
        }
        public static void columnas(DataGridView dgv, bool opcion = false)
        {
            try
            {
                if (opcion)
                {
                    dgv.Columns["Id"].Visible = false;
                    dgv.Columns["Descripcion"].Visible = true;
                    dgv.Columns["ImagenUrl"].Visible = false;
                    dgv.Columns["Codigo"].Visible = true;
                    dgv.Columns["CategoriaArticulo"].Visible = true;
                    dgv.Columns["MarcaArticulo"].Visible = true;
                }
                else
                {
                    dgv.Columns["Id"].Visible = false;
                    dgv.Columns["Descripcion"].Visible = false;
                    dgv.Columns["ImagenUrl"].Visible = false;
                    dgv.Columns["Codigo"].Visible = false;
                    dgv.Columns["CategoriaArticulo"].Visible = false;
                    dgv.Columns["MarcaArticulo"].Visible = false;
                    dgv.Columns["Precio"].DefaultCellStyle.Format = "C4";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static bool validarFiltro(ComboBox campo, ComboBox criterio, TextBox filtro)
        {
            if (campo.SelectedIndex < 0)
            {
                MessageBox.Show("Por favor, seleccione el campo para filtrar.", "Atención");
                return true;
            }
            if (criterio.SelectedIndex < 0)
            {
                MessageBox.Show("Por favor, seleccione el criterio para filtrar.","Atención");
                return true;
            }
            if (campo.SelectedItem.ToString() == "Precio")
            {
                if (string.IsNullOrEmpty(filtro.Text))
                {
                    MessageBox.Show("Por favor,cargue el filtro", "Atención");
                    return true;
                }
                if (!(soloNumeros(filtro.Text)))
                {
                    MessageBox.Show("Ingrese solo numeros para filtrar, por campo precio", "Atención");
                    return true;
                }
            }

            return false;
        }
        public static bool soloNumeros(string cadena)
        {
            foreach (char caracter in cadena)
            {
                if (!(char.IsNumber(caracter)))
                {
                    return false;
                }

            }
            return true;
        }
        public static bool validarCampos(Action<string> valor, TextBox campo, Label error)
        {
            if (string.IsNullOrEmpty(campo.Text))
            {
                campo.BackColor = Color.MistyRose;
                error.Text = "*";
                error.Visible = true;
                return true;
            }
            else
            {
                campo.BackColor = SystemColors.Window;
                error.Visible = false;
                valor(campo.Text);
                return false;
            }
        }
        public static bool validarCampo(Action<string> valor, TextBox campo, Label error, bool validar = true)
        {
            if (string.IsNullOrEmpty(campo.Text))
            {
                campo.BackColor = Color.MistyRose;
                error.Text = "*";
                error.Visible = true;
                return true;
            }
            else if (!(soloNumeros(campo.Text)) && validar == true)
            {
                campo.BackColor = Color.MistyRose;
                MessageBox.Show("Por favor, ingrese solo numeros", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }
            else
            {
                campo.BackColor = SystemColors.Window;
                error.Visible = false;
                valor(campo.Text);
                return false;
            }

        }
        public static string obtenerRutaImagen(string nombreImagen)
        {
            string directorio = ConfigurationManager.AppSettings["images-articles"];
            return Path.Combine(directorio, nombreImagen);
        }

    }
}
