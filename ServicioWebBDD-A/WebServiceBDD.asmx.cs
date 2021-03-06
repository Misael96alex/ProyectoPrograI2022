using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MySql.Data.MySqlClient;
using System.Web.Services;

namespace ServicioWebBDD_A
{
    /// <summary>
    /// Descripción breve de WebServiceBDD
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class WebServiceBDD : System.Web.Services.WebService
    {
        String servidor;
        String bd;
        String usuario;
        String password;
        String CadenaConexion;
         
        public WebServiceBDD()
        {
            servidor = "localhost";
            bd = "proyectoprogra";
            usuario = "root";
            password = "1234";
            CadenaConexion = "server = " + servidor+"; port=3306; User Id="+usuario+"; Password="+password+"; database="+bd+";";
        }

        /*
         * **********************************************
         * *                USUARIO                   * *
         * **********************************************
         * */

        [WebMethod]
        public DataSet WSselect_Usuario()
        {
            
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = CadenaConexion;
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT u.id_usuario as ID,CONCAT(u.nombre_usuario, ' ', u.apellido_usuario) as 'NOMBRE_USUARIO', cl.nombre_clinica as CLINICA" +
                ", date_format(u.fecha_nac, "+"'%d/%m/%Y'"+") as FECHA_NACIMIENTO, u.dpi as DPI, u.nit as NIT, u.direccion as DIRECCION_USUARIO"+
                ", u.correo as CORREO, u.no_telelefono as TELEFONO FROM usuario u JOIN clinica cl on u.id_clinica = cl.id_clinica; ",conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
            
        }

        [WebMethod]
        public bool WSValidar_Usuario(int id, int contrasena)
        {
            String id_ = "" + id;
            String contrasena_ = "" + contrasena;
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = CadenaConexion;
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT u.id_usuario as ID,CONCAT(u.nombre_usuario, ' ', u.apellido_usuario) as 'NOMBRE_USUARIO', cl.nombre_clinica as CLINICA" +
                ", date_format(u.fecha_nac, " + "'%d/%m/%Y'" + ") as FECHA_NACIMIENTO, u.dpi as DPI, u.nit as NIT, u.direccion as DIRECCION_USUARIO" +
                ", u.correo as CORREO, u.no_telelefono as TELEFONO, u.contrasena AS CONTRASEÑA FROM usuario u JOIN clinica cl on u.id_clinica = cl.id_clinica where id_usuario = " + id_ + " and contrasena = " +
                contrasena_ + "; ", conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            int num = ds.Tables[0].Rows.Count;
            if (num == 1)
            {
                return true;
            } else
            {
                return false;
            }

        }

        [WebMethod]
        public DataSet WSBuscar_Usuario_ID(int id)
        {
            String id_ = "" + id;
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = CadenaConexion;
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT u.id_usuario as ID,CONCAT(u.nombre_usuario, ' ', u.apellido_usuario) as 'NOMBRE_USUARIO', cl.nombre_clinica as CLINICA" +
                ", date_format(u.fecha_nac, " + "'%d/%m/%Y'" + ") as FECHA_NACIMIENTO, u.dpi as DPI, u.nit as NIT, u.direccion as DIRECCION_USUARIO" +
                ", u.correo as CORREO, u.no_telelefono as TELEFONO, u.contrasena AS CONTRASEÑA FROM usuario u JOIN clinica cl on u.id_clinica = cl.id_clinica where id_usuario = " + id_ + " ; ", conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;

        }

        [WebMethod]
        public DataSet WSBuscar_Usuario_ID_Nombre( String nombre)
        {
            
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = CadenaConexion;
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT distinct u.id_usuario as ID,CONCAT(u.nombre_usuario, ' ', u.apellido_usuario) as 'NOMBRE_USUARIO', cl.nombre_clinica as CLINICA" +
                ", date_format(u.fecha_nac, " + "'%d/%m/%Y'" + ") as FECHA_NACIMIENTO, u.dpi as DPI, u.nit as NIT, u.direccion as DIRECCION_USUARIO" +
                ", u.correo as CORREO, u.no_telelefono as TELEFONO, u.contrasena AS CONTRASEÑA FROM usuario u JOIN clinica cl on u.id_clinica = cl.id_clinica where   CONCAT(u.nombre_usuario, ' ', u.apellido_usuario) like  '%"+ nombre+ "%' ;", conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;

        }



        [WebMethod]
        public String WSinsert_Usuario( int id_clinica, string nombre_usuario, string apellido_usuario, string fecha_nac, string dpi, int nit, string direccion, string correo, int no_telelefono, int contrasena)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection();
                conn.ConnectionString = CadenaConexion;
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "insert into usuario(id_clinica,nombre_usuario,apellido_usuario,fecha_nac,dpi,nit,direccion,correo,no_telelefono,contrasena)values('" + id_clinica + "','" + nombre_usuario + "','" + apellido_usuario + "','" + fecha_nac + "','" + dpi + "','" + nit + "','" + direccion + "','" + correo + "','" + no_telelefono + "','" + contrasena + "')";
                cmd.Connection = conn;
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                return "Insercion con exito";
            }catch(Exception e)
            {
                return "error: " + e.Message;
            }
        }

        [WebMethod]
        public void Actualizar_Usuario(int id_usuario, int id_clinica, string nombre_usuario, string apellido_usuario, string fecha_nac, string dpi, int nit, string direccion, string correo, int no_telelefono, int contrasena)
        {
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = CadenaConexion;
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "update usuario set id_clinica = '"+ id_clinica+"' ,nombre_usuario = '" + nombre_usuario + "' , apellido_usuario = '" + apellido_usuario + "' , fecha_nac = '" + fecha_nac + "' ,dpi = '" + dpi + "' ,nit ='" + nit + "' ,direccion = '" + direccion + "' ,correo = '" + correo + "' ,no_telelefono = '" + no_telelefono + "', contrasena = '"+contrasena+"' where id_usuario ='" + id_usuario+"'";
            cmd.Connection = conn;
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

        }

        [WebMethod]
        public void WSBorrarUsuario(int id_usuario)
        {
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = CadenaConexion;
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "DELETE FROM usuario WHERE id_usuario = '" + id_usuario + "';";
            cmd.Connection = conn;
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

        }


        /*
         * **********************************************
         * *                CLINICA                   * *
         * **********************************************
         * */

        [WebMethod]
        public DataSet WSselect_Clinica()
        {

            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = CadenaConexion;
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT cl.id_clinica ID, cl.nombre_clinica CLINICA, cl.direccion DIRECCION, cl.correo CORREO, cl.no_telelefono TELEFONO, cl.nit NIT FROM clinica cl;  ", conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;

        }

        [WebMethod]
        public String WSinsert_Clinica(int id_clinica, string nombre_clinica,string direccion, string correo, int no_telelefono, int nit)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection();
                conn.ConnectionString = CadenaConexion;
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "insert into clinica(id_clinica,nombre_clinica,direccion,correo,no_telelefono,nit)values('" + id_clinica + "','" + nombre_clinica + "','" + direccion + "','" + correo + "','" + no_telelefono + "','" + nit + "')";
                cmd.Connection = conn;
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                return "Insercion con exito";
            }
            catch (Exception e)
            {
                return "error: " + e.Message;
            }
        }

        [WebMethod]
        public void WSBorrar_Clinica(int id_clinica)
        {
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = CadenaConexion;
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "delete from clinica where id_clinica = " + id_clinica;
            cmd.Connection = conn;
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        [WebMethod]
        public void WSActualizar_Clinica(int id_clinica, string nombre_clinica, string direccion, string correo, int no_telelefono, int nit)
        {
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = CadenaConexion;
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "update clinica set nombre_clinica = '" + nombre_clinica + "' , direccion = '" + direccion + "' , correo = '" + correo + "' , no_telelefono = '" + no_telelefono + "' ,nit ='" + nit + "' where id_clinica =" + id_clinica;
            cmd.Connection = conn;
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        [WebMethod]
        public DataSet WSBuscar_Clinica_ID(int id)
        {
            String id_ = "" + id;
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = CadenaConexion;
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT cl.id_clinica ID, cl.nombre_clinica CLINICA, cl.direccion DIRECCION, cl.correo CORREO, cl.no_telelefono TELEFONO, cl.nit NIT FROM clinica cl where cl.id_clinica = " + id_ + " ; ", conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        [WebMethod]
        public DataSet WSBuscar_Clinica_ID_Nombre(String nombre)
        {
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = CadenaConexion;
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT cl.id_clinica ID, cl.nombre_clinica CLINICA, cl.direccion DIRECCION, cl.correo CORREO, cl.no_telelefono TELEFONO, cl.nit NIT FROM clinica cl where cl.nombre_clinica  like  '%" + nombre + "%' ;", conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        /*
         * **********************************************
         * *                EMPLEADO                  * *
         * **********************************************
         * */

        [WebMethod]
        public DataSet WSselectEmpleado()
        {

            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = CadenaConexion;
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT em.id_empleados ID, cl.nombre_clinica CLINICA, concat(em.nombre_empleado,' ',em.apellido_empleado) NOMBRE , em.dpi DPI, em.nit NIT, em.direccion DIRECCION, em.correo CORREO, em.no_telelefono TELEFONO "+
                            " FROM empleados em JOIN clinica cl on cl.id_clinica = em.id_clinica; ", conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;

        }

        [WebMethod]
        public String WSinsertEmpleado(int id_clinica, string nombre_empleado, string apellido_empleado, string dpi, int nit, string direccion, string correo, int no_telelefono)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection();
                conn.ConnectionString = CadenaConexion;
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "INSERT INTO empleados ( id_clinica, nombre_empleado, apellido_empleado, dpi, nit, direccion, correo, no_telelefono) VALUES ( '"+ id_clinica + "', '"+ nombre_empleado + "', '"+ apellido_empleado + "', '"+ dpi + "', '"+ nit + "', '"+ direccion + "', '"+ correo + "', '"+ no_telelefono + "');";
                cmd.Connection = conn;
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                return "Insercion con exito";
            }
            catch (Exception e)
            {
                return "error: " + e.Message;
            }
        }

        [WebMethod]
        public void WSBorrarEmpleado(int id_empleados)
        {
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = CadenaConexion;
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "delete from empleados where id_empleados = " + id_empleados;
            cmd.Connection = conn;
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        [WebMethod]
        public void WSActualizarEmpleado(int id_empleados, int id_clinica, string nombre_empleado, string apellido_empleado, string dpi, int nit, string direccion, string correo, int no_telelefono)
        {
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = CadenaConexion;
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "update empleados set id_clinica = '" + id_clinica + "' , nombre_empleado = '" + nombre_empleado + "' , apellido_empleado = '" + apellido_empleado + "' , dpi = '" + dpi + "' ,nit ='" + nit + "' , direccion = '" + direccion + "' , correo = '" + correo + "' ,no_telelefono ='" + no_telelefono + "' where id_empleados =" + id_empleados;
            cmd.Connection = conn;
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        [WebMethod]
        public DataSet WSBuscar_Empleado_ID(int id)
        {
            String id_ = "" + id;
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = CadenaConexion;
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT em.id_empleados ID, cl.nombre_clinica CLINICA, concat(em.nombre_empleado,' ',em.apellido_empleado) NOMBRE , em.dpi DPI, em.nit NIT, em.direccion DIRECCION, em.correo CORREO, em.no_telelefono TELEFONO " +
                            " FROM empleados em JOIN clinica cl on cl.id_clinica = em.id_clinica where em.id_empleados = " + id_ + " ; ", conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        [WebMethod]
        public DataSet WSBuscar_Empleado_ID_Nombre(String nombre)
        {
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = CadenaConexion;
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT em.id_empleados ID, cl.nombre_clinica CLINICA, concat(em.nombre_empleado,' ',em.apellido_empleado) NOMBRE , em.dpi DPI, em.nit NIT, em.direccion DIRECCION, em.correo CORREO, em.no_telelefono TELEFONO " +
                            " FROM empleados em JOIN clinica cl on cl.id_clinica = em.id_clinica where concat(em.nombre_empleado,' ',em.apellido_empleado)  like  '%" + nombre + "%' ;", conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        /*
         * **********************************************
         * *                PACIENTE                  * *
         * **********************************************
         * */

        [WebMethod]
        public DataSet WSselectPaciente()
        {

            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = CadenaConexion;
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT p.id_paciente ID, concat(p.nombre_paciente,' ' ,p.apellido_paciente)NOMBRE"+
               " , p.familiar_responsable FAMILIAR_RESPONSABLE, p.fecha_nac FECHA_NACIMIENTO "+
               " , p.tipo_sangre TIPO_SANGRE, p.enferm_preexitente ENFERMEDAD_PREEXISTENTE "+
               " , p.dpi DPI, p.nit NIT, p.direccion DIRECCION, p.correo CORREO, p.no_telelefono TELEFONO FROM paciente p; ", conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;

        }

        [WebMethod]
        public String WSinsertPaciente(int id_paciente, string nombre_paciente, string apellido_paciente, string familiar_responsable, string fecha_nac, string tipo_sangre,string enferm_preexitente, string dpi,  int nit, string direccion, string correo, int no_telelefono)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection();
                conn.ConnectionString = CadenaConexion;
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "INSERT INTO PACIENTE ( id_paciente, nombre_paciente, apellido_paciente, familiar_responsable, fecha_nac, tipo_sangre, enferm_preexitente, dpi,nit,direccion,correo,no_telelefono ) VALUES ( '" + id_paciente + "', '" + nombre_paciente + "', '" + apellido_paciente + "', '" + familiar_responsable + "', '" + fecha_nac + "', '" + tipo_sangre + "', '" + enferm_preexitente + "', '" + dpi + "', '" + nit + "', '" + direccion + "', '" + correo + "', '" + no_telelefono + "');";
                cmd.Connection = conn;
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                return "Insercion con exito";
            }
            catch (Exception e)
            {
                return "error: " + e.Message;
            }
        }

        /*
         * **********************************************
         * *                MEDICAMENTO                  * *
         * **********************************************
         * */

        [WebMethod]
        public DataSet WSselectMedicamento()
        {

            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = CadenaConexion;
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT m.id_medicamento ID, m.descripcion DESCRIPCION, m.precio PRECIO, m.marca MARCA, m.cantidad CANTIDAD, m.dosis DOSIS, m.presentacion PRESENTACION FROM medicamentos m; ", conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;

        }

        [WebMethod]
        public String WSinsertMedicamento(int id_medicamento, string descripcion, string precio, string marca, string cantidad, string dosis, string presentacion)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection();
                conn.ConnectionString = CadenaConexion;
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "INSERT INTO medicamentos ( id_medicamento, descripcion, precio, marca, cantidad, dosis, presentacion ) VALUES ( '" + id_medicamento + "', '" + descripcion + "', '" + precio + "', '" + marca + "', '" + cantidad + "', '" + dosis + "', '" + presentacion +  "');";
                cmd.Connection = conn;
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                return "Insercion con exito";
            }
            catch (Exception e)
            {
                return "error: " + e.Message;
            }
        }

        /*
         * **********************************************
         * *                ENFERMEDAD                  * *
         * **********************************************
         * */

        [WebMethod]
        public DataSet WSselectEnfermedad()
        {

            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = CadenaConexion;
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT e.id_enfermedad ID, e.nombre_enfermedad ENFERMEDAD, e.tipo_enfermedad TIPO, e.nivel_contagio NIVEL_CONTAGIO FROM enfermedad e;", conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;

        }

        [WebMethod]
        public String WSinsertEnfermedad(int id_enfermedad, string nombre_enfermedad, string tipo_enfermedad, string nivel_contagio)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection();
                conn.ConnectionString = CadenaConexion;
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "INSERT INTO enfermedad ( id_enfermedad, nombre_enfermedad, tipo_enfermedad, nivel_contagio) VALUES ( '" + id_enfermedad + "', '" + nombre_enfermedad + "', '" + tipo_enfermedad + "', '" + nivel_contagio +  "');";
                cmd.Connection = conn;
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                return "Insercion con exito";
            }
            catch (Exception e)
            {
                return "error: " + e.Message;
            }
        }

        /*
         * *******************************************************
         * *                REGISTROS MEDICOS                  * *
         * *******************************************************
         * */

        [WebMethod]
        public String WSinsertAgendarCita(string id_reg_cita, string id_paciente, string id_clinica, string usuario_creador, string fecha, string costo, string no_consultorio , string forma_pago)
        {
            try
            {
                /*primer tabla*/
                MySqlConnection conn = new MySqlConnection();
                conn.ConnectionString = CadenaConexion;
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "INSERT INTO registro_citas ( id_reg_cita, id_paciente, id_clinica, usuario_creador) VALUES ( '" + id_reg_cita + "', '" + id_paciente + "', '" + id_clinica + "', '" + usuario_creador + "');";
                cmd.Connection = conn;
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                /*segunda tabla*/
                MySqlConnection conn1 = new MySqlConnection();
                conn1.ConnectionString = CadenaConexion;
                MySqlCommand cmd1 = new MySqlCommand();
                cmd1.CommandType = System.Data.CommandType.Text;
                cmd1.CommandText = "INSERT INTO cita ( id_cita, id_reg_cita, fecha, costo,no_consultorio,forma_pago) VALUES ( '" + id_reg_cita + "', '" + id_reg_cita + "', '" + fecha + "', '" + costo + "', '" + no_consultorio + "', '" + forma_pago + "');";
                cmd1.Connection = conn1;
                conn1.Open();
                cmd1.ExecuteNonQuery();
                conn1.Close();
                return "Insercion con exito";
            }
            catch (Exception e)
            {
                return "error: " + e.Message;
            }
        }

        [WebMethod]
        public DataSet WSBuscarCita(string id)
        {

            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = CadenaConexion;
            MySqlDataAdapter da = new MySqlDataAdapter("select (select distinct rc.id_paciente from registro_citas rc where rc.id_reg_cita = c.id_reg_cita) ID_PACIENTE "+
                ", (select distinct concat(p.nombre_paciente, ' ', p.apellido_paciente) from registro_citas rc, paciente p where rc.id_reg_cita = c.id_reg_cita and p.id_paciente = rc.id_paciente) PACIENTE "+
                ",(select distinct rc.usuario_creador from registro_citas rc where rc.id_reg_cita = c.id_reg_cita) ID_USUARIO "+
                ",(select distinct concat(u.nombre_usuario,' ',u.apellido_usuario) from registro_citas rc , usuario u where rc.id_reg_cita = c.id_reg_cita and u.id_usuario = usuario_creador) USUARIO   from cita c "+
                "where c.id_cita ='"+ id +
                "' and not exists(select 1 from registro_consultas rr where rr.id_reg_consul = c.id_cita); ", conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;

        }

        [WebMethod]
        public String WSTerminarConsulta(string id_reg_consul, string id_paciente, string fecha_creacion, string id_medicamento, string id_enfermedad, string medico, string diagnostico, string medicamentos,string estudios,string usuario_creador)
        {
            try
            {
                /*primer tabla*/
                MySqlConnection conn = new MySqlConnection();
                conn.ConnectionString = CadenaConexion;
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "INSERT INTO registro_consultas ( id_reg_consul, id_paciente, fecha_creacion,usuario_creador, id_medicamento,id_enfermedad) VALUES ( '" + id_reg_consul + "', '" + id_paciente + "', '" + fecha_creacion + "', '" + usuario_creador + "', '" + id_medicamento + "', '" + id_enfermedad + "');";
                cmd.Connection = conn;
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                /*segunda tabla*/
                MySqlConnection conn1 = new MySqlConnection();
                conn1.ConnectionString = CadenaConexion;
                MySqlCommand cmd1 = new MySqlCommand();
                cmd1.CommandType = System.Data.CommandType.Text;
                cmd1.CommandText = "INSERT INTO consulta ( id_consulta, id_reg_consul, medico, diagnostico,medicamentos,estudios) VALUES ( '" + id_reg_consul + "', '" + id_reg_consul + "', '" + medico + "', '" + diagnostico + "', '" + medicamentos + "', '" + estudios + "');";
                cmd1.Connection = conn1;
                conn1.Open();
                cmd1.ExecuteNonQuery();
                conn1.Close();
                return "Insercion con exito";
            }
            catch (Exception e)
            {
                return "error: " + e.Message;
            }
        }

        /*
         * *******************************************************
         * *                REPORTERIA                         * *
         * *******************************************************
         * */

        [WebMethod]
        public DataSet WSBusquedaPacienteConsulta(string paciente)
        {

            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = CadenaConexion;
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT concat(p.nombre_paciente,' ',p.apellido_paciente) PACIENTE, p.tipo_sangre TIPO_SANGRE, c.id_consulta ID_CONSULTA, clI.nombre_clinica CLINICA , c.medico DOCTOR, e.nombre_enfermedad DIAGNOSTICO, e.tipo_enfermedad TIPO_DIAGNOSTICO, c.estudios ESTUDIOS_OBSERVACIONES, me.descripcion MEDICAMENTO, rc.fecha_creacion FECHA_CONSULTA, ci.costo COSTO_CONSULTA, ci.forma_pago FORMA_DE_PAGO "+
                " FROM consulta c join registro_consultas rc on rc.id_reg_consul = c.id_reg_consul join paciente p on p.id_paciente = rc.id_paciente join usuario u on u.id_usuario = rc.usuario_creador join enfermedad e on e.id_enfermedad = rc.id_enfermedad join registro_citas rci on rci.id_reg_cita = rc.id_reg_consul join cita ci on ci.id_reg_cita = rci.id_reg_cita join clinica cli on cli.id_clinica = rci.id_clinica join medicamentos me on me.id_medicamento = rc.id_medicamento "+
                " where concat(p.nombre_paciente, ' ', p.apellido_paciente) like '%"+ paciente + "%' ; ", conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;

        }

        [WebMethod]
        public DataSet WSBusquedaCitas( )
        {

            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = CadenaConexion;
            MySqlDataAdapter da = new MySqlDataAdapter("select ci.id_cita ID_CITA_RESERVADA ,ci.fecha FECHA_CITA_RESERVADA ,cl.nombre_clinica CLINICA ,ci.no_consultorio NUMERO_CONSULTORIO ,concat(p.nombre_paciente,' ',p.apellido_paciente) PACIENTE from cita ci join registro_citas rc on rc.id_reg_cita = ci.id_reg_cita join paciente p on p.id_paciente = rc.id_paciente join clinica cl on cl.id_clinica = rc.id_clinica;", conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;

        }
    }
}
