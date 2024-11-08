using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;

namespace Pasantias
{
    public partial class Postulaciones : System.Web.UI.Page
    {
        Postulacion Postulacion;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               Postulacion = new Postulacion(); 
                Postulacion.grid_aplicantes(grid_aplicantes);
            }

        }


    }
}