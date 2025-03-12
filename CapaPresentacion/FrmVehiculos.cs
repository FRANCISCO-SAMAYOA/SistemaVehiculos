using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaDatos;

namespace CapaPresentacion
{
    public partial class FrmVehiculos : Form
    {
        CD_Vehiculos cD_vehiculos = new CD_Vehiculos();

        public FrmVehiculos()
        {
            InitializeComponent();
        }

        public void MtdMostrarVehiculos()
        {
            DataTable dtMostrarVehiculos = cD_vehiculos.MtMostrarVehiculos();
            dgvVehiculos.DataSource = dtMostrarVehiculos;
        }

        private void FrmVehiculos_Load(object sender, EventArgs e)
        {
            MtdMostrarVehiculos();
        }

        private bool ValidarCodigoVehiculo(out int VehiculoID)
        {
            VehiculoID = 0;

            if (string.IsNullOrWhiteSpace(txtVehiculoID.Text))
            {
                MessageBox.Show("Debe ingresar un Código del Vehiculo antes de continuar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!int.TryParse(txtVehiculoID.Text, out VehiculoID))
            {
                MessageBox.Show("El Código del Vehiculo debe ser un número válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private bool ValidarDatosVehiculo()
        {
            if (string.IsNullOrWhiteSpace(txtMarca.Text))
            {
                MessageBox.Show("El campo 'Marca' es obligatorio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtModelo.Text))
            {
                MessageBox.Show("El campo 'Modelo' es obligatorio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtAño.Text) || !int.TryParse(txtAño.Text, out int Año))
            {
                MessageBox.Show("El campo 'Año' debe ser un número válido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPrecio.Text) || !decimal.TryParse(txtPrecio.Text, out decimal Precio))
            {
                MessageBox.Show("El campo 'Precio' debe ser un valor decimal válido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(cboxEstado.Text))
            {
                MessageBox.Show("El campo 'Estado' es obligatorio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //CD_Vehiculos cD_vehiculos = new CD_Vehiculos();
            
            if (!ValidarDatosVehiculo()) return;

            try
            {
                cD_vehiculos.CD_MtdAgregarVehiculos(txtMarca.Text, txtModelo.Text, int.Parse(txtAño.Text), decimal.Parse(txtPrecio.Text), cboxEstado.Text);
                MessageBox.Show("El vehiculo se agregó con éxito", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MtdMostrarVehiculos();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (!ValidarCodigoVehiculo(out int VehiculoID)) return;
            if (!ValidarDatosVehiculo()) return;

            try
            {
                cD_vehiculos.CD_MtdActualizarVehiculos(VehiculoID, txtMarca.Text, txtModelo.Text, int.Parse(txtAño.Text), decimal.Parse(txtPrecio.Text), cboxEstado.Text);
                MessageBox.Show("El vehiculo se actualizó con éxito", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MtdMostrarVehiculos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (!ValidarCodigoVehiculo(out int VehiculoID)) return;

            try
            {
                cD_vehiculos.CD_MtdEliminarVehiculos(VehiculoID);
                MessageBox.Show("El vehiculo se eliminó con éxito", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MtdMostrarVehiculos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        private void dgvVehiculos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvVehiculos.Rows[e.RowIndex];
                txtVehiculoID.Text = fila.Cells[0].Value.ToString();
                txtMarca.Text = fila.Cells[1].Value.ToString();
                txtModelo.Text = fila.Cells[2].Value.ToString();
                txtAño.Text = fila.Cells[3].Value.ToString();
                txtPrecio.Text = fila.Cells[4].Value.ToString();
                cboxEstado.Text = fila.Cells[5].Value.ToString();
            }


        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                txtVehiculoID.Clear();
                txtMarca.Clear();
                txtModelo.Clear();
                txtAño.Clear();
                txtPrecio.Clear();
                cboxEstado.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al limpiar los campos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            DialogResult Resultado = MessageBox.Show("Desea salir del sistema?","Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (Resultado == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
