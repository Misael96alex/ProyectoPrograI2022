using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

namespace ConsumeServicio
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            WservicioBDD.WebServiceBDDSoapClient ws = new WservicioBDD.WebServiceBDDSoapClient();
            DataSet ds = ws.WSselect_Usuario();
            GridView1.DataSource = ds.Tables[0];
            GridView1.DataBind();
        }

        protected void Agregar_Click(object sender, EventArgs e)
        {
            Response.Redirect("https://localhost:44321/PaginasUsuario/agregarUsuario.aspx");
        }

        protected void Modificar_Click(object sender, EventArgs e)
        {
            Response.Redirect("https://localhost:44321/PaginasUsuario/modificarUsuario.aspx");
        }

        protected void Buscar_Click(object sender, EventArgs e)
        {
            Response.Redirect("https://localhost:44321/PaginasUsuario/buscarUsuario.aspx");
        }

        protected void Eliminar_Click(object sender, EventArgs e)
        {
            Response.Redirect("https://localhost:44321/PaginasUsuario/borrarUsuario.aspx");
        }
    }
}