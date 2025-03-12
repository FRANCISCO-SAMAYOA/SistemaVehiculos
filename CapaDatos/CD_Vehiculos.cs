using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Vehiculos
    {

        CD_Conexion db_conexion = new CD_Conexion();

        public DataTable MtMostrarVehiculos()
        {
            string QryMostrarVehiculos = "usp_Vehiculos_mostrar";
            SqlDataAdapter adapter = new SqlDataAdapter(QryMostrarVehiculos, db_conexion.MtdAbrirConexion());
            DataTable dtMostrarVehiculos = new DataTable();
            adapter.Fill(dtMostrarVehiculos);
            db_conexion.MtdCerrarConexion();
            return dtMostrarVehiculos;
        }

        public void CD_MtdAgregarVehiculos(string Marca, string Modelo, int Año, decimal Precio, String Estado)
        {
            String Usp_crear = "usp_vehiculo_crear";
            SqlCommand cmd_InsertarVehiculos = new SqlCommand(Usp_crear, db_conexion.MtdAbrirConexion());
            cmd_InsertarVehiculos.CommandType = CommandType.StoredProcedure;
            cmd_InsertarVehiculos.Parameters.AddWithValue("@Marca", Marca);
            cmd_InsertarVehiculos.Parameters.AddWithValue("@Modelo", Modelo);
            cmd_InsertarVehiculos.Parameters.AddWithValue("@Año", Año);
            cmd_InsertarVehiculos.Parameters.AddWithValue("@Precio", Precio);
            cmd_InsertarVehiculos.Parameters.AddWithValue("@Estado", Estado);
            cmd_InsertarVehiculos.ExecuteNonQuery();
        }

        public void CD_MtdActualizarVehiculos(int VehiculoID, string Marca, string Modelo, int Año, decimal Precio, String Estado)
        {
            String Usp_actualizar = "usp_Vehiculo_editar";
            SqlCommand cmd_ActualizarVehiculos = new SqlCommand(Usp_actualizar, db_conexion.MtdAbrirConexion());
            cmd_ActualizarVehiculos.CommandType = CommandType.StoredProcedure;
            cmd_ActualizarVehiculos.Parameters.AddWithValue("@VehiculoID", VehiculoID);
            cmd_ActualizarVehiculos.Parameters.AddWithValue("@Marca", Marca);
            cmd_ActualizarVehiculos.Parameters.AddWithValue("@Modelo", Modelo);
            cmd_ActualizarVehiculos.Parameters.AddWithValue("@Año", Año);
            cmd_ActualizarVehiculos.Parameters.AddWithValue("@Precio", Precio); 
            cmd_ActualizarVehiculos.Parameters.AddWithValue("@Estado", Estado);
            cmd_ActualizarVehiculos.ExecuteNonQuery();
        }

        public void CD_MtdEliminarVehiculos(int VehiculoID)
        {
            String Usp_eliminar = "usp_vehiculo_eliminar";
            SqlCommand cmd_EliminarVehiculos = new SqlCommand(Usp_eliminar, db_conexion.MtdAbrirConexion());
            cmd_EliminarVehiculos.CommandType = CommandType.StoredProcedure;
            cmd_EliminarVehiculos.Parameters.AddWithValue("@VehiculoID", VehiculoID);
            cmd_EliminarVehiculos.ExecuteNonQuery();

            db_conexion.MtdCerrarConexion();
        }
    }
}
