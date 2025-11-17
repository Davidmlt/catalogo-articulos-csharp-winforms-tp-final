using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace presentacion
{
    public partial class frmArticulos : Form
    {
        private List<Articulo> listaArticulos;
        private ArticuloNegocio negocio;
        public frmArticulos()
        {
            InitializeComponent();
        }
        private void frmArticulos_Load(object sender, EventArgs e)
        {
            negocio = new ArticuloNegocio();
            AppHelper.cargar(negocio, dgvArticulos, ref listaArticulos, pbUrlImagen);
            cbCampo.Items.Add("Precio");
            cbCampo.Items.Add("Nombre");
            cbCampo.Items.Add("Marca");
            cbCampo.Items.Add("Categoria");
            EstiloHelper.aplicarEstiloFormulario(
                this, txtFiltro,
                new[] { lblCampo, lblCriterio, lblFiltro },             
                new[] {cbCampo, cbCriterio});

            EstiloHelper.aplicarEstiloBotonPrincipal(btnAgregar);
            EstiloHelper.aplicarEstiloBotonPrincipal(btnModificar);
            EstiloHelper.aplicarEstiloBotonPrincipal(btnVerDetalle);
            EstiloHelper.aplicarEstiloBotonPrincipal(btnActualizar);
            EstiloHelper.aplicarEstiloBotonPrincipal(btnFiltrar);
            EstiloHelper.aplicarEstiloBotonEliminar(btnEliminar);

            EstiloHelper.aplicarEstiloDataGrid(dgvArticulos);

        }

        private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        {
            Articulo seleccionado;
            if (dgvArticulos.CurrentRow != null)
            {
                seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                AppHelper.cargarImagen(seleccionado.ImagenUrl, pbUrlImagen);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAltaArticulo alta = new frmAltaArticulo();
            alta.ShowDialog();
            AppHelper.cargar(negocio, dgvArticulos, ref listaArticulos, pbUrlImagen);
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Articulo seleccionado;
            if (dgvArticulos.CurrentRow != null)
            {
                seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                frmAltaArticulo modificar = new frmAltaArticulo(seleccionado);
                modificar.ShowDialog();
                AppHelper.cargar(negocio, dgvArticulos, ref listaArticulos, pbUrlImagen);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

            negocio = new ArticuloNegocio();
            Articulo seleccionado;
            try
            {
                DialogResult resultado = MessageBox.Show("¿Desea eliminar el articulo?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (resultado == DialogResult.Yes && dgvArticulos.CurrentRow != null)
                {
                    seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;

                    if (File.Exists(AppHelper.obtenerRutaImagen(Path.GetFileName(seleccionado.ImagenUrl))))
                    {
                        File.Delete(AppHelper.obtenerRutaImagen(Path.GetFileName(seleccionado.ImagenUrl)));
                    }
                    negocio.eliminar(seleccionado.Id);
                }
                AppHelper.cargar(negocio, dgvArticulos, ref listaArticulos, pbUrlImagen);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void cbCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string opcion = cbCampo.SelectedItem.ToString();

            if (opcion == "Precio")
            {
                cbCriterio.Items.Clear();
                cbCriterio.Items.Add("Mayor a");
                cbCriterio.Items.Add("Menor a");
                cbCriterio.Items.Add("Igual a");
            }
            else
            {
                cbCriterio.Items.Clear();
                cbCriterio.Items.Add("Comienza con");
                cbCriterio.Items.Add("Termina con");
                cbCriterio.Items.Add("Contiene");
            }
        }


        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            negocio = new ArticuloNegocio();
            try
            {
                if (AppHelper.validarFiltro(cbCampo, cbCriterio, txtFiltro))
                {
                    return;
                }
                string campo = cbCampo.SelectedItem.ToString();
                string criterio = cbCriterio.SelectedItem.ToString();
                string filtro = txtFiltro.Text;

                dgvArticulos.DataSource = negocio.filtrar(campo, criterio, filtro);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnVerDetalle_Click(object sender, EventArgs e)
        {
            negocio = new ArticuloNegocio();
            Articulo seleccionado;
            try
            {
                if (dgvArticulos.CurrentRow != null)
                {
                    seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                    dgvArticulos.DataSource = listaArticulos = new List<Articulo> { seleccionado };
                    AppHelper.columnas(dgvArticulos, true);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            AppHelper.cargar(negocio, dgvArticulos, ref listaArticulos, pbUrlImagen);
        }
    }
}
