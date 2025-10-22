using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using dominio;
using negocio;

namespace presentacion
{
    public partial class frmAltaArticulo : Form
    {
        private Articulo articulo = null;
        private OpenFileDialog archivo = null;
        private string imgAnterior;
        public frmAltaArticulo()
        {
            InitializeComponent();
        }
        public frmAltaArticulo(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
            Text = "Modificar Articulo";

        }
        private void frmAltaArticulo_Load(object sender, EventArgs e)
        {
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            EstiloHelper.aplicarEstiloFormulario(
                this,
                new[] { lblCodigo, lblNombre, lblDescripcion, lblMarca, lblCategoria, lblImagenUrl, lblPrecio },
                new[] { txtCodigo, txtNombre, txtDescripcion, txtImagenUrl, txtPrecio },
                new[] { cboMarca, cboCategoria },
                btnAceptar,
                btnCancelar,
                btnGuardarImagen);

            try
            {
                cboMarca.DataSource = marcaNegocio.listar();
                cboMarca.ValueMember = "Id";
                cboMarca.DisplayMember = "Descripcion";
                cboCategoria.DataSource = categoriaNegocio.listar();
                cboCategoria.ValueMember = "Id";
                cboCategoria.DisplayMember = "Descripcion";

                if (articulo != null)
                {
                    txtCodigo.Text = articulo.Codigo;
                    txtNombre.Text = articulo.Nombre;
                    txtDescripcion.Text = articulo.Descripcion;
                    cboMarca.SelectedValue = articulo.MarcaArticulo.Id;
                    cboCategoria.SelectedValue = articulo.CategoriaArticulo.Id;
                    txtImagenUrl.Text = articulo.ImagenUrl;
                    imgAnterior = Path.GetFileName(articulo.ImagenUrl);
                    AppHelper.cargarImagen(articulo.ImagenUrl, pbUrlImagen);
                    txtPrecio.Text = articulo.Precio.ToString("F4");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            bool hayErrores = false;
            bool validar = true;
            try
            {
                if (articulo == null)
                    articulo = new Articulo();

                hayErrores |= AppHelper.validarCampos(valor => articulo.Codigo = valor, txtCodigo, lblErrorCodigo);
                hayErrores |= AppHelper.validarCampos(valor => articulo.Nombre = valor, txtNombre, lblErrorNombre);
                hayErrores |= AppHelper.validarCampos(valor => articulo.Descripcion = valor, txtDescripcion, lblErrorDescripcion);
                hayErrores |= AppHelper.validarCampos(valor => articulo.ImagenUrl = valor, txtImagenUrl, lblErrorUrl);
                hayErrores |= AppHelper.validarCampo(valor => articulo.Precio = decimal.Parse(valor), txtPrecio, lblErrorPrecio, validar);
                if (hayErrores)
                {
                    return;
                }

                articulo.MarcaArticulo = (Marca)cboMarca.SelectedItem;
                articulo.CategoriaArticulo = (Categoria)cboCategoria.SelectedItem;

                if (archivo != null && !(txtImagenUrl.Text.ToUpper().Contains("HTTP")))
                {

                    if (articulo.Id != 0 && File.Exists(AppHelper.obtenerRutaImagen(imgAnterior)))
                        File.Delete(AppHelper.obtenerRutaImagen(imgAnterior));

                    File.Copy(archivo.FileName, AppHelper.obtenerRutaImagen(archivo.SafeFileName));

                }
                if (articulo.Id != 0)
                {
                    negocio.modificar(articulo);
                    MessageBox.Show("Modificación Exitosa");
                }
                else
                {
                    negocio.agregar(articulo);
                    MessageBox.Show("Agregado Exitosamente");
                }

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtImagenUrl_Leave(object sender, EventArgs e)
        {
            AppHelper.cargarImagen(txtImagenUrl.Text, pbUrlImagen);
        }

        private void btnGuardarImagen_Click(object sender, EventArgs e)
        {
            archivo = new OpenFileDialog();
            try
            {
                archivo.Filter = "jpg|*.jpg;|png|*.png";
                if (archivo.ShowDialog() == DialogResult.OK)
                {
                    txtImagenUrl.Text = archivo.FileName;
                    AppHelper.cargarImagen(archivo.FileName, pbUrlImagen);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
