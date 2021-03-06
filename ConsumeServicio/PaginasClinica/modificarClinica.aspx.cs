using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

namespace ConsumeServicio.PaginasClinica
{
    public partial class modificarClinica : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void verificar_Click(object sender, EventArgs e)
        {
            WservicioBDD.WebServiceBDDSoapClient ws = new WservicioBDD.WebServiceBDDSoapClient();
            int idBuscarP = Int32.Parse(idBuscar.Text);
            DataSet ds = ws.WSBuscar_Clinica_ID(idBuscarP);
            GridView1.DataSource = ds.Tables[0];
            GridView1.DataBind();
        }

        protected void agregar_Click(object sender, EventArgs e)
        {
            WservicioBDD.WebServiceBDDSoapClient ws = new WservicioBDD.WebServiceBDDSoapClient();
            int idBuscarP = Int32.Parse(idBuscar.Text);
            int nitP = Int32.Parse(nit.Text);
            int telP = Int32.Parse(no_telelefono.Text);
            ws.WSActualizar_Clinica(idBuscarP, nombre_clinica.Text, direccion.Text, correo.Text, telP, nitP);


            idBuscar.Text = "";
            nombre_clinica.Text = "";
            direccion.Text = "";
            correo.Text = "";
            no_telelefono.Text = "";
            nit.Text = "";
        }
    }
}